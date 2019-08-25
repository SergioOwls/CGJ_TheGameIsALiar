using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Converse : MonoBehaviour
{
    public static Converse say;
    public int textScreenTime;

    private Text text;
    private float typeSpeed = 0.05f;

    void Start()
    {
        if (say == null)
            say = this;

        text = GetComponent<Text>();
        text.enabled = false;
        this.Text("A TEST");
    }

    public void Text(string sentence)                       { StartCoroutine(this.TextBackEnd(sentence)); }
    public void Text(string sentence, float iniWaitTime)    { StartCoroutine(this.TextBackEnd(sentence, iniWaitTime)); }

    private IEnumerator TextBackEnd(string sentence)
    {
        text.enabled = true;
        text.text = "";

        foreach(char c in sentence.ToCharArray()){
            text.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        yield return new WaitForSeconds(textScreenTime);
        text.enabled = false;
    }

    public IEnumerator TextBackEnd(string sentence, float iniWaitTime)
    {
        yield return new WaitForSeconds(iniWaitTime);

        text.enabled = true;
        text.text = "";

        foreach (char c in sentence.ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(textScreenTime);
        text.enabled = false;
    }
}