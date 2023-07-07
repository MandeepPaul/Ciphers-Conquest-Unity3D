using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ManagerScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Q)) 
        {
            QuitGame();
        }
    }

    public void QuitGame() 
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
