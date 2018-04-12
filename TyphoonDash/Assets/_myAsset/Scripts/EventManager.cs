using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Class that is used for the events of the UI of the home scene
 * These functions are all connected to different buttons of the UI
*/
public class EventManager : MonoBehaviour
{

	private GameManager GM;

	void Awake ()
	{
		GM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		//Debug.Log ("awake EM");
	}

	public void play () //load the game scene
	{
		SceneManager.LoadScene (1);
	}

	public void loginUser () //checks and verify if user login correctly
	{
		TMP_InputField userInput = GameObject.Find ("UsernameInput").GetComponent<TMP_InputField> ();
		TMP_InputField pwInput = GameObject.Find ("passwordInput").GetComponent<TMP_InputField> ();

		bool valid = GM.DB.loginAcc (userInput.text, pwInput.text);
		if (valid) { 
			GM.DB.readProfile ();
			setProfileBoard ();
			GM.Login.SetActive (false);
			GM.ProfBoard.SetActive (true);
			//Debug.Log (" login"); //show error msg setactive
		} else {
			//Debug.Log ("error login"); //show error msg setactive
			GM.sysMsg.color = Color.red;
			GM.sysMsg.text = "Error, Incorrect Input";
			Invoke ("deActiveSysMsg", 3);
			pwInput.text = "";
		}
	}

	public void createAccount () //sets and check if the creation of new account is valid.
	{
		TMP_InputField userInput = GameObject.Find ("UsernameInput").GetComponent<TMP_InputField> ();
		TMP_InputField pwInput = GameObject.Find ("passwordInput").GetComponent<TMP_InputField> ();
		TMP_InputField chInput = GameObject.Find ("charnameInput").GetComponent<TMP_InputField> ();

		bool valid = GM.DB.newAccount (userInput.text, pwInput.text, chInput.text);
		if (valid) { 
			//return to login with success message
			//Debug.Log ("new acc cr8ed"); 
			GM.sysMsg.color = Color.yellow;
			GM.sysMsg.text = "Success, Please Login";
			userInput.text = "";
			pwInput.text = "";
			chInput.text = "";
			Invoke ("deActiveSysMsg", 3);
			GM.newACC.SetActive (false);
			GM.Login.SetActive (true);
		} else {
			//Debug.Log ("error cr8 acc"); //show error msg setactive
			GM.sysMsg.color = Color.red;
			GM.sysMsg.text = "Error, Incorrect Input";
			Invoke ("deActiveSysMsg", 3);
			userInput.text = "";
			pwInput.text = "";
			chInput.text = "";
		}

	}

	public void newChara () //checks if the new character name is valid and create that profile if valid.
	{
		TMP_InputField chInput = GameObject.Find ("charnameInput").GetComponent<TMP_InputField> ();

		//limits profile per account to 3
		if (GM.DB.charCount < 3) {
			bool valid = GM.DB.newCharProfile (chInput.text);
			if (valid) {
				//Debug.Log ("new prof cr8ed"); 
				GM.sysMsg.color = Color.yellow;
				GM.sysMsg.text = "Success Create Profile";
				Invoke ("deActiveSysMsg", 3);
				setProfileBoard ();
				GM.newProf.SetActive (false);
				GM.ProfBoard.SetActive (true);
			} else {
				GM.sysMsg.color = Color.red;
				GM.sysMsg.text = "Name already taken";
				Invoke ("deActiveSysMsg", 3);
				chInput.text = "";
			}
		} else {
			GM.sysMsg.color = Color.red;
			GM.sysMsg.text = "Exceeds Profile Limit";
			Invoke ("deActiveSysMsg", 3);
			chInput.text = "";
		}
	}

	public void clickedProfile () //sets up the profile fields selected in the profile screen
	{
		//takes the character name of the selected button
		string cname = EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text;
		float score = float.Parse (EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text);
		int coin = int.Parse (EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text);
		int pu1 = int.Parse (EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild (3).GetComponent<TextMeshProUGUI> ().text);
		int pu2 = int.Parse (EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild (4).GetComponent<TextMeshProUGUI> ().text);
		GM.DB.charname = cname;
		GM.DB.currHighScore = score;
		GM.DB.currCoins = coin;
		GM.DB.pu1Count = pu1;
		GM.DB.pu2Count = pu2;
		//Debug.Log ("clicked prof details:" + GM.DB.charname + " " + GM.DB.currHighScore + " " + GM.DB.currCoins + " " + GM.DB.pu1Count + " " + GM.DB.pu2Count);
	}

	public void clickedBuyButton () //This handles the shop when buying power up
	{
		int cost = 5;
		string btname = EventSystem.current.currentSelectedGameObject.name;
		if (btname == "pu1button") {
			if (GM.DB.currCoins < cost) { //if not enough coins to buy power up 1
				GM.sysMsg.color = Color.red;
				GM.sysMsg.text = "Balance,not enough";
				Invoke ("deActiveSysMsg", 3);
			} else { //add power up and update database
				GM.DB.pu1Count++;
				GM.DB.currCoins -= cost;
				GM.DB.updateProfile ();
			}
		}

		if (btname == "pu2button") { //if not enough coins to buy power up 2
			if (GM.DB.currCoins < cost) {
				GM.sysMsg.color = Color.red;
				GM.sysMsg.text = "Balance,not enough";
				Invoke ("deActiveSysMsg", 3);
			} else { //add power up and update database
				GM.DB.pu2Count++;
				GM.DB.currCoins -= cost;
				GM.DB.updateProfile ();
			}
		}
	}

	public void delChara () //deletes profile by running the database delete profile function
	{
		if (GM.DB.charname == null) {
			//Debug.Log ("null choose prof");
			GM.sysMsg.color = Color.red;
			GM.sysMsg.text = "Select Character";
			Invoke ("deActiveSysMsg", 3);
		} else {
			GM.DB.deleteProfile ();
			setProfileBoard ();
			//Debug.Log ("del prof s");
		}
	}

	public void delAcc () //deletes accounts by running the Database delete function
	{
		GM.DB.deleteAccount ();
		GM.reset ();
		GM.Login.SetActive (true);
		GM.MainMenu.SetActive (false);
	}

	public void loadProf () //activates the main menu screen after loading the profile chosen
	{
		if (GM.DB.charname == null) {
			//Debug.Log ("null choose prof");
			GM.sysMsg.color = Color.red;
			GM.sysMsg.text = "Select Character";
			Invoke ("deActiveSysMsg", 3);
		} else {
			GM.ProfBoard.SetActive (false);
			GM.MainMenu.SetActive (true);
		}
	}

	public void LogOut () //resets the necessary fields. and shows the log in screen again
	{
		GM.reset ();
		GM.ProfBoard.SetActive (false);
		GM.MainMenu.SetActive (false);
		GM.Login.SetActive (true);
	}

	private void deActiveSysMsg () //this deactivates the system error message
	{
		GM.sysMsg.text = "";
	}

	public void setProfileBoard () //this sets all the profile data of the account from the database into the profile screen
	{

		//clears all data first by destroying the previous list
		Transform temp = GM.ProfBoard.transform.GetChild (4).transform;
		for (int i = 0; i < temp.childCount; i++) {
			Destroy (temp.GetChild (i).gameObject);
		}

		//populate profile board
		for (int i = 0; i < GM.DB.profList.Count; i++) {
			//instantiate the profile UI look button
			GameObject tmpObj = Instantiate (GM.profDataPrefab);
			//gets profile details in the  profilelist variable which came from the database
			tmpObj.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text = GM.DB.profList [i].CharName;
			tmpObj.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = GM.DB.profList [i].Score;
			tmpObj.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = GM.DB.profList [i].Coins.ToString ();
			tmpObj.transform.GetChild (3).GetComponent<TextMeshProUGUI> ().text = GM.DB.profList [i].PU1.ToString ();
			tmpObj.transform.GetChild (4).GetComponent<TextMeshProUGUI> ().text = GM.DB.profList [i].PU2.ToString ();
			//sets the button clickable with on click event to verify the selected profile
			tmpObj.transform.SetParent (GM.ProfBoard.transform.GetChild (4).transform);
			tmpObj.GetComponent<Button> ().onClick.AddListener(clickedProfile);
			tmpObj.GetComponent<RectTransform> ().localScale = new Vector3 (0.45f, 1.35f, 1);
		
		}
	}
		
}
