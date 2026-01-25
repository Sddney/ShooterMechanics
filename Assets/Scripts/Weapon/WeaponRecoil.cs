using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] Transform recoilPos;
    [SerializeField] float kickBackAmount = -1;
    [SerializeField] float kickBackSpeed = 10;
    [SerializeField] float returnSpeed = 20;
    float currentRecoilPos;
    float finalRecoilPos;

    public GameObject muzzleFlash;

    void Start()
    {
        muzzleFlash.SetActive(false);
    }

    public void ApplyRecoil()
    {
        currentRecoilPos += kickBackAmount;
    }
    
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && muzzleFlash.activeSelf) muzzleFlash.SetActive(false);
        currentRecoilPos = Mathf.Lerp(currentRecoilPos, 0, returnSpeed * Time.deltaTime);
        finalRecoilPos = Mathf.Lerp(finalRecoilPos, currentRecoilPos, kickBackSpeed * Time.deltaTime);
        recoilPos.localPosition = new Vector3(0, 0, finalRecoilPos);
    }
}
