using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHit : MonoBehaviour {

	// Public varaibles
	public float timeBetweenHits = 1.0f;
	public int damage = 20;

	// Private variables
	private Animator animator;
	private GameObject player;
	private PlayerHealth playerHealth;
	private ZombieHealth zombieHealth;
	private bool inRange;
	private float timer;

	// Use this for initialization
	void Awake() 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		zombieHealth = GetComponent<ZombieHealth>();
		animator = GetComponent<Animator>();
	}

	/**
	 * Check if zombie is in range of the player.
	 */
	void OnTriggerEnter(Collider obj)
	{
		if(obj.gameObject == player) // if the obj is the player
			inRange = true; // then zombie is in range.
	}

	/**
	 * Check if zombie is no longer in range of the player.
	 */
	void OnTriggerExit(Collider obj)
	{
		if(obj.gameObject == player) // If the obj is the player
			inRange = false; // then the zombie is no longer in range.
	}
	
	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime; // Set timer for each time passed.

		if(timer > timeBetweenHits && inRange && zombieHealth.currentHealth > 0) // If it is time for the zombie to attack
			animator.SetBool("Hit", true);									     // and the player is in range then attack.
		else
			animator.SetBool("Hit", false);

		/*if(playerHealth.currentHealth <= 0) // Player is dead
			animator.SetTrigger("PlayerIsDead"); */
		// Above if only if I want the zombies to stop moving when
		// player dies, but zombies don't do that, so I disabled it
		// for now.
	}

	/**
	 *
	 */
	void Hit()
	{
		timer = 0f; // Reset time.

		if(playerHealth.currentHealth > 0 && inRange)
			playerHealth.HitPlayer(damage);
	}
}
