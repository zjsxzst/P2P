using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2P;
using System.Xml;
using P2P.DoMain;
using P2P.DoMain.Virtual;

namespace P2P.DataBaseMessage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            init();
            

            IList<Table1> IT1 = SqlProcessing<Table1>.ExeQuerys("select * from Table1");
            string[][] data = FilesClasses.GetXMLData("", "person", "name,value");
            FilesClasses.addXml("Config.xml", "root", "person", "name,value", "name1,value1");
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    string a = data[i][j];
                }
            }
        }
        /// <summary>
        /// 检查config文件是否存在，不存在则创建
        /// </summary>
        private void init()
        {

            if (!File.Exists("Config.xml"))
            {
                SqlData sd = new SqlData();
                sd.connStr= TextProcessing.SuperEncrypt("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Jet OLEDB:Database Password ={1}", "zjsxzsta", "zjsxzstb");
                sd.honeybee= TextProcessing.SuperEncrypt("", "zjsxzsta", "zjsxzstb");
                string erro = "";
                //判断写入单表xml是否成功
                if (!XmlTest<SqlData>.Serialize(sd, "Config.xml", ref erro))
                    MessageBox.Show(erro);
                //FilesClasses.InitXml();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = new char();
            }
            else
                textBox2.PasswordChar = '■';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists("Config.xml"))
            {
                SqlData sd = new SqlData();
                sd.connStr = TextProcessing.SuperEncrypt(textBox1.Text, "zjsxzsta", "zjsxzstb");
                sd.honeybee = TextProcessing.SuperEncrypt(textBox2.Text, "zjsxzsta", "zjsxzstb");
                string erro = "";
                //判断写入单表xml是否成功
                if (!XmlTest<SqlData>.Serialize(sd, "Config.xml", ref erro))
                    MessageBox.Show(erro);
                //if(FilesClasses.InitXml(textBox1.Text, textBox2.Text))
                //{
                //    textBox2.Text = "";
                //    textBox1.Text = "";
                //}
                //else
                //{
                //    MessageBox.Show("保存错误！");
                //}
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //创建范例表表
            Address ar = new Address();
            XmlLivingExample XLE = new XmlLivingExample();
            XLE.HouseNo = 4;
            XLE.StreetName = "Rohini";
            XLE.City = "Delhi";
            XLE.PoAddress = "asd";
           
            ar.HouseNo = XLE.HouseNo * 1;
            ar.Data = XLE.StreetName;

            XLE.Address = ar;
            string erro="";
            //判断写入单表xml是否成功
            if (!XmlTest<XmlLivingExample>.Serialize(XLE, "Config.xml", ref erro))
                MessageBox.Show(erro);
            //创建反序列化表
            XmlLivingExample XLE_DS = new XmlLivingExample();
            //单表反序列化，返回值也是bool形
            XmlTest<XmlLivingExample>.DSerialize(ref XLE_DS, "Config.xml",ref erro);
            XmlLivingExample XLE1 = new XmlLivingExample();
            XLE1.HouseNo = 5;
            XLE1.StreetName = "Rohini1";
            XLE1.City = "Delhi2";
            XLE1.PoAddress = "asd";
            ar.HouseNo = XLE1.HouseNo * 2;
            ar.Data = XLE1.StreetName;
            XLE1.Address = ar;
            List<XmlLivingExample> LXE = new List<XmlLivingExample>();
            LXE.Add(XLE); LXE.Add(XLE1);
            //判断写入多表xml是否成功
            if (!XmlTest<XmlLivingExample>.Serialize(LXE, "Config.xml", ref erro))
                MessageBox.Show(erro);
            //多表反序列化，返回值也是bool形
            List<XmlLivingExample> Test = new List<XmlLivingExample>();
            XmlTest<XmlLivingExample>.DSerializeList(ref Test, "Config.xml",ref erro);
        }
    }

}
