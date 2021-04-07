using UnityEngine;
using UnityEngine.SceneManagement;

namespace Res.Scripts.Manager
{
    
    /// <summary>
    /// This class contains functions to load scenes and quit the app.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {

        public void LoadTestScene()
        {
            SceneManager.LoadScene("test_scene",  LoadSceneMode.Single);
        }

        public void LoadWorkspaceScene()
        {
            SceneManager.LoadScene("mid_scene", LoadSceneMode.Single);
        }
        
        public void LoadWorkspaceScene2()
        {
            SceneManager.LoadScene("workspace_scene", LoadSceneMode.Single);
        }
    
        private void LoadStartScene()
        {
            SceneManager.LoadScene("start_scene", LoadSceneMode.Single);
        }

        public void LoadCinemaScene()
        {
            SceneManager.LoadScene("cinema_scene", LoadSceneMode.Single);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name == "start_scene")
                {
                    Application.Quit();
                }
                else
                {
                    LoadStartScene();
                }

            }
        }
    }
}
