using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attached to main camera
public class CameraController : MonoBehaviour {

	public GameObject player;
	//private Vector3 offset;
	private float offset;

	// Use this for initialization
	void Start () {
//		offset = transform.position - player.transform.position;
		offset = transform.position.z - player.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//LateUpdate runs after all update is process
	void LateUpdate ()
	{ 
		//transform.position = player.transform.position + offset;
		transform.position = new Vector3(0,5,player.transform.position.z+ offset);
	}
}
	