using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/ExitGameEventSO")]
public class ExitGameEventSO : ScriptableObject
{
    public void ExitGame()
    {
        Application.Quit();
    }
}
