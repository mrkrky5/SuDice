using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementScript : MonoBehaviour
{

	public Dictionary<string,Achievement> achievements = new Dictionary<string, Achievement> ();

	public void SetAchievement (string ID, int progress)
	{
		achievements [ID].progress = progress;
		if (!achievements [ID].completed) {
			Social.ReportProgress (achievements [ID].achievementID, GetPercentage (ID), b => {});
			if(achievements[ID].percentage>=100)
				achievements [ID].completed = true;
		}
	}

	public void Add (string ID, string achievementID, int required)
	{
		var achievement = new Achievement ();
		achievement.achievementID = achievementID;
		achievement.required = required;
		achievements.Add (ID, achievement);
	}
	
	public Achievement GetAchievement (string ID)
	{
		return achievements [ID];
	}
	
	public int GetProgress (string ID)
	{
		return GetAchievement (ID).progress;
	}
	
	public float GetPercentage (string ID)
	{
		return GetAchievement (ID).percentage;
	}

	public static AchievementScript Instance;

	public class Achievement
	{
		public string achievementID;
		public int required;

		public int progress {
			get{ return PlayerPrefs.GetInt (achievementID + "_progress");}
			set {
				PlayerPrefs.SetInt (achievementID + "_progress", value);
			}
		}

		public float percentage {
			get { return (progress / (float)required) * 100;}
		}

		public bool completed {
			get { return PlayerPrefs.GetInt (achievementID + "_completed") == 1;}
			set{ PlayerPrefs.SetInt (achievementID + "_completed", 1);}
		}
	}

	void Awake ()
	{
		Instance = this;
	}
}
