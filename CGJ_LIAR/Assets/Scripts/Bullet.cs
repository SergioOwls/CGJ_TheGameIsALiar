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
            // Later implement Blood Particle CAP
            foreach (ParticleSystem ps in blood)
            {
                RaycastHit rayData;
                Physics.Raycast(transform.position, transform.forward, out rayData);

                Instantiate(ps, other.transform.position, Quaternion.LookRotation(rayData.normal), other.transform);
            }
        }
    }
}