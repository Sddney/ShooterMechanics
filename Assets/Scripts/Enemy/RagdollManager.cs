using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    Rigidbody[] rbs;
    private BoxCollider coll;
    void Start()
    {
        
        rbs = GetComponentsInChildren<Rigidbody>(); 
        coll = GetComponent<BoxCollider>();
        foreach(Rigidbody rb in rbs) rb.isKinematic = true;
    }

    
   public void EnableRagdoll()
    {
        coll.enabled = false;
        foreach (Rigidbody rb in rbs) rb.isKinematic = false;

    }
}
