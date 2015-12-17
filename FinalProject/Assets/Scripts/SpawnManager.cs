/* SpawnManager.cs
 * Created by: Eric Wang
 * Date Created: December 8th, 2015
 * Date Modified: December 11th, 2015
 * Description: This script is used to manage all the dropping objects in level 2
 */

using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public GameObject coin;
	public GameObject diamond;
	public GameObject fireball;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	private GameObject item;
	private Transform _trans;
	private int rnd;
	private float time;

	private GameController gameController;
	// Use this for initialization
	void Start () {
		_trans = transform;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		StartCoroutine (SpawnWaves ());
	}

	// Create waves of dropping objects
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i<hazardCount; i++) {
				// Randomly generate a integer between 1 to 4.
				// If the number is 1 or 2, spawn a coin
				// If the number is 3, spawn a diamond
				// if the number is 4, spawn a firball
				rnd = Random.Range(1,5);
				Vector3 spawnPosition = _trans.position + new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				if (rnd == 1 || rnd == 2){
					item = coin;
				} else if(rnd == 3){
					item = diamond;
				} else if(rnd == 4){
					item = fireball;
				}
				// End the level when the timer goes to 0.
				if (gameController.GetTime () <= 0) {
					gameController.Win (2);
				}
				Instantiate (item, spawnPosition, _trans.rotation);
				yield return new WaitForSeconds (spawnWait);
			}
		}
	}
}
