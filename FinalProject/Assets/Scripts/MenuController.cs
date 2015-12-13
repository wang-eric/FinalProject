using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	private string[] cheatCode_life, cheatCode_level;
	private int index_life, index_level;
	private bool cheatLifeEnabled = false;
	private bool audioPlayed_life = false;
	private bool cheatLevelEnabled = false;
	private bool audioPlayed_level = false;
	private string level="Level1";
	private AudioSource[] _audioSources;
	private AudioSource _cheatLifeEnabledSound;
	private AudioSource _cheatLevelEnabledSound;
	private AudioSource _levelSelectSound;
	public GameObject gary;
	public GameObject btn1;
	public GameObject btn2;
	public GameObject btn3;
	// Use this for initialization
	void Start () {
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._cheatLifeEnabledSound = this._audioSources[1];
		this._cheatLevelEnabledSound = this._audioSources[2];
		this._levelSelectSound = this._audioSources[3];

		// Code is "idkfa", user needs to input this in the right order
		cheatCode_life = new string[] { "c", "r", "a", "b", "b", "y", "space", "p", "a", "t", "t", "y"}; 
		index_life = 0; 
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
				gary.SetActive(true);
			}
		}

	}
	public void Initialize(bool cheat)
	{
		PlayerPrefs.SetInt("currentGameScore",0);
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

	// Instruction Button Event Handler
	public void OnInstructionButtonClick() {
		Application.LoadLevel ("Instruction");
	}

	public void OnLevel1ButtonClick() {
		_levelSelectSound.Play ();
		level = "Level1";
	}
	public void OnLevel2ButtonClick() {
		_levelSelectSound.Play ();
		level = "Level2";
	}
	public void OnLevel3ButtonClick() {
		_levelSelectSound.Play ();
		level = "Level3";
	}
}