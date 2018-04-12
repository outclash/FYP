using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/*
 *Class that is used for the  events of the UI of the game scene 
*/
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
	//set ups the reference to objects
		DB = GameObject.Find ("DBManager").GetComponent<DBManager> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		pu1bt = GameObject.Find ("pu1").GetComponent<Button> ();
		pu2bt = GameObject.Find ("pu2").GetComponent<Button> ();
		pu1txt = GameObject.Find ("pu1count").GetComponent<Text> ();
		pu2txt = GameObject.Find ("pu2count").GetComponent<Text> ();
		honeytxt = GameObject.Find ("honeycount").GetComponent<Text> ();
		pausemenu = GameObject.Find ("pausemenu");
		//Debug.Log ("Awake GEM ");
	}

	void Start ()
	{
		//set pause menu scene false which mean game is running
		pausemenu.SetActive (false);
	}

	void Update ()
	{
		//sets the power-ups text count
		pu1txt.text = DB.pu1Count.ToString ();
		pu2txt.text = DB.pu2Count.ToString ();
		honeytxt.text = DB.coinGain.ToString ();
		//sets if the power-ups are interactable when they have value of >0 
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
	
	//function that reduce the power-ups value 
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
		//Debug.Log ("clicked powup:1");
	}
	//function that pause the game 
	public void pause ()
	{
		//player stop moving by player speed = 0
		tmpSpd = player.speed;
		player.speed = 0f;
		pausemenu.SetActive (true);

	}
	//resume game
	public void resume ()
	{
		pausemenu.SetActive (false);
		player.speed = tmpSpd;
	}
	
	//return to main menu
	public void mainMenu ()
	{
		//before returning update Database profile details such as coins collection and power-ups used
		DB.afterGameUpdate ();
		//loads the main menu scene
		SceneManager.LoadScene (0);
	}
}
