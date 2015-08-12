using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text scoreText;
	public static int newCount;

	// Use this for initialization
	void Awake () {
		scoreText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		newCount = MainScript.count;
		scoreText.text = string.Format ("Y O U R  S C O R E = " + newCount);

	}
}
