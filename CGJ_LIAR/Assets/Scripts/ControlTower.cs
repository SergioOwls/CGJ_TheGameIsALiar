using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTower : MonoBehaviour
{
    public static ControlTower tower;

    public RectTransform hp;
    public string[] dialogueForEnemyEntry;
    public string[] dialogueForPlayerAttack;

    private SpriteRenderer sprite;
    private bool isHit;

    private int dialogueEnemy, dialoguePlayer;

    private void Awake()
    {
        if (tower == null)
            tower = this;
    }

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();

        Cursor.visible = false;
        isHit = false;

        dialogueEnemy = 0;
        dialoguePlayer = 0;

        Converse.say.Text("Welcome to the field.");
        Converse.say.Text("Well... you're really in our control tower so...");
        Converse.say.Text("Welcome to your own baby drone.");
        Converse.say.Text("These aliens want to take us down.");
        Converse.say.Text("Defend humanity.");
    }

    public void PlayerHit()
    {
        if (!Converse.say.IsBusy())
        {
            Converse.say.Text(dialogueForPlayerAttack[dialoguePlayer++]);
            if (dialoguePlayer == dialogueForPlayerAttack.Length)
                dialoguePlayer = 0;
        }
            
        Hit();
    }

    private void Hit()
    {
        if (!CamLayout.feed.IsDisrupted())
            CamLayout.feed.Disrupt();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject e in enemies)
            StartCoroutine(e.GetComponent<Enemy>().Swap());

        if (hp.localScale.x == 0)
        {
            Debug.Log("boom");
            return;
        }
        float hpScale = hp.localScale.x - .5f;

        hp.localScale = new Vector3(hpScale, hp.localScale.y, hp.localScale.z);


        
        StartCoroutine(HitEffect());
    }

    public void EnemyHit()
    {
        if (!Converse.say.IsBusy())
        {
            Converse.say.Text(dialogueForEnemyEntry[dialogueEnemy++]);
            if (dialogueEnemy == dialogueForEnemyEntry.Length)
                dialogueEnemy = 0;
        }
            
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