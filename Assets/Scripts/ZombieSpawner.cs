using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The Zombie spawner also manages the round system
 * for the game.
 */
public class ZombieSpawner : MonoBehaviour {

	// Public variables:
	public GameObject zombie;	// Zombie to be spawned.
	public float spawnTime = 1.5f; // Time between each spawn of a zombie (1.5 sec).
	// [0.5 = normal], [1.0 = hard], [2.5 = undead]
	public float difficultyMult = 0.5f; // Multiplier to increase difficulty with each round.
	public Transform[] spawnPoints; // An array of all of the created spawn points located on the map.


	// Private variables:
	private int zombieSpawnCount;	 // Current zombie count.
	private int zombieKillCount;	 // Amount of zombies the player has killed for the round.
	private int currentRound;		 // Current round the player is on.
	private int maxRoundZombieCount; // Maximum amount of zombies to be spawned for the round.
	private bool spawn;				 // True until maximum zombies for round is reached or player is dead.

	// Next round sound alert
	private AudioSource roundSound; 	// Reference to audio source.

	// Use this for initialization
	void Start() 
	{
		// Initialize appriate variables at start of game:
		zombieSpawnCount = 0; 	// No zombies at start of game.
		zombieKillCount = 0; 	// No zombies killed at the start of the game.
		maxRoundZombieCount = calcZombieCount();
		currentRound = 1; 		// Game starts at round 1.
		UIManager.round = currentRound; // Initialize UI round text.
/**/	Debug.Log("Current Round: " + currentRound);

		roundSound = GetComponent<AudioSource>();

		// Spawn zombies every 1.5 seconds.
		// This function also does all of the checks to determine rounds
		// and when to spawn zombies.
		spawn = true;
		InvokeRepeating("Spawn", spawnTime, spawnTime);

	}

	void FixedUpdate()
	{
		// All checks for the game's round and zombie spawning.
		if(zombieKillCount == maxRoundZombieCount) // Player has completed the round
		{
			// Update round count.
			currentRound++;
			UIManager.round = currentRound; // Update UI.
			roundSound.Play(); // Alert player by audio.
/**/		Debug.Log("Current Round: " + currentRound);
			// Reset zombie kill count for round.
			zombieKillCount = 0;
			// Game gets more difficult after the first round.
			if(currentRound != 1)
				difficultyMult += 0.2f; // Add 0.2 to difficulty each round.
			// Determine new maxRoundZombieCount.
			maxRoundZombieCount = calcZombieCount();
			// Begin spawning for new round after a 3 second break.
			spawn = true;
		}

		if(zombieSpawnCount == maxRoundZombieCount) // Limit of spawning zombies reached.
		{
			// Stop spawning.
			spawn = false;
			// Reset zombie spawn count.
			zombieSpawnCount = 0;
		}
	}
	
	// Update is called once per frame
	void Spawn() 
	{
		if(!spawn) return; // Exit if we shouldn't spawn.

		// Get a random spawn point.
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		Instantiate(zombie, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		zombieSpawnCount++;
/**/	Debug.Log("Spawn Count: " + zombieSpawnCount);
	}

	/**
	 * Calculate the next round's maximum amount of zombies.
	 * Random value between 6 - 12, then added by foor(rand * difficulty).
	 * returns the int value of the maximum zombie count.
	 */
	int calcZombieCount()
	{
		// Generate random number from 6 - 12.
		int randCount = Random.Range(6, 13);
		int zombieCount = randCount + Mathf.FloorToInt(randCount * difficultyMult);
/**/	Debug.Log("New Max Count: " + zombieCount);
		return zombieCount;
	}

	// Update the kill count of zombies.
	public void zombieKilled()
	{
		zombieKillCount++;
/**/	Debug.Log("Kill Count For Round: " + zombieKillCount);
	}
	
}
