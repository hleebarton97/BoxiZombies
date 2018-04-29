using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

	// Public variables
	public static int round;
	public static int health;
	public static int ammo;
	public string pistol = "PISTOL";
	public string uzi = "UZI";
	public string shotgun = "SHOTGUN";
	public static string weapon;

	// Public variables
	public TextMeshProUGUI roundText;	// Reference to round text.
	public TextMeshProUGUI healthText; 	// Reference to health text.
	public TextMeshProUGUI ammoText; 	// Reference to ammo text.
	public TextMeshProUGUI weaponText; 	// Reference to ammo text.

	/*// Use this for initialization*/
//	void Awake() 
//	{
//	}
//	
	// Update is called once per frame
	void Update() 
	{
		roundText.text = "ROUND:   " + round; // Update round text.
		healthText.text = "" + health; // Easily convert int to string.
		DisplayAmmo();	// Updates ammo count
		weaponText.text = "" + weapon;	// Updates equipped weapon.

	}
		
	private void DisplayAmmo(){
		if(weapon == pistol)
			ammoText.text = "UNL";	// Updates ammo count
		else
			ammoText.text = "" + ammo;	// Updates ammo count
	}
		
}
