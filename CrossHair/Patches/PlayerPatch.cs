using UnityEngine;
using System;
using System.Collections;

using CTNOriginals.ContentWarning.CrossHair.Utilities;
using CTNOriginals.ContentWarning.CrossHair.Handlers;

namespace CTNOriginals.ContentWarning.CrossHair.Patches;

public class PlayerPatch {
	public static Box<bool> AimingCamera = new(false);

	private static bool hasStarted = false;
	private static bool registerOnStart = false;
	
	public static void Patch() {
		On.Player.Start += StartPatch;
		On.Player.Update += UpdatePatch;
	}

	private static IEnumerator StartPatch(On.Player.orig_Start orig, Player self) {
		IEnumerator origEnum = orig(self);
		while (origEnum.MoveNext()) yield return origEnum.Current;
		CLogger.LogDebug("Player.Start()");

		hasStarted = true;
		if (registerOnStart) {
			registerOnStart = false;
			RegisterFields();
		}
	}

	

	private static void UpdatePatch(On.Player.orig_Update orig, Player self) {
		orig(self);
		if (!self.data.isLocal) { return; }

		AimingCamera.Value = self.input.aimIsPressed && self.data.currentItem && self.data.currentItem.name.Contains("Camera");
	}

	public static void RegisterFields() {
		if (!hasStarted) {
			registerOnStart = true;
		}

		//? This is a very complex way of doing it but this is meant to be able to hold a lot of these changing values
		//?? nevermind its just bad and needs more work :/
		FadeHandler.AddFadeField(AimingCamera, true, 0, 0.1f, 1f);
	}
}