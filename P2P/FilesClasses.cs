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
        public static string[][] GetXMLData(string Path,string ChildNode,string ChildNodeInformation)
        {
            if (!string.IsNullOrWhiteSpace(Path)||Path.Length<=0)
                Path = "Config.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(Path);
            //XmlElement rootElem = doc.DocumentElement;
            XmlNodeList personNodes = XmlNodes(doc.DocumentElement, ChildNode);
            string[][] dates = GetChildNodeData(personNodes, ChildNodeInformation);
            return dates;
        }
        /// <summary>
        /// 获取XML中所有的目标子节点
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="ChildNode"></param>
        /// <returns></returns>
        private static XmlNodeList XmlNodes(XmlElement Data,string ChildNode)
        {
            return Data.GetElementsByTagName(ChildNode);
        }
        /// <summary>
        /// 获取具体值
        /// </summary>
        /// <param name="Nodes">XML子节点数据</param>
        /// <param name="DataList">需要读取的数据</param>
        /// <returns></returns>
        private static string[][] GetChildNodeData(XmlNodeList Nodes,string DataList)
        {
            string[] Data = DataList.Split(',');
            string[][] dates = new string[Nodes.Count][];
            for(int i=0;i<Nodes.Count;i++)
            {
                dates[i] = new string[Data.Length];
                for (int j=0;j< Data.Length;j++)
                {
                    dates[i][j] = ((XmlElement)Nodes.Item(i)).GetAttribute(Data[j]);
                }
            }
            return dates;
        }

    }
}
