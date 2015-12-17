/* MenuController.cs
 * Created by: Eric Wang
 * Date Created: November 1st, 2015
 * Date Modified: December 15th, 2015
 * Description: This script is used to manage all functions for the menu screen.
 */

using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	private string[] cheatCode_life, cheatCode_level;
	private int index_life, index_level;

	// Cheats are turned off by default
	private bool cheatLifeEnabled = false;
	private bool cheatLevelEnabled = false;

	// The audio clips to indicate the cheat code activation
	private bool audioPlayed_life = false;
	private bool audioPlayed_level = false;

	// The default starting level is set to level 1
	private string level="Level1";

	private AudioSource[] _audioSources;
	private AudioSource _cheatLifeEnabledSound;
	private AudioSource _cheatLevelEnabledSound;
	private AudioSource _levelSelectSound;

	// The level selection panel
	public GameObject gary;

	// The StartScreen UI elements
	public GameObject startScreen;

	// The controls panel
	public GameObject controlsScreen;

	// Use this for initialization
	void Start () {
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._cheatLifeEnabledSound = this._audioSources[1];
		this._cheatLevelEnabledSound = this._audioSources[2];
		this._levelSelectSound = this._audioSources[3];

		// Code for 30 lives is "crabby patty", user needs to input this in the right order
		cheatCode_life = new string[] { "c", "r", "a", "b", "b", "y", "space", "p", "a", "t", "t", "y"}; 
		index_life = 0; 
		// Code for level selection is "gary", user needs to input this in the right order
		cheatCode_level = new string[] { "g", "a", "r", "y"};
		index_level = 0;
	}
	
	// Update is called once per frame
	void Update () {

		// Check if any key is pressed
		if (!cheatLifeEnabled) {
			if (Input.anyKeyDown) {
				Debug.Log (cheatCode_life [index_life]);
				// Check if the next key in the code is pressed
				if (Input.GetKeyDown (cheatCode_life [index_life])) {
					// Add 1 to index to check the next key in the code
					index_life++;
				}
				// Wrong key entered, we reset code typing
				else {
					index_life = 0;    
				}

			}
			
			// If index reaches the length of the cheatCode string, 
			// the entire code was correctly entered
			if (index_life == cheatCode_life.Length) {
				// Cheat code successfully inputted!
				// Unlock crazy cheat code stuff
				if (!audioPlayed_life) {
					_cheatLifeEnabledSound.Play ();
					audioPlayed_life = true;
				}
				cheatLifeEnabled = true;
			}
		}
		// Check if any key is pressed
		if (!cheatLevelEnabled) {
			if (Input.anyKeyDown) {
				Debug.Log (cheatCode_level [index_level]);
				// Check if the next key in the code is pressed
				if (Input.GetKeyDown (cheatCode_level [index_level])) {
					// Add 1 to index to check the next key in the code
					index_level++;
				}
				// Wrong key entered, we reset code typing
				else {
					index_level = 0;    
				}
				
			}
			
			// If index reaches the length of the cheatCode string, 
			// the entire code was correctly entered
			if (index_level == cheatCode_level.Length) {
				// Cheat code successfully inputted!
				// Unlock crazy cheat code stuff
				if (!audioPlayed_level) {
					_cheatLevelEnabledSound.Play ();
					audioPlayed_level = true;
				}
				cheatLevelEnabled = true;
				// Make the level selection panel visible
				gary.SetActive(true);
			}
		}

	}

	// Initialize all the player's info.
	public void Initialize(bool cheat)
	{
		PlayerPrefs.SetInt("currentGameScore",0);
		// Check if the life cheat is enabled, and initialize the total life accordingly
		if (cheat) {
			PlayerPrefs.SetInt ("currentLife", 30);
		} else {
			PlayerPrefs.SetInt ("currentLife", 5);
		}
		PlayerPrefs.Save();
	}
	//PUBLIC METHODS ++++++++++++++++++++++++++++

	// Start Button Event Handler
	public void OnStartButtonClick() {
		Initialize (cheatLifeEnabled);
		Application.LoadLevel (level);
	}

	// Controls Button Event Handler
	public void OnControlsButtonClick() {
		controlsScreen.SetActive(true);
		startScreen.SetActive(false);
		cheatLevelEnabled = true;
		gary.SetActive(false);
	}

	// Beck to Menu Button Event Handler
	public void OnBackToMenuButtonClick() {
		controlsScreen.SetActive(false);
		startScreen.SetActive(true);

		if (index_level == cheatCode_level.Length) {
			gary.SetActive (true);
		} else {
			cheatLevelEnabled = false;
			index_level = 0;
			gary.SetActive (false);
		}
	}

	// Quit Button Event Handler
	public void OnQuitButtonClick() {
		Application.Quit();
	}

	// Level 1 Button on Level Selection Panel Event Handler
	public void OnLevel1ButtonClick() {
		_levelSelectSound.Play ();
		level = "Level1";
	}
	// Level 2 Button on Level Selection Panel Event Handler
	public void OnLevel2ButtonClick() {
		_levelSelectSound.Play ();
		level = "Level2";
	}
	// Level 3 Button on Level Selection Panel Event Handler
	public void OnLevel3ButtonClick() {
		_levelSelectSound.Play ();
		level = "Level3";
	}
	// Close Button on Level Selection Panel Event Handler
	public void OnCloseButtonClick() {
		audioPlayed_level = false;
		index_level = 0;  
		cheatLevelEnabled = false; 
		gary.SetActive(false);
		//Update ();
	}

}