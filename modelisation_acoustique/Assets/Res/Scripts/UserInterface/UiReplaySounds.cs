using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Res.Scripts.API;
using Res.Scripts.Spheres;
using TMPro;
using UnityEngine.UI;

namespace Res.Scripts.UserInterface
{
    public class UiReplaySounds : MonoBehaviour
    {
        private static UiReplaySounds _instance;
        public static UiReplaySounds Instance { get { return _instance; } }
        
        private List<GameObject> _uiSounds = new List<GameObject>();
        public static Sound[] sounds;
        public KeyCode key = KeyCode.LeftControl;
        public TextMeshProUGUI inputFieldText;
        public GameObject inputField;
        public bool uiReplaySoundsState = false;

        private void Awake()
        {
            _instance = this;
        }

        private void ChangeState()
        {
            foreach (Transform child in transform)
            {
                uiReplaySoundsState = !uiReplaySoundsState;
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
        
        public void Update()
        {
            if (Input.GetKeyDown(key))
            {
                StartCoroutine(ApiRequest.FindSoundBySceneId());
                ChangeState();
            }
        }

        public void UpdateUISound()
        {
            if(sounds.Length > 0)
            {
                _uiSounds.AddRange(GameObject.FindGameObjectsWithTag("UI_Sound"));
                for (int i = 0; i < sounds.Length; i++)
                {
                    TextMeshProUGUI id = _uiSounds[i].transform.Find("no_text").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI date = _uiSounds[i].transform.Find("date_text").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI frequency = _uiSounds[i].transform.Find("frequency_text").GetComponent<TextMeshProUGUI>();
                    id.SetText(sounds[i]._id.ToString());
                    date.SetText(sounds[i].date);
                    frequency.SetText(sounds[i].frequency.ToString());
                }
            }
        }

        public void ReplaySound()
        {
            int number;
            string text = inputField.GetComponent<TMP_InputField>().text;
            if (int.TryParse(text, out number))
            {
                for (int i = 0; i < sounds.Length; i++)
                {
                    if (sounds[i]._id == number)
                    {
                        for (int j = 0; j < sounds[i].spheres.Count; j++)
                        {
                            var result = sounds[i].spheres[j];
                            var result1 = result.Remove(result.Length - 1);
                            var result2 = result1.Remove(0, 16);
                            // coord d'une sphère
                            Coord[] coords = JsonHelper.getJsonArray<Coord>(result2);
                            SoundManager.Instance.InitSphere(j,coords);
                        }
                    }
                }
            }
            
        }
    }
}

[Serializable]
public class Coord
{
    public float x;
    public float y;
    public float z;

    public Coord(float _x, float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }
}

