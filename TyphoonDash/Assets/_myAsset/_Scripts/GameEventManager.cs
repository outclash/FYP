﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEventManager : MonoBehaviour
{

	public DBManager DB;
	public Player player;
	public Button pu1bt;
	public Button pu2bt;
	public Text pu1txt;
	public Text pu2txt;
	public Text honeytxt;
	public GameObject pausemenu;
	private float tmpSpd;

	void Awake ()
	{
		DB = GameObject.Find ("DBManager").GetComponent<DBManager> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		pu1bt = GameObject.Find ("pu1").GetComponent<Button> ();
		pu2bt = GameObject.Find ("pu2").GetComponent<Button> ();
		pu1txt = GameObject.Find ("pu1count").GetComponent<Text> ();
		pu2txt = GameObject.Find ("pu2count").GetComponent<Text> ();
		honeytxt = GameObject.Find ("honeycount").GetComponent<Text> ();
		pausemenu = GameObject.Find ("pausemenu");
		Debug.Log ("Awake GEM ");
		DB.pu1Count++; //remove this
		DB.pu2Count++;
	}

	void Start ()
	{
		pausemenu.SetActive (false);
	}

	void Update ()
	{

		pu1txt.text = DB.pu1Count.ToString ();
		pu2txt.text = DB.pu2Count.ToString ();
		honeytxt.text = DB.coinGain.ToString ();
		if (DB.pu1Count <= 0 || player.isBoost) { //or on speed boost
			pu1bt.interactable = false;
			//Debug.Log ("pu1: " + player.isBoost + DB.pu1Count );
		} else {
			pu1bt.interactable = true;
			//Debug.Log ("pu1: ready "); 
		}
		if (DB.pu2Count <= 0 || player.isShield) { //or p shield is true
			pu2bt.interactable = false;
			//Debug.Log ("pu2: " + player.isShield +DB.pu2Count );
		} else {
			pu2bt.interactable = true;
			//Debug.Log ("pu2: ready "); 
		}
	}

	public void clickedpowUp ()
	{
		//returns selected button name
		string name = EventSystem.current.currentSelectedGameObject.name;
		if (name == "pu1") {
			DB.pu1Count--;
			player.pu1Active ();
		}
		if (name == "pu2") {
			DB.pu2Count--;
			player.pu2Active ();
		}
		Debug.Log ("clicked powup:1");
	}

	public void pause ()
	{
		tmpSpd = player.speed;
		player.speed = 0f;
		pausemenu.SetActive (true);

	}

	public void resume ()
	{
		pausemenu.SetActive (false);
		player.speed = tmpSpd;
	}

	public void mainMenu ()
	{
		DB.afterGameUpdate ();
		SceneManager.LoadScene (0);
	}
}
