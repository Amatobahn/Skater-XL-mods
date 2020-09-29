using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using XLGraphics.Presets;
using XLGraphics.Utils;
using XLGraphicsUI.Elements;
using XLGraphicsUI.Elements.EffectsUI;

namespace XLGraphics.EffectHandlers.PresetEffects
{
	public class BloomHandler : PresetEffectHandler
	{
		BloomUI bloomUI;

		public override void ConnectUI() {
			bloomUI = BloomUI.Instance;

			// add listeners
			bloomUI.toggle.onValueChanged += v => PresetManager.Instance.selectedPreset.bloom.active = v;
			bloomUI.intensity.onValueChanged += v => PresetManager.Instance.selectedPreset.bloom.intensity.value = v;
			bloomUI.scatter.onValueChanged += v => PresetManager.Instance.selectedPreset.bloom.scatter.value = v;
			bloomUI.threshold.onValueChanged += v => PresetManager.Instance.selectedPreset.bloom.threshold.value = v;
		}

		public override void OnChangeSelectedPreset(Preset preset) {
			var bloom = preset.bloom;
			bloomUI.toggle.SetIsOnWithoutNotify(bloom.active);
			bloomUI.intensity.OverrideValue(bloom.intensity.value);
			bloomUI.scatter.OverrideValue(bloom.scatter.value);
			bloomUI.threshold.OverrideValue(bloom.threshold.value);
		}
	}
}
