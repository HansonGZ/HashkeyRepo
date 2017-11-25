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
        private string textbox1Note = "请输入待校验的字节，以英文逗号,分开";
        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }

        /// <summary>
        /// 填充列
        /// <summary>
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
            // 添加Table列
            // PartNumber
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

        private void InitDictionaryForHashkey(Dictionary<string, int> dicHash)
        {
            dicHash.Add("BBE Digital", 0); //index = 0;

        }
        private bool UpdateDictionaryForHashkey(Dictionary<string, int> dicHash, string value)
        {
            int i = 0;
            if(value.Length != 128)
            {
                return false;
            }
            foreach (string key in dicHash.Keys)
            {
                dicHash[key] = value[i++];
            }
            return true;
        }

        private bool CheckIPForHashkey(Dictionary<string, int> dicHash, string tmpvalue)
        {
            string[] sArray = tmpvalue.Split(',');
            foreach (string key in dicHash.Keys)
            {
                //dicHash[key] = sArray
            }
            return true;
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

        private void butUpdateFile_Click(object sender, EventArgs e)
        {
            clsHashKey objFileUpdateHash = new clsHashKey();
            objFileUpdateHash.CustomerID = this.textBox2.Text;
            objFileUpdateHash.ModelID = this.textBox3.Text;
            objFileUpdateHash.ChipID = this.textBox4.Text;
            objFileUpdateHash.CustomerIP = this.textBox5.Text;
            objFileUpdateHash.CustomerHash = this.textBox6.Text;

            //检查数据的合法性：
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

            UpdateHashFileByUserInput(this.txtEditFile2.Text, objFileUpdateHash);    //根据user填的数据来更新目标文件

            MessageBox.Show("更新成功！");
        }
        public frmHashKey()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            InitializeComponent();
            FillDataGridViewColums(this.dataGridView1);
        }

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

        private void butFile2_Click(object sender, EventArgs e)
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
                this.txtEditFile2.Text = objOpenFileDialog.FileName.Trim();
            }
        }


        private void Textbox1_Enter(object sender, EventArgs e)
        {
            if (textBox_WaitCheck.Text == textbox1Note)
            {
                textBox_WaitCheck.Text = "";
            }
            textBox_WaitCheck.ForeColor = Color.Black;
        }

        private void Textbox1_Leave(object sender, EventArgs e)
        {
            if (textBox_WaitCheck.Text == "")
            {
                textBox_WaitCheck.Text = textbox1Note;
                textBox_WaitCheck.ForeColor = Color.LightGray;
            }
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            string CustomerIp = textBox8.Text;
            string sWaitCheck = textBox_WaitCheck.Text;
            Dictionary<string, int> dicHash = new Dictionary<string, int>();
            InitDictionaryForHashkey(dicHash);
            if(UpdateDictionaryForHashkey(dicHash, CustomerIp) == true)
            {
                CheckIPForHashkey(dicHash, sWaitCheck);
            }
        }
    }

}
