using Ink.Parsed;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TestDialogue : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject VisualCue;

    [Header("Ink Json")]
    [SerializeField] private UnityEngine.TextAsset inkJson;

    private bool PlayerInRange;

    private void Awake()
    {
        PlayerInRange = false;
        VisualCue.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            VisualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJson);
            }
        }
        else
        {
            VisualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
            {
            PlayerInRange=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}
