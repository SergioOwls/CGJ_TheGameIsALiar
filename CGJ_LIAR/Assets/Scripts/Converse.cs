using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text))]
public class Converse : MonoBehaviour
{
    public static Converse say;
    public int textScreenTime;
    public Image fade;

    private readonly int MAX_LENGTH = 10;
    private readonly float textSFXVol = 0.4f;

    private Text text;
    private AudioSource textSFX;
    private bool isBusy;
    private string[] sentences;
    private int sentCount, usedSent;
    private float typeSpeed = 0.05f;

    private Queue<string> sentQueue;

    void Start()
    {
        if (say == null)
            say = this;

        text = GetComponent<Text>();
        textSFX = GetComponent<AudioSource>();
        sentQueue = new Queue<string>();

        sentences = new string[MAX_LENGTH];
        sentCount = 0; usedSent = 0;

        isBusy = false;
        text.enabled = false;
    }

    private void Update()
    {
        if(sentQueue.Count != 0)
        {
            if (!isBusy)
            {
                Text(sentQueue.Dequeue());
            }
        }
    }

    public void Text(string sentence)       { StartCoroutine(TextBackEnd(sentence)); }
    public void AlienText(string sentence)  { AlienTextBackEnd(sentence); }  
    public void FinalText(string sentence) { FinalTextBackEnd(sentence); }

    private IEnumerator TextBackEnd(string sentence)
    {
        if (isBusy)
        {
            sentQueue.Enqueue(sentence);
            yield break;
        }

        isBusy = true;
        text.enabled = true;
        text.text = "";

        foreach (char c in sentence.ToCharArray()){
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
        this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        text.color = Color.white;
    }

    private void AlienTextBackEnd(string sentence)
    {
        text.color = Color.green;
        this.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);

        StartCoroutine(TextBackEnd(sentence));
    }

    private void FinalTextBackEnd(string sentence)
    {
        text.enabled = false;
        typeSpeed = 0.1f;
        StartCoroutine(TextBackEnd(sentence));
        StartCoroutine(WaitForFadeOut());
    }

    private IEnumerator WaitForFadeOut()
    {
        yield return new WaitForSeconds(28);

        while (fade.color.a < 1)
        {
            Debug.Log(fade.color.a);

            float newA = fade.color.a;
            fade.color = new Color(0, 0, 0, newA += 0.05f);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(4.2f);
        SceneManager.LoadScene(0);
    }

    public void Trash() { sentQueue.Clear(); }

    public bool IsBusy()
    {
        return isBusy;
    }
}