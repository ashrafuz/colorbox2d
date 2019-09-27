using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static int score;
	public static bool isAlive;
	
	public static string explodeBoxColorString,playerBoxColorString;
	
	[SerializeField]
	private BoxPieces boxPiece;
	
	[SerializeField]
	private CircleBoxPieces point;
	
	private GameObject colorBoxes;
	
	//USED FOR DIRECTION
	// 0 = still, -1= left, 1=right
	private static int direction=0;
	private float moveX = 0f;
	private float boundaryX;
	
	//CHOOSER
	private int colorChooserTimer=0;
	private GameObject indicatorObject;
	private float indicatorHeight = 0.15f;
	
	//SOUND
	[SerializeField]
	private AudioClip bombAudioClip,pointAudioClip,deadAudioClip,boxChangeSound;
	
	void Awake () {
		Time.timeScale = 1f;
		isAlive = true;
		direction = 0;
		score = 0;
		indicatorHeight = 0.15f;
		playerBoxColorString = AllColors.GET_RANDOM_COLOR_STRING();
		
		indicatorObject = gameObject.transform.GetChild(0).gameObject;
		boundaryX = CameraProp.GetCameraWidth(Camera.main) / 2 - 55f;
		
		//default
		indicatorObject.transform.localScale = new Vector3(1f,indicatorHeight,1f);
		colorChooserTimer = 10;
		
		StopAllCoroutines();
		StopCoroutine("ChangePlayerColorInSeconds");
		StartCoroutine("ChangePlayerColorInSeconds");
	}//awake	
	
	IEnumerator ChangePlayerColorInSeconds(){
		yield return new WaitForSeconds(1f);
		colorChooserTimer--;
		if(colorChooserTimer <= 0){
			if(GamePreferences.GetSoundSetting() == 1){
				AudioSource.PlayClipAtPoint(boxChangeSound,gameObject.transform.position);
			}
			string prevColor = playerBoxColorString;
			playerBoxColorString = AllColors.GET_RANDOM_COLOR_STRING();
			//more randmized
			if(prevColor == playerBoxColorString){
				playerBoxColorString = AllColors.GET_RANDOM_COLOR_STRING();
			}
			Debug.Log("New COlor " + playerBoxColorString);
			colorChooserTimer = 10;
		}//timer is at 10
		
		if(Player.isAlive){
			StartCoroutine("ChangePlayerColorInSeconds");
		} else {
			StopCoroutine("ChangePlayerColorInSeconds");
		}
		
	}//ChangePlayerColor
	
	void Update () {
	
		if(!isAlive) {return ;}//gameOver
		//COLOR
		ApplyColor();
		if(GamePlayController.isPaused){ return ;}
		//MOVEMENT
		MouseTouchController();
		ScreenTouchController();
		ApplyMovement();
		//ApplyIndicator
		UpdateIndicator();
		
	}//update
	
	void UpdateIndicator(){
		float newX = colorChooserTimer/10f;
		indicatorObject.transform.localScale = new Vector3(newX,indicatorHeight,1f);
	}//updateIndicator
	
	
	void ApplyColor(){
		gameObject.GetComponent<Renderer>().material.color = AllColors.GET_COLOR(playerBoxColorString);
		indicatorObject.GetComponent<Renderer>().material.color = AllColors.GET_COLOR(playerBoxColorString);
	}//ApplyColor
	
	public static void IncreaseScore(int value){
		if(Player.isAlive){
			Player.score += value;
		}
	}//increaseScore
	
	void ScreenTouchController(){
		//only One Touch is allowed at a time!
		if(Input.touchCount == 1){
			Touch touch = Input.GetTouch(0);
			Vector3 screenPos = Camera.main.ScreenToViewportPoint(touch.position);
			if(touch.phase == TouchPhase.Began){
				if(screenPos.y < 0.87f){ //pauseButtonScope
					Player.direction = (screenPos.x <= 0.5f) ? -1 : 1;
				}
			}
			if (touch.phase == TouchPhase.Ended){
				Player.direction = 0;
			}
		}//touchedScreen
	}//screentouchcontroller
	
	void MouseTouchController(){
		Vector3 pos = Input.mousePosition;
		pos.z = 1f;
		if(Input.GetMouseButtonDown(0)){ // left mouse click
			Vector3 screenPos = Camera.main.ScreenToViewportPoint(pos);
			//pauseButtonScope
			if(screenPos.y < 0.87f){
				// -1 left, 1 right
				Player.direction = (screenPos.x <= 0.5f) ? -1 : 1;
			}//yIf
		}//
		if(Input.GetMouseButtonUp(0)){
			//Debug.Log("caught an up");
			Player.direction = 0;
		}
	}//MouseTouchController
	
	
	void ApplyMovement(){
		moveX = 0f;
		if(Player.direction < 0 && transform.position.x > -boundaryX){ // moveLeft
			moveX = -10f;
		} else if (Player.direction > 0 && transform.position.x < boundaryX){ // moveRight
			moveX = 10f;
		}
		var temp = transform.position;
		temp.x += moveX;
		transform.position = temp;
	}//move
	
	void OnTriggerEnter2D(Collider2D target){
		if(!Player.isAlive) { return ;}
		
		// first check if its a bomb
		if(target.tag == "bomb"){
			//SoundSetting
			if(GamePreferences.GetSoundSetting() == 1){
				AudioSource.PlayClipAtPoint(bombAudioClip,gameObject.transform.position);
			}//
			Debug.Log("got a bomb!");
			Destroy(target.gameObject);
			ExplodeAllBoxes();
		} else if(playerBoxColorString == "white"){
			Player.IncreaseScore(5);
			Explode(target.gameObject);	
		} else if(target.gameObject.tag != playerBoxColorString ){
			if(GamePreferences.GetSoundSetting() == 1){
				AudioSource.PlayClipAtPoint(deadAudioClip,gameObject.transform.position);
			}
			isAlive = false;
			Explode(gameObject);
			Debug.Log("DEAD!");
		} else if (target.gameObject.tag == playerBoxColorString ){
			if(GamePreferences.GetSoundSetting() == 1){
				AudioSource.PlayClipAtPoint(pointAudioClip,gameObject.transform.position);	
			}
			//PointAnimation
			CircleBoxPieces clone = 
				Instantiate(point,target.transform.position,Quaternion.identity) as CircleBoxPieces;
			//adding force to up
			clone.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 3500);
			Player.IncreaseScore(5);
			Explode(target.gameObject);
		}
	}//triggerEnter2d
	
	void ExplodeAllBoxes(){
		GameObject[] boxes = GameObject.FindGameObjectsWithTag("yellow");
		for(int i=0; i<boxes.Length; i++){
			Explode(boxes[i]);
			Player.IncreaseScore(1);
		}//yellow
		boxes = GameObject.FindGameObjectsWithTag("pink");
		for(int i=0; i<boxes.Length; i++){
			Explode(boxes[i]);
			Player.IncreaseScore(1);
		}//pink
		boxes = GameObject.FindGameObjectsWithTag("orange");
		for(int i=0; i<boxes.Length; i++){
			Explode(boxes[i]);
			Player.IncreaseScore(1);
		}//orange
		boxes = GameObject.FindGameObjectsWithTag("green");
		for(int i=0; i<boxes.Length; i++){
			Explode(boxes[i]);
			Player.IncreaseScore(1);
		}//green
		boxes = GameObject.FindGameObjectsWithTag("blue");
		for(int i=0; i<boxes.Length; i++){
			Explode(boxes[i]);
			Player.IncreaseScore(1);
		}//blue
		//Applying BombColr
		Player.explodeBoxColorString = "red";
	}//ExplodeColor
	
	void Explode(GameObject objectToExplode){
		if(objectToExplode.gameObject.tag != "Player"){
			Player.explodeBoxColorString = objectToExplode.gameObject.tag;
		} else {
			Player.explodeBoxColorString = playerBoxColorString;
		}
		Destroy(objectToExplode);
		var trans = objectToExplode.transform;
		for(int i=0;i<12; i++){
			var temp = trans.position;
			temp.x += (5f * Random.Range(-10,10));
			temp.y += (5f * Random.Range(-10,10));
			BoxPieces clone = Instantiate(boxPiece,temp,Quaternion.identity) as BoxPieces;
			// adding force to sideways
			clone.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Random.Range(-10,10) * 500);
			//adding force to up
			clone.GetComponent<Rigidbody2D>().AddForce(Vector3.up * Random.Range(-10,10) * 700);
		}//loop of explode
	}//onexplode
	
}
