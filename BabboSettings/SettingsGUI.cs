﻿using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace BabboSettings {

	internal partial class SettingsGUI : MonoBehaviour {
		// Settings stuff
		private PostProcessLayer post_layer;
		private PostProcessVolume post_volume;
		private bool focus_player = true;
		private CameraMode cameraMode = CameraMode.Normal;

		private FastApproximateAntialiasing GAME_FXAA;
		private TemporalAntialiasing GAME_TAA; // NOT SERIALIZABLE
		private SubpixelMorphologicalAntialiasing GAME_SMAA;

		private AmbientOcclusion GAME_AO;
		private AutoExposure GAME_EXPO;
		private Bloom GAME_BLOOM;
		private ChromaticAberration GAME_CA;
		private ColorGrading GAME_COLOR; // NOT SERIALIZABLE
		private DepthOfField GAME_DOF;
		private Grain GAME_GRAIN;
		private LensDistortion GAME_LENS;
		private MotionBlur GAME_BLUR;
		private ScreenSpaceReflections GAME_REFL;
		private Vignette GAME_VIGN;

		private void Update() {
			bool keyUp = Input.GetKeyUp(KeyCode.Backspace);
			if (keyUp) {
				if (showUI == false) {
					Open();
				}
				else {
					Close();
				}
			}
			if (focus_player) {
				GAME_DOF.focusDistance.Override(Vector3.Distance(PlayerController.Instance.skaterController.skaterTransform.position, Camera.main.transform.position));
			}
		}

		private void LateUpdate() {
			//follow();
			if (use_pov) {
				pov();
			}
		}
		
		private T reset<T>() where T : PostProcessEffectSettings {
			bool enabled = post_volume.profile.GetSetting<T>().enabled.value;
			post_volume.profile.RemoveSettings<T>();
			T eff = post_volume.profile.AddSettings<T>();
			eff.enabled.Override(enabled);
			return eff;
		}

		private void getSettings() {
			post_layer = Camera.main.GetComponent<PostProcessLayer>();
			if (post_layer == null) {
				log("Null post layer");
			}
			if (post_layer.enabled == false) {
				post_layer.enabled = true;
				log("post_layer was disabled,");
			}
			post_volume = FindObjectOfType<PostProcessVolume>();
			if (post_volume != null) {
				string not_found = "";
				if ((GAME_AO = post_volume.profile.GetSetting<AmbientOcclusion>()) == null) {
					not_found += "ao,";
					GAME_AO = post_volume.profile.AddSettings<AmbientOcclusion>();
					GAME_AO.enabled.Override(false);
				}
				if ((GAME_EXPO = post_volume.profile.GetSetting<AutoExposure>()) == null) {
					not_found += "expo,";
					GAME_EXPO = post_volume.profile.AddSettings<AutoExposure>();
					GAME_EXPO.enabled.Override(false);
				}
				if ((GAME_BLOOM = post_volume.profile.GetSetting<Bloom>()) == null) {
					not_found += "bloom,";
					GAME_BLOOM = post_volume.profile.AddSettings<Bloom>();
					GAME_BLOOM.enabled.Override(false);
				}
				if ((GAME_CA = post_volume.profile.GetSetting<ChromaticAberration>()) == null) {
					not_found += "ca,";
					GAME_CA = post_volume.profile.AddSettings<ChromaticAberration>();
					GAME_CA.enabled.Override(false);
				}
				if ((GAME_COLOR = post_volume.profile.GetSetting<ColorGrading>()) == null) {
					not_found += "color,";
					GAME_COLOR = post_volume.profile.AddSettings<ColorGrading>();
					GAME_COLOR.enabled.Override(false);
				}
				if ((GAME_DOF = post_volume.profile.GetSetting<DepthOfField>()) == null) {
					not_found += "dof,";
					GAME_DOF = post_volume.profile.AddSettings<DepthOfField>();
					GAME_DOF.enabled.Override(false);
				}
				if ((GAME_GRAIN = post_volume.profile.GetSetting<Grain>()) == null) {
					not_found += "grain,";
					GAME_GRAIN = post_volume.profile.AddSettings<Grain>();
					GAME_GRAIN.enabled.Override(false);
				}
				if ((GAME_BLUR = post_volume.profile.GetSetting<MotionBlur>()) == null) {
					not_found += "blur,";
					GAME_BLUR = post_volume.profile.AddSettings<MotionBlur>();
					GAME_BLUR.enabled.Override(false);
				}
				if ((GAME_LENS = post_volume.profile.GetSetting<LensDistortion>()) == null) {
					not_found += "lens,";
					GAME_LENS = post_volume.profile.AddSettings<LensDistortion>();
					GAME_LENS.enabled.Override(false);
				}
				if ((GAME_REFL = post_volume.profile.GetSetting<ScreenSpaceReflections>()) == null) {
					not_found += "refl,";
					GAME_REFL = post_volume.profile.AddSettings<ScreenSpaceReflections>();
					GAME_REFL.enabled.Override(false);
				}
				if ((GAME_VIGN = post_volume.profile.GetSetting<Vignette>()) == null) {
					not_found += "vign,";
					GAME_VIGN = post_volume.profile.AddSettings<Vignette>();
					GAME_VIGN.enabled.Override(false);
				}
				if (not_found.Length > 0) {
					log("Not found: " + not_found);
				}
			}
			else {
				log("Post_volume is null in getSettings");
			}

			GAME_FXAA = post_layer.fastApproximateAntialiasing;
			GAME_TAA = post_layer.temporalAntialiasing;
			GAME_SMAA = post_layer.subpixelMorphologicalAntialiasing;

			Preset map_preset = new Preset(SceneManager.GetActiveScene().name + " (Original)");
			SaveTo(map_preset);
			Main.presets[map_preset.name] = map_preset;
			Main.select(Main.settings.presetName);
			Apply(Main.selectedPreset);

			log("Done getSettings");
		}
	}
}
