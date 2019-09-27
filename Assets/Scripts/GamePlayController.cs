using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour {

	[SerializeField]
	private Text scoreText,gameOverScoreText,pausePanelScoreText,gameOverHighScoreText;
	
	[SerializeField]
	private GameObject gameOverPanel,pausePanel;
	
	public static bool isPaused,isGameOverSet;
	
	void Awake(){
		if(!PlayerPrefs.HasKey(GamePreferences.GAME_PLAYED_ATLEAST_ONCE)){
			//GAME IS PLAYED
			PlayerPrefs.SetInt(GamePreferences.GAME_PLAYED_ATLEAST_ONCE,1);
		}//YES
		
		
	}
	
	void Start(){
		isPaused = false;
		isGameOverSet = false;
		gameOverPanel.SetActive(false);
		pausePanel.SetActive(false);
	}//start
	
	void OnDisable(){
		Time.timeScale = 1f;
	}
	
	void FixedUpdate(){
		//IF BACK BUTTON IS PRESSED
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel(AllScenes.MAIN_MENU_SCENE);
		}//backbutton
		
		//IF PLAYER DED
		if(!Player.isAlive && !isGameOverSet){ //dead
			GameIsOver();
		}//gameover
		UpdateScoreUI();
	}//fixed Update
	
	public void PauseTheGame(){
		Time.timeScale = 0f;
		isPaused = true;
		pausePanelScoreText.text = "SCORE " + Player.score;
		pausePanel.SetActive(true);
		gameOverPanel.SetActive(false);
	}//pauseTheGame
	
	public void ResumeTheGame(){
		isPaused = false;
		Time.timeScale = 1f;
		pausePanel.SetActive(false);
		gameOverPanel.SetActive(false);
	}//pauseTheGame
	
	
	public void PlayAgainButtonClick(){
		Time.timeScale = 1f;
		Application.LoadLevel(AllScenes.GAMEPLAY_SCENE);
	}//PlayAgain
	
	public void PausePanelExitButton(){
		Time.timeScale = 1f;
		Application.LoadLevel(AllScenes.MAIN_MENU_SCENE);
	}
	
	public void ExitButtonClick(){
		Time.timeScale = 1f;
		Application.LoadLevel(AllScenes.MAIN_MENU_SCENE);
	}//PlayAgain
	
	void GameIsOver(){
		isGameOverSet = true;
		gameOverScoreText.text = "SCORE " + Player.score;
		gameOverPanel.SetActive(true);
		pausePanel.SetActive(false);
		
		int highscore  = GamePreferences.GetHighScore();
		if(Player.score > highscore){
			gameOverHighScoreText.text = "NEW HIGHSCORE!";
			GamePreferences.SetHighScore(Player.score);
		} else {
			gameOverHighScoreText.text = "BEST SCORE " + highscore ;
		}//if
	}//gameisOver
	
	void UpdateScoreUI(){
		if(Player.isAlive){
			scoreText.text = "Score : "+Player.score;
		}
	}//updateUI


}
