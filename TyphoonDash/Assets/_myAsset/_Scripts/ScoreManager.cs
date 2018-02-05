using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour {

	private TextMeshProUGUI score;
//	private string key1 = "Gold";
//	private string key2 = "Silver";
//	private string key3 = "Bronze";
	private string[] keys = {"Gold","Silver","Bronze"};
	private double[] highScores;
//	private double Gsc;
//	private double Ssc;
//	private double Bsc;

	// Use this for initialization
	void Awake(){
		score = GetComponent <TextMeshProUGUI> ();
		for(int i = 0; i < keys.Length; i++) {
			highScores[i] = PlayerPrefs.GetFloat(keys[i]);
		}
	}
	void Start () {
		score.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		score.text = Player.score.ToString ("F") + "M";

		//compare scores 
		double tmp;
		double tmp2;
		for(int i = 0; i < highScores.Length; i++) {
			if (Player.score > highScores [i]) {
				tmp = highScores [i];
				highScores[i] = Player.score;

				for (int j = i+1; i < highScores.Length; j++) {
					tmp2 = highScores [j];
					highScores [j] = tmp;
					tmp = tmp2;
				}
				break;
			} 
		}


		//set playerpref scoreboard
		for (int i = 0; i < highScores.Length; i++) {
			PlayerPrefs.SetFloat(keys[i], highScores [i]);
		}
	}
}
