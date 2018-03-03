using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
	//	public TextMeshProUGUI[] scoreBoard;
	//
	//	private string[] keys = {"Gold","Silver","Bronze"};
	//	private float[] highScores = new float[3];
	// Use this for initialization
	void Start () {
		//load the leaderboard files
		//		//PlayerPrefs.DeleteAll ();
		//		for(int i = 0; i < keys.Length; i++) {
		//			highScores[i] = PlayerPrefs.GetFloat(keys[i],0);
		//		}
	}
	
	// Update is called once per frame
	void Update () {
		//		//set playerpref scoreboard
		//		for (int i = 0; i < highScores.Length; i++) {
		//			scoreBoard[i].text = highScores[i].ToString("F") + "M";
		//		}
	}
}

//
//		rb.velocity = new Vector3 (HorizontalVelocity, VerticalVelocity, speed);
//
//		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
//			HorizontalVelocity = -3f;
//		}
//
//		if (Input.GetKeyDown (KeyCode.RightArrow)) {
//			HorizontalVelocity = 3f;
//		}
//
//		if (Input.GetKeyDown (KeyCode.UpArrow)) {
//			VerticalVelocity = 3f;
//		}
//
//		if (Input.GetKeyDown (KeyCode.DownArrow)) {
//			VerticalVelocity = -3f;
//		}

//		//Border Collision = bounce back
//		if (other.gameObject.name == "SkyBottom" || transform.position.y < 0.2f) {
//			rb.AddForce (Random.Range(-borderRicochet,borderRicochet),borderRicochet,0) ;
//		}
//		if (other.gameObject.name == "SkyTop" || transform.position.y > 9.5f) {
//			rb.AddForce (Random.Range(-borderRicochet,borderRicochet),-borderRicochet,0) ;
//		}
//		if (other.gameObject.name == "SkyLeft" || transform.position.x < -4.5f) {
//			rb.AddForce (borderRicochet,Random.Range(-borderRicochet,borderRicochet),0) ;
//		}
//		if (other.gameObject.name == "SkyRight" || transform.position.x > 4.5f) {
//			rb.AddForce (-borderRicochet,Random.Range(-borderRicochet,borderRicochet),0) ;
//		}

//collision
//if (other.gameObject.tag == "PowerUps") {
//	if (other.gameObject.name == "Burger(Clone)") {
//
//		Destroy (other.gameObject);
//	}
//	if (other.gameObject.name == "Pizza(Clone)") {
//		isShield = true;
//		//health = 1;
//		Destroy (other.gameObject);
//	}
//}