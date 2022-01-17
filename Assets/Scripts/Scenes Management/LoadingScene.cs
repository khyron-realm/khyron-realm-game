using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Authentification;
using Networking.Headquarters;
using Scenes;


public class LoadingScene : MonoBehaviour
{
    #region "Input data"
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Image _loadingBar;
    #endregion

    #region "Private vars"
    private float _progressSoFar;
    private bool _playerDataReceived = false;
    #endregion

    private void Awake()
    {       
        AutomaticLogIn.OnAutomaticLoginAccepted += ActivateLoadingScreen;
        ChangeScene.OnSceneChanged += ActivateLoadingScreen;

        HeadquartersManager.OnPlayerDataReceived += PlayerDataReceived;
        
        _loadingScreen.SetActive(false);
    }

    #region "Handlers" 
    public void ActivateLoadingScreen(AsyncOperation operation, bool playerData)
    {
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadingScenesInProgress(operation, playerData));
    }
    public void ActivateLoadingScreen(List<AsyncOperation> operation, bool playerData)
    {
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadingScenesInProgress(operation, playerData));
    }
    #endregion

    #region "Loading Process"
    private IEnumerator LoadingScenesInProgress(AsyncOperation operation, bool playerData)
    {
        _loadingBar.fillAmount = 0;

        while(!operation.isDone)
        {
            _loadingBar.fillAmount += Mathf.Clamp(operation.progress * 0.75f, 0, 1);
           yield return null;
        }

        if(playerData)
        {
            while (_playerDataReceived == false)
            {
                yield return null;
            }
        }
        
        _playerDataReceived = false;
        _loadingBar.fillAmount = 1;

        yield return new WaitForSeconds(0.25f);

        _loadingScreen.SetActive(false);
    }
    private IEnumerator LoadingScenesInProgress(List<AsyncOperation> operation, bool playerData)  
    {
        _progressSoFar = 0;
        _loadingBar.fillAmount = 0;

        foreach (AsyncOperation item in operation)
        {
            while (!item.isDone)
            {
                _progressSoFar += (item.progress * 0.75f) / operation.Count;              
                _loadingBar.fillAmount = Mathf.Clamp(_progressSoFar / operation.Count, 0, 1);

                yield return null;
            }
        }

        if(playerData)
        {
            while (_playerDataReceived == false)
            {
                yield return null;
            }
        }
        
        _playerDataReceived = false;
        _loadingBar.fillAmount = 1;

        yield return new WaitForSeconds(0.25f);

        _loadingScreen.SetActive(false);
    }
    #endregion

    private void PlayerDataReceived()
    {
        _playerDataReceived = true;
    }

    private void OnDestroy()
    {
        AutomaticLogIn.OnAutomaticLoginAccepted -= ActivateLoadingScreen;
        ChangeScene.OnSceneChanged -= ActivateLoadingScreen;

        HeadquartersManager.OnPlayerDataReceived -= PlayerDataReceived;
    }
}