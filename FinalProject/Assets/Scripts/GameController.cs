/* GameController.cs
 * Created by: Eric Wang
 * Date Created: November 1st, 2015
 * Date Modified: December 17th, 2015
 * Description: This script is used to manage scores, life, timer, and spawn points. It also controls scene loading.
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

	
	public Text scoreText;
	public Text lifeText;
	public Text timerText;

	private bool win;
	private int score;
	private int life;
	public float time;

	private GameObject player;
	private GameObject dragon;

	private Transform player_trans;
	private Transform dragon_trans;

	private Vector3 spawnPoint;
	
	void Start ()
	{
		// Load player infor from PlayerPrefs
		score = PlayerPrefs.GetInt("currentGameScore");
		life = PlayerPrefs.GetInt("currentLife");
		spawnPoint = new Vector3 (0.1f,-1.71f,0f);
		// Update the player info to UI.
		UpdateScore ();
		UpdateLife();
		UpdateTime ();

		GameObject player = GameObject.FindWithTag ("Player");
		GameObject dragon = GameObject.FindWithTag ("Dragon");
		player_trans = player.GetComponent<Transform> ();
		if (dragon != null) {
			dragon_trans = dragon.GetComponent<Transform> ();
		}
	}

	void Update(){
		// Update timer value every frame.
		UpdateTime ();
	}

	// Add input value to the player score
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	// Update the score on UI, and save it to PlayerPrefs
	void UpdateScore()
	{
		scoreText.text = score.ToString("0");
		// currentGameScore is declared and can be passed across scenes
		PlayerPrefs.SetInt("currentGameScore",score);
		PlayerPrefs.Save();
	}

	// Return remaining life as an integer
	public int GetLife()
	{
		return life;
	}

	// Return remaining time as an float
	public float GetTime()
	{
		return time;
	}

	// Return the lastest spawn point (check point)
	public Vector3 GetSpawnPoint()
	{
		return spawnPoint;
	}

	// Set the spawn point to the input vector
	public void SetSpawnPoint(Vector3 newSpawnPoint)
	{
		spawnPoint = newSpawnPoint;
	}

	// Respawn the player (and the dragon if in level 3) to the lastest check point
	public void Respawn(){
		player_trans.position = GetSpawnPoint ();
		if (dragon_trans != null) {
			// The dragon will respawn 10 units behind the player.
			dragon_trans.position = new Vector3 (GetSpawnPoint ().x - 10f, dragon_trans.position.y, dragon_trans.position.z);
		}
	}

	// The function will be called whenever the player takes any damage.
	public void TakeDamage(){
		life -= 1;
		UpdateLife();
		// Game over when the life goes to 0.
		if (life == 0) {
			GameOver();
		}
		else{
			Respawn();
		}
	}

	// Update the timer on UI
	void UpdateTime()
	{
		// Timer value goes down untill timer value goes to 0.
		if (time <= 0) {
			time = 0;
		} else {
			time -= Time.deltaTime;
		}
		timerText.text = time.ToString("0");
	}

	// Update the remmaining lives on UI
	void UpdateLife()
	{
		lifeText.text =life.ToString("0");
		PlayerPrefs.SetInt("currentLife",life);
		PlayerPrefs.Save();
	}

	//Load Lose scene
	public void GameOver()
	{
		Application.LoadLevel ("Lose");
	}

	// Load Win scene, the input value is used to determine the current winning level.
	public void Win(int level)
	{
		// The score is calculated differently depends on the level.
		if (level == 1) {
			PlayerPrefs.SetInt("currentGameScore",score+life*100+(int)time*3);
			PlayerPrefs.Save();
			Application.LoadLevel ("Win1");
		} else if (level == 2) {
			PlayerPrefs.SetInt("currentGameScore",score+life*125);
			PlayerPrefs.Save();
			Application.LoadLevel ("Win2");
		} else if (level == 3) {
			PlayerPrefs.SetInt("currentGameScore",score+life*150+(int)time*10);
			PlayerPrefs.Save();
			Application.LoadLevel ("Win3");
		}
	}
}
