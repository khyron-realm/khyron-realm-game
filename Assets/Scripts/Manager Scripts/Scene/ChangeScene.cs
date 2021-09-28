using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    [SerializeField] string _sceneToLoad;
    // Start is called before the first frame update
    public void GoToScene()
    {
        SceneManager.LoadSceneAsync(_sceneToLoad);
    }
}