using UnityEngine;
using System.Collections;

public class AllColors : MonoBehaviour {

	
	public static Color GET_COLOR(string code){
		
		if (code == "yellow")	{return new Color(1f,1f,0.1960f);}
		else if (code == "pink")	{return new Color(1f,0.235f,0.784f);}
		else if (code == "blue")	{return new Color(0f,1f,1f);} 
		else if (code == "orange")	{return new Color(1f,0.1691f,0.1691f);} 
		else if (code == "red")		{return new Color(1f,0f,0f);}
		else if (code == "black")	{return new Color(0f,0f,0f);}
		//default is white
		return new Color(1f,1f,1f);
	}//GET_COLOR
	
	public static string GET_RANDOM_COLOR_STRING(){
		int code = Random.Range(0,100) % 6;
		if (code == 1)	{return "yellow";} 
		else if (code == 2)	{return "pink";} 
		else if (code == 3)	{return "blue";} 
		else if (code == 4)	{return "orange";}
		//default is white
		return "pink";
	}//GET_COLOR
	
	public void ColorCodes(){
		GameObject colorBoxes;
		colorBoxes = GameObject.FindGameObjectWithTag("orange");
		Debug.Log("orange r " + colorBoxes.GetComponent<SpriteRenderer>().color.r);
		Debug.Log("orange g " + colorBoxes.GetComponent<SpriteRenderer>().color.g);
		Debug.Log("orange b " + colorBoxes.GetComponent<SpriteRenderer>().color.b);
		
		colorBoxes = GameObject.FindGameObjectWithTag("pink");
		Debug.Log("pink r" + colorBoxes.GetComponent<SpriteRenderer>().color.r);
		Debug.Log("orange g" + colorBoxes.GetComponent<SpriteRenderer>().color.g);
		Debug.Log("orange b" + colorBoxes.GetComponent<SpriteRenderer>().color.b);
		
		colorBoxes = GameObject.FindGameObjectWithTag("green");
		Debug.Log("green r" + colorBoxes.GetComponent<SpriteRenderer>().color.r);
		Debug.Log("orange g" + colorBoxes.GetComponent<SpriteRenderer>().color.g);
		Debug.Log("orange b" + colorBoxes.GetComponent<SpriteRenderer>().color.b);
		
		colorBoxes = GameObject.FindGameObjectWithTag("blue");
		Debug.Log("blue r" + colorBoxes.GetComponent<SpriteRenderer>().color.r);
		Debug.Log("orange g" + colorBoxes.GetComponent<SpriteRenderer>().color.g);
		Debug.Log("orange b" + colorBoxes.GetComponent<SpriteRenderer>().color.b);
		
		colorBoxes = GameObject.FindGameObjectWithTag("yellow");
		Debug.Log("yellow r" + colorBoxes.GetComponent<SpriteRenderer>().color.r);
		Debug.Log("orange g" + colorBoxes.GetComponent<SpriteRenderer>().color.g);
		Debug.Log("orange b" + colorBoxes.GetComponent<SpriteRenderer>().color.b);
	}
}
