using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource biedronka;

    private void Start()
    {
        biedronka = GetComponent<AudioSource>();
    }

    public void PlayBiedronkaSound()
    {
        biedronka.Play();
    }
}
