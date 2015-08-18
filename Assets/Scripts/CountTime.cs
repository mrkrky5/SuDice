using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountTime : MonoBehaviour
{

	Text mytext;
	public float min;
	public float second = 0.0f;


	void Start ()
	{
		mytext = GetComponent<Text> ();
	}

	public bool started = false;

	
	void Update ()
	{
		if (started) {
			second -= Time.deltaTime;
			if (second < 0) {
				min --;
				second = 60.0f;
			}
			mytext.text = string.Format ("T I M E = " + ((int)min).ToIntString (2) + " : " + ((int)second).ToIntString (2));
			if (min < 0) {
				var emre = GameObject.FindObjectOfType<MainScript> ();
				emre.sudoku.SetActive (false);
				emre.levels.SetActive (false);
				emre.minutes.SetActive (false);
				emre.zaman.SetActive (false);
				emre.score.SetActive (false);
				emre.theEnd.SetActive(true);
				emre.music.Stop();
			}
		}
	}
}
