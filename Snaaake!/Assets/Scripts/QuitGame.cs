using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif 
public class QuitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            QuitSnaaake();
        }
    }

    void QuitSnaaake()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else 
        #endif
    }
}
