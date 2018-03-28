using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Class that instantiate or sets up the application to the correct running form
*/
public class GameManager : MonoBehaviour {

	public DBManager DB;
	public TextMeshProUGUI sysMsg;
	public TextMeshProUGUI pu1txt;
	public TextMeshProUGUI pu2txt;
	public TextMeshProUGUI coinstxt;
	public GameObject Login;
	public GameObject newACC;
	public GameObject MainMenu;
	public GameObject ProfBoard;
	public GameObject newProf;
	public GameObject shop;
	public GameObject profDataPrefab;

	//make db singleton just make thjis every time
	void Awake(){
		DB = GameObject.Find ("DBManager").GetComponent<DBManager> ();
		sysMsg = GameObject.Find ("sysMsg").GetComponent<TextMeshProUGUI>();
		pu1txt = GameObject.Find ("ownpu1").GetComponent<TextMeshProUGUI>();
		pu2txt = GameObject.Find ("ownpu2").GetComponent<TextMeshProUGUI>();
		coinstxt = GameObject.Find ("money").GetComponent<TextMeshProUGUI>();
		Login = GameObject.Find ("Login");
		newACC = GameObject.Find ("NewAccount");
		MainMenu = GameObject.Find ("MainMenu");
		ProfBoard = GameObject.Find ("ProfileBoard");
		newProf = GameObject.Find ("NewChara");
		shop = GameObject.Find ("Shop");
		//Debug.Log ("awake GM");
	}

	void Start () {
		//Debug.Log ("start GM");
		sysMsg.text = "";
		if (DB.logName != null && DB.charname != null) {
			MainMenu.SetActive (true);
			Login.SetActive (false);
			newACC.SetActive (false);
			ProfBoard.SetActive (false);
			newProf.SetActive (false);
			shop.SetActive (false);
			//Debug.Log ("Login and Ready to play");
		} else {
			Login.SetActive (true);
			newACC.SetActive (false);
			ProfBoard.SetActive (false);
			MainMenu.SetActive (false);
			newProf.SetActive (false);
			shop.SetActive (false);
		}

		//Debug.Log (DB.logName+DB.logName);
	}

	void Update(){
		pu1txt.text = DB.pu1Count.ToString();
		pu2txt.text = DB.pu2Count.ToString();
		coinstxt.text = DB.currCoins.ToString ();
	}

	public void reset(){
		DB.logName = null;
		DB.charCount = 0;
		DB.charname = null;
		DB.profList.Clear ();
		Transform temp = ProfBoard.transform.GetChild (4).transform;
		for(int i= 0; i < temp.childCount; i++) {
			Destroy (temp.GetChild(i).gameObject);
		}
	}
}
