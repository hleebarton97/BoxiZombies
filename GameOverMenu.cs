using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour {

	public TextMeshProUGUI roundText;

	// Use this for initialization
	void Start () 
	{
		roundText.text = "" + UIManager.round; // Set rounds survived at start of scene.
	}

}
