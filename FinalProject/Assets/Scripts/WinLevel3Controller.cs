/* WinLevel3Controller.cs
 * Created by: Eric Wang
 * Date Created: December 1st, 2015
 * Date Modified: December 9th, 2015
 * Description: This script is used to control all behaviour in level 3 winning scene.
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLevel3Controller : MonoBehaviour {

	public Text scoreLabel;

	// Use this for initialization
	void Start () {
		this._SetScore ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	//Display the finalscore
	private void _SetScore() {
		this.scoreLabel.text = "Score: " + PlayerPrefs.GetInt("currentGameScore");
	}

	// Return to Menu Button Event Handler
	public void OnRestartButtonClick() {
		Application.LoadLevel ("Start");
	}
}
