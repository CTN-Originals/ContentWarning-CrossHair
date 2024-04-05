using BepInEx;
using BepInEx.Logging;
using CTNOriginals.ContentWarning.CrossHair.Handlers;
using CTNOriginals.ContentWarning.CrossHair.Utilities;

namespace CTNOriginals.ContentWarning.CrossHair;

[BepInPlugin("com.ctnoriginals.cw.crosshair", "CrossHair", "2.0.0")]
public class Plugin : BaseUnityPlugin {
	public static ManualLogSource ManualLog;

	public static string PluginGUID = "com.ctnoriginals.cw.crosshair";
	public static string PluginName = "CrossHair";
	public static string PluginVersion = "2.0.0";

	public static bool DebugMode = false;
	public static bool OutputDebugLogs = false;

	public static Config ConfigFile = new();

	private void Awake() {
		#if DEBUG
			DebugMode = true;
		#endif
		
		ManualLog = Logger;
		ManualLog.LogInfo($"Plugin {PluginName} is loaded! Version: {PluginVersion} ({(DebugMode ? "Debug" : "Release")})");

		ConfigFile.LoadFile();

		// Patches
		Patches.HelmetUIPatch.Patch();
		Patches.PlayerPatch.Patch();
	}

	private void Update() {
		// FadeHandler.Update();
	}
}


//- GAME/HelmetUI/Pivot/Others/
//> GAME/HelmetUI/Pivot/Others/Item Name