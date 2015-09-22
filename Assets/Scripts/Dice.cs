using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Dice : MonoBehaviour {

	public GameObject[] beyazOn = new GameObject[6];
	public GameObject[] pinnedOn = new GameObject[6]; 
	public GameObject[] lockedOn = new GameObject[6]; 
	public GameObject[] beyazUst = new GameObject[6];
	public GameObject[] beyazAlt = new GameObject[6]; 
	public GameObject[] beyazSag = new GameObject[6];
	public GameObject[] beyazSol = new GameObject[6];
	public GameObject zar1;
	public GameObject zar2; 
	public GameObject zar3; 

	public AudioSource diceSound;


	public void Awake()
	{
		zar2.SetActive (false);
		zar3.SetActive (false);
		for (int i=0; i<6; i++) {
			lockedOn[i].SetActive(false);
			beyazOn[i].SetActive(false);
			beyazAlt[i].SetActive(false);
			beyazUst[i].SetActive(false);
			beyazSol[i].SetActive(false);
			beyazSag[i].SetActive(false);
			pinnedOn[i].SetActive(false);
		}
	}

	public int _value = -1;
	public int Value{
		get{return _value;}
		set{
			if(value!=_value && !IsAnimationPlaying)
			{
				_value = value;
				PreSet ();
			}
		}
	}
	private bool locked;
	public bool Locked {
		get{return locked;}
		set{
			locked=value;
			if(value){
				lockedOn[Value].SetActive(true);
				zar3.SetActive(true);
				zar1.SetActive(false);
			}
			else{
				lockedOn[Value].SetActive(false);
				zar3.SetActive(false);
				zar1.SetActive(true);
		}
	}
}
	private bool pinned;
	public bool Pinned {
		get{return pinned;}
		set{
			pinned=value;
			if(value)
			{
				beyazOn[Value].SetActive(false);
				pinnedOn[Value].SetActive(true);
				zar2.SetActive(true);		
			}			
			else
			{
				beyazOn[Value].SetActive(true);
				pinnedOn[Value].SetActive(false);
				zar2.SetActive(false);
			}	
		}
	}
	
	private bool IsAnimationPlaying;
	public float TargetAngle = 0;
	private float TempTime = 0;
	public bool IsDirectionVertical = false;

	void Update()
	{
		if (IsAnimationPlaying)
		{
			if(Time.time-TempTime>0.2f)
			{
				IsAnimationPlaying = false;
				Set();
				Completed();
			}
			else
			{
				var percentage = (Time.time-TempTime)/0.2f;
				transform.localEulerAngles = new Vector3(IsDirectionVertical?Mathf.Lerp(0,TargetAngle,percentage):0,IsDirectionVertical?0:Mathf.Lerp(0,TargetAngle,percentage),0);
			}
		}
	}
	private void Completed()
	{
		MainScript.Instance.CheckBoard ();
	}

	 public void AwakeMe(){
		if (Value == -1) {
			_value = Random.Range (0, 6);
		} 
		else {
			Locked=true;
		}
			Set ();
	}

	private void Set(){
		transform.rotation = Quaternion.Euler (0, 0, 0);
		foreach (var go in beyazOn) {
			go.SetActive(false);
		}
		if (Locked) {
			lockedOn [Value].SetActive (true);
		} else if (Pinned) {
		pinnedOn [Value].SetActive (true);
		} else {
			beyazOn [Value].SetActive (true);
		}
	}

	private void PreSet()
	{
		if (IsDirectionVertical) {
			if (TargetAngle > 0) {
				SetVisible(beyazAlt[Value]);
			} else {
				SetVisible(beyazUst[Value]);			
			}
		} else {
			if(TargetAngle>0)
			{
				SetVisible(beyazSag[Value]);			
			}
			else
			{
				SetVisible(beyazSol[Value]);		
			}
		}
		PlayAnimation ();
	}

	private void SetVisible(GameObject go)
	{
		GameObject[][] gos = new GameObject[6][];
		gos [0] = beyazAlt;
		gos [1] = beyazUst;
		gos [2] = beyazSag;
		gos [3] = beyazSol;
		gos [4] = lockedOn;
		gos [5] = pinnedOn;

		foreach (GameObject[] list in gos) {
		foreach(GameObject gogo in list)
			{
				if(gogo == go)
				{
					gogo.SetActive(true);
				}
				else{
					gogo.SetActive(false);
				}
			}
		}
	}

	private void PlayAnimation()
	{
		IsAnimationPlaying = true;
		TempTime = Time.time;
		diceSound.Play ();
	}
}
