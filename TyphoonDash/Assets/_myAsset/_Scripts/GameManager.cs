using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

	public TextMeshProUGUI[] scoreBoard;

	private string[] keys = {"Gold","Silver","Bronze"};
	private float[] highScores = new float[3];

	void Awake(){
		
	}

	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();
		for(int i = 0; i < keys.Length; i++) {
			highScores[i] = PlayerPrefs.GetFloat(keys[i],0);
		}
	}


	// Update is called once per frame
	void Update () {
		//set playerpref scoreboard
		for (int i = 0; i < highScores.Length; i++) {
			scoreBoard[i].text = highScores[i].ToString("F") + "M";
		}
	}
}
