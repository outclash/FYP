using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GameEventManager : MonoBehaviour {

	public DBManager DB;
	public Player player;
	public Button pu1bt;
	public Button pu2bt;
	public TextMeshProUGUI pu1txt;
	public TextMeshProUGUI pu2txt;
	public TextMeshProUGUI honeytxt;

	void Start () {
		DB = GameObject.Find ("DBManager").GetComponent<DBManager> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		pu1bt = GameObject.Find ("pu1").GetComponent<Button>();
		pu2bt = GameObject.Find ("pu2").GetComponent<Button>();
		pu1txt = GameObject.Find ("pu1count").GetComponent<TextMeshProUGUI>();
		pu2txt = GameObject.Find ("pu2count").GetComponent<TextMeshProUGUI>();
		honeytxt = GameObject.Find ("honeycount").GetComponent<TextMeshProUGUI>();
		Debug.Log ("GEM ");
	}

	void Update(){

		pu1txt.text = DB.pu1Count.ToString();
		pu2txt.text = DB.pu2Count.ToString();
		honeytxt.text = DB.coinGain.ToString();
		if (DB.pu1Count <= 0 || player.isBoost) { //or on speed boost
			pu1bt.interactable = !pu1bt.interactable;

		} else {
			pu1bt.interactable = pu1bt.interactable;
		}
		if (DB.pu2Count <= 0 || player.isShield) { //or p shield is true
			pu2bt.interactable = !pu2bt.interactable;

		} else {
			pu2bt.interactable = pu2bt.interactable;
		}

	}
	public void clickedpowUp ()
	{
		//returns selected button name
		string name = EventSystem.current.currentSelectedGameObject.name;

		if (name == "pu1") {
			DB.pu1Count--;
			player.pu1Active ();
			//player() fn to boost
		}
		if (name == "pu2") {
			DB.pu2Count--;
			player.pu2Active ();
		}
		Debug.Log ("clicked powup");
	}
}
