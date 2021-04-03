using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using Res.Scripts.Sounds;
using Res.Scripts.Spheres;
using UnityEditor;
using UnityEngine.Networking;
using Random = UnityEngine.Random;
using Res.Scripts.UserInterface;
using UnityEngine.SceneManagement;

namespace Res.Scripts.API
{
    public class ApiRequest
    {
        public const string URL = "http://localhost:3000/";

        private static string ParseSoundToJson(List<GameObject> spheresList)
        {
            var spheresCoord = new List<string>();
            for (var i = 0; i < spheresList.Count; i++)
            {

                SphereCoords sphereCoords = new SphereCoords(spheresList[i].GetComponent<Sphere>().Coordinates);
                spheresCoord.Add(JsonUtility.ToJson(sphereCoords));
            }

            var newSound = new Sound(SceneManager.GetActiveScene().buildIndex,SoundData.Instance.Frequency, spheresCoord);
            var json = JsonUtility.ToJson(newSound);
            return json;
        }
        
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

/*
 *
 *
 *
 *
 *
 * 
*/

[Serializable]
public class Sound
{
    public int _id;
    public int scene;
    public string date;
    public int frequency;
    public List<string> spheres;

    public Sound(int _scene, int _frequency, List<string> _spheres)
    {
        _id = Random.Range(0,999999);
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

    public SphereCoords(List<Vector3> _coords)
    {
        sphereCoords = _coords;
    }
}



// public class Sound
// {
//     public int id;
//     public string date;
//     public int frequency;
//     public List<Vector3> spheres;
//
//     public Sound(int _frequency, List<Vector3> _spheres)
//     {
//         id = Random.Range(0,999999);
//         date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
//         frequency = _frequency;
//         spheres = _spheres;
//     }
// }
// public class Sound
// {
//     public int id;
//     public string date;
//     public int frequency;
//     public List<List<Vector3>> spheres;
//
//     public Sound(int _frequency, List<List<Vector3>> _spheres)
//     {
//         id = Random.Range(0,999999);
//         date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
//         frequency = _frequency;
//         spheres = _spheres;
//     }
// }

// string path = "Assets/Res/Scripts/API/datainsert.txt";
//         
// //Write some text to the test.txt file
// StreamWriter writer = new StreamWriter(path, true);
// writer.WriteLine(json);
// writer.Close();
//
// yield return null;

// Sound[] sounds = JsonHelper.getJsonArray<Sound> (result);
// Debug.Log(sounds[0].spheres[0]);
// Debug.Log(sounds[1]._id);
// Debug.Log(result);
// string path = "Assets/Res/Scripts/API/datafind.txt";
//
// //Write some text to the test.txt file
// StreamWriter writer = new StreamWriter(path, true);
// writer.WriteLine(result);
// writer.Close();