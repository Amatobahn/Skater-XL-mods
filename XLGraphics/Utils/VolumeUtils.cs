using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace XLGraphics.Utils
{
	public class VolumeUtils
	{
		static public VolumeUtils Instance { get; private set; }

		private VolumeUtils() { }

		static public void Instantiate() {
			Instance = new VolumeUtils();
		}

		public float GetHighestPriority() {
			var volumes = UnityEngine.Object.FindObjectsOfType<Volume>();
			Main.Logger.Log($"Found {volumes.Length} volumes");
			if (volumes.Length == 0) {
				return 0;
			}
			// minimum return value should be 0
			return Math.Max(volumes.Select(v => v.priority).Max(), 0);
		}

		public Volume CreateVolume(float priority) {
			var volume = new GameObject().AddComponent<Volume>();
			volume.gameObject.SetActive(true);
			volume.priority = priority;
			volume.isGlobal = true;
			return volume;
		}
	}
}
