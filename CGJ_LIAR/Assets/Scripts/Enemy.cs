using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform controlTower; // CONTROL TOWER (Need to change for instantiations).

    public GameObject poof;
    private NavMeshAgent agent;
    private bool isHit;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isHit = false;

        agent.SetDestination(controlTower.position);
    }

    public void hit() {
        StartCoroutine(Death());
    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, controlTower.position) <= 3)
            Debug.Log("triggers are garbo");

    }

    private IEnumerator Death()
    {
        isHit = true;

        yield return new WaitForSeconds(1f);

        Instantiate(poof, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
    }

    public bool getIsHit()
    {
        return isHit;
    }
}