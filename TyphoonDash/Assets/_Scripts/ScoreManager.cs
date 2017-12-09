using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	Text score;
	// Use this for initialization
	void Awake(){
		
		score = GetComponent<Text> ();

	}
	void Start () {
		score.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		score.text = Player.score.ToString ("F") + "M";
	}
}
