using UnityEngine;
using System.Collections;

public class TrackSpawner : MonoBehaviour {

	[SerializeField]
	private Box[] boxObjects;
	
	private float screenHeight,screenWidth, rangeX,startY;
	private float positionX1,positionX2,positionX3;
	
	void Awake(){
		screenWidth = CameraProp.GetCameraWidth(Camera.main);
		screenHeight = CameraProp.GetCameraHeight(Camera.main);
		
		rangeX = screenWidth / 2f;
		startY = transform.position.y;
		
		positionX1 = 0f;
		positionX2 = (rangeX/2f);
		positionX3 = -positionX2;
		
		
		// generate objects
		// if it crosses screen height, deactivate it
		
	}//awake
	
	void Start(){
		StopCoroutine("GenerateTracks");
		StartCoroutine("GenerateTracks");
	} // start
	
	
	IEnumerator GenerateTracks(){
		yield return new WaitForSeconds(0.1f);
		if (Player.isAlive) {
			int randomX =  Random.Range(0,100) % 3;
			//default
			float selectedX  = positionX1;
			if(randomX == 1){
				selectedX = positionX2;
			} else if (randomX==2){
				selectedX = positionX3;
			}
			var pos = new Vector3(selectedX,startY,0f);
			
			Instantiate(boxObjects[Random.Range(0,boxObjects.Length)], pos, Quaternion.identity);
			StartCoroutine("GenerateTracks");
		}
	}
}
