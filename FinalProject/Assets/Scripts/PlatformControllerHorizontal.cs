/* PlatformControllerHorizontal.cs
 * Created by: Eric Wang
 * Date Created: November 1st, 2015
 * Date Modified: December 11th, 2015
 * Description: This script is used to control horizontally moving platforms in the game. 
 * NOTE****: This control is not used in the game due to a known issue with the platform movement
 */

using UnityEngine;
using System.Collections;

public class PlatformControllerHorizontal : MonoBehaviour {
	// PUBLIC INSTANCE VARIABLES
	public float speed = 0.1f;
	public bool moveRight = true;
	private Transform _playerTransform;
	private SimplePlatformController _playerController;
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
			moveRight = !moveRight;
			counter = 0;
		}

		if (moveRight) {
			this._transform.position += new Vector3(speed,0,0);
		} else {
			this._transform.position -= new Vector3(speed,0,0);
		}
		counter += 1;
	}
}
