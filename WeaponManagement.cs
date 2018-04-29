using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : MonoBehaviour {

	private string pistol = "PISTOL";
	private string uzi = "UZI";
	private string shotgun= "SHOTGUN";
	private string currentWeapon = "PISTOL";

	public GameObject Pistol;
	public GameObject Uzi;
	public GameObject Shotgun;
	public GameObject Barrel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SwapWeaponModels ();
	}

	public void SwapWeaponModels(){
		if (Input.GetButtonDown ("Pistol")) {
			Pistol.SetActive (true);
			Uzi.SetActive(false);
			Shotgun.SetActive (false);

			if (currentWeapon == uzi) {
				Barrel.transform.Translate (0.00f, -0.06f, -0.30f);
			}
			if (currentWeapon == shotgun) {
				Barrel.transform.Translate (0.00f, 0.00f, -0.60f);
			}
			currentWeapon = pistol;
		}
		if (Input.GetButtonDown ("Uzi")) {
			Pistol.SetActive (false);
			Uzi.SetActive(true);
			Shotgun.SetActive (false);

			if (currentWeapon == pistol) {
				Barrel.transform.Translate(0.00f, 0.06f, 0.30f);
			}
			if (currentWeapon == shotgun) {
				Barrel.transform.Translate(0.00f, 0.06f, -0.30f);
			}
			currentWeapon = uzi;
		}
		if (Input.GetButtonDown ("Shotgun")) {
			Pistol.SetActive (false);
			Uzi.SetActive(false);
			Shotgun.SetActive (true);

			if(currentWeapon == pistol)
				Barrel.transform.Translate(0.00f, 0.00f, 0.60f);
			if(currentWeapon == uzi)
				Barrel.transform.Translate(0.00f, -0.06f, 0.30f);
			
			currentWeapon = shotgun;

		}
	}
}
