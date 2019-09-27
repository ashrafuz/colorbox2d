using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstrucitonController : MonoBehaviour {
	
	//SET UP FROM UI
	[SerializeField]
	private Image instructionImage;
	[SerializeField]
	private Sprite ins0,ins1,ins2;
	[SerializeField]
	private Text pageNumberText,headLineText;
	
	public static bool showOnce=false;
	
	private int currentPage;
	
	void Awake(){
		currentPage = 0;
		SetImage(currentPage);
	}//awake
	
	public void Next(){
		currentPage++;
		SetImage(currentPage);
	}//next
	
	void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			CloseInstruction();
		}
	}
	
	public void CloseInstruction(){
		if (!showOnce){
			Application.LoadLevel(AllScenes.MAIN_MENU_SCENE);
		} else {
			Application.LoadLevel(AllScenes.GAMEPLAY_SCENE);
		}
	}//closeInstruction
	
	void SetImage(int pagenumber){
		if(currentPage > 2 && (showOnce)){
			CloseInstruction();
			showOnce = false;
			currentPage = 0;
			return;
		}
		//normal case
		currentPage = currentPage > 2 ? 0 : currentPage;
		if(currentPage == 0){
			instructionImage.sprite = ins0;
			pageNumberText.text = "Page 1 of 3";
			headLineText.text = "Movement";
		} else if (currentPage ==1){
			instructionImage.sprite = ins1;
			pageNumberText.text = "Page 2 of 3";
			headLineText.text = "Points";
		} else if (currentPage ==2){
			instructionImage.sprite = ins2;
			pageNumberText.text = "Page 3 of 3";
			headLineText.text = "Timer";
		}
	}//setImage
}
