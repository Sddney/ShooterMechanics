using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Audio;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] float fireRate;
    [SerializeField]float fireRateTimer;
    [SerializeField] bool semiAuto;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletPerShot;
    Movement aim;

    [SerializeField] AudioClip gunShot;
    WeaponAmmo ammo;    
    [SerializeField]AudioSource source;
    public AudioMixer audioMixer;

    WeaponRecoil recoil;
    WeaponBloom bloom;

   
    void Start()
    {
        bloom = GetComponent<WeaponBloom>();
        recoil = GetComponentInChildren<WeaponRecoil>();
        ammo = GetComponentInChildren<WeaponAmmo>();
        source = GetComponent<AudioSource>();
        aim = GetComponentInParent<Movement>();
        fireRateTimer = fireRate;
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer <= fireRate) return false; 
        if(ammo.currentAmmo == 0) return false;
        if(semiAuto && Input.GetMouseButtonDown(0)) return true;
        if(!semiAuto && Input.GetMouseButton(0)) return true;
        return false;
    }

    void Fire()
    {

        fireRateTimer = 0f;
        firePoint.LookAt(aim.TrueAimPos);
        firePoint.localEulerAngles = bloom.BloomAngle(firePoint);
        source.PlayOneShot(gunShot);
        ammo.currentAmmo--;
        recoil.ApplyRecoil();
        recoil.muzzleFlash.SetActive(true);
        for(int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, firePoint.position, firePoint.rotation); 
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        }
    }

    
    void Update() 
    {
        if (ShouldFire()) Fire();
        Debug.Log(ammo.currentAmmo);
    }
}
