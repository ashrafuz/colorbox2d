using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
	
	public float speed;
	private float boxPositionY,boxBoundaryY; 
	public static string colorCode;
	
	[SerializeField]
	private CircleBoxPieces circleBoxPiece,pointMinus10;

	private int chooser;
	
	void Start () {
		boxPositionY = gameObject.transform.position.y;
		boxBoundaryY = -(CameraProp.GetCameraHeight(Camera.main) / 2 - 10f);
		
		//default
		colorCode = "white";
		speed = 7f;
		chooser = 0;
	}//start
	
	void Update(){
		//Control Speed Of the boxes
		if(Player.score < 100)			{speed = 7f;}
		else if (Player.score >= 100 && Player.score < 200)	{speed = 8f;} 
		else if (Player.score >= 200 && Player.score < 400)	{speed = 9f;}
		else if (Player.score >= 400 && Player.score < 700)	{speed = 10f;}
		else if (Player.score >= 700 && Player.score < 900)	{speed = 12f;}
		else if (Player.score >= 900 && Player.score < 1000) {speed = 14f;}
		else if (Player.score >= 1000) {speed = 16f;}
	}
	
	void FixedUpdate(){
		boxPositionY -= speed;
		gameObject.transform.position = new Vector2(gameObject.transform.position.x, boxPositionY);
		if(boxPositionY  < boxBoundaryY){
			if(gameObject.tag == Player.playerBoxColorString){
				Player.IncreaseScore(-10);
				chooser = -5;
			} else {
				Player.IncreaseScore(1);
				chooser = 1;
			}
			
			if(gameObject.tag != "bomb"){
				colorCode = gameObject.tag;
			} else {
				colorCode = "black";
				chooser = 0;
			}
			Explode();
		}
	}//fixedUpdate
	
	void Explode(){
		Destroy(gameObject);
		var trans = gameObject.transform;
		for(int i=0;i<10; i++){
			var temp = trans.position;
			temp.x += (5f * Random.Range(-10,10));
			temp.y += (5f * Random.Range(-10,10));
			CircleBoxPieces clone = Instantiate(circleBoxPiece,temp,Quaternion.identity) as CircleBoxPieces;
			// adding force to sideways
			clone.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Random.Range(-10,10) * 500);
			//adding force to up
			clone.GetComponent<Rigidbody2D>().AddForce(Vector3.up * Random.Range(1,12) * 700);
		}//loop of explode
		
		if (chooser == -5 && Player.isAlive){ 
			//point 1
			CircleBoxPieces point = Instantiate(pointMinus10,trans.position,Quaternion.identity) as CircleBoxPieces;
			point.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 5000);
			
		}
	}
	
}
