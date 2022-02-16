using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Dino_Interact : MonoBehaviour
{
    public Dino dino;
    public DialogueRunner dialogueRunner;
    public void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }
    public void InteractDino()
    {
        dialogueRunner.Add(dino.scriptToLoad);
        dialogueRunner.StartDialogue(startNode: "start");
    }

 
}
