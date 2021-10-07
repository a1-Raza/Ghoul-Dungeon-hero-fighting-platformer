using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    //[SerializeField] AudioClip attackSound;
    //[SerializeField] AudioClip hurtSound;
    //[SerializeField] AudioClip dieSound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (!audioClip) { return; }
        //audioSource.clip = audioClip;
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
    }
}
