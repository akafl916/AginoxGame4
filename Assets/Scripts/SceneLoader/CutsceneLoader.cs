using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CutsceneLoader : MonoBehaviour
{
    public GameObject canvas;
    public GameObject speechBox;
    public GameObject imageBox;
    public SceneScript[] cutscenes;
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private SceneTracker tracker;
    private float skipTimer = 0.0f;
    private int currentScene;
    private int sceneCounter = 0;
    private int dialogueCounter = 0;
    private void Start()
    {
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        canvas = GameObject.Find("Canvas");
        tracker = GameObject.Find("SceneTracker").gameObject.GetComponent<SceneTracker>();

        currentScene = tracker.getCurrentScene();
        audioSource.volume = GlobalVariables.VOLUME;

        playScene(cutscenes[currentScene]);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Skip"))
        {
            playScene(cutscenes[currentScene]);
        }
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
        if(sceneCounter >= scene.overlays.Length)
        {
            endScene();
        } else
        {
            Sprite currentImage = scene.overlays[sceneCounter];
            string[] currentDialogue = scene.dialogues[sceneCounter];

            if (dialogueCounter < currentDialogue.Length - 1)
            {
                dialogueCounter++;
            }
            else
            {
                sceneCounter++;
                dialogueCounter = 0;
            }

            speechBox.GetComponent<TextMeshProUGUI>().text = currentDialogue[dialogueCounter];
            imageBox.GetComponent<Image>().sprite = currentImage;
        }

        if(!audioSource.isPlaying)
        {
            Debug.Log("yo audio be doing");
            audioSource.clip = audioClips[currentScene];
            audioSource.Play();
        }

        Debug.Log("yo audio be not doing");
    }

    private void endScene()
    {
        if(tracker.getCurrentScene() == 2)
        {
            new SceneButtonManager().quit();
        }
        Debug.Log(tracker.getSceneTo());
        SceneManager.LoadScene(tracker.getSceneTo());
    }
}