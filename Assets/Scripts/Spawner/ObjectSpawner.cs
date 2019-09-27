using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {
	
	[SerializeField]
	private Box[] boxObjects;
	
	private float screenHeight,screenWidth, rangeX,startY,offsetX,offsetY;
	private float minTimeWait,maxTimeWait;
	
	void Awake(){
		screenWidth = CameraProp.GetCameraWidth(Camera.main);
		screenHeight = CameraProp.GetCameraHeight(Camera.main);
		
		offsetX = 55f;
		offsetY = 55f;
		rangeX = screenWidth / 2f - offsetX;
		startY = screenHeight / 2f + offsetY;
		
		minTimeWait = 0.4f;
		maxTimeWait = 0.9f;
		
	}//awake
	
	void Start(){
		StopCoroutine("GenerateObjects");
		StartCoroutine("GenerateObjects");
	} // start
	
	
	IEnumerator GenerateObjects(){
		yield return new WaitForSeconds(Random.Range(minTimeWait,maxTimeWait));
		if (Player.isAlive) {
			// find a position within screen width
			float positionX = Random.Range(0f, rangeX);
			int negativeMultiplier = Random.Range(0,99) % 2 == 0 ? -1:1;
			var pos = new Vector3(positionX * negativeMultiplier,startY,0f);
			
			Instantiate(boxObjects[Random.Range(0,boxObjects.Length)], pos, Quaternion.identity);
			StartCoroutine("GenerateObjects");
		}
	}
}
