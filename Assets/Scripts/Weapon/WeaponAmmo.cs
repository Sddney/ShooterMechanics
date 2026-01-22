using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize = 30;
    public int reserveAmmo = 120;
    public int currentAmmo;

    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip sildeReleaseSound;
    void Start()
    {
        currentAmmo = clipSize;
    }

    public void Reload()
    {
        if (reserveAmmo >= clipSize)
        {
            int ammoToBeAdded = clipSize - currentAmmo;
            reserveAmmo -= ammoToBeAdded;
            currentAmmo += ammoToBeAdded;
        }
        else if (reserveAmmo > 0)
        {
            if (reserveAmmo + currentAmmo > clipSize)
            {
                int leftAmmo = reserveAmmo + currentAmmo - clipSize;
                reserveAmmo = leftAmmo;
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += reserveAmmo;
                reserveAmmo = 0;
            } 
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) Reload();
        
    }
}
