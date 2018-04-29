using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    // Public Variables:
    public int damage = 1;
    public float pistolDelay = 0.15f;
	public float uziDelay = 0.15f;
	public float shotgunDelay = 0.50f;
    public float range = 100f;
	public static int uziAmmo = 100;
	public static int shotgunAmmo = 30;
	public static int currentAmmo;
	private string equippedWeapon;
	private string pistol = "PISTOL";
	private string uzi = "UZI";
	private string shotgun= "SHOTGUN";

    // Private Variables:
    private float timer;
    private Ray ray;
    private RaycastHit hit;
	private Ray ray2;
	private RaycastHit hit2;
	private Ray ray3;
	private RaycastHit hit3;
    private int shootable;
    //private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private Light gunLight;
	private LineRenderer gunLine2;
	private LineRenderer gunLine3;
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

		gunLine2 = GetComponent<LineRenderer>();
		gunLine3 = GetComponent<LineRenderer>();

		equippedWeapon = pistol;
		currentAmmo = 0;
		UIManager.ammo = currentAmmo;
		UIManager.weapon = pistol;
    }

    // Update is called once per frame
    void Update ()
    {
        // Update timer
        timer += Time.deltaTime;

        // If player clicks LMouseButton and delay has been passed, then shoot.
//		if (equippedWeapon == pistol && Input.GetButtonDown("Fire1") && timer >= pistolDelay) // (GetButtonDown instead of GetButton so LMouseButton can't be held down to fire)
		if (Input.GetButtonDown ("Fire1")) { // (GetButtonDown instead of GetButton so LMouseButton can't be held down to fire)
			if (equippedWeapon == pistol && timer >= pistolDelay)
				Shoot ();
			if (equippedWeapon == shotgun && timer >= shotgunDelay) {
				Shoot ();
			}
		}
		if (Input.GetButton ("Fire1"))
			if(equippedWeapon == uzi && timer >= uziDelay)
				Shoot ();
		

		if (Input.GetButtonDown ("Pistol")) { // (switches weapon to pistol)
			UIManager.weapon = pistol;
			equippedWeapon = pistol;
			currentAmmo = 0;
			Debug.Log ("Pistol selected");
		}

		if (Input.GetButtonDown ("Uzi")) { // (switches weapon to Uzi)
			UIManager.weapon = uzi;
			UIManager.ammo = uziAmmo;
			equippedWeapon = uzi;
			currentAmmo = uziAmmo;
			Debug.Log ("Uzi selected");
		}

		if (Input.GetButtonDown ("Shotgun")) { // (switches weapon to Uzi)
			UIManager.weapon = shotgun;
			UIManager.ammo = shotgunAmmo;
			equippedWeapon = shotgun;
			currentAmmo = shotgunAmmo;
			Debug.Log ("Shotgun selected");
		}

		if (equippedWeapon == pistol && timer >= pistolDelay * displayEffectsTime)
            TurnOffGunEffects();
		if (equippedWeapon == uzi && timer >= uziDelay * displayEffectsTime)
			TurnOffGunEffects();
		if (equippedWeapon == shotgun && timer >= shotgunDelay * displayEffectsTime)
			TurnOffGunEffects();
	}

    /**
     * Make the player shoot on left click.
     */
    void Shoot()
    {
		if (equippedWeapon == pistol || currentAmmo > 0){	// only fires the weapon if it is a pistol or has ammo.

			if (equippedWeapon != pistol) {
				currentAmmo--;	// decrements ammo when fired.
				UIManager.ammo = currentAmmo;	// sets new ammo to the Ammo UI.
				if(equippedWeapon == uzi)	
					uziAmmo = currentAmmo;	// changes the value of variable uziAmmo.
				if (equippedWeapon == shotgun)
					shotgunAmmo = currentAmmo;	// changes the value of variable shotgunAmmo.
			}
			timer = 0f; // Reset timer.

			if (equippedWeapon == shotgun) {
				damage = 3;
			} else {
				damage = 1;
			}
			// Enable gun effects.
			gunLight.enabled = true;
			//gunParticles.Stop(); // If particles are still playing, stop them
			//gunParticles.Play(); // and start them again.
			gunLine.enabled = true;
			gunLine.SetPosition (0, transform.position); // Set line at position of barrel.
			gunSound.Play ();

			ray.origin = transform.position;
			ray.direction = transform.forward;

			if (Physics.Raycast (ray, out hit, range, shootable)) { //('out hit' provides information of what was hit with raycast)
				/* Take away zombie's health. */
				ZombieHealth health = hit.collider.GetComponent<ZombieHealth> (); // Get zombie health script.

				if (health != null) { // Make sure we hit a Zombie and not something like the vehicles.
					// Make Zombie take damage:
					health.TakeDamage (damage, hit.point);
				}
				gunLine.SetPosition (1, hit.point); // Set line end point to what we shot.
			} else { // Just draw a line if not shooting at something that is 'shootable'.
				gunLine.SetPosition (1, ray.origin + ray.direction * range);
			}

			if (equippedWeapon == shotgun) {	// this will make additional bullets, if weapon is shotgun

				Vector3 line2Forward = transform.forward;
				gunLine2.enabled = true;
				gunLine2.SetPosition (0, transform.position);

				Debug.Log ("ORG X:" + line2Forward.x);
				Debug.Log ("ORG Z:" + line2Forward.z);

				bool changed = false;
				if (line2Forward.x < -.75f) {
					line2Forward.z = line2Forward.z + .25f;	// shifts the direction of the second bullet to the right.
					changed = true;
				}
				if (line2Forward.x > .75f) {
					line2Forward.z = line2Forward.z - .25f;	// shifts the direction of the second bullet to the right.
					changed = true;
				}

				if (line2Forward.z < -.75f) {
					line2Forward.x = line2Forward.x - .25f;	// shifts the direction of the second bullet to the right.
					changed = true;
				}
				if (line2Forward.z > .75f) {
					line2Forward.x = line2Forward.x + .25f;	// shifts the direction of the second bullet to the right.
					changed = true;
				}

				if (!changed) {
					if (line2Forward.z > 0) {
						line2Forward.x = line2Forward.x + .25f;	// shifts the direction of the second bullet to the right.
						changed = true;	
					} else {
						line2Forward.x = line2Forward.x - .25f;	// shifts the direction of the second bullet to the right.
						changed = true;
					}
				}



				Debug.Log ("line 2 X:" + line2Forward.x);
				Debug.Log ("line 2 Z:" + line2Forward.z);

				ray2.origin = transform.position;
				ray2.direction = line2Forward;

				if (Physics.Raycast (ray2, out hit2, range, shootable)) { //('out hit' provides information of what was hit with raycast)
					/* Take away zombie's health. */
					ZombieHealth health = hit2.collider.GetComponent<ZombieHealth> (); // Get zombie health script.

					if (health != null) { // Make sure we hit a Zombie and not something like the vehicles.
						// Make Zombie take damage:
						health.TakeDamage (damage, hit2.point);
					}
					gunLine2.SetPosition (1, hit2.point); // Set line end point to what we shot.
				} else { // Just draw a line if not shooting at something that is 'shootable'.
					gunLine2.SetPosition (1, ray2.origin + ray2.direction * range);
				}

/*********************************************************************************************************************************
**********************************************************************************************************************************
**********************************************************************************************************************************
*********************************************************************************************************************************/

				Vector3 line3Forward = transform.forward;	// direction of the third bullet;
				gunLine3.enabled = true;
				gunLine3.SetPosition (0, transform.position);

				changed = false;
				if (line3Forward.x < -.75f) {
					line3Forward.z = line3Forward.z - .25f;	// shifts the direction of the third bullet to the left.
					changed = true;
				}
				if (line3Forward.x > .75f) {
					line3Forward.z = line3Forward.z + .25f;	// shifts the direction of the third bullet to the left.
					changed = true;
				}

				if (line3Forward.z < -.75f) {
					line3Forward.x = line3Forward.x + .25f;	// shifts the direction of the third bullet to the left.
					changed = true;
				}
				if (line3Forward.z > .75f) {
					line3Forward.x = line3Forward.x - .25f;	// shifts the direction of the third bullet to the left.
					changed = true;
				}

				if (!changed) {
					if (line3Forward.z > 0) {
						line3Forward.x = line3Forward.x - .25f;	// shifts the direction of the third bullet to the left.
						changed = true;
					} else {
						line3Forward.x = line3Forward.x + .25f;	// shifts the direction of the third bullet to the left.
						changed = true;
					}
				}


				ray3.origin = transform.position;
				ray3.direction = line3Forward;

				Debug.Log ("line 3 new X:" + line3Forward.x);
				Debug.Log ("line 3 new Z:" + line3Forward.z);

				if (Physics.Raycast (ray3, out hit3, range, shootable)) { //('out hit' provides information of what was hit with raycast)
					/* Take away zombie's health. */
					ZombieHealth health = hit3.collider.GetComponent<ZombieHealth> (); // Get zombie health script.

					if (health != null) { // Make sure we hit a Zombie and not something like the vehicles.
						// Make Zombie take damage:
						health.TakeDamage (damage, hit3.point);
					}
					gunLine3.SetPosition (1, hit3.point); // Set line end point to what we shot.
				} else { // Just draw a line if not shooting at something that is 'shootable'.
					gunLine3.SetPosition (1, ray3.origin + ray3.direction * range);
				}
			}
		} else {
			Debug.Log ("No Ammo");
		}
    }


    /**
     * Disable the gun effects (LineRenderer and Light).
     */
    public void TurnOffGunEffects()
    {
        gunLine.enabled = false;    // Disable LineRenderer.
		gunLine2.enabled = false;
		gunLine3.enabled = false;
        gunLight.enabled = false;   // Disable Light. 
    }
		
}
