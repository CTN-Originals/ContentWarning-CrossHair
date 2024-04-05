using UnityEngine;
using System;
using System.Collections;

using CTNOriginals.ContentWarning.CrossHair.Utilities;
using CTNOriginals.ContentWarning.CrossHair.Handlers;

namespace CTNOriginals.ContentWarning.CrossHair.Patches;

public class PlayerPatch {
	public static Box<bool> AimingCamera = new(false); //TODO make this work across scenes
	// public static FadeHandler.FadeValueField AimingCameraFade = new(ref AimingCamera, true);

	public static void Patch() {
		// On.Player.Start += StartPatch;
		// On.Player.Update += UpdatePatch;
	}

	static bool hasStarted = false;
	private static IEnumerator StartPatch(On.Player.orig_Start orig, Player self) {
		IEnumerator origEnum = orig(self);
		while (origEnum.MoveNext()) yield return origEnum.Current;
		CLogger.LogDebug("Player.Start()");

		//? This is a very complex way of doing it but this is meant to be able to hold a lot of these changing values
		FadeHandler.FadeFields.Add(new FadeHandler.FadeValueField(AimingCamera, true, 0, 0.1f, 1f));
	}

	private static void UpdatePatch(On.Player.orig_Update orig, Player self) {
		orig(self);

		AimingCamera.Value = (bool)(self.input.aimIsPressed && self.data.currentItem && self.data.currentItem.name.Contains("Camera"));

		FadeHandler.Update();
	}
}