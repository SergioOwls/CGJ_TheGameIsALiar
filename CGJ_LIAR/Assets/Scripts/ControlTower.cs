using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTower : MonoBehaviour
{
    public static ControlTower tower;

    public RectTransform hp;

    private SpriteRenderer sprite;
    private bool isHit;

    private void Awake()
    {
        if (tower == null)
            tower = this;
    }

    private void Start()
    {
        Cursor.visible = false;
        isHit = false;

        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void PlayerHit()
    {
        if (!Converse.say.IsBusy())
            Converse.say.Text("Careful! We are on your side maggot!");
        Hit();
    }

    private void Hit()
    {
        if (!CamLayout.feed.IsDisrupted())
            CamLayout.feed.Disrupt();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject e in enemies)
            StartCoroutine(e.GetComponent<Enemy>().Swap());

        float hpScale = hp.localScale.x;

        hp.localScale = new Vector3(hpScale - .5f, hp.localScale.y, hp.localScale.z);
        
        StartCoroutine(HitEffect());
    }

    public void EnemyHit()
    {
        if (!Converse.say.IsBusy())
            Converse.say.Text("Sh&! THE SHREKS ARE IN HERE! HURRY");
        Hit();
    }

    private IEnumerator HitEffect()
    {
        isHit = true;

        float g = sprite.color.g;
        float b = sprite.color.b;

        while(sprite.color != Color.red)
        {
            sprite.color = new Color(1, g -= 0.05f, b -= 0.05f, 1);
            yield return new WaitForSeconds(0.01f);
        }
        
        sprite.color = Color.white;
        isHit = false;
    }

    public bool IsHit()
    {
        return isHit;
    }
}