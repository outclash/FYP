using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Class that is used for the events of the UI of the home scene
*/
public class EventManager : MonoBehaviour
{

	private GameManager GM;

	void Awake ()
	{
		GM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		//Debug.Log ("awake EM");
	}

	public void play ()
	{
		SceneManager.LoadScene (1);
	}

	public void loginUser ()
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

	public void createAccount ()
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

	public void newChara ()
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

	public void clickedProfile ()
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

	public void clickedBuyButton ()
	{
		int cost = 5;
		string btname = EventSystem.current.currentSelectedGameObject.name;
		if (btname == "pu1button") {
			if (GM.DB.currCoins < cost) {
				//Debug.Log ("not enough money pu1");
				GM.sysMsg.color = Color.red;
				GM.sysMsg.text = "Balance,not enough";
				Invoke ("deActiveSysMsg", 3);
			} else {
				GM.DB.pu1Count++;
				GM.DB.currCoins -= cost;
				GM.DB.updateProfile ();
				//Debug.Log ("bought pu1");
			}
		}

		if (btname == "pu2button") {
			if (GM.DB.currCoins < cost) {
				//Debug.Log ("not enough money pu2");
				GM.sysMsg.color = Color.red;
				GM.sysMsg.text = "Balance,not enough";
				Invoke ("deActiveSysMsg", 3);
			} else {
				GM.DB.pu2Count++;
				GM.DB.currCoins -= cost;
				GM.DB.updateProfile ();
				//Debug.Log ("bought pu2");
			}
		}
	}

	public void delChara ()
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

	public void delAcc ()
	{
		GM.DB.deleteAccount ();
		GM.reset ();
		GM.Login.SetActive (true);
		GM.MainMenu.SetActive (false);
	}

	public void loadProf ()
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

	public void LogOut ()
	{
		GM.reset ();
		GM.ProfBoard.SetActive (false);
		GM.MainMenu.SetActive (false);
		GM.Login.SetActive (true);
	}

	private void deActiveSysMsg ()
	{
		GM.sysMsg.text = "";
	}

	public void setProfileBoard ()
	{

		//clears all data first
		Transform temp = GM.ProfBoard.transform.GetChild (4).transform;
		for (int i = 0; i < temp.childCount; i++) {
			Destroy (temp.GetChild (i).gameObject);
		}

		//populate profile board
		for (int i = 0; i < GM.DB.profList.Count; i++) {

			GameObject tmpObj = Instantiate (GM.profDataPrefab);

			tmpObj.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text = GM.DB.profList [i].CharName;
			tmpObj.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = GM.DB.profList [i].Score;
			tmpObj.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = GM.DB.profList [i].Coins.ToString ();
			tmpObj.transform.GetChild (3).GetComponent<TextMeshProUGUI> ().text = GM.DB.profList [i].PU1.ToString ();
			tmpObj.transform.GetChild (4).GetComponent<TextMeshProUGUI> ().text = GM.DB.profList [i].PU2.ToString ();

			tmpObj.transform.SetParent (GM.ProfBoard.transform.GetChild (4).transform);
			tmpObj.GetComponent<Button> ().onClick.AddListener(clickedProfile);
			tmpObj.GetComponent<RectTransform> ().localScale = new Vector3 (0.45f, 1.35f, 1);
			//Debug.Log ("PROFIEL");
		}
	}
		
}
