/* SimplePlatFormController.cs
 * Created by: Eric Wang
 * Date Created: November 1st, 2015
 * Date Modified: December 17th, 2015
 * Description: This script is used to control the player movement.
 */

using UnityEngine;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 700f;
	public Transform groundEnd;


	private bool grounded = false;
	private bool fallOff = false;
	private bool screamStart = false;
	private Animator anim;
	private Rigidbody2D rb2d;
	private AudioSource[] _audioSources;
	private AudioSource _jumpSound;
	private AudioSource _walkSound;
	private AudioSource _screamSound;


	private GameController gameController;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._jumpSound = this._audioSources [0];
		this._walkSound = this._audioSources [1];
		this._screamSound = this._audioSources [2];
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}

	// Update is called once per frame
	void Update () {
		Debug.DrawLine (this.transform.position, groundEnd.position, Color.green);
		// Check if the player is on the ground
		grounded = Physics2D.Linecast (this.transform.position, groundEnd.position, 1 << LayerMask.NameToLayer ("Ground"));
		// Check if the player is on the DeathTrigger
		fallOff = Physics2D.Linecast (this.transform.position, groundEnd.position, 1 << LayerMask.NameToLayer ("DeathTrigger"));
		// Check if the player is on the ScreamTrigger
		screamStart = Physics2D.Linecast (this.transform.position, groundEnd.position, 1 << LayerMask.NameToLayer ("ScreamTrigger"));

		// Only jump when the jump key is pressed, and the player is on the ground
		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
			grounded = false;
		}
		// Play scream sound when the player touches the scream trigger.
		if (screamStart && !this._screamSound.isPlaying) {
			this._screamSound.Play ();
		}
		// Character takes damage when the player touches the death trigger.
		if (fallOff) {
			gameController.TakeDamage ();
		}
	}

	void FixedUpdate()
	{
		// Player movement
		float h = Input.GetAxis ("Horizontal");
		if (h != 0) {
			// Play Walk animation
			anim.SetInteger ("AnimState", 1);
			if (grounded && !this._walkSound.isPlaying)
				this._walkSound.Play();
			else if (!grounded && this._walkSound.isPlaying)
				this._walkSound.Stop();
			if (h * rb2d.velocity.x < maxSpeed)
				rb2d.AddForce (Vector2.right * h * moveForce);
			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		} else{
			// Play Idle animation
			anim.SetInteger ("AnimState", 0);
			this._walkSound.Stop ();
		}
		// Flip the player sprite base on the facing direction
		if (h>0 && !facingRight)
			Flip ();
		else if (h<0 && facingRight)
			Flip ();
		// Player jump
		if (jump){
			// Play jump sound
			this._jumpSound.Play();
			// Apply jump force
			rb2d.AddForce (new Vector2(0f,jumpForce));
			jump = false;
			// Set grounded back to true to avoid double jump
			grounded = true;
			// Play jump animation
			anim.SetInteger ("AnimState", 2);
		}
	}

	// Flips the player sprite
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
