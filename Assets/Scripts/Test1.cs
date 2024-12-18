using Ink.Parsed;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Test1 : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject VisualCue;

    [Header("Ink Json")]
    [SerializeField] private UnityEngine.TextAsset inkJson;

    private bool PlayerInRange;

    private void Awake()
    {
        PlayerInRange = false;
    }

    private void Update()
    {
        if (PlayerInRange) //&& !DialogueManager.GetInstance().dialogueIsPlaying) // PREVENTS INTERUPTIONS
        {
            PlayerInRange = false;
            DialogueManager.GetInstance().EnterDialogueMode(inkJson);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
            PlayerInRange =true;
            this.GetComponent<BoxCollider>().enabled = false;
        //Debug.Log("DISABLED BOX COLLIDER");
    }
}
