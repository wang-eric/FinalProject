using UnityEngine;
using System.Collections;

public class Coin_l2 : MonoBehaviour {
	public int score;
	private GameController gameController;

	private CircleCollider2D[] colliders;
	private CircleCollider2D inner;
	private CircleCollider2D outter;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		this.colliders = gameObject.GetComponents<CircleCollider2D> ();
		this.inner = this.colliders [0];
		this.outter = this.colliders [1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Destroy the coin if hit by player.
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			gameController.AddScore (score);

			gameObject.GetComponent<Renderer> ().enabled = false;
			this.inner.enabled = false;
			this.outter.enabled = false;
			GetComponent<AudioSource> ().Play ();
			// Delay 1f for the destroy action to ensure the audio is played
			Destroy (gameObject, 1f);
		}
	}
	void OnTriggerStay2D (Collider2D other){
		if (other.gameObject.CompareTag ("Platform")){
			Destroy (gameObject,2f);
		}
	}
}
