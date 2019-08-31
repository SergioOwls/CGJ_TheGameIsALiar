using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music music;
    public AudioClip endMusic;

    private AudioSource song;
    private WaitForSeconds fade;

    private void Awake()
    {
        if (music == null)
            music = this;
    }

    private void Start()
    {
        song = GetComponent<AudioSource>();
        fade = new WaitForSeconds(0.02f);
    }

    public void FadeOut() { StartCoroutine(ToFadeOut()); }

    private IEnumerator ToFadeOut()
    {
        while(song.volume > 0)
        {
            song.volume -= 0.03f;
            yield return fade;
        }
        song.Stop();
        song.volume = 0.18f;
        song.clip = endMusic;
        song.Play();
    }
}