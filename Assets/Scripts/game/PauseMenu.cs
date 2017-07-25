﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using UnityEngine.SceneManagement;

public class PauseMenu : SoftwareBehaviour {
	
	public GameObject resumeButton, quitButton, pauseMenuCanvas, startMenuCanvas;
	public GameObject debugText, timerText;

	private bool gamePaused = false;
	private bool GamePaused {
		set {
			gamePaused = value;
			SoftwareModel.GameRunning = !gamePaused && GameHasStarted;
		}
	}
/*	public bool GameIsPaused {
		get { return gamePaused && gameHasStarted; }
	}*/
	private bool gameHasStarted = false;
	private bool startedBefore = false;
	public bool GameHasStarted {
		get { return gameHasStarted; }
		set { 
			if (!startedBefore) {
				gameHasStarted = true;
				startedBefore = value;
			}
		}
	}

	void Start() {

		TogglePause (false);
	}


	void Update(){
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (gamePaused) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}

	public void StartGame() {

		SoftwareModel.netwRout.TCPRequest(
			NetworkRoutines.EmptyCallback,
			new string[] { "req", "sessionId" },
			new string[] { "startTheGame", UserStatics.SessionId.ToString() });
	}
		
	public void Pause() {
		if (gameHasStarted) {
			SoftwareModel.netwRout.TCPRequest (
				NetworkRoutines.EmptyCallback,
				new string[] { "req", "sessionId" },
				new string[] { "pauseSession", UserStatics.SessionId.ToString () });
			TogglePause (true);
		}
	}

	public void Resume() {
		if (gameHasStarted) {
			SoftwareModel.netwRout.TCPRequest (
				NetworkRoutines.EmptyCallback,
				new string[] { "req", "sessionId" },
				new string[] { "resumeSession", UserStatics.SessionId.ToString () });
			TogglePause (false);
		}
	}

	public void End(int time) {

		if (gameHasStarted) {
			resumeButton.SetActive (false);
			if (time <= 0) {
			
			}
			Pause ();
		}
	}

	public void Quit() {

		SoftwareModel.netwRout.TCPRequest(
			StartMainMenu,
			new string[] { "req", "userId" },
			new string[] { "leaveSession", UserStatics.IdSelf.ToString() });
	}

	private void StartMainMenu(string[][] response){
		Time.timeScale = 1f;
		SceneManager.LoadScene ("Menu");
	}

	public void TogglePause(bool stop) {

		pauseMenuCanvas.SetActive(stop);
		startMenuCanvas.SetActive (false);
		GamePaused = stop;

		Time.timeScale = (stop ? 0f : 1f);
	}

	public void ShowStartingMenu() {

		startMenuCanvas.SetActive (true);
		pauseMenuCanvas.SetActive (false);
		Time.timeScale = 0f;
	}

	// TODO dev. helper
	public void ShowState(string state) {

		debugText.GetComponent<Text> ().text = "Game state: " + state;
	}
}
