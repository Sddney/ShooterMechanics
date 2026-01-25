using UnityEngine;

public class WeaponBloom : MonoBehaviour
{
    [SerializeField] float defBloomAngle = 3;
    [SerializeField] float walkBloomMultiplier = 1.5f;
    [SerializeField] float crouchBloomMultiplier = 0.5f;
    [SerializeField] float runBloomMultiplier = 2;
    [SerializeField] float adsBloomMultiplier = 0.5f;

    Movement movement;
    float currentBloomAngle; 

    void Start()
    {
        movement = GetComponentInParent<Movement>();
    }

    
    public Vector3 BloomAngle(Transform barrelPos)
    {
        if(movement.currentState == movement.idle) currentBloomAngle = defBloomAngle;
        else if(movement.currentState == movement.walk) currentBloomAngle = defBloomAngle * walkBloomMultiplier;
        else if(movement.currentState == movement.run) currentBloomAngle = defBloomAngle * runBloomMultiplier;
        else if(movement.currentState == movement.crouch)
        {
            if(movement.direction.magnitude == 0) currentBloomAngle = defBloomAngle * crouchBloomMultiplier;
            else currentBloomAngle = defBloomAngle * walkBloomMultiplier * crouchBloomMultiplier;
        }
        if(movement.currentAimState == movement.aim) currentBloomAngle *= adsBloomMultiplier;

        float randomX = Random.Range(-currentBloomAngle, currentBloomAngle);
        float randomY = Random.Range(-currentBloomAngle, currentBloomAngle);
        float randomZ = Random.Range(-currentBloomAngle, currentBloomAngle);

        Vector3 randomRotation = new Vector3(randomX, randomY, randomZ);
        return barrelPos.localEulerAngles + randomRotation;
    }
}
