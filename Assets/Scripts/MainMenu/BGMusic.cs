using UnityEngine;
using System.Collections;

public class BGMusic : MonoBehaviour {

	static BGMusic instance = null;
	
	GameObject[] musicObjects;
	
	void Awake(){
		Debug.Log("My ID " + GetInstanceID());
		if(instance != null){
			Destroy(gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}//awake
	
	void OnLevelWasLoaded(int level){
		if(level == 0){
			DestroyExtraResource();
		}
	}
	
	void DestroyExtraResource(){
		//musicObjects = GameObject.FindGameObjectsWithTag("bgmusic");
		//Destroy Extra
		//if(musicObjects.Length>1){
		//	for(int i = musicObjects.Length-1; i>0;i--){
		//		Destroy(musicObjects[i]);
		//	}
		//}
	}
}//class
