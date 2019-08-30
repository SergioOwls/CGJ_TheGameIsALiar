using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamLayout : MonoBehaviour
{
    public static CamLayout feed;

    public Sprite[] sprites;

    private Image image;
    private AudioSource sfx;
    private bool isDisrupted;

    private void Awake()
    {
        if (feed == null)
            feed = this;
    }

    private void Start()
    {
        image = GetComponent<Image>();
        sfx = GetComponent<AudioSource>();
    }

    public void Disrupt() { StartCoroutine(Disruption()); }

    public bool IsDisrupted() { return isDisrupted; }

    private IEnumerator Disruption()
    {
        isDisrupted = true;
        image.sprite = sprites[1];
        sfx.Play();

        yield return new WaitForSeconds(sfx.clip.length);

        isDisrupted = false;
        image.sprite = sprites[0];
    }
}