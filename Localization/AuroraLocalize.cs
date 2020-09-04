using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AuroraFramework.Localization
{
    /// <summary>
    /// 本地化
    /// </summary>
    internal class AuroraLocalize
    {
        private DataSet languageDataSet;

        public string DefaultLanguage()
        {
            return "ENU";
        }

        public string CurrentLanguage()
        {
            string culture = Application.CurrentCulture.Name;
            string twoISO = Application.CurrentCulture.TwoLetterISOLanguageName;
            string threeISO = Application.CurrentCulture.ThreeLetterISOLanguageName;
            string win = Application.CurrentCulture.ThreeLetterWindowsLanguageName;
            string displayName = Application.CurrentCulture.DisplayName;
            string englishName = Application.CurrentCulture.EnglishName;

            /*
            CULTURE ISO ISO WIN DISPLAYNAME            ENGLISHNAME
            zh-CN  zh  zho CHS Chinese (Simplified)   Chinese (Simplified)
            zh-CHT  zh  zho CHT Chinese (Traditional)  Chinese (Traditional)
            en      en  eng ENU English                English
            */

            if (win.Length == 0)
                win = this.DefaultLanguage();
            return win;
        }

        public AuroraLocalize(string ctrlName)
        {
            this.ImportManifestResource(ctrlName);
        }

        public AuroraLocalize(Control ctrl)
        {
            this.ImportManifestResource(ctrl.Name);
        }

        private void ImportManifestResource(string ctrlName)
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            string name = callingAssembly.GetName().Name + ".Localization." + this.CurrentLanguage() + "." + ctrlName + ".xml";

            Stream manifestResourceStream = callingAssembly.GetManifestResourceStream(name);
            if (manifestResourceStream == null)
            {
                string defaultName = callingAssembly.GetName().Name + ".Localization." + this.DefaultLanguage() + "." + ctrlName + ".xml";
                manifestResourceStream = callingAssembly.GetManifestResourceStream(defaultName);
            }
            if (this.languageDataSet == null)
                this.languageDataSet = new DataSet();
            if (manifestResourceStream == null)
                return;
            DataSet dataSet = new DataSet();
            int num = (int)dataSet.ReadXml(manifestResourceStream);
            this.languageDataSet.Merge(dataSet);
            manifestResourceStream.Close();
        }

        private string ConvertVar(object var)
        {
            return var == null ? "" : var.ToString();
        }

        public string Translate(string key)
        {
            if (string.IsNullOrEmpty(key))
                return "";
            if (this.languageDataSet == null || this.languageDataSet.Tables["Localization"] == null)
                return "~" + key;
            DataRow[] dataRowArray = this.languageDataSet.Tables["Localization"].Select("Key='" + key + "'");
            return dataRowArray.Length <= 0 ? "~" + key : dataRowArray[0]["Value"].ToString();
        }

        public string Translate(string key, object var1)
        {
            return this.Translate(key).Replace("#1", this.ConvertVar(var1));
        }

        public string Translate(string key, object var1, object var2)
        {
            return this.Translate(key).Replace("#1", this.ConvertVar(var1)).Replace("#2", this.ConvertVar(var2));
        }

        public string GetValue(string key, object var1, object var2, object var3)
        {
            return this.Translate(key).Replace("#1", this.ConvertVar(var1)).Replace("#2", this.ConvertVar(var2)).Replace("#3", this.ConvertVar(var3));
        }

        public string GetValue(string key, object var1, object var2, object var3, object var4)
        {
            return this.Translate(key).Replace("#1", this.ConvertVar(var1)).Replace("#2", this.ConvertVar(var2)).Replace("#3", this.ConvertVar(var3)).Replace("#4", this.ConvertVar(var4));
        }

        public string GetValue(string key, object var1, object var2, object var3, object var4, object var5)
        {
            return this.Translate(key).Replace("#1", this.ConvertVar(var1)).Replace("#2", this.ConvertVar(var2)).Replace("#3", this.ConvertVar(var3)).Replace("#4", this.ConvertVar(var4)).Replace("#5", this.ConvertVar(var5));
        }
    }
}
