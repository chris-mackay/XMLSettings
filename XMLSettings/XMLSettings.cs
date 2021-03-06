using System.Collections.Generic;
using System.IO;
using System.Xml;

public static class XMLSettings
{
    public static string AppSettingsFile;

    private static bool SettingsFileExists()
    {
        bool flag = false;

        if (File.Exists(AppSettingsFile))
            flag = true;
        else
            flag = false;

        return flag;
    }

    public static string GetSettingsValue(string _Field)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(AppSettingsFile);

        XmlNode node = null;
        node = doc.SelectSingleNode("//Settings/" + _Field);

        string value = string.Empty;

        if (node == null)
            value = string.Empty;
        else
            value = node.InnerText;

        return value;
    }

    public static void SetSettingsValue(string _Field, string _Value)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(AppSettingsFile);

        _Field = "//Settings/" + _Field;

        if (doc.SelectSingleNode(_Field) == null)
        {
            _Field = _Field.Replace("//Settings/", "");
            XmlNode field = doc.CreateElement(_Field);
            field.InnerText = _Value;
            doc.DocumentElement.AppendChild(field);
            doc.Save(AppSettingsFile);
        }
        else
        {
            XmlNode node = null;
            node = doc.SelectSingleNode(_Field);
            node.InnerText = _Value;
            doc.Save(AppSettingsFile);
        }
    }

    public static void InitializeSettings(Dictionary<string,string> _AppSettings)
    {
        if (!SettingsFileExists())
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter XmlWrt = XmlWriter.Create(AppSettingsFile, settings);

            {
                var withBlock = XmlWrt;
                withBlock.WriteStartDocument();

                withBlock.WriteComment("Application Settings");
                withBlock.WriteStartElement("Settings");

                foreach (KeyValuePair<string, string> entry in _AppSettings)
                {

                    string settingName = entry.Key;
                    string defaultValue = entry.Value;

                    withBlock.WriteStartElement(settingName);
                    withBlock.WriteString(defaultValue);
                    withBlock.WriteEndElement();
                }

                withBlock.WriteEndDocument();
                withBlock.Close();
            }

            XmlWrt = null;
        }
        else
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(AppSettingsFile);
            XmlElement elm = xmlDoc.DocumentElement;
            XmlNodeList lstSettings = elm.ChildNodes;
            List<string> nodeNames = new List<string>();

            foreach (XmlNode node in lstSettings) nodeNames.Add(node.Name);

            foreach (KeyValuePair<string, string> entry in _AppSettings)
            {
                string settingName = entry.Key;
                string defaultValue = entry.Value;

                if (!nodeNames.Contains(settingName))
                {
                    XmlNode newSetting = xmlDoc.CreateElement(settingName);
                    newSetting.InnerText = defaultValue;
                    xmlDoc.DocumentElement.AppendChild(newSetting);
                    xmlDoc.Save(AppSettingsFile);
                }
            }
        }
    }
}

