using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to generate obstacle objects
 * attached to the empty gameObject that parents this objects
 * obtacles object are rigid body kinematic dynamic not read everytime, optimisation 
*/
public class GameObjectGenerator : MonoBehaviour {

	public Transform rockPrefab;
	public Transform binPrefab;
	public Transform shieldPrefab;
	public Transform slowtimePrefab;

	private int rndObsObj; //randomly pick what obstacle object to instantiate
	public static int obsCount; //to be global? increase per distance

	public  static int pwCount; //global 
	private int rndPwObj; //randomly pick what power up object to instantiate

	// Use this for initialization
	void Start () {

		// procedural instantiation of obstacle objects
		for (int i = 0; i < obsCount; i++) {
			rndObsObj = Random.Range (0, 2); 
			Vector3 position = new Vector3 (Random.Range (-4.5f, 4.5f), Random.Range (1f, 9f), transform.position.z  + Random.Range (-42.0f, 40.0f));

			if (rndObsObj == 0) {
				Transform go = Instantiate (rockPrefab,  position, rockPrefab.rotation) ;
				go.transform.parent = transform;

			}
			if (rndObsObj == 1) {
				Transform go = Instantiate (binPrefab,  position, binPrefab.rotation) ;
				go.transform.parent = transform;
			}
		}

		// procedural instantiation of power up objects
		for (int i = 0; i < pwCount; i++) {
			rndPwObj = Random.Range (0, 2); 
			Vector3 position = new Vector3 (Random.Range (-4.5f, 4.5f), Random.Range (1f, 9f), transform.position.z  + Random.Range (-42.0f, 40.0f));

			if (rndPwObj == 0) {
				Transform go = Instantiate (shieldPrefab,  position, shieldPrefab.rotation) ;
				go.transform.parent = transform;

			}
			if (rndPwObj == 1) {
				Transform go = Instantiate (slowtimePrefab,  position, slowtimePrefab.rotation) ;
				go.transform.parent = transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
