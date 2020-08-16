using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace XLGraphicsUI.Elements.EffectsUI
{
	public class ColorAdjustementsUI : UIsingleton<ColorAdjustementsUI>
	{
		public Toggle toggle;
		public XLSlider postExposure;
		public XLSlider contrast;
		//public SerializableColor colorFilter;
		public XLSlider hueShift;
		public XLSlider saturation;
	}
}
