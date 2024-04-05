using UnityEngine;

namespace CTNOriginals.ContentWarning.CrossHair.Utilities;

public static class CLogger {
	public static void Log(string message) 			{ SendLog(message, "Log"); }
	public static void LogInfo(string message) 		{ SendLog(message, "LogInfo"); }
	public static void LogError(string message) 	{ SendLog(message, "LogError"); }
	public static void LogWarning(string message) 	{ SendLog(message, "LogWarning"); }
	public static void LogDebug(string message) 	{ SendLog(message, "LogDebug"); }
	public static void LogFatal(string message) 	{ SendLog(message, "LogFatal"); }
	public static void LogMessage(string message) 	{ SendLog(message, "LogMessage"); }

	private static void SendLog(string message, string level = null) {
		if (!Plugin.DebugMode && (level == "LogDebug" || level == "LogInfo")) return;

		switch(level) {
			case "LogInfo": 	Plugin.ManualLog.LogInfo(message); 		break;
			case "LogError": 	Plugin.ManualLog.LogError(message); 	break;
			case "LogWarning": 	Plugin.ManualLog.LogWarning(message);	break;
			case "LogDebug": 	Plugin.ManualLog.LogDebug(message); 	break;
			case "LogFatal": 	Plugin.ManualLog.LogFatal(message); 	break;
			case "LogMessage": 	Plugin.ManualLog.LogMessage(message);	break;
			default: {
				if (level != "Log") Debug.Log($"[{level}]: {message}");
				else Debug.Log(message);
			} break;
		}
	}
}
