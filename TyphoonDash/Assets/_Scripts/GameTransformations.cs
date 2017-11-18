using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class that transforms the obstacles objects
 * attached to obstacle objects
*/
public class GameTransformations : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//rotates the obstacle objects
		if(gameObject.tag == "Danger"){
			transform.Rotate ((new Vector3 ( Random.Range(1.0f, 50.0f), Random.Range(1.0f, 50.0f), Random.Range(1.0f, 50.0f)) * Time.deltaTime) * Random.Range (1.0f, 10.0f)) ; //Time.deltatime to be smooth and framerate independent
			//size too
		}

		if(gameObject.tag == "PowerUps"){
			transform.Rotate ((new Vector3 ( Random.Range(1.0f, 50.0f), Random.Range(1.0f, 50.0f), Random.Range(1.0f, 50.0f)) * Time.deltaTime) * Random.Range (1.0f, 10.0f)) ; //Time.deltatime to be smooth and framerate independent
			//size too
		}
	}
}
