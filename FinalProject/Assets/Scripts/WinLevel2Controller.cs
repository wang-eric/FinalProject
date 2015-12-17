/* WinLevel2Controller.cs
 * Created by: Eric Wang
 * Date Created: December 1st, 2015
 * Date Modified: December 9th, 2015
 * Description: This script is used to control all behaviour in level 2 winning scene.
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLevel2Controller : MonoBehaviour {

	public Text scoreLabel;

	// Use this for initialization
	void Start () {
		this._SetScore ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void _SetScore() {
		this.scoreLabel.text = "Score: " + PlayerPrefs.GetInt("currentGameScore");
	}

	// Restart Button Event Handler
	public void OnRestartButtonClick() {
		Application.LoadLevel ("Level3");
	}
}
