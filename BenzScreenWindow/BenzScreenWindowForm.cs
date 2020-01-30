using BenzScreenTools;
using BenzScreenTools.XML;
using BenzScreenTools.XML.Model;
using BenzScreenWindowWebsite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace BenzScreenWindow
{
    public partial class BenzScreenWindowForm : Form
    {
        
        public BenzScreenWindowForm()
        {
            InitializeComponent();

           

            
        }

        private void BenzScreenWindowForm_Load(object sender, EventArgs e)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XMLConfig.xml"));

            var model = XmlSerializeHelper.DESerializer<XMLConfig>(xmlDoc.InnerXml);

            //获取屏幕信息
            Screen[] sc;
            sc = Screen.AllScreens;

            foreach (var i in model.Nodes)
            {
                if (i.Window >= sc.Length)
                {
                    i.Window = 0;
                }

                var from = new BenzScreenWindowWebsiteForm(i, sc[i.Window].Bounds.Left, sc[i.Window].Bounds.Top);

                if (model.Nodes.IndexOf(i) == model.Nodes.Count() - 1)
                {
                    from.SetIsLast();
                }

                from.Show();

                Thread.Sleep(1000);
            }
        }  

    }
}
