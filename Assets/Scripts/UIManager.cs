using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

	// Public variables
	public static int round;
	public static int health;

	// Public variables
	public TextMeshProUGUI roundText;	// Reference to round text.
	public TextMeshProUGUI healthText; 	// Reference to health text.

	/*// Use this for initialization
	void Awake() 
	{
	}*/
	
	// Update is called once per frame
	void Update() 
	{
		roundText.text = "ROUND:   " + round; // Update round text.
		healthText.text = "" + health; // Easily convert int to string.
	}
}
