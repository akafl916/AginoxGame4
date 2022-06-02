using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    public void toLevelSelect()
    {
        SceneManager.LoadScene("WorldSelect");
    }

    public void toTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
