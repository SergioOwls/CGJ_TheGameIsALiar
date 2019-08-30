using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem[] blood;

    private void OnParticleCollision(GameObject o)
    {

        if (o.CompareTag("Enemy"))
        {
            if (o.GetComponent<Enemy>().getIsHit())
                return;
        
          o.GetComponent<Enemy>().Hit();
            
            foreach (ParticleSystem ps in blood)
            {
                RaycastHit rayData;
                Physics.Raycast(transform.position, transform.forward, out rayData);
        
                Instantiate(ps, o.transform.position, Quaternion.LookRotation(rayData.normal), o.transform);
            }
        }

        if (o.CompareTag("Tower"))
        {
            if (ControlTower.tower.IsHit())
                return;

            ControlTower.tower.PlayerHit();
        }
    }
}