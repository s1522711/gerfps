using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    [SerializeField] BulletHudScript bulletHud;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;
    public float destroyDelay = 5f; // New variable for delay before destruction

    bool readyToThrow;

    // Start is called before the first frame update
    void Start()
    {
        readyToThrow = true;
        bulletHud.Bullets = totalThrows;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
        
        // update bullet hud
        bulletHud.Bullets = totalThrows;
    }

    private void Throw()
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody of projectile
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 direction = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            direction = (hit.point - attackPoint.position).normalized;
        }

        // add force to projectile
        Vector3 forceToAdd = direction * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        // reduce total throws
        totalThrows--;

        // start coroutine to destroy the projectile after a delay
        StartCoroutine(DestroyProjectileAfterDelay(projectile));

        // implement cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private IEnumerator DestroyProjectileAfterDelay(GameObject projectile)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(projectile);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
