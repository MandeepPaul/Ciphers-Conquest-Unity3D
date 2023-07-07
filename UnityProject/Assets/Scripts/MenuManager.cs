using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{   
    public void LoadGame() 
    {
        SceneManager.LoadScene("Scene");
    }
}
