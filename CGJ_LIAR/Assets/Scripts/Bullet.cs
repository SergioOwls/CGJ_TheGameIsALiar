using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem[] blood;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy>().getIsHit())
                return;

            other.GetComponent<Enemy>().hit();
            
            foreach (ParticleSystem ps in blood)
            {
                RaycastHit rayData;
                Physics.Raycast(transform.position, transform.forward, out rayData);

                Instantiate(ps, other.transform.position, Quaternion.LookRotation(rayData.normal), other.transform);
            }
        }
    }
}