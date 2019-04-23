﻿using UnityEngine;

namespace BabboSettings {
	internal partial class SettingsGUI : MonoBehaviour {
		private void Label(string text, GUIStyle style) {
			GUILayout.Label(text, style);
		}
		private void Label(string text) {
			GUILayout.Label(text);
		}
		private void Separator() {
			Label(separator, separatorStyle);
		}
		private bool Button(string text) {
			return GUILayout.Button(text);
		}
		private bool Spoiler(string text) {
			return GUILayout.Button(text, spoilerBtnStyle);
		}
		private void BeginHorizontal() {
			GUILayout.BeginHorizontal();
		}
		private void EndHorizontal() {
			GUILayout.EndHorizontal();
		}
		private float Slider(string name, float current, float min, float max, bool horizontal = true) {
			if (horizontal) BeginHorizontal();
			Label(name + ": " + current.ToString("0.00"));
			float res = GUILayout.HorizontalSlider(current, min, max, sliderStyle, thumbStyle);
			if (horizontal) EndHorizontal();
			return res;
		}
		private int SliderInt(string name, int current, int min, int max, bool horizontal = true) {
			if (horizontal) BeginHorizontal();
			Label(name + ": " + current);
			float res = GUILayout.HorizontalSlider(current, min, max, sliderStyle, thumbStyle);
			if (horizontal) EndHorizontal();
			return Mathf.FloorToInt(res);
		}
	}
}