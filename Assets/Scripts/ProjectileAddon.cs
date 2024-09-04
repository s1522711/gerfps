using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ProjectileAddon : MonoBehaviour
{
    private Rigidbody rb;

    private SphereCollider sc;

    private bool targetHit = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // make sure only to stick to the first target you hit
        if (targetHit)
            return;
        else if (collision.transform.tag == "Player")
            return;
        else
            targetHit = true;

        // make sure projectile sticks to surface
        rb.isKinematic = true;

        // disable collider
        sc.enabled = false;

        // make sure projectile moves with target
        transform.SetParent(collision.transform);
    }
}
