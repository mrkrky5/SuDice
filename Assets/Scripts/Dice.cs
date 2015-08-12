using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Dice : MonoBehaviour {

	private const int ON = 4;
	private const int SAG = 2;
	private const int SOL = 5;
	private const int UST = 3;
	private const int ALT = 1;

	private MeshRenderer rend;

	public AudioSource diceSound;

	private int dakika;

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
				rend.materials[4].color=new Color(0.717f,0.776f,0.545f);
				rend.materials[0].color=new Color(0.717f,0.776f,0.545f);
			}
			else{
				rend.materials[4].color=new Color (1,1,1);
			    rend.materials[0].color=new Color(1,1,1);
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
				rend.materials[4].color=new Color(0.6f,0.9f,0.8f);
				rend.materials[0].color=new Color(0.6f,0.9f,0.8f);
			}			
			else
			{
				rend.materials[4].color=new Color (1,1,1);
				rend.materials[0].color=new Color(1,1,1);
			}		}
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

	// Use this for initialization
	 public void AwakeMe(){
		rend = GetComponent<MeshRenderer> ();
		if (Value == -1) {
			_value = Random.Range (0, 6);
		} 
		else {
			Locked=true;
		}
			Set ();
	}

	private void Set(){
		rend.materials [ON].mainTexture = MainScript.Instance.textures [Value];
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void PreSet()
	{
		if (IsDirectionVertical) {
			if (TargetAngle > 0) {
				rend.materials [ALT].mainTexture = MainScript.Instance.textures [Value];
			} else {
				rend.materials [UST].mainTexture = MainScript.Instance.textures [Value];
			}
		} else {
			if(TargetAngle>0)
			{
				rend.materials [SAG].mainTexture = MainScript.Instance.textures [Value];
			}
			else
			{
				rend.materials [SOL].mainTexture = MainScript.Instance.textures [Value];
			}
		}
		PlayAnimation ();
	}

	private void PlayAnimation()
	{
		IsAnimationPlaying = true;
		TempTime = Time.time;
		diceSound.Play ();
	}
}
