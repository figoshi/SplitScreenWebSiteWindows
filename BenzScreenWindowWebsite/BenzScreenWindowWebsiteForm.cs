using BenzScreenTools.XML.Model;
using CefSharp.WinForms;
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

namespace BenzScreenWindowWebsite
{
    public partial class BenzScreenWindowWebsiteForm : Form
    {
        static BenzScreenWindowWebsiteForm()
        {
            var setting = new CefSettings();
            
            // 设置语言
            setting.Locale = "zh-CN";
            //cef设置userAgent
            setting.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
            //配置浏览器路径

            var path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "CefSharp.BrowserSubprocess.exe");

            setting.BrowserSubprocessPath = path;

            CefSharp.Cef.Initialize(setting, performDependencyCheck: true, browserProcessHandler: null);
        }

        public BenzScreenWindowWebsiteForm()
        {
        }

        public BenzScreenWindowWebsiteForm(Node node,int x,int y)
        {
            SetNode(node,x,y);
        }



        private void SetNode(Node node, int x, int y)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(x, y);

            InitializeComponent();

            var wb = new ChromiumWebBrowser(node.Src);

            //设置停靠方式
            wb.Dock = DockStyle.Fill;

            //加入到当前窗体中
            this.Controls.Add(wb);

            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void BenzScreenWindowWebsiteForm_Load(object sender, EventArgs e)
        {

        }
    }
}
