using UnityEngine;
using System.Collections;

public class DragonController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	private float speed = 4f;
	public bool fire = false;
	public int Range = 300;

	public GameObject fireball;
	public Transform fireSpawn;

	private Transform _playerTransform;
	private Rigidbody2D rb2d;
	private SimplePlatformController _playerController;
	private Animator anim;

	// PRIVATE INSTANCE VARIABLES
	private Transform _transform;
	private AudioSource[] _audioSources;
	private AudioSource _fireSound;
	private AudioSource _biteSound;
	private AudioSource _roarSound;
	private GameController gameController;

	private float actionTimer1 = 0.0f;
	private float actionTimer2 = 0.0f;
	private float actionTimer3 = 0.0f;
	private float timing1 = 3f;
	private float timing2 = 0.3f;
	private float timing3 = 0.8f;
	private bool timing1trigger = true;
	private bool timing2trigger = false;
	private bool timing3trigger = false;

	private int counter = 0;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		//this._transform = gameObject.GetComponent<Transform> ();
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._fireSound = this._audioSources[0];
		this._biteSound = this._audioSources[1];
		this._roarSound = this._audioSources[2];
		rb2d = GetComponent<Rigidbody2D> ();
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
	}
	
	// Update is called once per frame
	void Update () {

		if (timing1trigger) {
			actionTimer1 += Time.deltaTime;
		}
		if (timing2trigger) {
			actionTimer2 += Time.deltaTime;
		}
		if (timing3trigger) {
			actionTimer3 += Time.deltaTime;
		}
		if (actionTimer1 > timing1) {
			actionTimer1 = 0;
			timing1trigger = false;
			timing2trigger = true;
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			this.anim.SetBool ("fire", true);
			if (!_fireSound.isPlaying) {
				this._fireSound.Play ();
			}

		}
		if (actionTimer2 > timing2) {
			actionTimer2 = 0;
			timing2trigger = false;
			timing3trigger = true;
			Instantiate (fireball, fireSpawn.position, fireSpawn.rotation);
			Debug.Log ("Fire");
		}
		if (actionTimer3 > timing3) {
			actionTimer3 = 0;
			timing3trigger = false;
			timing1trigger = true;
			this.anim.SetBool ("fire", false);
			rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
		}
		/*
		if (actionTimer > timing3 || !timing3trigger) {
			timing3trigger = true;
			timing2trigger = false;
			rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
			actionTimer = 0;
		} else if (actionTimer > timing2 || !timing2trigger) {
			timing2trigger = true;
			timing1trigger = false;
			this.anim.SetBool ("fire", false);
			Instantiate (fireball, fireSpawn.position, fireSpawn.rotation);
			Debug.Log ("Fire");
		} else if (actionTimer > timing1 || !timing1trigger) {
			timing1trigger = true;
			timing3trigger = false;
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			this.anim.SetBool ("fire", true);
			if (!_fireSound.isPlaying) {
				this._fireSound.Play ();
			}
		} else {
			rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
		}
		Debug.Log (actionTimer);
		//StartCoroutine (DragonMovement ());
		/*
		if (counter >= Range && counter < Range+80) {
			this.anim.SetBool ("fire", true);
			if (!_fireSound.isPlaying){
				this._fireSound.Play ();
			}
			if (counter == Range+20){
				Instantiate (fireball, fireSpawn.position, fireSpawn.rotation);
				//GetComponet<AudioSource>.Play();
			}
		} else if (counter == Range + 80) {
			this.anim.SetBool ("fire", false);
			counter = 0;
			_roarSound.Play();
			this._transform.position += new Vector3(speed,0,0);
		} else {
			this._transform.position += new Vector3(speed,0,0);
		}
		counter += 1;
		*/
	}

	IEnumerator DragonMovement ()
	{
		//yield return new WaitForSeconds (startWait);
		while (true) {
			rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
			yield return new WaitForSeconds (3f);
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			this.anim.SetBool ("fire", true);
			this._fireSound.Play ();
			yield return new WaitForSeconds (0.5f);
			Instantiate (fireball, fireSpawn.position, fireSpawn.rotation);
			yield return new WaitForSeconds (5f);
			/*
			for (int i = 1; i<= (5); i++) {
				if (i == 3) {
					this.anim.SetBool ("fire", true);
					if (!_fireSound.isPlaying){
						this._fireSound.Play ();
					}
				}else if (i == 4){
						Instantiate (fireball, fireSpawn.position, fireSpawn.rotation);
				} else if (i == 5) {
					this.anim.SetBool ("fire", false);
					_roarSound.Play();
					rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
				} else {
					rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
				}
				Debug.Log (Range);
				Debug.Log (i);
				/*
				for (int i = 0; i<hazardCount; i++) {
					rnd = Random.Range(1,5);
					Vector3 spawnPosition = _trans.position + new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					if (rnd == 1 || rnd == 2){
						item = coin;
					} else if(rnd == 3){
						item = diamond;
					} else if(rnd == 4){
						item = fireball;
					}
					if (gameController.GetTime () <= 0) {
						gameController.Win (2);
					}
					Instantiate (item, spawnPosition, _trans.rotation);

				}
				yield return new WaitForSeconds (5f);
			}*/
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("Player")){
			this._biteSound.Play();
			gameController.TakeDamage();
		}
	}
}
