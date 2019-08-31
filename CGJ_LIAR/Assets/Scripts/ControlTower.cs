using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTower : MonoBehaviour
{
    public static ControlTower tower;

    public Sprite destroyedTower;
    public RectTransform hp;
    public EnemySpawner spawner;
    public string[] dialogueForEnemyEntry;
    public string[] dialogueForPlayerAttack;

    private SpriteRenderer sprite;
    private bool isHit, isDestroyed;

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
        isDestroyed = false;

        dialogueEnemy = 0;
        dialoguePlayer = 0;

        Converse.say.Text("Welcome to the field.");
        Converse.say.Text("Well... you're really in our control tower so...");
        Converse.say.Text("Welcome to your own baby drone.");
        Converse.say.Text("These aliens want to take us down.");
        Converse.say.Text("Defend humanity.");

        StartCoroutine(WaitForIntro());
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

        if (hp.localScale.x == 0)
        {
            if(!isDestroyed) {

                Converse.say.Trash();

                Converse.say.FinalText("W҉e҉ t҉r҉i҉e҉d҉ t҉e҉l҉l҉i҉n҉g҉ y҉o҉u҉");
                Converse.say.FinalText("T҉h҉e҉y҉ m҉o҉d҉i҉f҉i҉e҉d҉ o҉u҉r҉ f҉a҉c҉e҉s҉..." +
                                       " m҉a҉d҉e҉ u҉s҉ l҉o҉o҉k҉ l҉i҉k҉e҉ t҉h҉e҉ e҉n҉e҉m҉y҉");
                Converse.say.FinalText("I҉t҉s҉ O҉v҉e҉r҉ N҉o҉w҉.");
                Converse.say.FinalText("H҉o҉p҉e҉f҉u҉l҉l҉y҉.");

                sprite.sprite = destroyedTower;

                foreach (GameObject e in enemies)
                    e.GetComponent<Enemy>().End();

                Music.music.FadeOut();

                isDestroyed = true;
            }
            return;
        }

        if (!isDestroyed)
        {
            foreach (GameObject e in enemies)
                StartCoroutine(e.GetComponent<Enemy>().Swap());
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

            if (sprite.color.g <= 0)
                sprite.color = Color.red;
        }
        
        sprite.color = Color.white;
        isHit = false;
    }

    public bool IsHit()
    {
        return isHit;
    }

    private IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(19);
        spawner.enabled = true;
    }

    public bool IsDestroyed() { return isDestroyed; }
}