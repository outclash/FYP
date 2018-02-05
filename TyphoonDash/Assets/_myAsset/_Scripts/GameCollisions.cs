using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* not using
 * Class that contains all collisions
 * attached to player object
*/
public class GameCollisions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	//add collisions here
	void OnCollisionEnter(Collision other ){
		if(other.gameObject.tag == "Danger"){
		}
		if(other.gameObject.tag == "PowerUps"){
		}
	}
}
