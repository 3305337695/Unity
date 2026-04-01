using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public GameSceneSO sceneToLoad;
    public Vector3 positionToGo;
    public SceneLoadEventSO sceneLoadEvent;

    private bool isDone;

    public void TriggerAction()
    {
        if (!isDone)
        {
            sceneLoadEvent.RaiseLoadSceneEvent(sceneToLoad, positionToGo, true);

            isDone = true;
        }
    }
}
