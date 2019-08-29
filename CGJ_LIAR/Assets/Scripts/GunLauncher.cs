using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLauncher : MonoBehaviour
{
    public Transform cam;
    public AudioClip[] gunClips;

    [Range(0,.5f)]  public float shakeAmplitude;

    private ParticleSystem[] gunVFX;
    private AudioSource gunSFX;
    private bool isShooting, isReloading;

    private void Start()
    {
        gunVFX = GetComponentsInChildren<ParticleSystem>();
        gunSFX = GetComponent<AudioSource>();

        isShooting = false;
        isReloading = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") & !isReloading)
            StartGun();

        if (Input.GetButtonUp("Fire1"))
            isShooting = false;
    }

    private void StartGun()
    {
        isShooting = true;
        gunSFX.clip = gunClips[0];

        if (!gunVFX[0].isPlaying)                      
            foreach (ParticleSystem ps in gunVFX)
                ps.Play();

        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        Vector3 ogPos = cam.localPosition;

        while (isShooting)
        {
            if (!gunSFX.isPlaying)
                gunSFX.Play();

            float x = Random.Range(-1, 1) * shakeAmplitude;
            float y = Random.Range(-1, 1) * shakeAmplitude;

            cam.localPosition = new Vector3(x, y, ogPos.z);
            yield return null;
        }

        isReloading = true;

        cam.localPosition = ogPos;

        gunSFX.clip = gunClips[1];
        gunSFX.Play();

        foreach (ParticleSystem ps in gunVFX)
            ps.Stop();

        yield return new WaitForSeconds(1);

        isReloading = false;
    }
}