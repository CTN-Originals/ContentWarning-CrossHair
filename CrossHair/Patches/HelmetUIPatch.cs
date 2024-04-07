using TMPro;
using UnityEngine;

using CTNOriginals.ContentWarning.CrossHair.Utilities;
using System;
using CTNOriginals.ContentWarning.CrossHair.Handlers;

namespace CTNOriginals.ContentWarning.CrossHair.Patches;

public class HelmetUIPatch {
	public static Transform CrossHair;
	public static TextMeshProUGUI CrossHairTMP;
	public static float CrossHairAlpha = 200;
	public static Transform CrossHairShadow;
	public static float CrossHairShadowAlpha = 100;

	public static void Patch() {
		On.HelmetUIToggler.Start += StartPatch;
		On.HelmetUIToggler.Update += UpdatePatch;
	}

	private static void StartPatch(On.HelmetUIToggler.orig_Start orig, HelmetUIToggler self) {
		orig(self);
		CLogger.LogDebug($"HelmetUIToggler.Start() called");

		Transform parent = GameObject.Find("GAME/HelmetUI/Pivot/Others/").transform;
		CLogger.LogDebug($"CrossHair Parent: {parent.name}");

		CrossHair = new GameObject().AddComponent<TextMeshProUGUI>().transform;
		CrossHairTMP = CrossHair.GetComponent<TextMeshProUGUI>();
		RectTransform rect = CrossHairTMP.rectTransform;

		CrossHair.name = "CrossHair";
		CrossHairTMP.SetText("-  +  -");
		CrossHairTMP.alignment = TextAlignmentOptions.Center;

		rect.SetParent(parent, false);
		rect.anchoredPosition = new Vector2(0, 0);
		rect.offsetMin = new Vector2(-500, -500);
		rect.offsetMax = new Vector2(500, 500);

		string hexColor = Config.CrossHairColor.Value;
		if (hexColor.Length != 6) { hexColor = HexFormatException($"character amount: \"{hexColor}\""); }

		int argb = 0xffffff;
		try { argb = int.Parse(hexColor.Replace("#", ""), System.Globalization.NumberStyles.HexNumber); }
		catch (System.FormatException) { argb = int.Parse(HexFormatException($"color: \"{hexColor}\""), System.Globalization.NumberStyles.HexNumber); }
		System.Drawing.Color clr = System.Drawing.Color.FromArgb(argb);

		CrossHairAlpha = (byte)(Config.CrossHairOpacity.Value * 255 / 100); //? convert 0 - 100 to 0 - 255
		CrossHairShadowAlpha = (byte)(CrossHairAlpha * 50 / 100); //? Calculate shadow alpha as 50% of the crosshair alpha from 0-100 to 0-255

		CrossHairTMP.text = Config.CrossHairText.Value;
		CrossHairTMP.fontSize = Config.CrossHairSize.Value;
		CrossHairTMP.color = new Color32(clr.R, clr.G, clr.B, (byte)Mathf.RoundToInt(CrossHairAlpha));

		CLogger.LogDebug($"CrossHairColor: ({clr.R}, {clr.G}, {clr.B}, {CrossHairAlpha})");

		CrossHairTMP.alignment = TextAlignmentOptions.Center;
		// CrossHairTMP.font = __instance.controlTipLines[0].font;
		CrossHairTMP.enabled = true;

		if (Config.CrossHairShadow.Value != true) { return; }

		CrossHairShadow = GameObject.Instantiate(CrossHair, parent);
		TextMeshProUGUI shadowText = CrossHairShadow.GetComponent<TextMeshProUGUI>();
		CrossHairShadow.name = "CrossHairShadow";
		shadowText.fontSize = Config.CrossHairSize.Value;
		shadowText.color = new Color32(byte.MinValue, byte.MinValue, byte.MinValue, (byte)CrossHairShadowAlpha);
		shadowText.rectTransform.localPosition = new Vector3(2, -2, 0);

		rect.SetAsLastSibling();

		FadeHandler.FadeFields.Clear();
		FadeHandler.Initiate();
	}

	private static void UpdatePatch(On.HelmetUIToggler.orig_Update orig, HelmetUIToggler self) {
		orig(self);
		FadeHandler.Update();
	}
	
	private static string HexFormatException(string message = "color") {
		CLogger.LogMessage($"Invalid hex {message}, using default color (ffffff)");

		Config.CrossHairColor.Value = "ffffff";
		Config.Configurations.Save();
		return "ffffff";
	}
}