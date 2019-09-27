using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CircleBoxPieces : MonoBehaviour {

	private float t = 0.0f;
	private Color start,end;
	private int timeDivider = 3;//default
	
	[SerializeField]
	private bool point;
	
	// Use this for initialization
	void Start () {
		timeDivider = Player.isAlive == true ? 1 : 3;
		
		//if(Application.loadedLevelName == AllScenes.MAIN_MENU_SCENE){
		if(SceneManager.GetActiveScene().name == AllScenes.MAIN_MENU_SCENE){
			timeDivider = 4;
		}
		
		if(point){ 
			start = new Color(1f,1f,1f);
			timeDivider = 2;
		}else {
			//for changing the box color
			start = AllColors.GET_COLOR(Box.colorCode);
			gameObject.GetComponent<Renderer>().material.color =  start;		
		}
		end = new Color (start.r, start.g,start.b,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		GetComponent<Renderer>().material.color = Color.Lerp (start, end, (t/timeDivider));
		if (GetComponent<Renderer>().material.color.a <= 0.0) {
			Destroy (gameObject);
		}
	}
}
