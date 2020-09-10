using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using XLGraphics.Presets;
using XLGraphicsUI.Elements.EffectsUI;

namespace XLGraphics.EffectHandlers.PresetEffects
{
	public class FovOverrideHandler : PresetEffectHandler
	{
		FovOverrideUI fovUI;

		public override void ConnectUI() {
			fovUI.toggle.onValueChanged.AddListener(new UnityAction<bool>(v => PresetManager.Instance.selectedPreset.fovOverrideData.active = v));
			fovUI.fov.onValueChange += v => PresetManager.Instance.selectedPreset.fovOverrideData.fov = v;
		}

		public override void OnChangeSelectedPreset(Preset preset) {
			var fov = preset.fovOverrideData;
			fovUI.toggle.SetIsOnWithoutNotify(fov.active);
			fovUI.fov.OverrideValue(fov.fov);
		}
	}
}
