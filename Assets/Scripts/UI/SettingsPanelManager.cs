using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPanelManager : MonoBehaviour
{
    public void openSettings()
    {
        GameObject panel = this.gameObject.transform.GetChild(0).gameObject; 
        if(panel.activeInHierarchy)
        {
            panel.SetActive(false);
        } else
        {
            panel.SetActive(true);
        }
    }
}
