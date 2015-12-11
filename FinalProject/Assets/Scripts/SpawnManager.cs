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

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
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
				yield return new WaitForSeconds (spawnWait);
			}
		}
	}
}
