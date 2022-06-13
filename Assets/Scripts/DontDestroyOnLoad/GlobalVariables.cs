using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static float VOLUME;
    public static bool DEBUG;

    public static int currentSceneNum;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void setDebug(bool val)
    {
        DEBUG = !DEBUG;
    }

    public void setVolume(float f)
    {
        VOLUME = f;
    }

    private void Update()
    {
        Debug.Log(VOLUME);
    }
}
