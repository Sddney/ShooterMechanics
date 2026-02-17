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
        towers = GameObject.FindGameObjectWithTag("Towers").GetComponent<Towers>();
    }

    void Update()
    {
        if (health <= 0)
        {
            //mesh.enabled = false;
            //boxCollider.enabled = false;
            towers.towers.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
