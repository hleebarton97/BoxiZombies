using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

	// Public variables.
	public int startHealth = 100;	// The amount of health the player starts with.
	public int currentHealth;	// Current health the player has during game.

	// UI
	/*public UIHealth uiHealthCount;	// UI's health count.
	*/
	public Image hitOverlay; 	// Overlay to let the player know they were hit.
	public float fadeTime = 5f; 	// How quick the overlay will fade.
	public Color overlayColor = new Color(1f, 0f, 0f, 0.1f); // Red tint.

	// Audio
	//public AudioClip deathSound;	// Sound played when player dies.

	// Private variables.
	private Animator animator;			// Reference to player animator.
	private PlayerMove playerMove;		// Reference to player movement.
	private PlayerShoot playerShoot;	// Reference to PlayerShoot script.
	private GameObject weapon;			// Reference to the weapon the player is holding.
	private AudioSource playerAudio;	// Reference to audio source.
	private AudioSource gameAudio;		// Reference to background music.
	
	// Checks
	private bool isDead;	// Is the player dead?
	private bool isHit;		// True when the zombie attacks the player.
							// Used to display image damage overlay.

	// Use this for initialization
	void Awake() 
	{
		// Get components.
		animator = GetComponent<Animator>();		// Animator component.
		playerMove = GetComponent<PlayerMove>();	// PlayerMove script.
		playerShoot = GetComponentInChildren<PlayerShoot>();	// PlayerShoot script.
		playerAudio = GetComponent<AudioSource>();
		gameAudio = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();

		weapon = GameObject.FindGameObjectWithTag("Weapon"); // Weapon object the player is holding.

		currentHealth = startHealth; // Set health at start of game.
		UIManager.health = currentHealth; // Set health UI at start of game.
	}
	
	// Update is called once per frame
	void Update() 
	{
		// Use isHit to display overlay to user.
		if(isHit)
		{
			hitOverlay.color = overlayColor;
			playerAudio.Play(); // Play hit sound when hit by zombie.
		}
		else
			hitOverlay.color = Color.Lerp(hitOverlay.color, Color.clear, fadeTime * Time.deltaTime);
		
		// Reset isHit
		isHit = false;
	}

	/**
	 * When zombie collides with player, hit the player,
	 * and damage the player.
	 */
	public void HitPlayer(int damage)
	{
		isHit = true; // Player has been hit.
		currentHealth -= damage; // Update current health.
		if(currentHealth <= 0)
			UIManager.health = 0;	// Make sure UI doesn't show -health.
		else
			UIManager.health = currentHealth; // Update health UI.
		//playerAudio.Play();

		Debug.Log("Health: " + currentHealth);
		
		// If player should be dead, but hasn't been set to dead - KillPlayer().
		if(currentHealth <=0 && !isDead)
			KillPlayer();
	}

	/**
	 * Kill the player and go to game over menu.
	 */
	private void KillPlayer()
	{
		isDead = true; // Player is dead.
		
		playerShoot.TurnOffGunEffects(); // Turn off any effects from shooting.
		
		gameAudio.pitch = .65f; // Make a creepy pitch change in audio upon death.

		animator.SetTrigger("Die"); // Animate the player's death.

		Destroy(weapon, 0f); // Remove weapon model from the game.

		//playerAudio.clip = playerDeath;
		//playerAudio.Play();

		// Disable movement and shooting.
		playerMove.enabled = false;
		playerShoot.enabled = false;

		// Change to End Scene after 3 seconds of the player being dead.
		Invoke("GameOver", 3);
	}

	/**
	 * Change the scene to "End" (game over menu).
	 */
	private void GameOver()
	{
		SceneManager.LoadScene("End");
	}
}
