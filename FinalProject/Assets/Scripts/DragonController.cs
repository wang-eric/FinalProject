using UnityEngine;
using System.Collections;

public class DragonController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed = 0.01f;
	public bool fire = false;
	public float Range = 300;

	public GameObject fireball;
	public Transform fireSpawn;

	private Transform _playerTransform;
	private SimplePlatformController _playerController;
	private Animator anim;
	// PRIVATE INSTANCE VARIABLES
	private Transform _transform;
	private AudioSource[] _audioSources;
	private AudioSource _fireSound;
	private AudioSource _biteSound;
	private AudioSource _roarSound;
	private GameController gameController;
	private int counter = 0;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		this._transform = gameObject.GetComponent<Transform> ();
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._fireSound = this._audioSources[0];
		this._biteSound = this._audioSources[1];
		this._roarSound = this._audioSources[2];
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
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("Player")){
			this._biteSound.Play();
			gameController.TakeDamage();
		}
	}
}
