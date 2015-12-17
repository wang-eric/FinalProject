/* ExitPointController.cs
 * Created by: Eric Wang
 * Date Created: November 1st, 2015
 * Date Modified: December 17th, 2015
 * Description: This script is used to level exit.
 */
using UnityEngine;
using System.Collections;

public class ExitPointController : MonoBehaviour {
	private GameController gameController;
	public int currentLevel;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Call Win() when the player reaches the exit.
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("Player")){
			gameController.Win(currentLevel);
		}
	}
}
