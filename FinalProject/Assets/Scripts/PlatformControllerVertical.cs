/* PlatformControllerVertical.cs
 * Created by: Eric Wang
 * Date Created: November 1st, 2015
 * Date Modified: December 11th, 2015
 * Description: This script is used to control vertical moving platforms in the game. 
 * NOTE****: This control does not work with the pause function and is replaced with PlatformControllerVertical_v2.cs
 */

using UnityEngine;
using System.Collections;

public class PlatformControllerVertical : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed = 0.1f;
	public bool moveUp = true;

	// PRIVATE INSTANCE VARIABLES
	private Transform _transform;
	
	public int moveRange = 20;
	private int counter = 0;
	// Use this for initialization
	void Start () {
		this._transform = gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (counter == moveRange){
			moveUp = !moveUp;
			counter = 0;
		}

		if (moveUp) {
			this._transform.position += new Vector3(0,speed,0);
		} else {
			this._transform.position -= new Vector3(0,speed,0);
		}
		counter += 1;
	}
}
