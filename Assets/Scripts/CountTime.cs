using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountTime : MonoBehaviour
{

	Text mytext;
	private float minute = 1.0f;
	private float second = 1.0f;


	void Start ()
	{
		mytext = GetComponent<Text> ();
	}

	void Update ()
	{
		second -= Time.deltaTime;
		if (second < 0 ) {
			minute --;
			second = 60.0f;
		}
		mytext.text = string.Format ("T I M E = " + ((int)minute).ToIntString (2) + " : " + ((int)second).ToIntString (2));
		if (minute < 0) {
			mytext.text = string.Format ("GAME OVER");
		}
	}
}
