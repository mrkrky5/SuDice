using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class MainScript : MonoBehaviour
{
	
	public Dice[] dices = new Dice[36] ;
	public Vector2 pos;
	public Texture[] textures;
	public static MainScript Instance;
	public GameObject sudoku;
	public GameObject levels;
	public GameObject minutes;
	public GameObject zaman;
	public GameObject theEnd;
	public GameObject score;
	public GameObject menu;
	public GameObject voice;
	public GameObject voice2;
	public GameObject panels;
	public GameObject loading;
	public GameObject backButton3;
	public GameObject tutorialText;
	public GameObject EasyLevelText;
	public GameObject MediumLevelText;
	public GameObject HardLevelText;
	public GameObject leaderBoard;
	public GameObject achievements;
	public GameObject leaderboardLevels;
	public GameObject easyBoards; 
	public GameObject mediumBoards; 
	public GameObject hardBoards; 

	public float startTime;
	public float finishTime;
	public static float passingTime;
	private int minute;
	public Text newText;
	public int levelPoint1;
	public int passTime1;
	public static int total;
	public static int level;
	public int count = -1;
	public int selectedLevel = 0;
	public int selectedMinute = 0;
	public AudioSource lockClick;
	public AudioSource music;
	public AudioSource completed;
	public AudioSource achievement;
	public Button soundButton;
	public Button soundButton2;
	public Sprite newSprite;
	public Sprite oldSprite;
	public string[,] leaderBoardsID = new string[3, 3]{
		{"CgkIzv3U6eAFEAIQAA","CgkIzv3U6eAFEAIQAQ","CgkIzv3U6eAFEAIQAg"},
		{"CgkIzv3U6eAFEAIQAw","CgkIzv3U6eAFEAIQBA","CgkIzv3U6eAFEAIQBQ"},
		{"CgkIzv3U6eAFEAIQBg","CgkIzv3U6eAFEAIQBw","CgkIzv3U6eAFEAIQCA"}
	};

	void Awake ()
	{

		Instance = this;
		music = gameObject.GetComponent<AudioSource> ();

		menu = GameObject.Find ("MainMenu");
		menu.SetActive (true);

		sudoku = GameObject.Find ("Sudoku");
		sudoku.SetActive (false);

		levels = GameObject.Find ("EasyMediumHard");
		levels.SetActive (false);

		minutes = GameObject.Find ("Minutes");
		minutes.SetActive (false);

		zaman = GameObject.Find ("Zaman");
		zaman.SetActive (false);

		theEnd = GameObject.Find ("TheEnd");
		theEnd.SetActive (false);

		score = GameObject.Find ("Score");
		score.SetActive (false);

		voice = GameObject.Find ("Voice");
		voice.SetActive (true);

		voice2 = GameObject.Find ("Voice2");
		voice2.SetActive (false);

		panels = GameObject.Find ("Panels");
		panels.SetActive (false);

		loading = GameObject.Find ("Loading");
		loading.SetActive (false);

		backButton3 = GameObject.Find ("BackButton3");
		backButton3.SetActive (false);

		tutorialText = GameObject.Find ("TutorialText");
		tutorialText.SetActive (false);

		EasyLevelText = GameObject.Find ("EasyLevelText");
		EasyLevelText.SetActive (false);

		MediumLevelText = GameObject.Find ("MediumLevelText");
		MediumLevelText.SetActive (false);

		HardLevelText = GameObject.Find ("HardLevelText");
		HardLevelText.SetActive (false);

		leaderBoard = GameObject.Find ("LeaderBoard");
		achievements = GameObject.Find ("Achievements");
		achievements.SetActive (false);

		leaderboardLevels = GameObject.Find ("LeaderboardLevels");
		leaderboardLevels.SetActive (false);

		easyBoards = GameObject.Find ("EasyLeaderboardLevels");
		easyBoards.SetActive (false);

		mediumBoards = GameObject.Find ("MediumLeaderboardLevels");
 		mediumBoards.SetActive (false);

		hardBoards = GameObject.Find ("HardLeaderboardLevels");
		hardBoards.SetActive (false);


		PlayGamesPlatform.Activate ();
		if (Social.localUser.authenticated) {
			leaderBoard.SetActive (true);
			achievements.SetActive (true);
		}

	}

	void Start ()
	{
		AchievementScript.Instance.Add ("Newbie", "CgkIzv3U6eAFEAIQDQ", 1);
		AchievementScript.Instance.Add ("Veteran", "CgkIzv3U6eAFEAIQDg", 20);
		AchievementScript.Instance.Add ("Master", "CgkIzv3U6eAFEAIQDw", 50);
		AchievementScript.Instance.Add ("Itself", "CgkIzv3U6eAFEAIQEA", 150);

		AchievementScript.Instance.Add ("EasyFast", "CgkIzv3U6eAFEAIQCg", 1);
		AchievementScript.Instance.Add ("MediumFast", "CgkIzv3U6eAFEAIQCw", 1);
		AchievementScript.Instance.Add ("HardFast", "CgkIzv3U6eAFEAIQDA", 1);

		AchievementScript.Instance.Add ("Easy10", "CgkIzv3U6eAFEAIQEQ", 1);
		AchievementScript.Instance.Add ("Easy20", "CgkIzv3U6eAFEAIQEg", 1);
		AchievementScript.Instance.Add ("Easy30", "CgkIzv3U6eAFEAIQEw", 1);

		AchievementScript.Instance.Add ("Medium10", "CgkIzv3U6eAFEAIQFA", 1);
		AchievementScript.Instance.Add ("Medium20", "CgkIzv3U6eAFEAIQFQ", 1);
		AchievementScript.Instance.Add ("Medium30", "CgkIzv3U6eAFEAIQFg", 1);

		AchievementScript.Instance.Add ("Hard10", "CgkIzv3U6eAFEAIQFw", 1);
		AchievementScript.Instance.Add ("Hard20", "CgkIzv3U6eAFEAIQGA", 1);
		AchievementScript.Instance.Add ("Hard30", "CgkIzv3U6eAFEAIQGQ", 1);

		AchievementScript.Instance.Add ("RealMaster", "CgkIzv3U6eAFEAIQGg", 16);

		StartCoroutine (WaitTwoSeconds ());
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
		passingTime = finishTime - startTime;
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("MainScene");
		}
	}
	IEnumerator WaitTwoSeconds(){
	
		yield return new WaitForSeconds (2);
		LogIn ();
	}

	public enum Solution
	{
		Unique,
		NotUnique,
		NoSolution
	}

	public Solution IsUnique (int[] sudoku)
	{
		int tempX = 0;
		int tempY = 0;
		int[] tempP = null;
		int ctempP = 7;

		for (int y=0; y<6; y++) {
			for (int x=0; x<6; x++) {
				if (sudoku [y * 6 + x] == -1) {
					int[] P = {-1,0,1,2,3,4,5};
					for (int a=0; a<6; a++) {
						P [sudoku [y * 6 + a] + 1] = -1;
					}
					for (int b=0; b<6; b++) {
						P [sudoku [b * 6 + x] + 1] = -1;
					}
					var dy = Mathf.FloorToInt (y / 2f);
					var dx = Mathf.FloorToInt (x / 3f);
					for (int j = 0; j < 2; j++) {
						for (int k = 0; k < 3; k++) {
							P [sudoku [((dy * 12) + (dx * 3)) + k + (j * 6)] + 1] = -1;
						}
					}
					int cP = 0;
					for (int d=1; d<7; d++) {
						cP += P [d] == -1 ? 0 : 1;
					}
					if (cP < ctempP) {
						ctempP = cP;
						tempP = P;
						tempX = x;
						tempY = y;
					}
				}
			}
		}
		if (ctempP == 7) {
			return Solution.Unique;
		}
		if (ctempP == 0) {
			return Solution.NoSolution;
		}
		int success = 0;
		for (int i=1; i<7; i++) {
			if (tempP [i] != -1) {
				sudoku [tempY * 6 + tempX] = tempP [i];
				switch (IsUnique (sudoku)) {
				case Solution.Unique:
					success++;
					break;
				case Solution.NoSolution:
					break;
				case Solution.NotUnique:
					return Solution.NotUnique;
				}

				if (success > 1)
					return Solution.NotUnique;
			}
		}
		sudoku [tempY * 6 + tempX] = -1;
		switch (success) {
		case 0:
			return Solution.NoSolution;
		case 1:
			return Solution.Unique;
		default:
			return Solution.NotUnique;
		}
	}

	public int[] RemoveNumbers (int[] generated)
	{
		var deleted = level;
		var sudoku = (int[])generated.Clone (); 
		var list = new List<int> (36);
		for (int i=0; i<36; i++) {
			list.Add (i);
		}
		while (deleted>0) {
			var selected = list [Random.Range (0, list.Count)];
			var temp = sudoku [selected];
			sudoku [selected] = -1;
			var clone = (int[])sudoku.Clone ();
			if (IsUnique (clone) != Solution.Unique) {
				sudoku [selected] = temp;
			} else {
				list.Remove (selected);
				deleted--;
			}
		}
		return sudoku;
	}
	
	public int[] Genisys ()
	{
		var generated = new int[36];

		for (int y=0; y<6; y++) {
			for (int x =0; x<6; x++) {
				generated [y * 6 + x] = -1;
			}
		}
		for (int y=0; y<6; y++) {
			for (int x =0; x<6; x++) {
				var list = new List<int> (6){0,1,2,3,4,5};
				ClearList (generated, list, x, y);
				if (list.Count == 0) {
					var dy = Mathf.FloorToInt (y / 2f);
					var clear = dy * 12;
					for (int i = clear; i<36; i++) {
						generated [i] = -1;
					}
					x = -1;
					y = dy * 2;
				} else {
					var random = Random.Range (0, list.Count);
					generated [y * 6 + x] = list [random];
				}
			}
		}
		return generated;
	}

	public void ClearList (int[] generated, List<int> list, int x, int y)
	{
		var dy = Mathf.FloorToInt (y / 2f);
		var dx = Mathf.FloorToInt (x / 3f);

		for (int i = 0; i < 6; i++) {	
			list.Remove (generated [y * 6 + i]);
			list.Remove (generated [x + 6 * i]);
		}
		
		for (int j = 0; j < 2; j++) {
			for (int k = 0; k < 3; k++) {
				list.Remove (generated [((dy * 12) + (dx * 3)) + k + (j * 6)]);
			}
		}
	}

	public void Sifirla ()
	{
		for (int i =0; i<36; i++) {
			dices [i].Locked = false;
			dices [i].Pinned = false;
			dices [i]._value = -1;
		}
	}

	public void CheckBoard ()
	{
		var istrue = ControlBoard ();
		if (istrue) {
			Sifirla ();
			SudokuGenerate ();
		}
	}
	
	private bool ControlBoard ()
	{
		for (int k = 0; k < 6; k++) {	
			for (int i = 0; i < 6; i++) {
				bool horizontalCheck = false;
				bool verticalCheck = false;
				for (int j = 0; j < 6; j++) {
					if (dices [j * 6 + k].Value == i)
						horizontalCheck = true;
					if (dices [k * 6 + j].Value == i)
						verticalCheck = true;
				}
				if (!horizontalCheck || !verticalCheck)
					return false;
			}
		}
		for (int k = 0; k < 3; k++) {
			for (int j = 0; j < 2; j++) {
				for (int i = 0; i < 6; i++) {
					bool boxCheck = false;
					for (int l = 0; l < 3; l++) {
						for (int m = 0; m < 2; m++) {
							if (dices [k * 12 + (j * 3 + (m * 6 + l))].Value == i)
								boxCheck = true;
						}
					}
					if (!boxCheck)
						return false;
				}
			}
		}
		return true;
	}

	public void beginDrag ()
	{

		pos = Input.mousePosition;
		used = false;
	}
	
	bool used = true;

	public void onDrag (int i)
	{
		if (!dices [i].Locked && !dices [i].Pinned) {
			if (!used) {
				Vector2 pos2 = Input.mousePosition;
				Vector2 diff = pos2 - pos;
				float x = (Screen.width) / 24;

				if (Mathf.Abs (diff.x) > x && !used) {
					if (diff.x < 0) {
						dices [i].IsDirectionVertical = false;
						dices [i].TargetAngle = 90;
						dices [i].Value = (dices [i].Value + 1).Mod (6);
						used = true;
					} else if (diff.x > 0) {
						dices [i].IsDirectionVertical = false;
						dices [i].TargetAngle = -90;
						dices [i].Value = (dices [i].Value - 1).Mod (6);
						used = true;
					}
				} else if (Mathf.Abs (diff.y) > x && !used) {
					if (diff.y > 0) {
						dices [i].IsDirectionVertical = true;
						dices [i].TargetAngle = 90;
						dices [i].Value = (dices [i].Value - 1).Mod (6);
						used = true;
					} else if (diff.y < 0) {
						dices [i].IsDirectionVertical = true;
						dices [i].TargetAngle = -90;
						dices [i].Value = (dices [i].Value + 1).Mod (6);
						used = true;
					}
				}
			}
		}
	}

	private float tempClickTime;
	private int previousClick = -1;

	public void DoubleClick (int i)
	{
		if (dices [i].Locked)
			return;
		if (previousClick == i && (Time.time - tempClickTime) < 0.3f) {

			lockClick.Play ();	
			dices [i].Pinned = ! dices [i].Pinned;
		} else {
			tempClickTime = Time.time;
			previousClick = i;
		}
	}

	public void ToMinutes (int i)
	{
		selectedLevel = i;
		switch (i) {
		case 0:
			level = 18;
			break;
		case 1:
			level = 22;
			break;
		case 2:
			level = 24;
			break;

		}

		sudoku.SetActive (false);
		levels.SetActive (false);
		minutes.SetActive (true);
		voice.SetActive (true);
		panels.SetActive (false);

	}

	public void ToSudoku (int j)
	{
		selectedMinute = j;
		switch (j) {
		case 0:
			minute = 10;
			break;
		case 1:
			minute = 20;
			break;
		case 2:
			minute = 30;
			break;
		}
		sudoku.SetActive (true);
		levels.SetActive (false);
		minutes.SetActive (false);
		voice.SetActive (false);
		voice2.SetActive (true);
		zaman.SetActive (true);
		panels.SetActive (true);

		StartSudoku ();
		achievement.volume = 0;

	}

	public void ToLevels ()
	{
		sudoku.SetActive (false);
		levels.SetActive (true);
		minutes.SetActive (false);
		menu.SetActive (false);
		voice.SetActive (true);	
		panels.SetActive (false);
		leaderBoard.SetActive (false);
		
	}

	public void ToMenu ()
	{
		sudoku.SetActive (false);
		levels.SetActive (false);
		minutes.SetActive (false);
		menu.SetActive (true);
		voice.SetActive (true);
		panels.SetActive (false);
		backButton3.SetActive (false);
		tutorialText.SetActive (false);
		leaderboardLevels.SetActive (false);
		leaderBoard.SetActive (true);

	}

	public void ToHowToPlay ()
	{
		sudoku.SetActive (false);
		levels.SetActive (false);
		minutes.SetActive (false);
		menu.SetActive (false);
		voice.SetActive (true);	
		panels.SetActive (false);
		backButton3.SetActive (true);
		tutorialText.SetActive (true);
		leaderBoard.SetActive (false);
	}

	public void ToLeaderboards(){

		leaderboardLevels.SetActive (true);
		menu.SetActive (false);
		leaderBoard.SetActive (false);
		easyBoards.SetActive (false);
		mediumBoards.SetActive (false);
		hardBoards.SetActive (false);

	}

	public void ToEasyBoards(){
		selectedLevel = 0;
		easyBoards.SetActive (true);
		leaderboardLevels.SetActive (false);
	}
	public void ToMediumBoards(){
		selectedLevel = 1;
		mediumBoards.SetActive (true);
		leaderboardLevels.SetActive (false);
	}
	public void ToHardBoards(){
		selectedLevel = 2;
		hardBoards.SetActive (true);
		leaderboardLevels.SetActive (false);
	}

	public void FromEndToMenu ()
	{

		Application.LoadLevel ("MainScene");
	}

	public void StartSudoku ()
	{
		count = -1;
		SudokuGenerate ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		var ahmet = GameObject.FindObjectOfType<CountTime> ();
		ahmet.started = true;
		ahmet.min = minute;
	}

	public void SudokuGenerate ()
	{
		var generated = Genisys ();
		var removed = RemoveNumbers (generated);
		for (int i =0; i<36; i++) {
			dices [i]._value = removed [i];
		}
		for (int i =0; i<36; i++) {
			dices [i].AwakeMe ();
		}
		StartCoroutine (WaitSeconds ());

	}
	
	bool sound = true;

	public void SoundOpen ()
	{
		if (sound) {
			AudioListener.volume = 0;
			soundButton.image.sprite = newSprite;
			soundButton2.image.sprite = newSprite;
			sound = false;
		} else {
			AudioListener.volume = 1;
			soundButton.image.sprite = oldSprite;
			soundButton2.image.sprite = oldSprite;
			sound = true;
		}
	}

	public void NextSudoku ()
	{

		panels.SetActive (false);
		sudoku.SetActive (false);
		score.SetActive (false);
		loading.SetActive (true);
		EasyLevelText.SetActive (false);
		MediumLevelText.SetActive (false);
		HardLevelText.SetActive (false);
		
	}

	IEnumerator WaitSeconds ()
	{
		count++;
		Debug.Log (count);
		NextSudoku ();
		var eray = GameObject.FindObjectOfType<CountTime> ();
		eray.stopped = false;
		eray.mytext = newText;
		newText.text = string.Format ("R E M A I N I N G  T I M E = " + ((int)eray.min).ToIntString (2) + " : " + ((int)eray.second).ToIntString (2));
		achievement.volume = 0.2f;	
		achievement.Play ();
		finishTime = Time.time;
		if (count > 0) {
			CalculatePoints ();
			AchievementScript.Instance.SetAchievement ("Newbie", 1);
			AchievementScript.Instance.SetAchievement ("Veteran", AchievementScript.Instance.GetProgress ("Veteran") + 1);
			AchievementScript.Instance.SetAchievement ("Master", AchievementScript.Instance.GetProgress ("Master") + 1);
			AchievementScript.Instance.SetAchievement ("Itself", AchievementScript.Instance.GetProgress ("Itself") + 1);
			passingTime = finishTime - startTime;
			if (passingTime <= 60) {
				if (selectedLevel == 0) {
					AchievementScript.Instance.SetAchievement ("EasyFast", 1);
				} else if (selectedLevel == 1) {
					AchievementScript.Instance.SetAchievement ("MediumFast", 1);
				} else {
					AchievementScript.Instance.SetAchievement ("HardFast", 1);
				}
			}
		}
		yield return new WaitForSeconds (2);
		
		startTime = Time.time;
		panels.SetActive (true); 
		sudoku.SetActive (true);
		score.SetActive (true);
		LevelTextAppears ();
		loading.SetActive (false);
		var banu = GameObject.FindObjectOfType<CountTime> ();
		completed.Play ();
		banu.stopped = true;
		newText = banu.mytext;
		banu.mytext.text = string.Format ("T I M E = " + ((int)banu.min).ToIntString (2) + " : " + ((int)banu.second).ToIntString (2));
	}

	public void LevelTextAppears ()
	{
		if (level == 18) {
			EasyLevelText.SetActive (true);
		}
		if (level == 22) {
			MediumLevelText.SetActive (true);
		}
		if (level == 24) {
			HardLevelText.SetActive (true);
		}
	}

	public void CalculatePoints ()
	{
		passingTime = Time.time - startTime;
		if (level == 18) {
			passTime1 = 90 - (int)passingTime;
			if (passTime1 < 0) {
				passTime1 = 0;
			}
		}
		if (level == 22) {
			passTime1 = 150 - (int)passingTime;
			if (passTime1 < 0) {
				passTime1 = 0;
			}
		}
		if (level == 24) {
			passTime1 = 210 - (int)passingTime;
			if (passTime1 < 0) {
				passTime1 = 0;
			}
		}
		var total2 = ((10 + passTime1) * ((selectedLevel+1) * count));
		Debug.Log ("Selected level =" + selectedLevel);
		Debug.Log ("Passing time = " + passingTime);
		Debug.Log ("Pass Time 1 =" + passTime1);
		Debug.Log ("Total =" + total);
		Debug.Log ("Total 2 =" + total2);

		total += total2; 
		var ek = GetComponent<Score> ();
		ek.NewFunction (total);

	}

	public void LogIn ()
	{
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.InitializeInstance (config);
		((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI (leaderBoardsID [0, 0]);
		Social.localUser.Authenticate ((bool success) =>
		{
			if (success) {
				Debug.Log ("Login Sucess");
				achievements.SetActive (true);
			} else {
				Debug.Log ("Login failed");
			}
		});

	}

	public void OnShowLeaderBoard (int a)
	{
		selectedMinute = a;
		((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (leaderBoardsID [selectedLevel, selectedMinute]);
	}

	public void OnAddScoreToLeaderBoard ()
	{
		if (Social.localUser.authenticated) {
			Social.ReportScore (total, leaderBoardsID [selectedLevel, selectedMinute], (bool success) =>
			{
				if (!success) {
					Debug.Log ("Add Score Fail");
				}
			});
		}
	}

	public void OnShowAchievements ()
	{
		if (Social.localUser.authenticated) {
			Social.ShowAchievementsUI ();
		}
	}
}