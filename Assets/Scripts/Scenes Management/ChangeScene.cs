using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Scenes
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField] string _sceneToLoad;

        public void GoToScene()
        {
            SceneManager.LoadSceneAsync(_sceneToLoad);
        }
    }
}