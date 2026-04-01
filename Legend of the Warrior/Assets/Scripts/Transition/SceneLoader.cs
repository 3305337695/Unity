using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour,ISaveable
{
    public float fadeDuration;
    public Transform playerTrans;

    [Header("场景")]
    public GameSceneSO menuScene;
    public GameSceneSO firstLoadScene;

    [Header("位置")]
    public Vector3 menuPosition;
    public Vector3 firstPosition;

    [Header("事件监听")]
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO newGameEvent;
    public VoidEventSO backToMenuEvent;

    [Header("广播")]
    public VoidEventSO afterSceneLoadedEvent;
    public FadeEventSO fadeEvent;

    private GameSceneSO currentLoadedScene;
    private GameSceneSO sceneToLoad;
    private Vector3 posToGo;
    private bool fadeScreen;

    private void Start()
    {
        sceneLoadEvent.RaiseLoadSceneEvent(menuScene, menuPosition, true);

        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnEnable()
    {
        sceneLoadEvent.LoadSceneEvent += OnLoadSceneEvent;
        newGameEvent.OnEventRaised += NewGame;
        backToMenuEvent.OnEventRaised += OnBackToMenuEvent;
    }

    private void OnDisable()
    {
        sceneLoadEvent.LoadSceneEvent -= OnLoadSceneEvent;
        newGameEvent.OnEventRaised -= NewGame;
        backToMenuEvent.OnEventRaised -= OnBackToMenuEvent;

        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
    }

    private void NewGame()
    {
        sceneLoadEvent.RaiseLoadSceneEvent(firstLoadScene, firstPosition, true);
    }

    private void OnLoadSceneEvent(GameSceneSO sceneToLoad, Vector3 posToGo, bool fadeScreen)
    {
        this.sceneToLoad = sceneToLoad;
        this.posToGo = posToGo;
        this.fadeScreen = fadeScreen;

        if (currentLoadedScene)
            StartCoroutine(SwitchScene());
        else
            LoadNewScene();
    }

    IEnumerator SwitchScene()
    {
        if (fadeScreen)
        {
            fadeEvent.FadeIn(fadeDuration);

            yield return new WaitForSeconds(fadeDuration);
        }

        yield return currentLoadedScene.sceneReference.UnLoadScene();
        
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        currentLoadedScene = sceneToLoad;
        var loadingOption = currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);

        loadingOption.Completed += OnLoadCompleted;
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        playerTrans.position = posToGo;

        if (fadeScreen)
        {
            fadeEvent.FadeOut(fadeDuration);
        }

        if(currentLoadedScene.sceneType == SceneType.Location)
            afterSceneLoadedEvent.RaiseEvent();
    }

    public void OnBackToMenuEvent()
    {
        sceneLoadEvent.RaiseLoadSceneEvent(menuScene, menuPosition, true);
    }

    public DataDefination GetGuid()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        data.SaveScene(currentLoadedScene);
    }

    public void LoadSaveData(Data data)
    {
        if (data.sceneToSave != string.Empty)
        {
            var sceneToGo = data.LoadScene();

            var playerID = playerTrans.GetComponent<DataDefination>().ID;
            var posToLoad = data.characterPosDict[playerID];

            sceneLoadEvent.RaiseLoadSceneEvent(sceneToGo, posToLoad, true);
        }
    }
}
