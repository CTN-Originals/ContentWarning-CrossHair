using BepInEx;
using BepInEx.Configuration;

using CTNOriginals.ContentWarning.CrossHair.Utilities;

namespace CTNOriginals.ContentWarning.CrossHair;

public class Config {
	public static ConfigFile Configurations = new ConfigFile(Paths.ConfigPath + "\\" + Plugin.PluginGUID + ".cfg", true);

	public static ConfigEntry<string> CrossHairText;
	public static ConfigEntry<float> CrossHairSize;
	public static ConfigEntry<bool> CrossHairShadow;

	public static ConfigEntry<string> CrossHairColor;
	public static ConfigEntry<int> CrossHairOpacity;
	// public static ConfigEntry<bool> CrossHairFading;

	public void LoadFile() {
		CrossHairText = Configurations.Bind("!General", "CrossHairText", "-  +  -", "Text to display as crosshair (use \\n for new line)");
		CrossHairSize = Configurations.Bind("!General", "CrossHairSize", 25f, "Size of the crosshair");
		CrossHairShadow = Configurations.Bind("!General", "CrossHairShadow", true, "Whether to display a shadow behind the crosshair");

		CrossHairColor = Configurations.Bind("Appearance", "CrossHairColor", "ffffff", "Color of the crosshair in hexadecimal (Do not include the #)");
		CrossHairOpacity = Configurations.Bind("Appearance", "CrossHairOpacity", 50, "Opacity of the crosshair (0 to 100)%");
		// CrossHairFading = Configurations.Bind("Appearance", "CrossHairFading", true, "Whether the crosshair should fade in and out in specific situations");

		CLogger.LogInfo($"CrossHairText: {CrossHairText.Value}");
		CLogger.LogInfo($"CrossHairSize: {CrossHairSize.Value}");
		CLogger.LogInfo($"CrossHairShadow: {CrossHairShadow.Value}");

		CLogger.LogInfo($"CrossHairColor: {CrossHairColor.Value}");
		CLogger.LogInfo($"CrossHairOpacity: {CrossHairOpacity.Value}");
		// CLogger.LogInfo($"CrossHairFading: {CrossHairFading.Value}");

		Configurations.Save();
	}
}