using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource biedronka;

    private bool clicked;
    
    private void Start()
    {
        biedronka = GetComponent<AudioSource>();
    }

    public void PlayBiedronkaSound()
    {
        if (clicked) return;
        clicked = true;
        biedronka.Play();
    }
}
