using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace XLGraphicsUI.Elements.EffectsUI
{
	public class WhiteBalanceUI : UIsingleton<WhiteBalanceUI>
	{
		public Toggle toggle;
		public XLSlider temperature;
		public XLSlider tint;
	}
}
