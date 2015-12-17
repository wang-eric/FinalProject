/* EnemyController.cs
 * Created by: Eric Wang
 * Date Created: November 1st, 2015
 * Date Modified: December 5th, 2015
 * Description: This script is used to control the zombies in level 1.
 */

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed = 0.5f;
	public bool moveRight;

	// PRIVATE INSTANCE VARIABLES
	private Rigidbody2D _rigidbody2D;
	private Transform _transform;

	public Transform frontCheck;
	public float checkRadius = 0.1f;
	public LayerMask whatIsWall;
	private bool hittingWall;

	public Transform edgeCheck;
	private bool notAtEdge;

	public LayerMask whatIsPlayer;

	public Transform topCheck1;
	public Transform topCheck2;
	public Transform topCheck3;
	public Transform backCheck1;
	private bool hittingPlayerFront,hittingPlayerBack;
	private bool hittingPlayerTop;
	private bool enemyKilled = false;
	private AudioSource[] _audioSources;
	private AudioSource _zombieKillSound;
	private AudioSource _zombieBiteSound;

	private GameController gameController;
	
	// Use this for initialization
	void Start () {
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._zombieKillSound = this._audioSources[0];
		this._zombieBiteSound = this._audioSources[1];
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		this._rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		this._transform = gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		// check if enemy is grounded
		if (!enemyKilled) {
			// Check for the wall
			hittingWall = Physics2D.OverlapCircle (frontCheck.position, checkRadius, whatIsWall);
			// Check if the player touches the zombie from the front
			hittingPlayerFront = Physics2D.OverlapCircle (frontCheck.position, checkRadius, whatIsPlayer);
			// Check if the player touches the zombie from the back
			hittingPlayerBack = Physics2D.OverlapCircle (backCheck1.position, checkRadius, whatIsPlayer);
			// Check if the player steps on the zombie's head
			hittingPlayerTop = (Physics2D.OverlapCircle (topCheck1.position, checkRadius, whatIsPlayer))
				|| (Physics2D.OverlapCircle (topCheck2.position, checkRadius, whatIsPlayer))
				|| (Physics2D.OverlapCircle (topCheck3.position, checkRadius, whatIsPlayer));

			// Check for the edge
			notAtEdge = Physics2D.OverlapCircle (edgeCheck.position, checkRadius, whatIsWall);

			// Move to one direct if the zombie is not hitting a wall or near the edge
			if (hittingWall || !notAtEdge) {
				moveRight = !moveRight;
			}


			// Kill zombies when the player jump on their heads
			if (hittingPlayerTop) {
				_zombieKillSound.Play ();
				//this.gameObject.GetComponent<Renderer> ().enabled = false;
				//gameObject.GetComponent<CircleCollider2D> ().enabled = false;
				gameObject.GetComponent<BoxCollider2D> ().enabled = false;
				gameObject.GetComponent<CircleCollider2D> ().enabled = false;
				gameController.AddScore(50);
				Destroy (gameObject, 1f);
				hittingPlayerTop=false;
				enemyKilled = true;
				//Destroy (this.gameObject);

			} else if (hittingPlayerFront || hittingPlayerBack){ // Player get killed when hit by a zombie
				_zombieBiteSound.Play ();
				gameController.TakeDamage();

			}
		}

		// Turn the zombie when it moves to a different direction
		if (moveRight) {
			this._transform.localScale = new Vector3(-2.8f, 2.8f, 1f); 
			this._rigidbody2D.velocity = new Vector2 (speed, this._rigidbody2D.velocity.y);
		} else {
			this._transform.localScale = new Vector3(2.8f, 2.8f, 1f); 
			this._rigidbody2D.velocity = new Vector2 (-speed, this._rigidbody2D.velocity.y);
		}
	}

}
