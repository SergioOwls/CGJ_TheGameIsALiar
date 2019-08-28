using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLauncher : MonoBehaviour
{
    public Transform cam;
    [Range(0,.5f)]  public float shakeAmplitude;

    private ParticleSystem[] vfx;
    private bool isShooting;

    private void Start()
    {
        vfx = GetComponentsInChildren<ParticleSystem>();
        isShooting = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            StartGun();

        if (Input.GetButtonUp("Fire1"))
            StopGun();

    }

    // If a particle collides, throw "hit" particle effect
    // ?
    private void StartGun()
    {
        if (!vfx[0].isPlaying)                      
            foreach (ParticleSystem ps in vfx)      // Play particles.
                ps.Play();

        isShooting = true;

        StartCoroutine(ShakeCam());
    }

    private void StopGun()
    {
        foreach (ParticleSystem ps in vfx)
            ps.Stop();

        isShooting = false;
    }

    private IEnumerator ShakeCam()
    {
        Vector3 ogPos = cam.localPosition;

        while (isShooting)
        {
            float x = Random.Range(-1, 1) * shakeAmplitude;
            float y = Random.Range(-1, 1) * shakeAmplitude;

            cam.localPosition = new Vector3(x, y, ogPos.z);

            yield return null;
        }
        cam.localPosition = ogPos;
    }
}
