using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Scenes
{
    public class ChangeScene : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private ScenesName _unloadScene;
        [SerializeField] private ScenesName _loadScene;

        [SerializeField] private bool _playerData;
        #endregion

        private List<AsyncOperation> _loadingOperation;

        public static event Action<List<AsyncOperation>, bool> OnSceneChanged;

        private void Awake()
        {
            _loadingOperation = new List<AsyncOperation>();
        }

        public void GoToScene()
        {
            _loadingOperation.Clear();
            _loadingOperation.Add(SceneManager.UnloadSceneAsync((int)_unloadScene));
            _loadingOperation.Add(SceneManager.LoadSceneAsync((int)_loadScene, LoadSceneMode.Additive));

            OnSceneChanged?.Invoke(_loadingOperation, _playerData);
        }
    }
}