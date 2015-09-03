using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountTime : MonoBehaviour
{

	public Text mytext;
	public float min;
	public float second = 0.0f;

	void Start ()
	{
		mytext = GetComponent<Text> ();
	}

	public bool started = false;
	public bool stopped = true;
	
	void Update ()
	{
		if (started && stopped) {
			second -= Time.deltaTime;
			if (second < 0) {
				min --;
				second = 60.0f;
			}
			mytext.text = string.Format ("T I M E = " + ((int)min).ToIntString (2) + " : " + ((int)second).ToIntString (2));
			if (min < 0) {
				var emre = GameObject.FindObjectOfType<MainScript> ();
				var oha = emre.selectedLevel *3 + emre.selectedMinute;
				switch(oha)
				{
				case 0:
					if(emre.count>=12)
					{
						AchievementScript.Instance.SetAchievement ("Easy10", 1);
					}
					break;
				case 1:
					if(emre.count>=25)
					{
						AchievementScript.Instance.SetAchievement ("Easy20", 1);
					}
					break;
				case 2:
					if(emre.count>=40)
					{
						AchievementScript.Instance.SetAchievement ("Easy30", 1);
					}
					break;
				case 3:
					if(emre.count>=8)
					{
						AchievementScript.Instance.SetAchievement ("Medium10", 1);
					}
					break;
				case 4:
					if(emre.count>=15)
					{
						AchievementScript.Instance.SetAchievement ("Medium20", 1);
					}
					break;
				case 5:
					if(emre.count>=25)
					{
						AchievementScript.Instance.SetAchievement ("Medium30", 1);
					}
					break;
				case 6:
					if(emre.count>=5)
					{
						AchievementScript.Instance.SetAchievement ("Hard10", 1);
					}
					break;
				case 7:
					if(emre.count>=12)
					{
						AchievementScript.Instance.SetAchievement ("Hard20", 1);
					}
					break;
				case 8:
					if(emre.count>=20)
					{
						AchievementScript.Instance.SetAchievement ("Hard30", 1);
					}
					break;
				}
				emre.sudoku.SetActive (false);
				emre.levels.SetActive (false);
				emre.minutes.SetActive (false);
				emre.zaman.SetActive (false);
				emre.theEnd.SetActive (true);
				emre.panels.SetActive (false);
				emre.EasyLevelText.SetActive (false);
				emre.HardLevelText.SetActive (false);
				emre.MediumLevelText.SetActive (false);
				emre.music.Stop ();
				emre.OnAddScoreToLeaderBoard ();
			}
		}
	}
	
}
