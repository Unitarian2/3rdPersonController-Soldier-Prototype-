using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Settings")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    [SerializeField] AimStateManager aimManager;

    [Header("Sound Settings")]
    [SerializeField] AudioClip gunShotSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanFire()) Fire();
    }

    bool CanFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if(semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;//Semi Automatic
        if(!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;//Fully Automatic
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aimManager.aimPos);
        audioSource.PlayOneShot(gunShotSound);
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet,barrelPos.position,barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
