﻿using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace BabboSettings {
	internal partial class SettingsGUI : MonoBehaviour {
		internal void SaveToSettings() {
			log("Saving to settings");
			try {
				if (post_layer == null) throw new Exception("Post_layer is null");
				if (post_volume == null) throw new Exception("Post_volume is null");
				if (Camera.main == null) throw new Exception("maincamera is null");

				Main.settings.ENABLE_POST = post_volume.enabled;
				Main.settings.VSYNC = QualitySettings.vSyncCount;
				Main.settings.SCREEN_MODE = (int)Screen.fullScreenMode;

				Main.settings.AA_MODE = post_layer.antialiasingMode;
				Main.settings.TAA_sharpness = post_layer.temporalAntialiasing.sharpness;
				Main.settings.TAA_jitter = post_layer.temporalAntialiasing.jitterSpread;
				Main.settings.TAA_stationary = post_layer.temporalAntialiasing.stationaryBlending;
				Main.settings.TAA_motion = post_layer.temporalAntialiasing.motionBlending;
				Main.settings.SMAA = DeepClone(GAME_SMAA);

				Main.settings.CAMERA = cameraMode;
				Main.settings.NORMAL_FOV = normal_fov;
				Main.settings.LOW_FOV = low_fov;
				Main.settings.FOLLOW_FOV = follow_fov;
				Main.settings.FOLLOW_SHIFT = follow_shift;
				Main.settings.POV_FOV = pov_fov;
				Main.settings.POV_SHIFT = pov_shift;
				Main.settings.SKATE_FOV = skate_fov;
				Main.settings.SKATE_SHIFT = skate_shift;

				Main.settings.Save();
			}
			catch (Exception e) {
				throw new Exception("Failed saving to settings, ex: " + e);
			}
			log("Saved settings");
		}

		internal void SaveToPreset(Preset preset) {
			log("Saving to " + preset.name);
			try {
				if (preset == null) throw new Exception("preset is null");
				if (post_layer == null) throw new Exception("Post_layer is null");
				if (post_volume == null) throw new Exception("Post_volume is null");
				if (Camera.main == null) throw new Exception("maincamera is null");

				preset.AO = DeepClone(GAME_AO);
				preset.EXPO = DeepClone(GAME_EXPO);
				preset.BLOOM = DeepClone(GAME_BLOOM);
				preset.CA = DeepClone(GAME_CA);
				preset.COLOR_enabled = GAME_COLOR.enabled.value;
				preset.COLOR_tonemapper = GAME_COLOR.tonemapper.value;
				preset.COLOR_temperature = GAME_COLOR.temperature.value;
				preset.COLOR_tint = GAME_COLOR.tint.value;
				preset.COLOR_post_exposure = GAME_COLOR.postExposure.value;
				preset.COLOR_hueShift = GAME_COLOR.hueShift.value;
				preset.COLOR_saturation = GAME_COLOR.saturation.value;
				preset.COLOR_contrast = GAME_COLOR.contrast.value;
				preset.COLOR_lift = GAME_COLOR.lift.value.z;
				preset.COLOR_gamma = GAME_COLOR.gamma.value.z;
				preset.COLOR_gain = GAME_COLOR.gain.value.z;
				preset.DOF = DeepClone(GAME_DOF);
				preset.FOCUS_MODE = focus_mode;
				preset.GRAIN = DeepClone(GAME_GRAIN);
				preset.LENS = DeepClone(GAME_LENS);
				preset.BLUR = DeepClone(GAME_BLUR);
				preset.REFL = DeepClone(GAME_REFL);
				preset.VIGN = DeepClone(GAME_VIGN);

				preset.Save();
			}
			catch (Exception e) {
				throw new Exception("Failed saving to preset, ex: " + e);
			}
			log("Saved to " + preset.name);
		}

		internal void ApplySettings() {
			log("Applying settings");
			// Basic
			{
				post_volume.enabled = Main.settings.ENABLE_POST;
				QualitySettings.vSyncCount = Main.settings.VSYNC;
				Screen.fullScreenMode = (FullScreenMode)Main.settings.SCREEN_MODE;

				// AntiAliasing
				{
					post_layer.antialiasingMode = Main.settings.AA_MODE;
					GAME_SMAA.quality = Main.settings.SMAA.quality;
					GAME_TAA.sharpness = Main.settings.TAA_sharpness;
					GAME_TAA.jitterSpread = Main.settings.TAA_jitter;
					GAME_TAA.stationaryBlending = Main.settings.TAA_stationary;
					GAME_TAA.motionBlending = Main.settings.TAA_motion;
				}
			}

			// Camera
			{
				cameraMode = Main.settings.CAMERA;
				normal_fov = Main.settings.NORMAL_FOV;
				low_fov = Main.settings.LOW_FOV;
				follow_fov = Main.settings.FOLLOW_FOV;
				follow_shift = Main.settings.FOLLOW_SHIFT;
				pov_fov = Main.settings.POV_FOV;
				pov_shift = Main.settings.POV_SHIFT;
				skate_fov = Main.settings.SKATE_FOV;
				skate_shift = Main.settings.SKATE_SHIFT;
			}

			log("Applied settings");
		}

		internal void ApplyPreset(Preset preset) {
			log("Applying " + preset.name);

			// Effects
			{
				// Ambient Occlusion
				{
					GAME_AO.enabled.Override(preset.AO.enabled.value);
					GAME_AO.intensity.Override(preset.AO.intensity.value);
					GAME_AO.quality.Override(preset.AO.quality.value);
					GAME_AO.mode.Override(preset.AO.mode.value);
				}

				// Automatic Exposure
				{
					GAME_EXPO.enabled.Override(preset.EXPO.enabled.value);
					GAME_EXPO.keyValue.Override(preset.EXPO.keyValue.value);
				}

				// Bloom
				{
					GAME_BLOOM.enabled.Override(preset.BLOOM.enabled.value);
					GAME_BLOOM.intensity.Override(preset.BLOOM.intensity.value);
					GAME_BLOOM.threshold.Override(preset.BLOOM.threshold.value);
					GAME_BLOOM.diffusion.Override(preset.BLOOM.diffusion.value);
					GAME_BLOOM.fastMode.Override(preset.BLOOM.fastMode.value);
				}

				// Chromatic aberration
				{
					GAME_CA.enabled.Override(preset.CA.enabled.value);
					GAME_CA.intensity.Override(preset.CA.intensity.value);
					GAME_CA.fastMode.Override(preset.CA.fastMode.value);
				}

				// Color Grading
				{
					GAME_COLOR.gradingMode.Override(GradingMode.HighDefinitionRange);
					GAME_COLOR.enabled.Override(preset.COLOR_enabled);
					GAME_COLOR.tonemapper.Override(preset.COLOR_tonemapper);
					GAME_COLOR.temperature.Override(preset.COLOR_temperature);
					GAME_COLOR.tint.Override(preset.COLOR_tint);
					GAME_COLOR.postExposure.Override(preset.COLOR_post_exposure);
					GAME_COLOR.hueShift.Override(preset.COLOR_hueShift);
					GAME_COLOR.saturation.Override(preset.COLOR_saturation);
					GAME_COLOR.contrast.Override(preset.COLOR_contrast);
					GAME_COLOR.lift.Override(new Vector4(preset.COLOR_lift, preset.COLOR_lift, preset.COLOR_lift, preset.COLOR_lift));
					GAME_COLOR.gamma.Override(new Vector4(preset.COLOR_gamma, preset.COLOR_gamma, preset.COLOR_gamma, preset.COLOR_gamma));
					GAME_COLOR.gain.Override(new Vector4(preset.COLOR_gain, preset.COLOR_gain, preset.COLOR_gain, preset.COLOR_gain));
				}

				// Depth Of Field
				{
					GAME_DOF.enabled.Override(preset.DOF.enabled.value);
					GAME_DOF.focusDistance.Override(preset.DOF.focusDistance.value);
					GAME_DOF.aperture.Override(preset.DOF.aperture.value);
					GAME_DOF.focalLength.Override(preset.DOF.focalLength.value);
					GAME_DOF.kernelSize.Override(preset.DOF.kernelSize.value);
					focus_mode = preset.FOCUS_MODE;
				}

				// Grain
				{
					GAME_GRAIN.enabled.Override(preset.GRAIN.enabled.value);
					GAME_GRAIN.colored.Override(preset.GRAIN.colored.value);
					GAME_GRAIN.intensity.Override(preset.GRAIN.intensity.value);
					GAME_GRAIN.size.Override(preset.GRAIN.size.value);
					GAME_GRAIN.lumContrib.Override(preset.GRAIN.lumContrib.value);
				}

				// Lens Distortion
				{
					GAME_LENS.enabled.Override(preset.LENS.enabled.value);
					GAME_LENS.intensity.Override(preset.LENS.intensity.value);
					GAME_LENS.intensityX.Override(preset.LENS.intensityX.value);
					GAME_LENS.intensityY.Override(preset.LENS.intensityY.value);
					GAME_LENS.scale.Override(preset.LENS.scale.value);
				}

				// Motion Blur
				{
					GAME_BLUR.enabled.Override(preset.BLUR.enabled.value);
					GAME_BLUR.shutterAngle.Override(preset.BLUR.shutterAngle.value);
					GAME_BLUR.sampleCount.Override(preset.BLUR.sampleCount.value);
				}

				// Screen Space Reflections
				{
					GAME_REFL.enabled.Override(preset.REFL.enabled.value);
					GAME_REFL.preset.Override(preset.REFL.preset.value);
				}

				// Vignette
				{
					GAME_VIGN.enabled.Override(preset.VIGN.enabled.value);
					GAME_VIGN.mode.Override(VignetteMode.Classic);
					GAME_VIGN.intensity.Override(preset.VIGN.intensity.value);
					GAME_VIGN.smoothness.Override(preset.VIGN.smoothness.value);
					GAME_VIGN.roundness.Override(preset.VIGN.roundness.value);
					GAME_VIGN.rounded.Override(preset.VIGN.rounded.value);
				}
			}

			log("Applied " + preset.name);
		}
	}
}