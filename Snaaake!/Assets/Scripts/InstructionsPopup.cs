using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstructionsPopup : MonoBehaviour
{
    public GameObject instructionsPop;
    public GameObject snakeLogo;
    void Start()
    {
        CloseInstruct();
    }

    public void Instruct()
    {
        instructionsPop.SetActive(true);
        snakeLogo.SetActive(false);
    }

    public void CloseInstruct()
    {
        instructionsPop.SetActive(false);
        snakeLogo.SetActive(true);
    }
}
