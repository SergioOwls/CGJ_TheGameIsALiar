using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
            Destroy(other);
    }
}
