using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Text;
using Res.Scripts.Sounds;
using Res.Scripts.Spheres;
using UnityEngine.Networking;
using Random = UnityEngine.Random;
using Res.Scripts.UserInterface;
using UnityEngine.SceneManagement;

namespace Res.Scripts.API
{
    public static class ApiRequest
    {
        public const string URL = "http://localhost:3000/";

        /// <summary>
        /// This functions returns a json string : id, scene id, date, frequency, list of spheres' coordinates
        /// </summary>
        /// <param name="List of sphere"></param>
        private static string ParseSoundToJson(IReadOnlyList<GameObject> spheresList)
        {
            var spheresCoord = new List<string>();
            foreach (var sphere in spheresList)
            {
                SphereCoords sphereCoords = new SphereCoords(sphere.GetComponent<Sphere>().Coordinates);
                spheresCoord.Add(JsonUtility.ToJson(sphereCoords));
            }

            var newSound = new Sound(SceneManager.GetActiveScene().buildIndex,SoundData.Instance.Frequency, spheresCoord);
            var json = JsonUtility.ToJson(newSound);
            return json;
        }
        
        /// <summary>
        /// This coroutine send a POST request in order to store the newly created sound's data.
        /// </summary>
        /// <param name="spheresList"></param>
        /// <returns></returns>
        public static IEnumerator InsertSound(List<GameObject> spheresList)
        {
            var json = ParseSoundToJson(spheresList);

            var request = new UnityWebRequest ("http://localhost:3000/createSound", "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.error != null)
            {
                Debug.Log("Error: " + request.error);
            }
            else
            {
                Debug.Log("All OK");
                Debug.Log("Status Code: " + request.responseCode);
            }
        }

        /// <summary>
        /// This coroutine send a GET request to retrieve every stored sounds.
        /// </summary>
        /// <returns></returns>
        public static IEnumerator FindSound()
        {
            using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/findSound"))
            {
                yield return www.SendWebRequest();
        
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    string result = www.downloadHandler.text;
                    UiReplaySounds.sounds = JsonHelper.getJsonArray<Sound>(result);
                }
            }
        }

        /// <summary>
        /// This coroutine send a GET request to retrieve sounds played in the current scene.
        /// </summary>
        /// <returns></returns>
        public static IEnumerator FindSoundBySceneId()
        {
            string url = "http://localhost:3000/findSoundBySceneId/" + SceneManager.GetActiveScene().buildIndex;
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
        
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    string result = www.downloadHandler.text;
                    UiReplaySounds.sounds = JsonHelper.getJsonArray<Sound>(result);
                }
            }            
        }
    }
}

public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJson);
        return wrapper.array;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}

[Serializable]
public class Sound
{
    public int id;
    public int scene;
    public string date;
    public int frequency;
    public List<string> spheres;

    public Sound(int _scene, int _frequency, List<string> _spheres)
    {
        id = Random.Range(0,999999);
        scene = _scene;
        date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        frequency = _frequency;
        spheres = _spheres;
    }
}

[Serializable]
public class SphereCoords
{
    public List<Vector3> sphereCoords;

    public SphereCoords(List<Vector3> coords)
    {
        sphereCoords = coords;
    }
}