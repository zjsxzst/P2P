using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.XmlConfiguration;
namespace P2P.DoMain
{
    //修改根目录名，但是似乎无效
    //[XmlRoot("SheduledShows")]
    public class XmlLivingExample
    {
        //将对象属性序列化为父标记的属性
        [XmlAttribute("Number")]
        public int HouseNo { get; set; }
        //序列化时修改名称
        [XmlElement("Street")]
        public string StreetName { get; set; }
        //序列化时修改名称
        [XmlElement("CityName")]
        public string City { get; set; }
        //不进行序列化
        [XmlIgnore]
        public string PoAddress { get; set; }
        public Address Address { get; set; }
    }
    public class Address
    {
        //介绍
        [XmlAttribute("HouseNo")]
        public int HouseNo { get; set; }
        //将特定的属性添加为标签的内在文本
        [XmlText]
        public string Data { get; set; }
    }
}
