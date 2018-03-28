using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 *Class that is used for the  events of the UI of the end scene 
*/

public class EndEventManager : MonoBehaviour {

	private DBManager DB;
	private TextMeshProUGUI score;
	private TextMeshProUGUI coinearn;

	void Awake(){
		DB = GameObject.Find ("DBManager").GetComponent<DBManager> ();
		score = GameObject.Find ("Score").GetComponent<TextMeshProUGUI> ();
		coinearn = GameObject.Find ("coinsgain").GetComponent<TextMeshProUGUI> ();
	}

	void Start(){
		score.text = DB.currScore.ToString ("F") + "M";
		coinearn.text = DB.coinGain.ToString ();
	}

	public void play(){
		DB.coinGain = 0;
		SceneManager.LoadScene (1);
	}

	public void mainMenu(){
		SceneManager.LoadScene (0);
	}
}
