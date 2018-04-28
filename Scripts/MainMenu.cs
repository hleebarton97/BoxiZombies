using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

	/************************************************************
	* Purpose: When the user clicks "Play", start a random level
	************************************************************/
	public void PlayGame()
	{
		int i = Random.Range(0,2);//generate random nuber

		if(i.Equals(0))
		{
			SceneManager.LoadScene("Bridge");
		}
		else
		{
			SceneManager.LoadScene("Bridge2");
		}
	}

	/************************************************************
	* Purpose: 
	************************************************************/
	public void LevelSelect()
	{
		SceneManager.LoadScene("LevelSelect");
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
