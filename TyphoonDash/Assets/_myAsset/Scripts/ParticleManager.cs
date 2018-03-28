using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	public GameObject player;
	private float offset;

	// Use this for initialization
	void Start () {
		offset = transform.position.z - player.transform.position.z;
	}

	// Update is called once per frame
	void Update () {

	}

	//LateUpdate runs after all update is process
	void LateUpdate ()
	{ 
		transform.position = new Vector3(0,5,player.transform.position.z+ offset);
	}
}
