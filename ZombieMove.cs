using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMove : MonoBehaviour {

    private Transform player;                       // Player transform component.
    private NavMeshAgent navigation; // Zombie's nav mesh agent.

    void Awake()
    {
        // Find the player GameObject and get it's transform component.
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the nav mesh agent of the zombie.
        navigation = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Follow the player.
        if(navigation.isActiveAndEnabled) // If navmeshagent exists
            navigation.SetDestination(player.position);
	}
}
