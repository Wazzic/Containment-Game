using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDisabler : MonoBehaviour
{
    [SerializeField] private GameObject DoorToActivateCol;
    private bool InRange = false;
    // Start is called before the first frame update
    void Start()
    {
        DoorToActivateCol.GetComponent<DoorMovement>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Door Unlocked");
            DoorToActivateCol.GetComponent<DoorMovement>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            InRange = true;
        //Debug.Log("player within range");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            InRange = false;
        //Debug.Log("player without range");
    }
}
