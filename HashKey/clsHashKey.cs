using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.IO;

namespace HashKey
{
    public class clsHashKeyList
    {
        public List<clsHashKey> gobjHashKeyList = new List<clsHashKey>();

        public clsHashKeyList(string strFileName)
        {
            DataTable objDataTable = LoadExcelFileByNPOI(strFileName);
            if (objDataTable != null)
            {
                gobjHashKeyList.Clear();
                for (int i = 0; i < objDataTable.Rows.Count; i++)
                {
                    DataRow dr = objDataTable.Rows[i];
                    int j = 0;
                    clsHashKey objKey = new clsHashKey();
                    objKey.PartNumber   = dr[j++].ToString();
                    objKey.CustomerID   = dr[j++].ToString();
                    objKey.ModelID      = dr[j++].ToString();
                    objKey.ChipID       = dr[j++].ToString();
                    objKey.Technology   = dr[j++].ToString();
                    objKey.CustomerIP   = dr[j++].ToString();
                    objKey.CustomerHash = dr[j++].ToString();
                    objKey.Type         = dr[j++].ToString();
                    objKey.Area         = dr[j++].ToString();
                    gobjHashKeyList.Add(objKey);
                }
            }
        }

        private System.Data.DataTable LoadExcelFileByNPOI(string strFileName)
        {
            FileStream fs = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);

            // 判断是否第0个Sheets的 Cell[0, 0] = mstar
            NPOI.SS.UserModel.ISheet objSheet0 = book.GetSheetAt(0);
            if (objSheet0 == null) return null;
            NPOI.SS.UserModel.IRow objRow = objSheet0.GetRow(0);
            if (objRow == null) return null;
            int firstCell = objRow.FirstCellNum;
            int lastCell = objRow.LastCellNum;
            //if (objRow.GetCell(firstCell).StringCellValue != "mstar")
            //    return null;

            DataTable dt = new System.Data.DataTable(objSheet0.SheetName);
            dt.Columns.Add("PartNumber", typeof(string));
            dt.Columns.Add("CustomerID", typeof(string));
            dt.Columns.Add("ModelID", typeof(string));
            dt.Columns.Add("ChipID", typeof(string));
            dt.Columns.Add("Technology", typeof(string));
            dt.Columns.Add("CustomerIP", typeof(string));
            dt.Columns.Add("CustomerHash", typeof(string));
            dt.Columns.Add("TYPE", typeof(string));
            dt.Columns.Add("AREA", typeof(string));

            for (int i = 1; i <= objSheet0.LastRowNum; i++)
            {
                //DataRow newRow = dt.Rows.Add();
                DataRow newRow = dt.NewRow();
                for (int j = 0; j < lastCell; j++)
                {
                    if (objSheet0.GetRow(i).GetCell(j) == null)
                    {
                        newRow[j] = string.Empty;
                        continue;
                    }

                    string strType = objSheet0.GetRow(i).GetCell(j).CellType.ToString().ToUpper();
                    switch (strType)
                    {
                        case "STRING"://字符串类型
                            newRow[j] = objSheet0.GetRow(i).GetCell(j).StringCellValue;
                            break;
                        case "NUMERIC":
                            newRow[j] = objSheet0.GetRow(i).GetCell(j).NumericCellValue.ToString();
                            break;
                        default:
                            newRow[j] = string.Empty;
                            break;
                    }
                }
                dt.Rows.Add(newRow);// add by hanson
            }

            return dt;
        }
    }

    public class clsHashKey
    {
        public string PartNumber;
        public string CustomerID;
        public string ModelID;
        public string ChipID;
        public string Technology;
        public string CustomerIP;
        public string CustomerHash;

        public string Type;
        public string Area;

        public clsHashKey()
        { 
        
        }

        public bool UpdateFile(string strFileName)
        {
            return true;
        }

        public string CodeText()
        {
            clsHashKeyFeild obj = new clsHashKeyFeild(this);
            return obj.CodeText();
        }
        public string FormatCustomerIP(string strCustomerIP, int intIndex)
        { // 01102001CA0028502001030428F80000
            string strRes = string.Empty;

            if (strCustomerIP == null || strCustomerIP.Trim().Length != 32)
                return string.Empty;

            strCustomerIP = strCustomerIP.Trim();
            switch (intIndex)
            {
                case 1: strRes = strCustomerIP.Substring(0, 8); break;
                case 2: strRes = strCustomerIP.Substring(8, 8); break;
                case 3: strRes = strCustomerIP.Substring(16, 8); break;
                case 4: strRes = strCustomerIP.Substring(24, 8); break;
            }
            return strRes;
        }
        public string FormatCustomerHash(string strCustomerHash)
        { // b03f6ba1dc7ea0c706772aabbcaa4be0 
            // {0x99,0xff,0x8c,0x9b,0xe5,0x48,0x26,0x29,0xca,0x11,0xdf,0xc3,0x8a,0x32,0xff,0x5e}
            strCustomerHash = strCustomerHash.Trim();
            if (strCustomerHash.Length % 2 != 0)
                return string.Empty;

            string strRes = string.Empty;
            string strItem = string.Empty;
            for (int i = 0; i < strCustomerHash.Length; i = i + 2)
            {
                strItem = strCustomerHash.Substring(i, 2);
                if (strRes == string.Empty)
                    strRes += " {0x" + strItem;
                else
                    strRes += ",0x" + strItem;
            }
            strRes += "}";

            return strRes;
        }
    }

    public class clsHashKeyFeild
    {
        public string INPUT_CUSTOMER_ID_LOW_BYTE;
        public string INPUT_CUSTOMER_ID_HIGH_BYTE;
        public string INPUT_MODEL_ID_LOW_BYTE;
        public string INPUT_MODEL_ID_HIGH_BYTE;
        public string INPUT_CHIP_ID_LOW_BYTE;
        public string INPUT_CHIP_ID_HIGH_BYTE;
        public string INPUT_DOLBY_VER_LOW_BYTE;
        public string INPUT_DOLBY_VER_HIGH_BYTE;
        public string DRM_MODEL_ID;

        public string gconstIP_Cntrol_Mapping_1;
        public string gconstIP_Cntrol_Mapping_2;
        public string gconstIP_Cntrol_Mapping_3;
        public string gconstIP_Cntrol_Mapping_4;
        public string gconstCustomer_hash;

        public clsHashKeyFeild(clsHashKey objHashKey)
        {
            this.INPUT_CUSTOMER_ID_LOW_BYTE = "00"; // objHashKey.CustomerID;
            this.INPUT_CUSTOMER_ID_HIGH_BYTE = objHashKey.CustomerID;
            this.INPUT_MODEL_ID_LOW_BYTE = objHashKey.ModelID;
            this.INPUT_MODEL_ID_HIGH_BYTE = objHashKey.ModelID;
            this.INPUT_CHIP_ID_LOW_BYTE = objHashKey.ChipID;
            this.INPUT_CHIP_ID_HIGH_BYTE = objHashKey.ChipID;
            this.INPUT_DOLBY_VER_LOW_BYTE = "0x00";
            this.INPUT_DOLBY_VER_HIGH_BYTE = "0x00";
            this.DRM_MODEL_ID = objHashKey.ModelID;

            //this.gconstIP_Cntrol_Mapping_1 = FormatCustomerIP(objHashKey.CustomerIP, 1);
            //this.gconstIP_Cntrol_Mapping_2 = FormatCustomerIP(objHashKey.CustomerIP, 2);
            //this.gconstIP_Cntrol_Mapping_3 = FormatCustomerIP(objHashKey.CustomerIP, 3);
            //this.gconstIP_Cntrol_Mapping_4 = FormatCustomerIP(objHashKey.CustomerIP, 4);
            //this.gconstCustomer_hash = FormatCustomerHash(objHashKey.CustomerHash);
        }

        //private string FormatCustomerIP(string strCustomerIP, int intIndex)
        //{ // 01102001CA0028502001030428F80000
        //    string strRes = string.Empty;

        //    if (strCustomerIP == null || strCustomerIP.Trim().Length != 32)
        //        return string.Empty;

        //    strCustomerIP = strCustomerIP.Trim();
        //    switch (intIndex)
        //    {
        //        case 1: strRes = strCustomerIP.Substring(0, 8); break;
        //        case 2: strRes = strCustomerIP.Substring(8, 8); break;
        //        case 3: strRes = strCustomerIP.Substring(16, 8); break;
        //        case 4: strRes = strCustomerIP.Substring(24, 8); break;
        //    }
        //    return strRes;
        //}
        //private string FormatCustomerHash(string strCustomerHash)
        //{ // b03f6ba1dc7ea0c706772aabbcaa4be0 
        //    // {0x99,0xff,0x8c,0x9b,0xe5,0x48,0x26,0x29,0xca,0x11,0xdf,0xc3,0x8a,0x32,0xff,0x5e}
        //    strCustomerHash = strCustomerHash.Trim();
        //    if (strCustomerHash.Length % 2 != 0)
        //        return string.Empty;

        //    string strRes = string.Empty;
        //    string strItem = string.Empty;
        //    for (int i = 0; i < strCustomerHash.Length; i = i + 2)
        //    {
        //        strItem = strCustomerHash.Substring(i, 2);
        //        if (strRes == string.Empty)
        //            strRes += " {0x" + strItem;
        //        else
        //            strRes += ",0x" + strItem;
        //    }
        //    strRes += "}";

        //    return strRes;
        //}

        /// <summary>
        /// 生成模版文件
        /// </summary>
        /// <param name="strFileName">模版文件路径</param>
        /// <returns>返回代码段</returns>
        public string CodeText()
        {
            string strFileName = "Customer_Info.TXT";
            System.Windows.Forms.RichTextBox objRichTextBox = new System.Windows.Forms.RichTextBox();
            objRichTextBox.LoadFile(strFileName, System.Windows.Forms.RichTextBoxStreamType.PlainText);
            string strCode = objRichTextBox.Text;
            strCode = strCode.Replace("<%INPUT_CUSTOMER_ID_LOW_BYTE%>", this.INPUT_CUSTOMER_ID_LOW_BYTE);
            strCode = strCode.Replace("<%INPUT_CUSTOMER_ID_HIGH_BYTE%>", this.INPUT_CUSTOMER_ID_HIGH_BYTE);
            strCode = strCode.Replace("<%INPUT_MODEL_ID_LOW_BYTE%>", this.INPUT_MODEL_ID_LOW_BYTE);
            strCode = strCode.Replace("<%INPUT_MODEL_ID_HIGH_BYTE%>", this.INPUT_MODEL_ID_HIGH_BYTE);
            strCode = strCode.Replace("<%INPUT_CHIP_ID_LOW_BYTE%>", this.INPUT_CHIP_ID_LOW_BYTE);
            strCode = strCode.Replace("<%INPUT_CHIP_ID_HIGH_BYTE%>", this.INPUT_CHIP_ID_HIGH_BYTE);
            strCode = strCode.Replace("<%INPUT_DOLBY_VER_LOW_BYTE%>", this.INPUT_DOLBY_VER_LOW_BYTE);
            strCode = strCode.Replace("<%INPUT_DOLBY_VER_HIGH_BYTE%>", this.INPUT_DOLBY_VER_HIGH_BYTE);
            strCode = strCode.Replace("<%DRM_MODEL_ID%>", this.DRM_MODEL_ID);

            strCode = strCode.Replace("<%gconstIP_Cntrol_Mapping_1%>", this.gconstIP_Cntrol_Mapping_1);
            strCode = strCode.Replace("<%gconstIP_Cntrol_Mapping_2%>", this.gconstIP_Cntrol_Mapping_2);
            strCode = strCode.Replace("<%gconstIP_Cntrol_Mapping_3%>", this.gconstIP_Cntrol_Mapping_3);
            strCode = strCode.Replace("<%gconstIP_Cntrol_Mapping_4%>", this.gconstIP_Cntrol_Mapping_4);
            strCode = strCode.Replace("<%gconstCustomer_hash%>", this.gconstCustomer_hash);

            return strCode;
        }
    }
}
