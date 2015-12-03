using UnityEngine;
using System.Collections;

public class SpawnPointController : MonoBehaviour {
	public float x;
	public float y;
	public float z;
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
			gameController.SetSpawnPoint( new Vector3 (x,y,z));
		}
	}
}
