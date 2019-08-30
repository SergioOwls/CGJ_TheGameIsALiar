using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Converse : MonoBehaviour
{
    public static Converse say;
    public int textScreenTime;

    private readonly int MAX_LENGTH = 10;
    private readonly float typeSpeed = 0.05f;
    private readonly float textSFXVol = 0.4f;

    private Text text;
    private AudioSource textSFX;
    private bool isBusy;
    private string[] sentences;
    private int sentCount, usedSent;

    void Start()
    {
        if (say == null)
            say = this;

        text = GetComponent<Text>();
        textSFX = GetComponent<AudioSource>();

        sentences = new string[MAX_LENGTH];
        sentCount = 0; usedSent = 0;

        isBusy = false;
        text.enabled = false;
    }


    private void Update()
    {
        if(sentCount != 0)
        {
            if (!isBusy)
            {
                arrayCrap();
            }
        }
    }

    public void Text(string sentence)                       { StartCoroutine(this.TextBackEnd(sentence)); }
    public void Text(string sentence, float iniWaitTime)    { StartCoroutine(this.TextBackEnd(sentence, iniWaitTime)); }

    private void arrayCrap()
    {
        Text(sentences[usedSent]);
        usedSent++;
        if (usedSent == sentCount)
        {
            usedSent = 0;
            sentCount = 0;
        }
    }

    private IEnumerator TextBackEnd(string sentence)
    {
        if (isBusy)
        {
            sentences[sentCount] = sentence;
            sentCount++;
            yield break;
        }

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