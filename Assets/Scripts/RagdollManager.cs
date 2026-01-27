using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    Rigidbody[] rbs;
    void Start()
    {
        
        rbs = GetComponentsInChildren<Rigidbody>(); 
        foreach(Rigidbody rb in rbs) rb.isKinematic = true;
    }

    
   public void EnableRagdoll()
    {
        foreach (Rigidbody rb in rbs) rb.isKinematic = false;
    }
}
