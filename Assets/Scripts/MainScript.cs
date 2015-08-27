using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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


	private int minute;

	public Text newText;

	public static int level;
	public static int count;

	public AudioSource lockClick;
	public AudioSource music;
	public AudioSource completed;
	public AudioSource achievement;


	public Button soundButton;
	public Button soundButton2;

	public Sprite newSprite;
	public Sprite oldSprite;

	void Awake ()
	{
		Instance = this;
		count = 0;
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
			count++;
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

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	public void ToMinutes (int i)
	{
		switch (i) {
		case 0:
			level = 18;
			break;
		case 1:
			level = 21;
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

	}

	public void StartSudoku ()
	{
		SudokuGenerate ();
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

	}

	IEnumerator WaitSeconds ()
	{
		NextSudoku ();
		var eray = GameObject.FindObjectOfType<CountTime> ();
		eray.stopped = false;
		eray.mytext = newText;
		newText.text = string.Format ("R E M A I N I N G  T I M E = " + ((int)eray.min).ToIntString (2) + " : " + ((int)eray.second).ToIntString (2));
		achievement.volume = 0.2f;	
		achievement.Play ();
		yield return new WaitForSeconds (3);
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

	public void LevelTextAppears(){
		if (level == 18) {
			EasyLevelText.SetActive (true);
		}
		if (level == 21) {
			MediumLevelText.SetActive (true);
		}
		if (level == 24){
			HardLevelText.SetActive (true);
		}
	}
	
}