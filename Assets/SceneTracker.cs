using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTracker : MonoBehaviour
{
    public int currentScene;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
