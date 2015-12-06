using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {
	public float speed = 1;
	//public GameObject impactEffect;
	//public PlayerController player;
	public float scale = 0.01f;
	public float rotationSpeed = -360;
	private Rigidbody2D rb2d;
	private Transform trans;
	// Use this for initialization
	void Start () {
		//player = FindObjectOfType<PlayerController>();
		rb2d = GetComponent<Rigidbody2D> ();
		trans = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (trans.localScale.x < 1) {
			scale+=0.05f;
		}
		trans.localScale = (new Vector3 (scale, scale, 0));
		rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
		rb2d.angularVelocity = rotationSpeed;

	}
}
