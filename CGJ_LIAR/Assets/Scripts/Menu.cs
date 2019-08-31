using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Image fade;

    public void FadeToScene()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        while(fade.color.a < 1)
        {
            Debug.Log(fade.color.a);

            float newA = fade.color.a;
            fade.color = new Color(0, 0, 0, newA += 0.05f);
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(1);
    }
}