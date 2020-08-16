using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace XLGraphicsUI.Elements.EffectsUI
{
	public class LensDistortionUI : UIsingleton<LensDistortionUI>
	{
		public Toggle toggle;
		public XLSlider intensity;
		public XLSlider xMultiplier;
		public XLSlider yMultiplier;
		public XLSlider scale;
	}
}
