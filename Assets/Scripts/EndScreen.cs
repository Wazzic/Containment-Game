using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject OriginalCanvas;

    private void OnTriggerEnter(Collider other)
    {
        GameOverScreen.SetActive(true);
        OriginalCanvas.SetActive(false);
    }
}
