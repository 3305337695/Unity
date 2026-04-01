using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data
{
    public string sceneToSave;

    public Dictionary<string,Vector3> characterPosDict = new Dictionary<string,Vector3>();

    public Dictionary<string,float> characterFloatDict = new Dictionary<string,float>();

    public Dictionary<string,bool> itemStateDict = new Dictionary<string,bool>();

    public void SaveScene(GameSceneSO sceneToSave)
    {
        this.sceneToSave = JsonUtility.ToJson(sceneToSave);
    }

    public GameSceneSO LoadScene()
    {
        var sceneToLoad = ScriptableObject.CreateInstance<GameSceneSO>();
        JsonUtility.FromJsonOverwrite(sceneToSave,sceneToLoad);

        return sceneToLoad;
    }
}
