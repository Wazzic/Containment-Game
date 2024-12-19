using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnabler : MonoBehaviour
{
    [SerializeField] private GameObject DoorToActivateCol;
    private Animator anim;
    private bool InRange = false;
    void Start()
    {
        anim = DoorToActivateCol.GetComponent<Animator>();
        DoorToActivateCol.GetComponent<DoorMovement>().enabled = true;
    }

    void Update()
    {
        if (InRange && Input.GetKeyDown(KeyCode.E))
        {
            anim.Play("ClosedPerm");
            //Debug.Log("Door locked");
            DoorToActivateCol.GetComponent<DoorMovement>().enabled = false;
            StartCoroutine(Waiting());
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

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(30f);
        DoorToActivateCol.GetComponent<DoorMovement>().enabled = true;
        anim.Play("Closed");
        //Debug.Log("Waited 50 seconds");
    }
}
