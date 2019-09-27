using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {
	
	public void OriginalTrack(){
		Application.OpenURL("https://www.youtube.com/watch?v=1WP_YLn1D1c");
	}//reteus
	
	public void VisitArtist(){
		Application.OpenURL("https://soundcloud.com/jim-yosef");
	}//reteus
	
	public void Channel(){
		Application.OpenURL("https://www.youtube.com/channel/UC_aEa8K-EOJ3D6gOs7HcyNg");
	}//reteus
	
	public void GoBack(){
		Application.LoadLevel(AllScenes.MAIN_MENU_SCENE);
	}//reteus
}
