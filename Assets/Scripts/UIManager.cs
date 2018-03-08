using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

	// Public variables
	public static int round;

	// Private variables
	private TextMeshProUGUI roundText;

	// Use this for initialization
	void Awake() 
	{
		roundText = GetComponentInChildren<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		roundText.text = "ROUND:   " + round;
	}
}
