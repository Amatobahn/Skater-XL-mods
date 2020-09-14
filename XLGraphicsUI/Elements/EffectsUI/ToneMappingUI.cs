using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XLGraphicsUI.Elements.EffectsUI
{
	public class ToneMappingUI : UIsingleton<ToneMappingUI>
	{
		public XLToggle toggle;
		//public GameObject container;
		public TMP_Dropdown mode;
		public XLSlider toeStrength;
		public XLSlider toeLength;
		public XLSlider shoulderStrength;
		public XLSlider shoulderLength;
		public XLSlider shoulderAngle;
		public XLSlider gamma;
	}
}
