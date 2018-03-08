using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    // Public Variables:
    public int damage = 1;
    public float delay = 0.15f;
    public float range = 100f;

    // Private Variables:
    private float timer;
    private Ray ray;
    private RaycastHit hit;
    private int shootable;
    //private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private Light gunLight;
    private AudioSource gunSound;
    private float displayEffectsTime = 0.2f;

    // Use this for initialization
    void Awake()
    {
        shootable = LayerMask.GetMask("Shootable");     // Get shootable layer.
        //gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        gunSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        // Update timer
        timer += Time.deltaTime;

        // If player clicks LMouseButton and delay has been passed, then shoot.
        if (Input.GetButtonDown("Fire1") && timer >= delay) // (GetButtonDown instead of GetButton so LMouseButton can't be held down to fire)
            Shoot();

        if (timer >= delay * displayEffectsTime)
            TurnOffGunEffects();
	}

    /**
     * Make the player shoot on left click.
     */
    void Shoot()
    {
        timer = 0f; // Reset timer.

        // Enable gun effects.
        gunLight.enabled = true;
        //gunParticles.Stop(); // If particles are still playing, stop them
        //gunParticles.Play(); // and start them again.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position); // Set line at position of barrel.
        gunSound.Play();

        ray.origin = transform.position;
        ray.direction = transform.forward;

        if(Physics.Raycast(ray, out hit, range, shootable)) //('out hit' provides information of what was hit with raycast)
        {
            /* Take away zombie's health. */
            ZombieHealth health = hit.collider.GetComponent<ZombieHealth>(); // Get zombie health script.

            if(health != null) // Make sure we hit a Zombie and not something like the vehicles.
            {
                // Make Zombie take damage:
                health.TakeDamage(damage, hit.point);
            }
            gunLine.SetPosition(1, hit.point); // Set line end point to what we shot.
        }
        else // Just draw a line if not shooting at something that is 'shootable'.
        {
            gunLine.SetPosition(1, ray.origin + ray.direction * range);
        }
    }

    /**
     * Disable the gun effects (LineRenderer and Light).
     */
    public void TurnOffGunEffects()
    {
        gunLine.enabled = false;    // Disable LineRenderer.
        gunLight.enabled = false;   // Disable Light. 
    }
}
