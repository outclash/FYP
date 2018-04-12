using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	* Handles the particles wind and rain effect
*/
public class ParticleManager : MonoBehaviour {
	
	//gets the player object as reference
	public GameObject player;
	private float offset;

	// Use this for initialization
	void Start () {
		//offset the position of the particles
		offset = transform.position.z - player.transform.position.z;
	}

	//LateUpdate runs after all update is process
	void LateUpdate ()
	{	
		//updates particles position
		transform.position = new Vector3(0,5,player.transform.position.z+ offset);
	}
}
