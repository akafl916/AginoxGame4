using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTextManager : MonoBehaviour
{
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = "Debug: " + (GlobalVariables.DEBUG + "");
    }
}
