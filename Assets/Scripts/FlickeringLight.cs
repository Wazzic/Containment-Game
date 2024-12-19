using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light _light;
    [SerializeField] private float MinTime;
    [SerializeField] private float MaxTime;
    private float Timer;

    public Material[] _material;
    public int x;
    Renderer rend;

    private void Start()
    {
        Timer = Random.Range(MinTime, MaxTime);
        x = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = _material[x];
    }
    void Update()
    {
        FlickerLight();
        rend.sharedMaterial = _material[x];
    }

    private void FlickerLight()
    {
        if (Timer > 0)
            Timer -= Time.deltaTime;

            if (Timer <= 0 )
        {
            _light.enabled = !_light.enabled;
            //Debug.Log("lIGHTS State Change");
            NextColour();
            Timer = Random.Range(MinTime, MaxTime );
        }
    }

    public void NextColour()
    {
        if(x==0)
        {
            x++;
        }
        else
        {
            x = 0;
        }
    }
}
