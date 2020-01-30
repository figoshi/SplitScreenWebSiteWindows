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

        /// <summary>
        /// 该组字段用于判断哪个是最后一个循环的屏幕，后续需要让该屏幕设置焦点命中
        /// 否则多屏幕下在不用输入法情况下会出现无限卡顿
        /// </summary>
        bool m_IsLast = false;

        public bool IsLast
        {
            get { return m_IsLast; }
        }

        public BenzScreenWindowWebsiteForm(Node node, int x, int y)
        {
            SetNode(node, x, y);
        }

        /// <summary>
        /// 该组字段用于判断哪个是最后一个循环的屏幕，后续需要让该屏幕设置焦点命中
        /// 否则多屏幕下在不用输入法情况下会出现无限卡顿
        /// </summary>
        public void SetIsLast()
        {
            m_IsLast = true;
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



        //调用API
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow(); //获得本窗体的句柄
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体
        //定义变量,句柄类型
        public IntPtr Handle1;
        Timer timer2 = new Timer();


        //加载一个定时器控件,验证当前WINDOWS句柄是否和本窗体的句柄一样,如果不一样,则激活本窗体,用户解决无限循环卡顿问题
        private void timer2_Tick(object sender, EventArgs e)
        {
            while (true) //持续使该窗体置为最前,屏蔽该行则单次置顶
            {
                var handle = GetForegroundWindow();

                if (Handle1 != handle)
                {
                    SetForegroundWindow(Handle1);
                }
                else
                {
                    timer2.Stop();

                    break;
                }
            }
        }
    

        private void BenzScreenWindowWebsiteForm_Load(object sender, EventArgs e)
        {
            var form = (BenzScreenWindowWebsiteForm)sender;

            if (form.IsLast) {
                Handle1 = this.Handle;
                timer2.Tick += new EventHandler(timer2_Tick);
                timer2.Interval = 1000;
                timer2.Start();
            }
        }
    }
}
