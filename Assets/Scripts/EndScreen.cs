using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject OriginalCanvas;
    [SerializeField] GameObject SprintBar;
    private void OnTriggerEnter(Collider other)
    {
        GameOverScreen.SetActive(true);
        OriginalCanvas.SetActive(false);
        SprintBar.SetActive(false);
    }
}
