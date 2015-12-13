using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {
	private bool isPaused = false;
	public GameObject pauseMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("escape") && !isPaused) {
			Time.timeScale = 0;
			pauseMenu.SetActive(true);
			isPaused = true;
		} else if (Input.GetKeyDown ("escape") && isPaused) {
			pauseMenu.SetActive(false);
			Time.timeScale = 1;
			isPaused = false;
		}
	}
	public void OnResumeButtonClick() {
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
		isPaused = false;
	}
	public void OnMenuButtonClick() {
		Time.timeScale = 1;
		isPaused = false;
		Application.LoadLevel ("Start");
	}
}
