using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour {

	private TextMeshProUGUI score;

	// Use this for initialization
	void Awake(){
		score = GetComponent <TextMeshProUGUI> ();
	}
	void Start () {
		score.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		score.text = Player.score.ToString ("F") + "M";
	}
}
