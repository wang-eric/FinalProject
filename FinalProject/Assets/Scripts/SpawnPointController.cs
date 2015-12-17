/* SpawnPointController.cs
 * Created by: Eric Wang
 * Date Created: November 25st, 2015
 * Date Modified: December 6th, 2015
 * Description: This script is used to control the collider of the check point.
 */

using UnityEngine;
using System.Collections;

public class SpawnPointController : MonoBehaviour {
	public GameObject spawnposition;
	private GameController gameController;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		else if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Update the spawnpoint when player walks through the spawnpoint checker
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("Player")){
			gameController.SetSpawnPoint(spawnposition.transform.position);
		}
	}
}
