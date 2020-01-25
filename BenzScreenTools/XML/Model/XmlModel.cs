using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenzScreenTools.XML.Model
{
    public class XMLConfig
    {
        public List<Node> Nodes { get; set; }
    }

    /// <summary>
    /// 用户信息类
    /// </summary>
    public class Node
    {
        public string Src { get; set; }

        public int Window { get; set; }
    }
}
