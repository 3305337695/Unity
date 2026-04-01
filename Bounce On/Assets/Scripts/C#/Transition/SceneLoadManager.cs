using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [Header("基本参数")]
    public Transform playerTrans;

    [Header("事件监听")]
    public IntEventSO menuLoadEvent;
    public IntEventSO levelLoadEvent;

    [Header("场景")]
    public List<GameSceneSO> GameScenes;
    public List<Vector3> playerPos;

    private GameSceneSO currentLoadedScene;
    private int currentNum;

    private void OnEnable()
    {
        menuLoadEvent.OnEventRaised += MenuLoadEvent;
        levelLoadEvent.OnEventRaised += LevelLoadEvent;
    }

    private void OnDisable()
    {
        menuLoadEvent.OnEventRaised -= MenuLoadEvent;
        levelLoadEvent.OnEventRaised -= LevelLoadEvent;
    }

    private void Start()
    {
        menuLoadEvent.RaiseEvent(0);
    }

    private void MenuLoadEvent(int num)
    {
        playerTrans.gameObject.SetActive(false);

        if (currentLoadedScene != null)
            currentLoadedScene.scene.UnLoadScene();

        currentNum = num;
        currentLoadedScene = GameScenes[currentNum];
        currentLoadedScene.scene.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void LevelLoadEvent(int num)
    {
        playerTrans.gameObject.SetActive(false);

        var unloadingOption = currentLoadedScene.scene.UnLoadScene();

        if (num == 0)
            unloadingOption.Completed += ReLoadLevel;
        else if (num == 1)
            currentNum++;
        else
            currentNum = num;

        currentLoadedScene = GameScenes[currentNum];

        if (num > 0)
        {
            var loadingOption = currentLoadedScene.scene.LoadSceneAsync(LoadSceneMode.Additive);
            loadingOption.Completed += OnLevelLoadCompleted;
        }
    }

    private void ReLoadLevel(AsyncOperationHandle<SceneInstance> handle)
    {
        var loadingOption = currentLoadedScene.scene.LoadSceneAsync(LoadSceneMode.Additive);
        loadingOption.Completed += OnLevelLoadCompleted;
    }

    private void OnLevelLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        playerTrans.position = playerPos[currentNum - 3];
        playerTrans.gameObject.SetActive(true);
    }
}
