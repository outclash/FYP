using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void Update () {

	}

	//LateUpdate runs after all update is process
	void LateUpdate ()
	{ 
		transform.position = player.transform.position+ offset;
	}
}
