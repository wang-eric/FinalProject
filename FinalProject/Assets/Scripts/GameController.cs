using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

	
	public Text scoreText;
	public Text lifeText;
	public Text timerText;

	public bool level1;

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
		if (level1) {
			resetScore();
		}
		score = PlayerPrefs.GetInt("currentGameScore");
		life = PlayerPrefs.GetInt("currentLife");
		spawnPoint = new Vector3 (0.1f,-1.71f,0f);
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
		UpdateTime ();
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore()
	{
		scoreText.text = score.ToString("0");
		// currentGameScore is declared and can be passed across scenes
		PlayerPrefs.SetInt("currentGameScore",score);
		PlayerPrefs.Save();
	}
	
	public int GetLife()
	{
		return life;
	}

	public float GetTime()
	{
		return time;
	}


	public Vector3 GetSpawnPoint()
	{
		return spawnPoint;
	}

	public void SetSpawnPoint(Vector3 newSpawnPoint)
	{
		spawnPoint = newSpawnPoint;
	}

	public void Respawn(){
		player_trans.position = GetSpawnPoint ();
		if (dragon_trans != null) {
			dragon_trans.position = new Vector3 (GetSpawnPoint ().x - 10f, dragon_trans.position.y, dragon_trans.position.z);
		}
	}
	public void TakeDamage(){
		life -= 1;
		UpdateLife();
		if (life == 0) {
			GameOver();
		}
		else{
			Respawn();
		}
	}

	void UpdateTime()
	{
		if (time <= 0) {
			time = 0;
		} else {
			time -= Time.deltaTime;
		}
		timerText.text = time.ToString("0");
	}

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

	// Load Win scene
	public void Win(int level)
	{
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
	public void resetScore()
	{
		PlayerPrefs.SetInt("currentGameScore",0);
		PlayerPrefs.SetInt("currentLife",5);
		PlayerPrefs.Save();
	}
}
