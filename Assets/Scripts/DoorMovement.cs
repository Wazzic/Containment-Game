using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    public Transform door;
    void Update()
    {
        float distance = Vector3.Distance(player.position, door.position);

        if (distance < 5)
        {
            //Debug.Log("NearDoor");
            anim.SetBool("Near", true);
        }
        else
            //Debug.Log("AwayDoor");
        anim.SetBool("Near", false);
    }
}
