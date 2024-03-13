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

    [Header("Muzzle Light Settings")]
    [SerializeField] float lightReturnSpeed;

    //References
    WeaponAmmo ammo;
    [SerializeField] ActionStateManager actionManager;
    WeaponRecoil recoil;
    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticles;
    WeaponBloom bloom;
    float lightIntensity;


    // Start is called before the first frame update
    void Start()
    {
        recoil = GetComponent<WeaponRecoil>();
        audioSource = GetComponent<AudioSource>();
        ammo = GetComponent<WeaponAmmo>();
        bloom = GetComponent<WeaponBloom>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        fireRateTimer = fireRate;  
    }

    // Update is called once per frame
    void Update()
    {
        if(CanFire()) Fire();
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
    }

    bool CanFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo == 0) return false;//Þarjör boþ.
        if (actionManager.currentState == actionManager.Reload) return false;//Reload'dayýz.
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;//Semi Automatic
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;//Fully Automatic
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        ammo.currentAmmo--;

        barrelPos.LookAt(aimManager.aimPos);
        barrelPos.localEulerAngles = bloom.BloomAngle(barrelPos);

        audioSource.PlayOneShot(gunShotSound);
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();
        
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet,barrelPos.position,barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    void TriggerMuzzleFlash()
    {
        muzzleFlashParticles.Play();
        muzzleFlashLight.intensity = lightIntensity;
    }
}
