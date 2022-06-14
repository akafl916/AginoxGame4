using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        Debug.Log("is it playing " + audioSource.isPlaying);
        audioSource.volume = GlobalVariables.VOLUME;
    }
}
