using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneLoader : MonoBehaviour
{
    public SceneScript[] cutscenes;

    private float skipTimer = 0.0f;
    private int currentScene;
    private int sceneCounter = 0;

    private void Start()
    {
        currentScene = GameObject.Find("SceneTracker").gameObject.GetComponent<SceneTracker>().currentScene;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Skip"))
        {
            sceneCounter++;
        }
        playScene(cutscenes[currentScene]);
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Skip")) {
            skipTimer += Time.deltaTime;
        } else {
            skipTimer = 0;
        }

        if (skipTimer >= 2)
        {
            endScene();
        }
    }

    private void playScene(SceneScript scene)
    {
        Debug.Log("playing scene");
    }

    private void endScene()
    {
        Debug.Log("scene ended");
    }
}