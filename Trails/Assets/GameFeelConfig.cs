using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class GameFeelConfig : MonoBehaviour
    {

        // Togglable
        public static Dictionary<GameFeelFeature, bool> config = new();

        public GameFeelFeature ControllingFeature;
        public string KeyShortcut;

        private Toggle toggle;
        private string labelText { get {
                switch (ControllingFeature)
                {
                    case GameFeelFeature.CameraShake:
                        return "Camera Shake";
                    case GameFeelFeature.MovementTrail:
                        return "Movement Trail";
                    case GameFeelFeature.Particles:
                        return "Particle Effect";
                    case GameFeelFeature.TextAnimation:
                        return "Score Animation";
                    case GameFeelFeature.SlowMoOnDeath:
                        return "Slow-mo Death";
                    case GameFeelFeature.ScreenFlash:
                        return "Screen Flash";
                    default:
                        return "Unknown";
                }
            }
       }

        private void Start()
        {
            toggle = GetComponent<Toggle>();
            GetComponentInChildren<Text>().text = $"[{KeyShortcut}] {labelText}";
            foreach (GameFeelFeature feature in Enum.GetValues(typeof(GameFeelFeature)))
            {
                if (!config.ContainsKey(feature))
                {
                    config[feature] = true;
                }
            }
            toggle.isOn = config[ControllingFeature];
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyShortcut)) {
                toggle.isOn = !toggle.isOn;
            }
            config[ControllingFeature] = toggle.isOn;
        }
    }

    public enum GameFeelFeature
    {
        CameraShake,
        Particles,
        MovementTrail,
        TextAnimation,
        SlowMoOnDeath,
        ScreenFlash
    }
}