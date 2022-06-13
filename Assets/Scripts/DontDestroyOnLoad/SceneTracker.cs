using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTracker : MonoBehaviour
{
    private int currentScene;
    private int sceneTo;

    private void Update()
    {
        Debug.Log(sceneTo);
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int getCurrentScene()
    {
        return currentScene;
    }

    public void setCurrentScene(int scene)
    {
        currentScene = scene;
    }

    public int getSceneTo()
    {
        return sceneTo;
    }

    public void setSceneTo(int scene)
    {
        sceneTo = scene;
    }
}
