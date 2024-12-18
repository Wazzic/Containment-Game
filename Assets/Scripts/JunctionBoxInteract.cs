using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionBoxInteract : MonoBehaviour
{
    private bool InRange = false;


    public Light[] _lights;
    public bool isOn = true;

    public void Update()
    {
        if (InRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLight();
        }
    }
    private void ToggleLight()
    {
       isOn = !isOn;
        for(int i = 0; i < _lights.Length; i++)
        {
            Debug.Log("Switching light");
            _lights[i].enabled = isOn;
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
