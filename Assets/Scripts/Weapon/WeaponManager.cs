using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Audio;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] float fireRate;
    [SerializeField]float fireRateTimer;
    [SerializeField] bool semiAuto;

    [SerializeField] GameObject bullet;
    public Transform firePoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletPerShot;
    public float damage = 20;
    Movement aim;

    [SerializeField] AudioClip gunShot;
    [HideInInspector] public WeaponAmmo ammo;    
    [HideInInspector] public AudioSource source;
    public AudioMixer audioMixer;

    WeaponRecoil recoil;
    WeaponBloom bloom;

    public float enemyKickBack = 100f;

    public Transform leftHandTarget, leftHandHint;

    WeaponClassManager weaponClass;
    ActionStateManager actions;
    [SerializeField] PauseGame pause;
   
    void Start()
    {
        actions = GetComponentInParent<ActionStateManager>();
        bloom = GetComponent<WeaponBloom>();
        aim = GetComponentInParent<Movement>();
        fireRateTimer = fireRate;
    }

    private void OnEnable()
    {
        if(weaponClass == null)
        {
            weaponClass = GetComponentInParent<WeaponClassManager>();
            recoil = GetComponentInChildren<WeaponRecoil>();
            source = GetComponent<AudioSource>();
            ammo = GetComponentInChildren<WeaponAmmo>();
            recoil.recoilPos = weaponClass.recoilPos;

        } 
        weaponClass.SetCurrentWeapon(this);

    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer <= fireRate) return false; 
        if(ammo.currentAmmo == 0) return false;
        if(actions.currentState == actions.reloadState) return false;
        if(actions.currentState == actions.swap) return false;
        if(semiAuto && Input.GetMouseButtonDown(0)) return true;
        if(!semiAuto && Input.GetMouseButton(0)) return true;
        if(pause.isPaused) return false;
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

            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.weapon = this;

            bulletScript.direction = firePoint.transform.forward;

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        }
    }

    
    void Update() 
    {
        if (ShouldFire()) Fire();
    }
}
