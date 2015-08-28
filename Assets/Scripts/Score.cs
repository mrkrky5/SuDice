using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

	public Text scoreText;

	// Use this for initialization
	void Awake ()
	{
	}

	// Update is called once per frame
	public void NewFunction (int total)
	{
		scoreText.text = string.Format ("Y O U R  S C O R E = " + total);
	}
}
