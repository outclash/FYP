using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to make the camera follow the character
 * attached to main camera
*/
public class CameraController : MonoBehaviour {

	public GameObject player;
	private float offset;

	void Start () {
//		offset = transform.position - player.transform.position;
		offset = transform.position.z - player.transform.position.z;
	}

	//LateUpdate runs after all update is process
	void LateUpdate ()
	{ 
		//transform.position = player.transform.position + offset;
		transform.position = new Vector3(0,5,player.transform.position.z+ offset);
	}
}
	