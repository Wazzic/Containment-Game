using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionBoxDoor : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    public Transform door;
    void Update()
    {
        float distance = Vector3.Distance(player.position, door.position);

        if (distance < 2)
        {
            anim.SetBool("IsOpening", false);
        }
        else
            anim.SetBool("IsOpening", true);
    }

    private void Start()
    {
        anim.SetBool("IsOpening", true);
    }
}