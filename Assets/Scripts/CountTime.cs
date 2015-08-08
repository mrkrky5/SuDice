using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountTime : MonoBehaviour
{


	Text mytext;
	private float hour = 0.0f;
	private float minute = 0.0f;
	private float second = 0.0f;

	void Start ()
	{
		mytext = GetComponent<Text> ();
	}

	void Update ()
	{
		second += Time.deltaTime;
		if (second >= 60) {
			minute += 1.0f;
			second = Time.deltaTime;
		}
		if (minute >= 60) {
			hour += 1.0f;
			minute = Time.deltaTime;
		}
		mytext.text = string.Format ("T I M E = " + ((int)hour).ToIntString (2) + ":" + ((int)minute).ToIntString (2) + ":" + ((int)second).ToIntString (2));
	}
}
