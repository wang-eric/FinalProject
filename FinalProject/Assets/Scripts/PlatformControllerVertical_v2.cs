using UnityEngine;
using System.Collections;

public class PlatformControllerVertical_v2 : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed = 1f;
	public float timer = 0f;
	public float timimg = 4f;

	// PRIVATE INSTANCE VARIABLES
	private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > timimg) {
			timer = 0;
			speed = -speed;
		}
		rb2d.velocity = new Vector2 (rb2d.velocity.x, speed);
		/*
		if (counter == moveRange){
			moveUp = !moveUp;
		}

		if (moveUp) {
			rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
			this._transform.position += new Vector3(0,speed,0);
		} else {
			rb2d.velocity = new Vector2 (-speed, rb2d.velocity.y);
			this._transform.position -= new Vector3(0,speed,0);
		}*/
	}
}
