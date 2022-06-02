using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject pauseMenu;
    private GameObject actionMenu;

    /*
     * Gets all of the UI game objects from the hierarchy at runtime 
     */
    void Start()
    {
        pauseMenu = this.gameObject.transform.GetChild(0).gameObject;
        actionMenu = this.gameObject.transform.GetChild(1).gameObject;
        pauseMenu.SetActive(false);
        actionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (!pauseMenu.activeInHierarchy)
            {
                if (actionMenu.activeInHierarchy) actionMenu.SetActive(false);
                else actionMenu.SetActive(true);
            }
        }
        if (Input.GetButtonDown("Pause"))
        {
            if (actionMenu.activeInHierarchy) actionMenu.SetActive(false);
            if (pauseMenu.activeInHierarchy) pauseMenu.SetActive(false);
            else pauseMenu.SetActive(true);
        }
    }
}
