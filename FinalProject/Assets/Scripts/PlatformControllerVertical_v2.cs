/* PlatformControllerVertical_v2.cs
 * Created by: Eric Wang
 * Date Created: November 1st, 2015
 * Date Modified: December 11th, 2015
 * Description: This script is used to control vertical moving platforms in the game.
 */

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
		// Change the moving direction after the assigned timing value
		if (timer > timimg) {
			timer = 0;
			speed = -speed;
		}
		rb2d.velocity = new Vector2 (rb2d.velocity.x, speed);
		/* Early version of the code (does not work with pause function)
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
