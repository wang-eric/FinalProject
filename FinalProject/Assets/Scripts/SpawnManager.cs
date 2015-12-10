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

	// Use this for initialization
	void Start () {
		_trans = transform;
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i<hazardCount; i++) {
				rnd = Random.Range(1,4);
				Vector3 spawnPosition = _trans.position + new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				if (rnd == 1){
					item = coin;
				} else if(rnd == 2){
					item = diamond;
				} else if(rnd == 3){
					item = fireball;
				}
				Instantiate (item, spawnPosition, _trans.rotation);
				yield return new WaitForSeconds (spawnWait);
			}

		}
	}
}
