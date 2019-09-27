using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonController : MonoBehaviour {
	
	private Color fade,normal;
	
	[SerializeField]
	private Image soundButton;
	
	public AudioSource audioSource;
	
	void Awake(){
		Time.timeScale = 1f;
		if(PlayerPrefs.HasKey(GamePreferences.GAME_PLAYED_ATLEAST_ONCE)){
			//yesGameis played
		} else {
			GamePreferences.SetSoundSetting(1);
			GamePreferences.SetHighScore(0);
			InstrucitonController.showOnce = true;
		}
		
		bool isNowActive = GamePreferences.GetSoundSetting() == 1 ? true : false;
		fade = new Color(soundButton.color.r, soundButton.color.g, soundButton.color.b,0.35f);
		normal = new Color(soundButton.color.r, soundButton.color.g, soundButton.color.b,1f);
		soundButton.color = isNowActive ? normal : fade;
		
		audioSource = GameObject.FindGameObjectWithTag("bgmusic").GetComponent<AudioSource>();
	}//awake
	
	
	void OnLevelWasLoaded(){
		Time.timeScale = 1f;
	}//levelload
	
	void Update(){
		CheckMusic();
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
	
	void CheckMusic(){
		if(GamePreferences.GetSoundSetting() == 0){
			//Debug.Log("Sound setting is off");
			audioSource.Stop();
		} else if (GamePreferences.GetSoundSetting() == 1){
			//Debug.Log("Sound setting is on");
			if(!audioSource.isPlaying){
				audioSource.Play();
				audioSource.loop = true;
			}
		}
	}
	
	public void LoadGame(){
		if(!InstrucitonController.showOnce){
			//Application.LoadLevel(AllScenes.GAMEPLAY_SCENE);
			SceneManager.LoadScene(AllScenes.GAMEPLAY_SCENE);
		} else {
			SceneManager.LoadScene(AllScenes.INSTRUCTION);
			//Application.LoadLevel(AllScenes.INSTRUCTION);
		}
	}//loadGame
	
	public void ShowInfo(){
		//Application.LoadLevel(AllScenes.INSTRUCTION);
		SceneManager.LoadScene(AllScenes.INSTRUCTION);
	}//showInfo
	
	public void Exit(){
		Application.Quit();
	}//exit
	
	public void GoToCredits(){
		//Application.LoadLevel(AllScenes.CREDITS);
		SceneManager.LoadScene(AllScenes.CREDITS);
	}//exit
	
	public void SoundButtonClick(){
		//toggle
		//MAKE IT OFF
		if(GamePreferences.GetSoundSetting() == 1){
			soundButton.color = fade;
			GamePreferences.SetSoundSetting(0);
		} else {
			//MAKE IT ON
			soundButton.color = normal;
			GamePreferences.SetSoundSetting(1);
		}
	}//soundButtonClick
	
	public void RateUs(){
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.thebinarywolf.colorboard");
	}//reteus
	
	
}//MainMenu
