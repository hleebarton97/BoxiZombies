using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	/**
	 * When the user clicks "Play", start the game at
	 * the appropriate scene.
	 */
	public void PlayGame()
	{
		SceneManager.LoadScene("Bridge");
	}

	/**
	 * When user clicks "Quit", close the application.
	 */
	public void QuitGame()
	{
		Application.Quit();
	}

	/**
	 * When the user clicks "Quit" at the game over scene,
	 * take the user back to the start scene.
	 */
	public void QuitToMenu()
	{
		SceneManager.LoadScene("Start");
	}
}
