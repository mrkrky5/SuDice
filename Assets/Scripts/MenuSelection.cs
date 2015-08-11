using UnityEngine;
using System.Collections;

public class MenuSelection : MonoBehaviour {

	// Use this for initialization
	public void firstClick(){
		Application.LoadLevel ("MainMenu2");
		}
	public void lastClickForEasy(){
		Application.LoadLevel ("EasyScene");
	}
	public void backClick(){
		Application.LoadLevel ("MainMenu");
	}
}
