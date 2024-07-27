using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif 
public class QuitGame : MonoBehaviour
{
    public void QuitSnake()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else 
        Application.Quit();
        #endif
    }
}
