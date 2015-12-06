using UnityEngine;
using System.Collections;

public class DragonController : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed = 0.01f;
	public bool fire = false;
	private Transform _playerTransform;
	private SimplePlatformController _playerController;
	private Animator anim;
	// PRIVATE INSTANCE VARIABLES
	private Transform _transform;
	
	public int Range = 20;
	private int counter = 0;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		this._transform = gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (counter == Range){
			this.anim.SetBool ("fire", true);
			speed=0;
			counter = 0;
		} else {
			this.anim.SetBool ("fire", false);
			speed = 0.01f;
		}
		this._transform.position += new Vector3(speed,0,0);
		counter += 1;
	}
}
