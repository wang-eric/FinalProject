using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	private string[] cheatCode;
	private int index;
	private bool cheatEnabled = false;
	private bool audioPlayed = false;

	private AudioSource[] _audioSources;
	private AudioSource _cheatEnabledSound;
	// Use this for initialization
	void Start () {
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._cheatEnabledSound = this._audioSources[1];
		// Code is "idkfa", user needs to input this in the right order
		cheatCode = new string[] { "c", "r", "a", "b", "b", "y", "space", "p", "a", "t", "t", "y"}; 
		index = 0;    
	}
	
	// Update is called once per frame
	void Update () {
		// Check if any key is pressed
		if (!cheatEnabled) {
			if (Input.anyKeyDown) {
				Debug.Log (cheatCode [index]);
				// Check if the next key in the code is pressed
				if (Input.GetKeyDown (cheatCode [index])) {
					// Add 1 to index to check the next key in the code
					index++;
				}
				// Wrong key entered, we reset code typing
				else {
					index = 0;    
				}

			}
			
			// If index reaches the length of the cheatCode string, 
			// the entire code was correctly entered
			if (index == cheatCode.Length) {
				// Cheat code successfully inputted!
				// Unlock crazy cheat code stuff
				if (!audioPlayed) {
					_cheatEnabledSound.Play ();
					audioPlayed = true;
				}
				cheatEnabled = true;
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
		Initialize (cheatEnabled);
		Application.LoadLevel ("Level1");
	}

	// Instruction Button Event Handler
	public void OnInstructionButtonClick() {
		Application.LoadLevel ("Instruction");
	}
}