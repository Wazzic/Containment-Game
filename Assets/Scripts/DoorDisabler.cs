using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDisabler : MonoBehaviour
{
    [SerializeField] private GameObject DoorToActivateCol;
    private bool InRange = false;
    void Start()
    {
        DoorToActivateCol.GetComponent<DoorMovement>().enabled = false;
    }

    void Update()
    {
        if (InRange && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("Door Unlocked");
            DoorToActivateCol.GetComponent<DoorMovement>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            InRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            InRange = false;
    }
}
