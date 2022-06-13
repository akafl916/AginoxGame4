using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    public Sprite[] overlays;
    public string[] rawDialogues;
    public string[][] dialogues;

    private void Awake()
    {
        partitionDialogue();
        //foreach(string[] s in dialogues)
        //{
        //    string debug = "";
        //    foreach(string str in s) {
        //        debug += str + "---";
        //    }
        //    Debug.Log(debug);
        //}
    }

    private void partitionDialogue()
    {
        dialogues = new string[rawDialogues.Length][];
        for (int i = 0; i < rawDialogues.Length; i++)
        {
            string[] s = rawDialogues[i].Split('@');
            dialogues[i] = s;
        }
    }
}
