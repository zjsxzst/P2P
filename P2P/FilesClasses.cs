using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace P2P
{
    /// <summary>
    /// 文件调用类
    /// </summary>
    public class FilesClasses
    {
        /// <summary>
        /// 读取XML数据
        /// </summary>
        /// <param name="Path">XML文件地址,默认为Config.xml</param>
        /// <param name="ChildNode">子节点名</param>
        /// <param name="ChildNodeInformation">带获取数据(','分开)</param>
        /// <returns></returns>
        public static string[][] GetXMLData(string Path, string ChildNode, string ChildNodeInformation)
        {
            if (!string.IsNullOrWhiteSpace(Path) || Path.Length <= 0)
                Path = "Config.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(Path);
            //XmlElement rootElem = doc.DocumentElement;
            XmlNodeList personNodes = XmlNodes(doc.DocumentElement, ChildNode);
            return GetChildNodeData(personNodes, ChildNodeInformation);
        }
        /// <summary>
        /// 获取XML中所有的目标子节点
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="ChildNode"></param>
        /// <returns></returns>
        private static XmlNodeList XmlNodes(XmlElement Data, string ChildNode)
        {
            return Data.GetElementsByTagName(ChildNode);
        }
        /// <summary>
        /// 获取具体值
        /// </summary>
        /// <param name="Nodes">XML子节点数据</param>
        /// <param name="DataList">需要读取的数据</param>
        /// <returns></returns>
        private static string[][] GetChildNodeData(XmlNodeList Nodes, string DataList)
        {
            string[] Data = DataList.Split(',');
            string[][] dates = new string[Nodes.Count][];
            for (int i = 0; i < Nodes.Count; i++)
            {
                dates[i] = new string[Data.Length];
                for (int j = 0; j < Data.Length; j++)
                {
                    dates[i][j] = ((XmlElement)Nodes.Item(i)).GetAttribute(Data[j]);
                }
            }
            return dates;
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="stu"></param>
        /// <param name="root"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool addXml(string path,string superclass,string subclass,string listing,string data)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                if(!string.IsNullOrWhiteSpace(path))
                    path = "Config.xml";
                doc.Load(path);
                XmlNode node = doc.SelectSingleNode(superclass);
                XmlNodeList xnl = node.ChildNodes;
                XmlElement ChildNode = doc.CreateElement(subclass);
                string[] RowNameList = listing.Split(',');
                string[] DataList = data.Split(',');
                //XmlElement Branch = doc.CreateElement(subclass);
                for (int i=0;i< RowNameList.Length;i++)
                    ChildNode.SetAttribute(RowNameList[i], DataList[i]);
               //ChildNode.AppendChild(Branch);
                //xe1.SetAttribute("name", stu.ClassNo);
                //XmlElement xe2 = doc.CreateElement("student");
                //xe2.SetAttribute("studentId", stu.StudentId);
                //xe2.SetAttribute("name", stu.StudentName);
                //xe1.AppendChild(xe2);
                node.AppendChild(ChildNode);
                doc.Save(path);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public static bool InitXml()
        {
            try
            {

                //File.Create("Config.xml").Close();
                XmlDocument xmlDoc = new XmlDocument();
                //创建类型声明节点    
                XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                xmlDoc.AppendChild(node);
                //创建根节点    
                XmlNode root = xmlDoc.CreateElement("root");
                xmlDoc.AppendChild(root);
                //xmlDoc.AppendChild(xmlDoc.CreateElement("/root"));
                xmlDoc.Save("Config.xml");
                string data = "connStr," + TextProcessing.SuperEncrypt("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Jet OLEDB:Database Password ={1}", "zjsxzsta", "zjsxzstb");

                FilesClasses.addXml("Config.xml", "root", "content", "name,value", data);
                data = "honeybee,";
                FilesClasses.addXml("Config.xml", "root", "content", "name,value", data);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
