﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Converse : MonoBehaviour
{
    public static Converse say;
    public int textScreenTime;

    private readonly float typeSpeed = 0.05f;
    private readonly float textSFXVol = 0.4f;

    private Text text;
    private AudioSource textSFX;
    private bool isBusy;

    void Start()
    {
        if (say == null)
            say = this;

        text = GetComponent<Text>();
        textSFX = GetComponent<AudioSource>();

        isBusy = false;
        text.enabled = false;
        this.Text("According to all known laws of aviation,");
    }

    public void Text(string sentence)                       { StartCoroutine(this.TextBackEnd(sentence)); }
    public void Text(string sentence, float iniWaitTime)    { StartCoroutine(this.TextBackEnd(sentence, iniWaitTime)); }

    private IEnumerator TextBackEnd(string sentence)
    {
        isBusy = true;

        text.enabled = true;
        text.text = "";

        foreach(char c in sentence.ToCharArray()){
            text.text += c;
            if(!textSFX.isPlaying)
                textSFX.Play();
            yield return new WaitForSeconds(typeSpeed);
        }

        while (textSFX.volume != 0)
        {
            textSFX.volume -= 0.06f;
            yield return new WaitForSeconds(0.02f);
        }
            
        textSFX.Stop();
        textSFX.volume = textSFXVol;

        yield return new WaitForSeconds(textScreenTime);
        text.enabled = false;

        isBusy = false;
    }

    public IEnumerator TextBackEnd(string sentence, float iniWaitTime)
    {
        yield return new WaitForSeconds(iniWaitTime);
        this.Text(sentence);
    }

    public bool IsBusy()
    {
        return isBusy;
    }
}