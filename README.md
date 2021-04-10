# XMLSettings
A simple class for storing user settings in XML for a .NET application

## Implementation

```csharp
//---------------------------------------------------------

// Specify settings and default values

StringCollection settings = new StringCollection();

settings.Add("DefaultString,CodeProject");
settings.Add("DefaultChecked,True");

XMLSettings.AppSettingsFile = "Settings.xml"; // Set the file path
XMLSettings.InitializeSettings(settings); // Create file and set default values

//----------------------------------------------------------

// Read settings values

textBox1.Text = XMLSettings.GetSettingsValue("DefaultString");
checkBox1.Checked = bool.Parse(XMLSettings.GetSettingsValue("DefaultChecked"));
```
