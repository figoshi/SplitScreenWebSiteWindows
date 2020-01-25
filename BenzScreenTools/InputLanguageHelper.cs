using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenzScreenTools
{
    public class InputLanguageHelper
    {
        /// <summary>
        /// 获取当前输入法
        /// </summary>
        /// <returns></returns>
        public static string GetCultureType()
        {
            var currentInputLanguage = InputLanguage.CurrentInputLanguage;
            var cultureInfo = currentInputLanguage.Culture;
            //同 cultureInfo.IetfLanguageTag;
            return cultureInfo.Name;
        }

        /// <summary>
        /// 切换输入法
        /// </summary>
        public static void SwitchToEnLanguageMode()
        {
            var installedInputLanguages = InputLanguage.InstalledInputLanguages;

            var entity = installedInputLanguages.Cast<InputLanguage>().Single(i => i.LayoutName == "美式键盘");

            if (entity != null)
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(entity.Culture);
            }
        }
    }
}
