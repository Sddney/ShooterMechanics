using UnityEngine;
using UnityEngine.Animations.Rigging; 
using UnityEngine.Audio;

public class ActionStateManager : MonoBehaviour
{

    public AudioMixer audioMixer;

    public ActionBaseState currentState;
    public ReloadState reloadState = new ReloadState();
    public ActionDefaultState defaultState = new ActionDefaultState();

    public GameObject currentWeapon;
    public GameObject currentWeaponManaging;
    public WeaponAmmo ammo;

    public MultiAimConstraint rHandAim;
    public TwoBoneIKConstraint LHandIK;

    [HideInInspector]public Animator anim;
    [SerializeField] AudioSource audioSourceReload;
    void Start()
    {
        anim = GetComponent<Animator>();
        ChangeState(defaultState);
        ammo = currentWeapon.GetComponent<WeaponAmmo>();
    }
    
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(ActionBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        ammo.Reload();
        ChangeState(defaultState); 
    }

    public void MagOut()
    {
        audioSourceReload.PlayOneShot(ammo.magOutSound, 2f);
    }

    public void MagIn()
    {
        audioSourceReload.PlayOneShot(ammo.magInSound, 2f);
    }

    public void ReleaseSlide()
    {
       audioSourceReload.PlayOneShot(ammo.sildeReleaseSound, 2f); 
    }
}
