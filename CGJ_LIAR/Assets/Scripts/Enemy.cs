using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform controlTower; // CONTROL TOWER (Need to change for instantiations).
    public GameObject poof;
    public Sprite[] sprites;


    private NavMeshAgent agent;
    private SpriteRenderer sr;
    private bool isHit;

    public void Init(Transform towerTarget)
    {
        controlTower = towerTarget;

        agent = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();

        isHit = false;
        agent.SetDestination(controlTower.position);
    }

    public void Hit() {
        StartCoroutine(Death());
    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, controlTower.position) <= 3)
        {
            ControlTower.tower.EnemyHit();
            Destroy(this.gameObject);
        }
    }

    private IEnumerator Death()
    {
        isHit = true;

        yield return new WaitForSeconds(1f);

        Instantiate(poof, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
    }

    public IEnumerator Swap()
    {
        sr.sprite = sprites[1];
        yield return new WaitForSeconds(5);
        if (sr != null)
            sr.sprite = sprites[0];
    }

    public bool getIsHit()
    {
        return isHit;
    }
}