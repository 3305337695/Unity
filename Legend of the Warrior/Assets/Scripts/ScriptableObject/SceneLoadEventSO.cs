using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadSceneEvent;

    public void RaiseLoadSceneEvent(GameSceneSO sceneToLoad,Vector3 posToGo,bool fadeScreen)
    {
        LoadSceneEvent?.Invoke(sceneToLoad, posToGo, fadeScreen);
    }
}
