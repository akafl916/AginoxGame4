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

    public void toTower1()
    {
        SceneManager.LoadScene("L2S1_Tower1");
    }

    public void toBossfight()
    {
        SceneManager.LoadScene("L2S3_Bossfight");
    }

    public void toTower2()
    {
        SceneManager.LoadScene("L2S4_Tower2");
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit()
    {
        Debug.Log("lmao quitter");
        Application.Quit();
    }
    public void toCutscene(string thisissodumb)
    {
        int sceneTo = int.Parse(thisissodumb.Substring(0, 1));
        int cutscene = int.Parse(thisissodumb.Substring(1));
        SceneTracker tracker = GameObject.Find("SceneTracker").gameObject.GetComponent<SceneTracker>();
        tracker.setSceneTo(sceneTo);
        tracker.setCurrentScene(cutscene);

        SceneManager.LoadScene("Cutscenes");
    }
}
