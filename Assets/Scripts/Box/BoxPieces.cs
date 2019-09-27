using UnityEngine;
using System.Collections;

public class BoxPieces : MonoBehaviour {

	private float t = 0.0f;
	private Color start,end;
	
	private int timeDivider = 3;//default
	
	// Use this for initialization
	void Start () {
		timeDivider = Player.isAlive == true ? 1 : 3;
		
		start = AllColors.GET_COLOR(Player.explodeBoxColorString);
		//for changing the box color
		gameObject.GetComponent<Renderer>().material.color =  start;	
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
