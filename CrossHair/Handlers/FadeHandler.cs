using UnityEngine;
using TMPro;

using CTNOriginals.ContentWarning.CrossHair.Patches;
using CTNOriginals.ContentWarning.CrossHair.Utilities;
using System.Collections.Generic;

namespace CTNOriginals.ContentWarning.CrossHair.Handlers;

public class Box<T> where T : struct {
	public T Value { get; set; }
	public Box(T value) { this.Value = value; }
}

public class FadeHandler {
	public class FadeValueField {
		public bool targetValue; //? The value "value" should be set to start fade
		public Box<bool> currentValue;

		private readonly float fadeValue; //? The value "value" should be set to end fade
		private readonly float fadeOutDuration; //? The duration of the fade
		private readonly float fadeInDuration; //? The duration of the fade

		private bool isFading = false;
		public float currentFade = 1f;
		private float fadeTimer = 0f;

		public FadeValueField(Box<bool> refValue, bool targetValue, float fadeValue = 0.2f, float fadeOutDuration = 0.2f, float fadeInDuration = 0.5f) {
			this.targetValue = targetValue;
			currentValue = refValue;

			this.fadeValue = fadeValue;
			this.fadeOutDuration = fadeOutDuration;
			this.fadeInDuration = fadeInDuration;
		}

		public void Update() {
			if (isFading) {
				if (!(currentValue.Value == targetValue)) {
					isFading = false;
					fadeTimer = currentFade;
					// CLogger.LogDebug($"{fieldName} - Un-Matched target value !({targetValue})");
				}
				else if (currentFade > fadeValue) {
					currentFade = Mathf.Lerp(1f, fadeValue, fadeTimer);
					fadeTimer += Time.deltaTime / fadeOutDuration;
				}
			}
			else {
				if (currentValue.Value == targetValue) {
					isFading = true;
					fadeTimer = 1f - currentFade;
					// CLogger.LogDebug($"{fieldName} - Matched Target value ({targetValue})");
				}
				else if (currentFade < 1f) {
					currentFade = Mathf.Lerp(fadeValue, 1f, fadeTimer);
					fadeTimer += Time.deltaTime / fadeInDuration;
				}
			}
			currentFade = Mathf.Clamp(currentFade, 0, 1);
			fadeTimer = Mathf.Clamp(fadeTimer, 0, 1);
		}

		public void LogValues() {
			CLogger.LogDebug($"\nValue: {currentValue.Value} - Target: {targetValue}\nFade: {fadeValue} - Duration: {fadeOutDuration} - {fadeInDuration}");
		}
	}

	public static List<FadeValueField> FadeFields = new();

	static bool hasStarted = false;
	public static void Start() {
		foreach (FadeValueField targetField in FadeFields) {
			targetField.LogValues();
		}
	}

	public static void Update() {
		if (FadeFields.Count == 0) { return; }
		if (!hasStarted) {
			hasStarted = true;
			Start();
			return;
		}

		float lowestFade = 1f;
		foreach (FadeValueField targetField in FadeFields) {
			targetField.Update();
			if (targetField.currentFade < lowestFade) {
				lowestFade = targetField.currentFade;
			}
		}

		float aprox = Mathf.Abs(HelmetUIPatch.CrossHairTMP.alpha - lowestFade * (HelmetUIPatch.CrossHairAlpha / 255f));

		// //!! Debug Only
		// bool testState = testField.Get<bool>();
		// if (testState && !isHoldingTest) {
		// 	Console.LogDebug($"{lowestFieldName}: {HUDManagerPatch.CrossHairTMP.alpha} != ({lowestFade} * (({HUDManagerPatch.CrossHairAlpha} / 255)={HUDManagerPatch.CrossHairAlpha / 255}) = {lowestFade * HUDManagerPatch.CrossHairAlpha / 255}) = {aprox > 0.0001f}");
		// 	Console.LogDebug($"Current Aplha: {lowestFade * (HUDManagerPatch.CrossHairAlpha / 255)}");
		// 	Console.LogDebug($"Aprox: {aprox}");
		// 	isHoldingTest = true;
		// }
		// else if (!testState && isHoldingTest) {
		// 	isHoldingTest = false;
		// }
		
		if (aprox > 0.0001f) {
			CLogger.LogDebug($"Fading: {lowestFade}");
			SetCrossHairAlphaPercent(lowestFade);
		}
	}


	/// <summary>
	/// Set the alpha of the crosshair
	/// </summary>
	/// <param name="target">Target alpha value (1f = 100%)</param>
	private static void SetCrossHairAlphaPercent(float target) {
		if (!HelmetUIPatch.CrossHair) { return; }
		HelmetUIPatch.CrossHair.GetComponent<TextMeshProUGUI>().alpha = target * (HelmetUIPatch.CrossHairAlpha / 255f);

		if (!HelmetUIPatch.CrossHairShadow) { return; }
		HelmetUIPatch.CrossHairShadow.GetComponent<TextMeshProUGUI>().alpha = target * (HelmetUIPatch.CrossHairShadowAlpha / 255f);
	}
}