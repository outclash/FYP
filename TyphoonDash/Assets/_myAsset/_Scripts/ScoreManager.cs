using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour {

	private TextMeshProUGUI score;
	private string[] keys = {"Gold","Silver","Bronze"};
	private float[] highScores;
	private bool isDone;
	// Use this for initialization
	void Awake(){
		score = GetComponent <TextMeshProUGUI> ();

	}
	void Start () {
		score.text = "0";
		isDone = false;
		highScores = new float[3];
		for(int i = 0; i < keys.Length; i++) {
			highScores[i] = PlayerPrefs.GetFloat(keys[i],0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		score.text = Player.score.ToString ("F") + "M";

		//compare scores 
		float tmp;
		float tmp2;
		for(int i = 0; i < highScores.Length; i++) {
			if (Player.score > highScores[i] && !isDone) {
				tmp = highScores[i];
				highScores[i] = Player.score;
				isDone = true;

				for (int j = i+1; j < highScores.Length; j++) {
					tmp2 = highScores[j];
					highScores[j] = tmp;
					tmp = tmp2;
				}
			}
		}
		//set playerpref scoreboard
		for (int i = 0; i < highScores.Length; i++) {
			PlayerPrefs.SetFloat(keys[i], highScores [i]);
		}
	}
}
