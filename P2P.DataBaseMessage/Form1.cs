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
            //IList<Table1> IT1 = SqlProcessing<Table1>.ExeQuerys("select * from Table1");
            //string[][] data = FilesClasses.GetXMLData("", "person", "name,value");
            //FilesClasses.addXml("Config.xml", "root", "person", "name,value", "name1,value1");
            //for (int i = 0; i < data.Length; i++)
            //{
            //    for (int j = 0; j < data[i].Length; j++)
            //    {
            //        string a = data[i][j];
            //    }
            //}
        }
        /// <summary>
        /// 检查config文件是否存在，不存在则创建
        /// </summary>
        private void init()
        {

            if (!File.Exists("Config.xml"))
            {
                FilesClasses.InitXml();
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
                if(FilesClasses.InitXml(textBox1.Text, textBox2.Text))
                {
                    textBox2.Text = "";
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("保存错误！");
                }
            }
        }
    }
}
