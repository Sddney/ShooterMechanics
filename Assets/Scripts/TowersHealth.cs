using System;
using UnityEngine;

public class TowersHealth : MonoBehaviour
{
    public int health = 100;
    private MeshRenderer mesh;
    [SerializeField] Towers towers;
    [HideInInspector] BoxCollider boxCollider;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (health <= 0)
        {
            mesh.enabled = false;
            boxCollider.enabled = false;
            int idx = Array.IndexOf(towers.towers, gameObject);
            if (idx >= 0) towers.towers[idx] = null;
        }
    }
}
