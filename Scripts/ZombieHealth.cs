using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour {

	// Public Variables:
	public float health = 3f;	// Starting health of Zombie.
	public float currentHealth;	// Amount of health the Zombie currently has.
	public float throwSpeed = 15f; // Speed at which the Zombie is pushed backwords upon death.

	// Private Variables:
	private Animator animator; // Zombie animator controller.
	private AudioSource zombieDeathSound; // Zombie audio source component.
	private ParticleSystem hit; // Blood when enemy is hit.
	private CapsuleCollider capCollider; // Zombie capsule collider compenent.
	private bool isDead;
	private bool isThrown;
	private ZombieSpawner zombieManager;

	// Use this for initialization
	void Awake () 
	{
		// Get components
		animator = GetComponent<Animator>();
		zombieDeathSound = GetComponent<AudioSource>();
		capCollider = GetComponent<CapsuleCollider>();
		hit = GetComponentInChildren<ParticleSystem>();

		// Get instance of ZombieSpawner
		zombieManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ZombieSpawner>();

		// Make the Zombie have full health upon spawn.
		currentHealth = health;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isThrown) // If Zombie should be thrown upon death
			transform.Translate(-Vector3.forward * throwSpeed * Time.deltaTime);
	}
	
	/**
	 * Public as it is used by player to damage Zombie.
	 */
	public void TakeDamage(int damage, Vector3 hitPoint)
	{
		if(isDead) return; // Do nothing if Zombie is dead.

		currentHealth -= damage; // Take damage.

		hit.transform.position = hitPoint; // Set position of blood.
		hit.Play(); // Play blood particles.

		if(currentHealth <= 0) // Zombie is dead.
			Die();

	}

	void Die()
	{
		isDead = true; // Zombie is now dead.
        isThrown = true; // Throw zombie back.

		capCollider.isTrigger = true; // Shots pass through body.
		animator.SetTrigger("Dead"); // Start death animation.

        zombieDeathSound.Play();

		zombieManager.zombieKilled(); // Update they amount of zombies the player has killed;
    }

	/**
	 * Function is called by zombie animation event.
	 */
	public void RemoveZombie()
	{
        isThrown = false; // Stop throwing zombie back.
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
		Destroy(gameObject, 5f); // Destroy Zombie after 5 seconds.
	}
}
