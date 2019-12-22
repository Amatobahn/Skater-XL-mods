﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityModManagerNet;

namespace BabboSettings
{


    [Serializable]
    public class Settings : UnityModManager.ModSettings
    {

        public List<string> presetOrder = new List<string>();
        public List<string> replay_presetOrder = new List<string>();
        public bool DEBUG = true;

        // Basic
        public bool ENABLE_POST = true;
        public int VSYNC = 1;
        public int SCREEN_MODE = 0;

        // AA
        public PostProcessLayer.Antialiasing AA_MODE = new PostProcessLayer.Antialiasing();
        public float TAA_sharpness = new TemporalAntialiasing().sharpness;
        public float TAA_jitter = new TemporalAntialiasing().jitterSpread;
        public float TAA_stationary = new TemporalAntialiasing().stationaryBlending;
        public float TAA_motion = new TemporalAntialiasing().motionBlending;
        public SubpixelMorphologicalAntialiasing SMAA = new SubpixelMorphologicalAntialiasing();

        // Camera
        public CameraMode CAMERA = CameraMode.Normal;
        public float REPLAY_FOV = 60;
        public float NORMAL_FOV = 60;
        public float NORMAL_REACT = 0.90f;
        public float NORMAL_REACT_ROT = 0.90f;
        public float NORMAL_CLIP = 0.01f;
        public float FOLLOW_FOV = 60;
        public float FOLLOW_REACT = 0.70f;
        public float FOLLOW_REACT_ROT = 0.70f;
        public float FOLLOW_CLIP = 0.01f;
        public Vector3 FOLLOW_SHIFT = Vector3.zero;
        public float POV_FOV = 60;
        public float POV_REACT = 1;
        public float POV_REACT_ROT = 0.07f;
        public float POV_CLIP = 0.01f;
        public bool HIDE_HEAD = true;
        public Vector3 POV_SHIFT = Vector3.zero;
        public float SKATE_FOV = 60;
        public float SKATE_REACT = 0.90f;
        public float SKATE_REACT_ROT = 0.90f;
        public float SKATE_CLIP = 0.01f;
        public Vector3 SKATE_SHIFT = Vector3.zero;

        public Settings() {
        }

        public override void Save(UnityModManager.ModEntry modEntry) {
            Save();
        }

        public Task Save() {
            return Task.Run(() => {
                var filepath = Main.modEntry.Path;
                try {
                    using (var writer = new StreamWriter($"{filepath}Settings.xml")) {
                        // serialized to string, then write (using sync, but in Task)
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
                        using (StringWriter textWriter = new StringWriter()) {
                            xmlSerializer.Serialize(textWriter, this);
                            var serialized = textWriter.ToString();
                            writer.Write(serialized);
                        }
                    }
                }
                catch (Exception e) {
                    Logger.Log($"Can't save {filepath}Settings.xml. ex: {e}");
                }
            });
        }
    }
}
