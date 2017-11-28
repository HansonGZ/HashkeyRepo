using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HashKey
{
    public partial class frmHashKey : Form
    {
        private clsHashKeyList gobjHashKeyList;

        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }
        /// <param name="dgvFillDataObject">DataGridView控件</param>
        private void FillDataGridViewColums(DataGridView dgvFillDataObject)
        {
            if (dgvFillDataObject.DataSource != null) dgvFillDataObject.DataSource = null;
            dgvFillDataObject.Rows.Clear();
            dgvFillDataObject.Columns.Clear();
            // Add Link Column
            DataGridViewLinkColumn objdgvColumn5 = new DataGridViewLinkColumn();
            objdgvColumn5.Name = "NewLinkColumn";
            objdgvColumn5.HeaderText = "操作";
            objdgvColumn5.ReadOnly = true;
            objdgvColumn5.Width = 80;
            objdgvColumn5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFillDataObject.Columns.Add(objdgvColumn5);
            // 添加Table列// PartNumber
            DataGridViewTextBoxColumn objdgvTextBoxColumnPartNumber = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnPartNumber.Name = "PartNumber";
            objdgvTextBoxColumnPartNumber.HeaderText = "PartNumber";
            objdgvTextBoxColumnPartNumber.Width = 80;
            objdgvTextBoxColumnPartNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFillDataObject.Columns.Add(objdgvTextBoxColumnPartNumber);
            // CustomerID
            DataGridViewTextBoxColumn objdgvTextBoxColumnCustomerID = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnCustomerID.Name = "CustomerID";
            objdgvTextBoxColumnCustomerID.HeaderText = "CustomerID";
            objdgvTextBoxColumnCustomerID.Width = 80;
            dgvFillDataObject.Columns.Add(objdgvTextBoxColumnCustomerID);
            // ModelID
            DataGridViewTextBoxColumn objdgvTextBoxColumnModelID = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnModelID.Name = "ModelID";
            objdgvTextBoxColumnModelID.HeaderText = "ModelID";
            objdgvTextBoxColumnModelID.Width = 80;
            dgvFillDataObject.Columns.Add(objdgvTextBoxColumnModelID);
            // ChipID
            DataGridViewTextBoxColumn objdgvTextBoxColumnChipID = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnChipID.Name = "ChipID";
            objdgvTextBoxColumnChipID.HeaderText = "ChipID";
            objdgvTextBoxColumnChipID.Width = 80;
            dgvFillDataObject.Columns.Add(objdgvTextBoxColumnChipID);
            // Technology
            DataGridViewTextBoxColumn objdgvTextBoxColumnTechnology = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnTechnology.Name = "Technology";
            objdgvTextBoxColumnTechnology.HeaderText = "Technology";
            objdgvTextBoxColumnTechnology.Width = 360;
            dgvFillDataObject.Columns.Add(objdgvTextBoxColumnTechnology);
            // CustomerIP
            DataGridViewTextBoxColumn objdgvTextBoxColumnCustomerIP = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnCustomerIP.Name = "CustomerIP";
            objdgvTextBoxColumnCustomerIP.HeaderText = "CustomerIP";
            objdgvTextBoxColumnCustomerIP.Width = 200;
            dgvFillDataObject.Columns.Add(objdgvTextBoxColumnCustomerIP);
            // CustomerHash
            DataGridViewTextBoxColumn objdgvTextBoxColumnCustomerHash = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnCustomerHash.Name = "CustomerHash";
            objdgvTextBoxColumnCustomerHash.HeaderText = "CustomerHash";
            objdgvTextBoxColumnCustomerHash.Width = 200;
            dgvFillDataObject.Columns.Add(objdgvTextBoxColumnCustomerHash);
            // Type
            DataGridViewTextBoxColumn objdgvTextBoxColumnType = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnType.Name = "Type";
            objdgvTextBoxColumnType.HeaderText = "Type";
            objdgvTextBoxColumnType.Width = 100;
            dgvFillDataObject.Columns.Add(objdgvTextBoxColumnType);
            // Area
            DataGridViewTextBoxColumn objdgvTextBoxColumnArea = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnArea.Name = "Area";
            objdgvTextBoxColumnArea.HeaderText = "Area";
            objdgvTextBoxColumnArea.Width = 200;
            dgvFillDataObject.Columns.Add(objdgvTextBoxColumnArea);
        }
        private void InitDictionaryForHashkey(Dictionary<string, int> dicHash)
        {
            dicHash.Add("BBE Digital", 0); //index = 0;
            dicHash.Add("BBEVIVA", 0);
            dicHash.Add("TSXT(SRS)", 0);
            dicHash.Add("TSHD", 0);
            dicHash.Add("SRS TruVolume", 0);
            dicHash.Add("VDS", 0);
            dicHash.Add("VSPK", 0);
            dicHash.Add("DTS_SUR", 0);
            dicHash.Add("Qsound", 0);
            dicHash.Add("Audyssey", 0);
            dicHash.Add("CV3", 0);
            dicHash.Add("DD", 0);
            dicHash.Add("DD+", 0);
            dicHash.Add("DDCO", 0);
            dicHash.Add("Dolby pulse", 0);
            dicHash.Add("DTS Dec", 0);
            dicHash.Add("(Empty)", 0);
            dicHash.Add("Dolby_THD", 0);
            dicHash.Add("DTS HD (DTS M6)", 0);
            dicHash.Add("MPEG2", 0);
            dicHash.Add("MPEG2_HD", 0);
            dicHash.Add("MPEG4", 0);
            dicHash.Add("MPEG4_SD", 0);
            dicHash.Add("MPEG4_HD", 0);
            dicHash.Add("DivX-1080P HD", 0);
            dicHash.Add("DivX_DRM", 0);
            dicHash.Add("DivX_Plus", 0);
            dicHash.Add("H.264", 0);
            dicHash.Add("RM", 0);
            dicHash.Add("VC1", 0);
            dicHash.Add("WMA", 0);
            dicHash.Add("WMDRM_PD", 0);
            dicHash.Add("WMDRM_ND", 0);
            dicHash.Add("AVS", 0);
            dicHash.Add("FLV", 0);
            dicHash.Add("DivX qMobile", 0);
            dicHash.Add("DivX Mobile", 0);
            dicHash.Add("DivX Home Theater", 0);
            dicHash.Add("DivX 720p HD", 0);
            dicHash.Add("DVB-C", 0);
            dicHash.Add("MVC", 0);
            dicHash.Add("PlayReady", 0);
            dicHash.Add("Youtube HTML5", 0);
            dicHash.Add("facebook", 0);
            dicHash.Add("SC1.2", 0);
            dicHash.Add("(None)", 0);
            dicHash.Add("(None1)", 0);
            dicHash.Add("(None2)", 0);
            dicHash.Add("VUDU", 0);
            dicHash.Add("CN - CinemaNow(BestBuy)", 0);
            dicHash.Add("CN - BlockBuster", 0);
            dicHash.Add("CN - Film Fresh", 0);
            dicHash.Add("CN - Sears Alphaline", 0);
            dicHash.Add("CN - RealD 3D", 0);
            dicHash.Add("Macrovision", 0);
            dicHash.Add("SRS StudioSound HD", 0);
            dicHash.Add("Dolby Volume", 0);
            dicHash.Add("Dolby DD+ Encode", 0);
            dicHash.Add("DTS LBR(Express)", 0);
            dicHash.Add("Empty", 0);
            dicHash.Add("VP6", 0);
            dicHash.Add("VP8", 0);
            dicHash.Add("Ginga-NCL", 0);
            dicHash.Add("Ginga-J", 0);
            dicHash.Add("DBX", 0);
            dicHash.Add("DRA", 0);
            dicHash.Add("SRS PureSound bit", 0);
            dicHash.Add("Cineplex", 0);
            dicHash.Add("Widevine", 0);
            dicHash.Add("DTS StudioSound 3D", 0);
            dicHash.Add("GAAC", 0);
            dicHash.Add("Empty2", 0);
            dicHash.Add("DTS Neo:Ultra", 0);
            dicHash.Add("DTS Transcode", 0);
            dicHash.Add("Zenterio MW control", 0);
            dicHash.Add("HEVC", 0);
            dicHash.Add("Flash Access", 0);
            dicHash.Add("Miracast", 0);
            dicHash.Add("Rovi_DPS", 0);
            dicHash.Add("(None3)", 0);
            dicHash.Add("Dolby_MS11", 0);
            dicHash.Add("Dolby_MS12_B", 0);
            dicHash.Add("Dolby_MS12_C", 0);
            dicHash.Add("Dolby_MS12_LC", 0);
            dicHash.Add("Dolby reserved bit", 0);
            dicHash.Add("DivX HEVC 4K", 0);
            dicHash.Add("DivX HEVC 1080p", 0);
            dicHash.Add("DivX HEVC 720p", 0);
            dicHash.Add("Microsoft Smooth Streaming", 0);
            dicHash.Add("Netflix", 0);
            dicHash.Add("Merlin", 0);
            dicHash.Add("VP9", 0);
            dicHash.Add("SonicEmotion", 0);
            dicHash.Add("Bongiovi DPS", 0);
            dicHash.Add("TTS (Cyberon, English)", 0);
            dicHash.Add("TTS (Cyberon, Spanish)", 0);
            dicHash.Add("TTS (Cyberon, French)", 0);
            dicHash.Add("TTS (Cyberon, Portuguese)", 0);
            dicHash.Add("TTS (Cyberon, Korean)", 0);
            dicHash.Add("HDCP", 0);
            dicHash.Add("DTCPIP", 0);
            dicHash.Add("WiDi", 0);
            dicHash.Add("H264_HD", 0);
            dicHash.Add("Know how movies", 0);
            dicHash.Add("Target Ticket", 0);
            dicHash.Add("Pandora", 0);
            dicHash.Add("RVU", 0);
            dicHash.Add("TTS (Hummer, English)", 0);
            dicHash.Add("TTS (Hummer, Spanish)", 0);
            dicHash.Add("Browser Engine (Sraf)", 0);
            dicHash.Add("HbbTV/FVP Browser (Sraf)", 0);
            dicHash.Add("HbbTV Engine", 0);
            dicHash.Add("Open Browser (Sraf)", 0);
            dicHash.Add("Portal (Sraf)", 0);
            dicHash.Add("Inview(Middleware)", 0);
            dicHash.Add("Dolby_MS11 w/o HE-AAC", 0);
            dicHash.Add("Dolby_MS12-B w/o HE-AAC", 0);
            dicHash.Add("Dolby_MS12-D w/o HE-AAC", 0);
            dicHash.Add("DOLBY_HDR", 0);
            dicHash.Add("MoPA EAD UI", 0);
            dicHash.Add("Youtube HTML5 (Seraphic YTTV)", 0);
            dicHash.Add("(None4)", 0);
            dicHash.Add("(None5)", 0);
            dicHash.Add("(None6)", 0);
            dicHash.Add("(None7)", 0);
            dicHash.Add("(None8)", 0);
            dicHash.Add("(None9)", 0);
            dicHash.Add("Dolby Demo(default off)", 0);
        }
        /*
        /// <summary>
        /// 填充DataGridView
        /// <summary>
        /// <param name="dgvFillDataObject">DataGridView控件</param>
        /// <param name="objsysEmployeeArr">表[sysEmployee]的数据对象</param>
        private void FillDataGridView(DataGridView dgvFillDataObject, List<clsHashKey> objHashKeyList)
        {
            dgvFillDataObject.Rows.Clear();
            if (objHashKeyList == null || objHashKeyList.Count <= 0) return;

            // 添加行
            for (int i = 0; i < objHashKeyList.Count; i++)
            {
                dgvFillDataObject.Rows.Add();

                // Add LinkCell
                DataGridViewLinkCell objdgvCell5 = new DataGridViewLinkCell();
                objdgvCell5.Value = "生成";
                objdgvCell5.ToolTipText = "提示：点我，选择该KEY生效！";
                dgvFillDataObject["NewLinkColumn", i] = objdgvCell5;
                // 添加Table列
                // PartNumber
                DataGridViewTextBoxCell objdgvTextBoxCell1 = new DataGridViewTextBoxCell();
                objdgvTextBoxCell1.Value = objHashKeyList[i].PartNumber;
                dgvFillDataObject["PartNumber", i] = objdgvTextBoxCell1;
                // CustomerID
                DataGridViewTextBoxCell objdgvTextBoxCell2 = new DataGridViewTextBoxCell();
                objdgvTextBoxCell2.Value = objHashKeyList[i].CustomerID;
                dgvFillDataObject["CustomerID", i] = objdgvTextBoxCell2;
                // ModelID
                DataGridViewTextBoxCell objdgvTextBoxCell3 = new DataGridViewTextBoxCell();
                objdgvTextBoxCell3.Value = objHashKeyList[i].ModelID;
                dgvFillDataObject["ModelID", i] = objdgvTextBoxCell3;
                // ChipID
                DataGridViewTextBoxCell objdgvTextBoxCell4 = new DataGridViewTextBoxCell();
                objdgvTextBoxCell4.Value = objHashKeyList[i].ChipID;
                dgvFillDataObject["ChipID", i] = objdgvTextBoxCell4;
                // Technology
                DataGridViewTextBoxCell objdgvTextBoxCell5 = new DataGridViewTextBoxCell();
                objdgvTextBoxCell5.Value = objHashKeyList[i].Technology;
                dgvFillDataObject["Technology", i] = objdgvTextBoxCell5;
                // CustomerIP
                DataGridViewTextBoxCell objdgvTextBoxCell6 = new DataGridViewTextBoxCell();
                objdgvTextBoxCell6.Value = objHashKeyList[i].CustomerIP;
                dgvFillDataObject["CustomerIP", i] = objdgvTextBoxCell6;
                // CustomerHash
                DataGridViewTextBoxCell objdgvTextBoxCell7 = new DataGridViewTextBoxCell();
                objdgvTextBoxCell7.Value = objHashKeyList[i].CustomerHash;
                dgvFillDataObject["CustomerHash", i] = objdgvTextBoxCell7;

                // Type
                DataGridViewTextBoxCell objdgvTextBoxCell8 = new DataGridViewTextBoxCell();
                objdgvTextBoxCell8.Value = objHashKeyList[i].Type;
                dgvFillDataObject["Type", i] = objdgvTextBoxCell8;
                // Area
                DataGridViewTextBoxCell objdgvTextBoxCell9 = new DataGridViewTextBoxCell();
                objdgvTextBoxCell9.Value = objHashKeyList[i].Area;
                dgvFillDataObject["Area", i] = objdgvTextBoxCell9;

                dgvFillDataObject.Rows[i].Tag = objHashKeyList[i];
            }

            if (this.dataGridView1.Rows.Count > 0)
            {
                this.dataGridView1.Rows[0].Selected = false;
                this.dataGridView1.Rows[0].Selected = true;
            }
        }
*/
        private void FillDataGridViewColumsForHashkey(Dictionary<string, int> dicHash)
        {
            DataGridViewTextBoxColumn objdgvTextBoxColumnSerialNumber1 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnSerialNumber1.Name = "number1";
            objdgvTextBoxColumnSerialNumber1.HeaderText = "Num";
            objdgvTextBoxColumnSerialNumber1.Width = 40;
            objdgvTextBoxColumnSerialNumber1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            objdgvTextBoxColumnSerialNumber1.DefaultCellStyle.BackColor = Color.GreenYellow;
            dataGridView1.Columns.Add(objdgvTextBoxColumnSerialNumber1);

            DataGridViewTextBoxColumn objdgvTextBoxColumnKey1 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnKey1.Name = "key1";
            objdgvTextBoxColumnKey1.HeaderText = "Key";
            objdgvTextBoxColumnKey1.Width = 80;
            objdgvTextBoxColumnKey1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnKey1);

            DataGridViewTextBoxColumn objdgvTextBoxColumnValue1 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnValue1.Name = "value1";
            objdgvTextBoxColumnValue1.HeaderText = "Val";
            objdgvTextBoxColumnValue1.Width = 30;
            objdgvTextBoxColumnValue1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnValue1);
            
            // Index 2
            DataGridViewTextBoxColumn objdgvTextBoxColumnSerialNumber2 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnSerialNumber2.Name = "number2";
            objdgvTextBoxColumnSerialNumber2.HeaderText = "Num";
            objdgvTextBoxColumnSerialNumber2.Width = 40;
            objdgvTextBoxColumnSerialNumber2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            objdgvTextBoxColumnSerialNumber2.DefaultCellStyle.BackColor = Color.GreenYellow;
            dataGridView1.Columns.Add(objdgvTextBoxColumnSerialNumber2);

            DataGridViewTextBoxColumn objdgvTextBoxColumnKey2 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnKey2.Name = "key2";
            objdgvTextBoxColumnKey2.HeaderText = "Key";
            objdgvTextBoxColumnKey2.Width = 80;
            objdgvTextBoxColumnKey2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnKey2);

            DataGridViewTextBoxColumn objdgvTextBoxColumnValue2 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnValue2.Name = "value2";
            objdgvTextBoxColumnValue2.HeaderText = "Val";
            objdgvTextBoxColumnValue2.Width = 30;
            objdgvTextBoxColumnValue2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnValue2);

            //Index 3
            DataGridViewTextBoxColumn objdgvTextBoxColumnSerialNumber3 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnSerialNumber3.Name = "number3";
            objdgvTextBoxColumnSerialNumber3.HeaderText = "Num";
            objdgvTextBoxColumnSerialNumber3.Width = 40;
            objdgvTextBoxColumnSerialNumber3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            objdgvTextBoxColumnSerialNumber3.DefaultCellStyle.BackColor = Color.GreenYellow;
            dataGridView1.Columns.Add(objdgvTextBoxColumnSerialNumber3);

            DataGridViewTextBoxColumn objdgvTextBoxColumnKey3 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnKey3.Name = "key3";
            objdgvTextBoxColumnKey3.HeaderText = "Key";
            objdgvTextBoxColumnKey3.Width = 80;
            objdgvTextBoxColumnKey3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnKey3);

            DataGridViewTextBoxColumn objdgvTextBoxColumnValue3 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnValue3.Name = "value3";
            objdgvTextBoxColumnValue3.HeaderText = "Val";
            objdgvTextBoxColumnValue3.Width = 30;
            objdgvTextBoxColumnValue3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnValue3);


            //Index 4
            DataGridViewTextBoxColumn objdgvTextBoxColumnSerialNumber4 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnSerialNumber4.Name = "number4";
            objdgvTextBoxColumnSerialNumber4.HeaderText = "Num";
            objdgvTextBoxColumnSerialNumber4.Width = 40;
            objdgvTextBoxColumnSerialNumber4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            objdgvTextBoxColumnSerialNumber4.DefaultCellStyle.BackColor = Color.GreenYellow;
            dataGridView1.Columns.Add(objdgvTextBoxColumnSerialNumber4);

            DataGridViewTextBoxColumn objdgvTextBoxColumnKey4 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnKey4.Name = "key4";
            objdgvTextBoxColumnKey4.HeaderText = "Key";
            objdgvTextBoxColumnKey4.Width = 80;
            objdgvTextBoxColumnKey4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnKey4);

            DataGridViewTextBoxColumn objdgvTextBoxColumnValue4 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnValue4.Name = "value4";
            objdgvTextBoxColumnValue4.HeaderText = "Val";
            objdgvTextBoxColumnValue4.Width = 30;
            objdgvTextBoxColumnValue4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnValue4);

            //Index 5
            DataGridViewTextBoxColumn objdgvTextBoxColumnSerialNumber5 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnSerialNumber5.Name = "number5";
            objdgvTextBoxColumnSerialNumber5.HeaderText = "Num";
            objdgvTextBoxColumnSerialNumber5.Width = 40;
            objdgvTextBoxColumnSerialNumber5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            objdgvTextBoxColumnSerialNumber5.DefaultCellStyle.BackColor = Color.GreenYellow;
            dataGridView1.Columns.Add(objdgvTextBoxColumnSerialNumber5);

            DataGridViewTextBoxColumn objdgvTextBoxColumnKey5 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnKey5.Name = "key5";
            objdgvTextBoxColumnKey5.HeaderText = "Key";
            objdgvTextBoxColumnKey5.Width = 80;
            objdgvTextBoxColumnKey5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnKey5);

            DataGridViewTextBoxColumn objdgvTextBoxColumnValue5 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnValue5.Name = "value5";
            objdgvTextBoxColumnValue5.HeaderText = "Val";
            objdgvTextBoxColumnValue5.Width = 30;
            objdgvTextBoxColumnValue5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnValue5);

            //Index 6
            DataGridViewTextBoxColumn objdgvTextBoxColumnSerialNumber6 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnSerialNumber6.Name = "number6";
            objdgvTextBoxColumnSerialNumber6.HeaderText = "Num";
            objdgvTextBoxColumnSerialNumber6.Width = 40;
            objdgvTextBoxColumnSerialNumber6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            objdgvTextBoxColumnSerialNumber6.DefaultCellStyle.BackColor = Color.GreenYellow;
            dataGridView1.Columns.Add(objdgvTextBoxColumnSerialNumber6);

            DataGridViewTextBoxColumn objdgvTextBoxColumnKey6 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnKey6.Name = "key6";
            objdgvTextBoxColumnKey6.HeaderText = "Key";
            objdgvTextBoxColumnKey6.Width = 80;
            objdgvTextBoxColumnKey6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnKey6);

            DataGridViewTextBoxColumn objdgvTextBoxColumnValue6 = new DataGridViewTextBoxColumn();
            objdgvTextBoxColumnValue6.Name = "value6";
            objdgvTextBoxColumnValue6.HeaderText = "Val";
            objdgvTextBoxColumnValue6.Width = 30;
            objdgvTextBoxColumnValue6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(objdgvTextBoxColumnValue6);

            dataGridView1.Rows.Clear();

            List<string> testList = new List<string>(dicHash.Keys);

            //First Row:
            dataGridView1.Rows.Add();

            DataGridViewTextBoxCell number1FirstRowNum = new DataGridViewTextBoxCell();
            number1FirstRowNum.Value = 0;
            DataGridViewTextBoxCell number1FirstRowKey = new DataGridViewTextBoxCell();
            number1FirstRowKey.Value = testList[0];
            DataGridViewTextBoxCell number1FirstRowValue = new DataGridViewTextBoxCell();
            number1FirstRowValue.Value = dicHash[testList[0]].ToString();

            DataGridViewTextBoxCell number1FirstRowLastNum = new DataGridViewTextBoxCell();
            number1FirstRowLastNum.Value = 127;
            DataGridViewTextBoxCell number1FirstRowLastKey = new DataGridViewTextBoxCell();
            number1FirstRowLastKey.Value = testList[127];
            DataGridViewTextBoxCell number1FirstRowLastValue = new DataGridViewTextBoxCell();
            number1FirstRowLastValue.Value = dicHash[testList[127]].ToString();

            dataGridView1["number1", 0] = number1FirstRowNum;
            dataGridView1["key1", 0] = number1FirstRowKey;
            dataGridView1["value1", 0] = number1FirstRowValue;

            dataGridView1["number6", 0] = number1FirstRowLastNum;
            dataGridView1["key6", 0] = number1FirstRowLastKey;
            dataGridView1["value6", 0] = number1FirstRowLastValue;

            dataGridView1.Rows[0].Cells[0].Selected = false;


            for (int i = 1; i < 21; i++ )
            {
                int m1 = i + 20;
                int m2 = i + 40;
                int m3 = i + 60;
                int m4 = i + 80;
                int m5 = i + 100;
                
                dataGridView1.Rows.Add();
                
                DataGridViewTextBoxCell hknumber1 = new DataGridViewTextBoxCell();
                hknumber1.Value = i.ToString();
                DataGridViewTextBoxCell hknumber2 = new DataGridViewTextBoxCell();
                hknumber2.Value = m1.ToString();
                DataGridViewTextBoxCell hknumber3 = new DataGridViewTextBoxCell();
                hknumber3.Value = m2.ToString();
                DataGridViewTextBoxCell hknumber4 = new DataGridViewTextBoxCell();
                hknumber4.Value = m3.ToString();
                DataGridViewTextBoxCell hknumber5 = new DataGridViewTextBoxCell();
                hknumber5.Value = m4.ToString();
                DataGridViewTextBoxCell hknumber6 = new DataGridViewTextBoxCell();
                hknumber6.Value = m5.ToString();

                dataGridView1["number1", i] = hknumber1;
                dataGridView1["number2", i] = hknumber2;
                dataGridView1["number3", i] = hknumber3;
                dataGridView1["number4", i] = hknumber4;
                dataGridView1["number5", i] = hknumber5;
                dataGridView1["number6", i] = hknumber6;

                DataGridViewTextBoxCell hkkey1 = new DataGridViewTextBoxCell();
                hkkey1.Value = testList[i];
                DataGridViewTextBoxCell hkkey2 = new DataGridViewTextBoxCell();
                hkkey2.Value = testList[m1];
                DataGridViewTextBoxCell hkkey3 = new DataGridViewTextBoxCell();
                hkkey3.Value = testList[m2];
                DataGridViewTextBoxCell hkkey4 = new DataGridViewTextBoxCell();
                hkkey4.Value = testList[m3];
                DataGridViewTextBoxCell hkkey5 = new DataGridViewTextBoxCell();
                hkkey5.Value = testList[m4];
                DataGridViewTextBoxCell hkkey6 = new DataGridViewTextBoxCell();
                hkkey6.Value = testList[m5];

                dataGridView1["key1", i] = hkkey1;
                dataGridView1["key2", i] = hkkey2;
                dataGridView1["key3", i] = hkkey3;
                dataGridView1["key4", i] = hkkey4;
                dataGridView1["key5", i] = hkkey5;
                dataGridView1["key6", i] = hkkey6;

                DataGridViewTextBoxCell hkvalue1 = new DataGridViewTextBoxCell();
                hkvalue1.Value = dicHash[testList[i]];
                DataGridViewTextBoxCell hkvalue2 = new DataGridViewTextBoxCell();
                hkvalue2.Value = dicHash[testList[m1]];
                DataGridViewTextBoxCell hkvalue3 = new DataGridViewTextBoxCell();
                hkvalue3.Value = dicHash[testList[m2]];
                DataGridViewTextBoxCell hkvalue4 = new DataGridViewTextBoxCell();
                hkvalue4.Value = dicHash[testList[m3]];
                DataGridViewTextBoxCell hkvalue5 = new DataGridViewTextBoxCell();
                hkvalue5.Value = dicHash[testList[m4]];
                DataGridViewTextBoxCell hkvalue6 = new DataGridViewTextBoxCell();
                hkvalue6.Value = dicHash[testList[m5]];


                dataGridView1["value1", i] = hkvalue1;
                dataGridView1["value2", i] = hkvalue2;
                dataGridView1["value3", i] = hkvalue3;
                dataGridView1["value4", i] = hkvalue4;
                dataGridView1["value5", i] = hkvalue5;
                dataGridView1["value6", i] = hkvalue6;
            }
        }
        private string PrintDictionKeyAndValue(Dictionary<string, int> dicHash)
        {
            string sPrintResult = null;
            List<string> test = new System.Collections.Generic.List<string>(dicHash.Keys);
            for (int i = 0; i < dicHash.Count; i++)
            {
                sPrintResult = sPrintResult + test[i] + ":" + dicHash[test[i]].ToString() + "   ";
            }
            return sPrintResult;
        }
        private bool UpdateDictionaryForHashkey(Dictionary<string, int> dicHash, string value)
        {
            int i = 1;
            if(value.Length != 128)
            {
                return false;
            }
            List<string> Templist = new System.Collections.Generic.List<string>();
            Templist.AddRange(dicHash.Keys);
            foreach (string t in Templist)
            {
                dicHash[t] = Convert.ToInt32(value.Substring(value.Length - i, 1));
                i = i + 1;
            }
            return true;
        }

        private string CheckInputForHashkey(Dictionary<string, int> dicHash, string tmpWaitCheck)
        {
            string sReturnstr = null;
            string[] sArray = tmpWaitCheck.Split(';');
            for (int i = 0; i < sArray.Length; i++ )
            {
                if(dicHash.ContainsKey(sArray[i]) == false)
                {
                    sReturnstr = sArray[i] + ";" + sReturnstr;
                }
            }
            return sReturnstr;
        }
        private bool CheckIPForHashkey(Dictionary<string, int> dicHash, string tmpvalue, ref string cbContain, ref string cbNotContain)
        {
            bool tmpReturn = true;
            string[] sArray = tmpvalue.Split(';');
            for (int i = sArray.Length - 1 ; i >= 0; i--)
            {
               if(dicHash[sArray[i]] == 0)
               {
                   cbNotContain = sArray[i] + ";" + cbNotContain;
                   tmpReturn = false;
               }
               else
               {
                   cbContain = sArray[i] + ";" + cbContain;
               }
            }
            return tmpReturn;
        }
        private void UpdateHashFileByUserInput(string filename, clsHashKey tmphashkey)
        {            
            string strINPUT_CUSTOMER_ID_LOW_BYTE = "INPUT_CUSTOMER_ID_LOW_BYTE";
            string strINPUT_CUSTOMER_ID_HIGH_BYTE = "INPUT_CUSTOMER_ID_HIGH_BYTE";
            string strINPUT_MODEL_ID_LOW_BYTE = "INPUT_MODEL_ID_LOW_BYTE";
            string strMINPUT_MODEL_ID_HIGH_BYTE = "INPUT_MODEL_ID_HIGH_BYTE";
            string strCINPUT_CHIP_ID_LOW_BYTE = "INPUT_CHIP_ID_LOW_BYTE";
            string strINPUT_CHIP_ID_HIGH_BYTE = "INPUT_CHIP_ID_HIGH_BYTE";
            string strgconstIP_Cntrol_Mapping_1 = "gconstIP_Cntrol_Mapping_1";
            string strgconstIP_Cntrol_Mapping_2 = "gconstIP_Cntrol_Mapping_2";
            string strgconstIP_Cntrol_Mapping_3 = "gconstIP_Cntrol_Mapping_3";
            string strgconstIP_Cntrol_Mapping_4 = "gconstIP_Cntrol_Mapping_4";
            string strgconstCustomer_hash = "gconstCustomer_hash";

            string[] allTextLines = File.ReadAllLines(filename, Encoding.UTF8); //读取当前文件的所有行

            for (int j = 0; j < allTextLines.Length; j++)       //遍历每一行
            {
                if (allTextLines[j].Contains(strINPUT_CUSTOMER_ID_LOW_BYTE))    //如果这行包含了INPUT_CUSTOMER_ID_LOW_BYTE关键字，就去替换这整行
                {
                    //allTextLines[j] = allTextLines[j].Replace(allTextLines[j], ("#define " + strINPUT_CUSTOMER_ID_LOW_BYTE + " 0x" + tmphashkey.CustomerID.PadLeft(4,'0').Substring(2, 2))); //PadLeft:一个字符串的长度小于指定的值，则在字符串的左侧（也就是前面）用指定的字符填充; Substring:从指定位置开始截取指定长度的字符串 
                    string tmpstr = "00";
                    for(int i=3; i< allTextLines[j].Length; i++)
                    {
                        if((allTextLines[j][i-3] == '0') && (allTextLines[j][i-2] == 'x'))  //取代"0x"后面的2个数字
                        {
                            tmpstr = allTextLines[j][i - 1].ToString() + allTextLines[j][i].ToString(); //tostring 转为字符串
                            break;
                        }
                    }
                    if (tmphashkey.CustomerID != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.CustomerID.PadLeft(4, '0').Substring(2,2));
                    }
                }
                else if(allTextLines[j].Contains(strINPUT_CUSTOMER_ID_HIGH_BYTE))
                {
                    string tmpstr = "00";
                    for (int i = 3; i < allTextLines[j].Length; i++)
                    {
                        if ((allTextLines[j][i - 3] == '0') && (allTextLines[j][i - 2] == 'x'))
                        {
                            tmpstr = allTextLines[j][i - 1].ToString() + allTextLines[j][i].ToString();
                            break;
                        }
                    }
                    if (tmphashkey.CustomerID != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.CustomerID.PadLeft(4, '0').Substring(0, 2));
                    }
                }
                else if (allTextLines[j].Contains(strINPUT_MODEL_ID_LOW_BYTE))
                {
                    string tmpstr = "00";
                    for (int i = 3; i < allTextLines[j].Length; i++)
                    {
                        if ((allTextLines[j][i - 3] == '0') && (allTextLines[j][i - 2] == 'x'))
                        {
                            tmpstr = allTextLines[j][i - 1].ToString() + allTextLines[j][i].ToString();
                            break;
                        }
                    }
                    if (tmphashkey.ModelID != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.ModelID.PadLeft(4, '0').Substring(2, 2));
                    }
                }
                else if (allTextLines[j].Contains(strMINPUT_MODEL_ID_HIGH_BYTE))
                {
                    string tmpstr = "00";
                    for (int i = 3; i < allTextLines[j].Length; i++)
                    {
                        if ((allTextLines[j][i - 3] == '0') && (allTextLines[j][i - 2] == 'x'))
                        {
                            tmpstr = allTextLines[j][i - 1].ToString() + allTextLines[j][i].ToString();
                            break;
                        }
                    }
                    if (tmphashkey.ModelID != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.ModelID.PadLeft(4, '0').Substring(0, 2));
                    }
                }
                else if (allTextLines[j].Contains(strCINPUT_CHIP_ID_LOW_BYTE))
                {
                    string tmpstr = "00";
                    for (int i = 3; i < allTextLines[j].Length; i++)
                    {
                        if ((allTextLines[j][i - 3] == '0') && (allTextLines[j][i - 2] == 'x'))
                        {
                            tmpstr = allTextLines[j][i - 1].ToString() + allTextLines[j][i].ToString();
                            break;
                        }
                    }
                    if (tmphashkey.ChipID != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.ChipID.PadLeft(4, '0').Substring(2, 2));
                    }
                }
                else if (allTextLines[j].Contains(strINPUT_CHIP_ID_HIGH_BYTE))
                {
                    string tmpstr = "00";
                    for (int i = 3; i < allTextLines[j].Length; i++)
                    {
                        if ((allTextLines[j][i - 3] == '0') && (allTextLines[j][i - 2] == 'x'))
                        {
                            tmpstr = allTextLines[j][i - 1].ToString() + allTextLines[j][i].ToString();
                            break;
                        }
                    }
                    if (tmphashkey.ChipID != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.ChipID.PadLeft(4, '0').Substring(0, 2));
                    }
                }
                else if (allTextLines[j].Contains(strgconstIP_Cntrol_Mapping_1))
                {
                    string tmpstr = string.Empty;
                    for (int i = 0; i < allTextLines[j].Length; i++)
                    {
                        if(allTextLines[j][i] == '"')
                        {
                            for(int m = 1; m<9; m++)
                            {
                                tmpstr += allTextLines[j][i + m].ToString();
                            }
                            break;
                        }
                    }
                    if (tmphashkey.CustomerIP != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.FormatCustomerIP(tmphashkey.CustomerIP, 4));
                    }
                }
                else if (allTextLines[j].Contains(strgconstIP_Cntrol_Mapping_2))
                {
                    string tmpstr = string.Empty;
                    for (int i = 0; i < allTextLines[j].Length; i++)
                    {
                        if (allTextLines[j][i] == '"')
                        {
                            for (int m = 1; m < 9; m++)
                            {
                                tmpstr += allTextLines[j][i + m].ToString();
                            }
                            break;
                        }
                    }
                    if (tmphashkey.CustomerIP != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.FormatCustomerIP(tmphashkey.CustomerIP,3));
                    }
                }
                else if (allTextLines[j].Contains(strgconstIP_Cntrol_Mapping_3))
                {
                    string tmpstr = string.Empty;
                    for (int i = 0; i < allTextLines[j].Length; i++)
                    {
                        if (allTextLines[j][i] == '"')
                        {
                            for (int m = 1; m < 9; m++)
                            {
                                tmpstr += allTextLines[j][i + m].ToString();
                            }
                            break;
                        }
                    }
                    if (tmphashkey.CustomerIP != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.FormatCustomerIP(tmphashkey.CustomerIP, 2));
                    }
                }
                else if (allTextLines[j].Contains(strgconstIP_Cntrol_Mapping_4))
                {
                    string tmpstr = string.Empty;
                    for (int i = 0; i < allTextLines[j].Length; i++)
                    {
                        if (allTextLines[j][i] == '"')
                        {
                            for (int m = 1; m < 9; m++)
                            {
                                tmpstr += allTextLines[j][i + m].ToString();
                            }
                            break;
                        }
                    }
                    if (tmphashkey.CustomerIP != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.FormatCustomerIP(tmphashkey.CustomerIP, 1));
                    }
                }
                else if (allTextLines[j].Contains(strgconstCustomer_hash))
                {
                    string tmpstr = string.Empty;
                    for (int i = 0; i < allTextLines[j].Length; i++)
                    {
                        if (allTextLines[j][i] == '{')
                        {
                            do
                            {
                                tmpstr += allTextLines[j][i++].ToString();
                            } while (allTextLines[j][i] != ';');
                            break;
                        }
                    }
                    if (tmphashkey.CustomerHash != string.Empty)
                    {
                        allTextLines[j] = allTextLines[j].Replace(tmpstr, tmphashkey.FormatCustomerHash(tmphashkey.CustomerHash));

                    }
                }
            }
            File.WriteAllLines(filename, allTextLines, Encoding.UTF8);  //最后写回去
        }
        private string GetCustomerIpFromHkFile(string filename)
        {
            string CutIpHk = null;
            string CutIpHk2 = null;
            string strgconstIP_Cntrol_Mapping_1 = "gconstIP_Cntrol_Mapping_1";
            string strgconstIP_Cntrol_Mapping_2 = "gconstIP_Cntrol_Mapping_2";
            string strgconstIP_Cntrol_Mapping_3 = "gconstIP_Cntrol_Mapping_3";
            string strgconstIP_Cntrol_Mapping_4 = "gconstIP_Cntrol_Mapping_4";
            string[] allTextLines = File.ReadAllLines(filename, Encoding.UTF8); //读取当前文件的所有行
            for (int j = 0; j < allTextLines.Length; j++)       //遍历每一行
            {
                if (allTextLines[j].Contains(strgconstIP_Cntrol_Mapping_1) || allTextLines[j].Contains(strgconstIP_Cntrol_Mapping_2) 
                    || allTextLines[j].Contains(strgconstIP_Cntrol_Mapping_3) || allTextLines[j].Contains(strgconstIP_Cntrol_Mapping_4))
                {
                    for (int i = 0; i < allTextLines[j].Length; i++)
                    {
                        if (allTextLines[j][i] == '"')
                        {
                            for (int m = 1; m < 9; m++)
                            {
                                CutIpHk += allTextLines[j][i + m].ToString();
                            }
                            break;
                        }
                    }
                    CutIpHk2 = CutIpHk + CutIpHk2;
                    CutIpHk = null;
                }
            }
            return CutIpHk2;
        }
        private void butUpdateFile_Click(object sender, EventArgs e)
        {
            string CustomerIpFromHkFile = null;
            int decimalresult = 0;
            string binaryresult = null;
            if(txtEditFile2.Text.Trim() == "")
            {
                MessageBox.Show("更新前请先打开文件！");
                return;
            }

            CustomerIpFromHkFile = GetCustomerIpFromHkFile(this.txtEditFile2.Text.Trim());

            Dictionary<string, int> dicHash1 = new Dictionary<string, int>();

            InitDictionaryForHashkey(dicHash1);//构造hashkey 128bit的字典

            for (int i = 0; i < 4; i++)
            {
                decimalresult = Convert.ToInt32(CustomerIpFromHkFile.Substring(i * 8, 8), 16);
                binaryresult += Convert.ToString(decimalresult, 2).PadLeft(32, '0');
            }

            if (UpdateDictionaryForHashkey(dicHash1, binaryresult) == true)
            {
                FillDataGridViewColumsForHashkey(dicHash1);
            }

            clsHashKey objFileUpdateHash = new clsHashKey();
            objFileUpdateHash.CustomerID = this.textBox2.Text.Trim();
            objFileUpdateHash.ModelID = this.textBox3.Text.Trim();
            objFileUpdateHash.ChipID = this.textBox4.Text.Trim();
            objFileUpdateHash.CustomerIP = this.textBox5.Text.Trim();
            objFileUpdateHash.CustomerHash = this.textBox6.Text.Trim();

            //检查数据的合法性：
            for (int i = 0; i < objFileUpdateHash.CustomerID.Length; i++)
            {
                if (!(objFileUpdateHash.CustomerID[i] >= '0' && objFileUpdateHash.CustomerID[i] <= '9'))
                {
                    MessageBox.Show("CustomerID 输入数据不合法！", "ERROR");
                    return;
                }
            }
            for (int i = 0; i < objFileUpdateHash.ModelID.Length; i++)
            {
                if (!(objFileUpdateHash.ModelID[i] >= '0' && objFileUpdateHash.ModelID[i] <= '9'))
                {
                    MessageBox.Show("ModelID 输入数据不合法！", "ERROR");
                    return;
                }
            }
            if (objFileUpdateHash.CustomerID.Length > 4 || objFileUpdateHash.ModelID.Length > 4 || objFileUpdateHash.ChipID.Length > 4)
            {
                MessageBox.Show("ID 输入数据过长！","ERROR");
                return;
            }
            else if (objFileUpdateHash.CustomerHash.Length != 0 && objFileUpdateHash.CustomerHash.Length != 32)
            {
                MessageBox.Show("CustomerHash 输入数据长度不标准！", "ERROR");
                return;
            }
            else if (objFileUpdateHash.CustomerIP.Length != 0 && objFileUpdateHash.CustomerIP.Length != 32)
            {
                MessageBox.Show("CustomerIP 输入数据长度不标准！", "ERROR");
                return;
            }

            UpdateHashFileByUserInput(this.txtEditFile2.Text.Trim(), objFileUpdateHash);    //根据user填的数据来更新目标文件

            MessageBox.Show("更新成功！");
        }
        public frmHashKey()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            InitializeComponent();
            //FillDataGridViewColums(this.dataGridView1);
        }
/*
        private void butFile1_Click(object sender, EventArgs e)
        {
            OpenFileDialog objOpenFileDialog = new OpenFileDialog();
            if (System.IO.File.Exists(this.txtEditFile1.Text))
            {
                objOpenFileDialog.FileName = this.txtEditFile1.Text;
            }
            objOpenFileDialog.Title = "请选择文件：";
            objOpenFileDialog.Filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx"; // |Excel Unicode Text File|*.txt
            objOpenFileDialog.DefaultExt = ".xls";
            if (objOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtEditFile1.Text = objOpenFileDialog.FileName.Trim();
                string strFilePath = objOpenFileDialog.FileName.Trim().ToUpper();
                Cursor = Cursors.WaitCursor;
                try
                {
                    gobjHashKeyList = new clsHashKeyList(strFilePath);
                    if (gobjHashKeyList == null)
                    {
                        MessageBox.Show("文件:" + this.txtEditFile1.Text, "非法文件", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    FillDataGridView(this.dataGridView1, gobjHashKeyList.gobjHashKeyList);
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("失败原因:[" + ex.Message + "]", "读取文件失败:");
                }

                Cursor = Cursors.Default;
            }
        }
*/
        private void butFile2_Click(object sender, EventArgs e)
        {
            OpenFileDialog objOpenFileDialog = new OpenFileDialog();
            if (System.IO.File.Exists(this.txtEditFile2.Text))
            {
                objOpenFileDialog.FileName = this.txtEditFile2.Text;
            }
            objOpenFileDialog.Title = "请选择文件：";
            objOpenFileDialog.Filter = "(Customer_Info.h)|Customer_Info.h|Customer_Info(*.h)|*.h"; // |Excel Unicode Text File|*.txt
            objOpenFileDialog.DefaultExt = ".h";
            if (objOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtEditFile2.Text = objOpenFileDialog.FileName.Trim();
            }
        }
/*
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog objOpenFileDialog = new OpenFileDialog();
            if (System.IO.File.Exists(this.txtEditFile1.Text))
            {
                objOpenFileDialog.FileName = this.txtEditFile1.Text;
            }
            objOpenFileDialog.Title = "请选择文件：";
            objOpenFileDialog.Filter = "(Customer_Info.h)|Customer_Info.h|Customer_Info(*.h)|*.h"; // |Excel Unicode Text File|*.txt
            objOpenFileDialog.DefaultExt = ".h";
            if (objOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox8.Text = objOpenFileDialog.FileName.Trim();
            }
        }
*/
        private void button1_Click(object sender, EventArgs e)
        {
            int decimalresult = 0;
            string tmpCbOK = null;
            string tmpCbNG = null;
            string binaryresult = null;
            string CustomerIp = textBox8.Text.Trim();
            Boolean checkresult = false;   
            string sWaitCheck = textBox_WaitCheck.Text.Trim();
            string sResultForCheckInput = null;

     
            //数据有效性校验
            if (CustomerIp.Length != 32)
            {
                label14.Text = null;
                MessageBox.Show("CustomerIp长度应该是64！");
                return;
            }
            if (sWaitCheck.Length == 0)
            {
                label14.Text = null;
                textBox7.Text = null;
                textBox9.Text = null;
                MessageBox.Show("请输入校验内容！");
                return;
            }
            for(int i = 0; i < 4; i++)
            {
                decimalresult = Convert.ToInt32(CustomerIp.Substring(i*8, 8), 16);
                binaryresult += Convert.ToString(decimalresult, 2).PadLeft(32, '0');
            }

            Dictionary<string, int> dicHash = new Dictionary<string, int>();
            
            InitDictionaryForHashkey(dicHash);//构造hashkey 128bit的字典

            sResultForCheckInput = CheckInputForHashkey(dicHash, sWaitCheck);

            if (sResultForCheckInput != null)
            {
                MessageBox.Show(sResultForCheckInput + "这些内容格式输入错误！");
            }
            else
            {
                if (UpdateDictionaryForHashkey(dicHash, binaryresult) == true)  //更新字典value
                {
                    checkresult = CheckIPForHashkey(dicHash, sWaitCheck, ref tmpCbOK, ref tmpCbNG);
                    FillDataGridViewColumsForHashkey(dicHash);
                    //richTextBox2.Text = PrintDictionKeyAndValue(dicHash);
                }
                else
                {
                    checkresult = false;
                }
            }
            textBox7.Text = tmpCbOK;
            textBox9.Text = tmpCbNG;
            if (checkresult == true)
            {
                label14.Text = "Pass!";
                label14.ForeColor = Color.Red;
            }
            else
            {
                label14.Text = "Fail!";
                label14.ForeColor = Color.Red;
            }
        }

        private string textBox_WaitCheck_Note = "请输入待校验的字节，以英文逗号';'分开,比如：VUDU;GAAC";
        private string textbox8_Note = "请输入32位16进制数字，比如：01102001CA0028502001030428F80000";
        private void Textbox1_Enter(object sender, EventArgs e)
        {
            if (textBox_WaitCheck.Text == textBox_WaitCheck_Note)
            {
                textBox_WaitCheck.Text = "";
            }
            textBox_WaitCheck.ForeColor = Color.Black;
        }
        private void Textbox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == textbox8_Note)
            {
                textBox8.Text = "";
            }
            textBox8.ForeColor = Color.Black;
        }
        private void Textbox1_Leave(object sender, EventArgs e)
        {
            if (textBox_WaitCheck.Text == "")
            {
                textBox_WaitCheck.Text = textBox_WaitCheck_Note;
                textBox_WaitCheck.ForeColor = Color.LightGray;
            }
        }
        private void Textbox8_Leave(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                textBox8.Text = textbox8_Note;
                textBox8.ForeColor = Color.LightGray;
            }
        }
        private void Textbox1_Enter(object sender, MouseEventArgs e)
        {
            if (textBox_WaitCheck.Text == textBox_WaitCheck_Note)
            {
                textBox_WaitCheck.Text = "";
            }
            textBox_WaitCheck.ForeColor = Color.Black;
        }
        private void Textbox8_Enter(object sender, MouseEventArgs e)
        {
            if (textBox8.Text == textbox8_Note)
            {
                textBox8.Text = "";
            }
            textBox8.ForeColor = Color.Black;
        }
    }

}
