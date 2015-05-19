using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

	public GoTweenChain triangular;
	public GoTweenChain vertical;
	bool triangular_move = false;
	bool vertical_move = false;
	bool buttonThree =false;
	bool paused = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Alpha1) && triangular_move == false && buttonThree == false) { 
			triangularMove ();
			buttonThree = true;
		} else if (Input.GetKeyUp (KeyCode.Alpha3) && vertical_move == true && buttonThree == true) {
			triangular.pause ();
			verticalMove ();
			vertical_move = true;
			buttonThree = true;
		}
		else if (Input.GetKeyUp (KeyCode.Alpha2) && triangular_move == false && buttonThree == false && paused==false) {
			StartCoroutine (TriMove ());
			buttonThree = true;
		}
	}


	
	void triangularMove(){

		triangular_move = true;
		vertical_move = true;

		GoTween t1 = new GoTween (transform, 2f, new GoTweenConfig ().position (new Vector3 (5, 0, 0)));
		GoTween t2 = new GoTween (transform, 2f, new GoTweenConfig ().position (new Vector3 (0, 0, 5)));
		GoTween t3 = new GoTween (transform, 2f, new GoTweenConfig ().position (new Vector3 (0, 0, 0)));
		
		triangular = new GoTweenChain (new GoTweenCollectionConfig().setIterations (-1)).append (t1).append (t2).append (t3);
		triangular.play ();
	}
	
	void verticalMove(){
		
		vertical_move = false;

		GoTween t1 = new GoTween (transform, 1f, new GoTweenConfig ().position (new Vector3 (transform.position.x, transform.position.y+5, transform.position.z)));
		GoTween t2 = new GoTween (transform, 1f, new GoTweenConfig ().position (new Vector3 (transform.position.x, transform.position.y, transform.position.z)));
		
		vertical = new GoTweenChain ().append (t1).append (t2);
		vertical.play ();
		vertical.setOnCompleteHandler (c=> triangular.play ());
	}
		IEnumerator TriMove(){
		triangular_move = true;
		vertical_move = true;

			for (int i=0; i<50; i++) {
				transform.Translate (.1f, 0, 0);
				yield return new WaitForSeconds(.04f);
			if (Input.GetKeyUp (KeyCode.Space) && vertical_move == true && buttonThree == true && paused==false ) {
				yield return StartCoroutine (vertiMove ());
				}
			}
			for (int i=0; i<50; i++) {
				transform.Translate (-.1f, 0, .1f);
				yield return new WaitForSeconds(.04f);
			if (Input.GetKeyUp (KeyCode.Space) && vertical_move == true && buttonThree == true && paused==false ) {
				yield return StartCoroutine (vertiMove ());
			}
			}
			for (int i=0; i<50; i++) {
				transform.Translate (0, 0,-.1f);
				yield return new WaitForSeconds(.04f);
			if (Input.GetKeyUp (KeyCode.Space) && vertical_move == true && buttonThree == true && paused==false ) {
				yield return StartCoroutine (vertiMove ());
			}
		}
		paused = false;
		yield return StartCoroutine (TriMove ());
	}
		IEnumerator vertiMove(){
		vertical_move = false;
		paused = true;
		for (int i=0; i<20; i++) {
			transform.Translate (0, .25f, 0);
			yield return new WaitForSeconds (.05f);
		}
		for (int i=0; i<20; i++) {
			transform.Translate (0, -.25f, 0);
			yield return new WaitForSeconds (.05f);
		}
		yield return StartCoroutine (TriMove ());

	}

	
}