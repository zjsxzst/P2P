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
    }
}
