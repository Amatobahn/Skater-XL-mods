﻿using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityModManagerNet;
using XLShredLib;

namespace BabboSettings {
	internal partial class SettingsGUI : MonoBehaviour {
		// GUI stuff
		private bool showUI = false;
		private GameObject master;
		private bool setUp;
		private Rect windowRect = new Rect(50f, 50f, 800f, 0f);
		private GUIStyle windowStyle;
		private GUIStyle spoilerBtnStyle;
		private GUIStyle columnLeftStyle = GUIStyle.none;
		private GUIStyle columnStyle = GUIStyle.none;
		private GUIStyle boxStyle;
		private GUIStyle toggleStyle;
		private GUIStyle sliderStyle;
		private GUIStyle thumbStyle;
		private readonly int largeFontSize = 28;
		private readonly int medFontSize = 18;
		private readonly int smallFontSize = 14;
		private readonly int spacing = 14;
		private readonly Color windowColor = new Color(0.2f, 0.2f, 0.2f);
		private readonly Color largeFontColor = Color.red;
		private readonly Color smallFontColor = Color.yellow;
		private GUIStyle fontLarge;
		private GUIStyle fontMed;
		private GUIStyle fontSmall;


		public SettingsGUI() {

		}

		public void Start() {
			getSettings();
			Open();
		}

		void log(string s) {
			UnityModManager.Logger.Log(s);
		}

		void show(string s) {
			ModMenu.Instance.ShowMessage(s);
		}

		private void SetUp() {
			DontDestroyOnLoad(gameObject);
			master = GameObject.Find("Master Prefab");
			DontDestroyOnLoad(master);

			fontLarge = new GUIStyle() {
				fontSize = largeFontSize
			};
			fontLarge.normal.textColor = largeFontColor;
			fontMed = new GUIStyle() {
				fontSize = medFontSize
			};
			fontMed.normal.textColor = largeFontColor;
			fontSmall = new GUIStyle() {
				fontSize = smallFontSize,
				padding = new RectOffset(1, 0, 2, 0)
			};
			fontSmall.normal.textColor = smallFontColor;

			windowStyle = new GUIStyle(GUI.skin.window) {
				padding = new RectOffset(10, 10, 25, 10),
				contentOffset = new Vector2(0, -23.0f)
			};

			boxStyle = new GUIStyle(GUI.skin.box) {
				padding = new RectOffset(14, 14, 24, 9),
				contentOffset = new Vector2(0, -20f)
			};

			columnLeftStyle.margin.right = spacing;

			toggleStyle = new GUIStyle(GUI.skin.toggle) {
				fontSize = smallFontSize,
				margin = new RectOffset(0, 0, 0, 0),
				padding = new RectOffset(20, 0, 2, 0),
				contentOffset = new Vector2(0, 0)
			};
			toggleStyle.normal.textColor = toggleStyle.active.textColor = toggleStyle.hover.textColor = largeFontColor;
			toggleStyle.onNormal.textColor = toggleStyle.onActive.textColor = toggleStyle.onHover.textColor = smallFontColor;
			toggleStyle.padding.left = 20;
			toggleStyle.imagePosition = ImagePosition.TextOnly;

			spoilerBtnStyle = new GUIStyle(GUI.skin.button) {
				fixedWidth = 100
			};

			sliderStyle = new GUIStyle(GUI.skin.horizontalSlider) {
				fixedWidth = 200
			};

			thumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb) {

			};
		}

		private void Open() {
			showUI = true;
			ModMenu.Instance.RegisterShowCursor(Main.modId, () => 3);
		}

		private void Close() {
			showUI = false;
			ModMenu.Instance.UnregisterShowCursor(Main.modId);
			Main.Save();
		}

		private void OnGUI() {
			if (!setUp) {
				setUp = true;
				SetUp();
			}

			GUI.backgroundColor = windowColor;

			if (master == null) {
				if (GameObject.Find("Master Prefab")) {
					master = GameObject.Find("Master Prefab");
					DontDestroyOnLoad(master);
				}
			}

			if (showUI) {
				windowRect = GUILayout.Window(GUIUtility.GetControlID(FocusType.Passive), windowRect, RenderWindow, "Graphic Settings by Babbo", windowStyle, GUILayout.Width(600)); // no windowStyle?
				if (DateTime.Now.Second % 2 == 0) {
					//Main.Save();
				}
			}
		}

		private T DeepClone<T>(T obj) {
			var ms = new MemoryStream();
			var formatter = new XmlSerializer(typeof(T));
			try {
				formatter.Serialize(ms, obj);
				ms.Position = 0;
			}
			catch (Exception ex) {
				log(ex.Message);
			}
			return (T)formatter.Deserialize(ms);
		}

		public void Save() {
			log("Called SettingsGUI.save()");
			if (reading_main) {
				log("Cannot save while reading main");
			}
			else {
				Main.settings.ENABLE_POST = post_volume.enabled;
				Main.settings.FOV = main.fieldOfView;

				Main.settings.AA_MODE = post_layer.antialiasingMode;
				Main.settings.TAA_sharpness = post_layer.temporalAntialiasing.sharpness;
				Main.settings.TAA_jitter = post_layer.temporalAntialiasing.jitterSpread;
				Main.settings.TAA_stationary = post_layer.temporalAntialiasing.stationaryBlending;
				Main.settings.TAA_motion = post_layer.temporalAntialiasing.motionBlending;
				Main.settings.SMAA = DeepClone(GAME_SMAA);

				Main.settings.AO = DeepClone(GAME_AO);
				Main.settings.EXPO = DeepClone(GAME_EXPO);
				Main.settings.BLOOM = DeepClone(GAME_BLOOM);
				Main.settings.CA = DeepClone(GAME_CA);
				Main.settings.COLOR_enabled = GAME_COLOR.enabled.value;
				Main.settings.COLOR_temperature = GAME_COLOR.temperature.value;
				Main.settings.COLOR_post_exposure = GAME_COLOR.postExposure.value;
				Main.settings.COLOR_saturation = GAME_COLOR.saturation.value;
				Main.settings.COLOR_contrast = GAME_COLOR.contrast.value;
				Main.settings.DOF = DeepClone(GAME_DOF);
				Main.settings.GRAIN = DeepClone(GAME_GRAIN);
				Main.settings.LENS = DeepClone(GAME_LENS);
				Main.settings.BLUR = DeepClone(GAME_BLUR);
				Main.settings.REFL = DeepClone(GAME_REFL);
				Main.settings.VIGN = DeepClone(GAME_VIGN);
			}
		}
	}
}