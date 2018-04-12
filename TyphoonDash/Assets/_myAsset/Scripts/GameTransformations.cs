using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class that transforms the obstacles objects
 * attached to obstacle objects
 * each objects will have different transformation 
*/
public class GameTransformations : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		//rotates the all obstacle objects
		if(gameObject.tag == "Danger"){
			transform.Rotate ((new Vector3 ( Random.Range(1.0f, 50.0f), Random.Range(1.0f, 50.0f), Random.Range(1.0f, 50.0f)) * Time.deltaTime) * Random.Range (1.0f, 10.0f)) ; //Time.deltatime to be smooth and framerate independent
			transform.localScale = new Vector3 ( Random.Range(0.007f, 0.009f), Random.Range(0.007f, 0.009f), Random.Range(0.007f, 0.009f));
		}
		
		//rotates the all honey objects
		if(gameObject.tag == "honeycoin"){
			transform.Rotate ((new Vector3 ( Random.Range(1.0f, 50.0f), Random.Range(1.0f, 50.0f), Random.Range(1.0f, 50.0f)) * Time.deltaTime) * Random.Range (1.0f, 10.0f)) ; //Time.deltatime to be smooth and framerate independent
		
		}
	}
}
