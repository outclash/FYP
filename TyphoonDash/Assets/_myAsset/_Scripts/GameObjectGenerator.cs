using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to generate obstacle objects
 * attached to the empty gameObject that parents this objects
 * obtacles object are rigid body kinematic not dynamic = not reading everytime, = optimisation 
*/
public class GameObjectGenerator : MonoBehaviour
{

	public Transform rock1Prefab;
	public Transform rock2Prefab;
	public Transform rock3Prefab;

	public Transform honeyPrefab; // are coins

	//public Transform BurgerPrefab;
	//public Transform PizzaPrefab;
	private int rndObsObj;
	//randomly pick what obstacle object to instantiate
	public static int obsCount;
	//to be global? increase per distance
	public  static int coinCount;
	//global coin count

	//public  static int pwCount; //global
	//private int rndPwObj; //randomly pick what power up object to instantiate

	void Start ()
	{
		// procedural instantiation of obstacle objects
		for (int i = 0; i < obsCount; i++) {
			rndObsObj = Random.Range (0, 3); 

			// Position range is calculated on base on camer FoV and character movement limit
			Vector3 position = new Vector3 (Random.Range (-4f, 4f) - transform.position.x, Random.Range (3.5f, 6f) - transform.position.y, Random.Range (-42.0f, 40.0f) ); 

			//could add different obstacle besides rock
			if (rndObsObj == 0) {
				Transform go = Instantiate (rock1Prefab, position, rock1Prefab.rotation);
				go.transform.SetParent (transform,false);
			}
			if (rndObsObj == 1) {
				Transform go = Instantiate (rock2Prefab, position, rock2Prefab.rotation);
				go.transform.SetParent (transform,false);
				//go.transform.parent = transform;
			}
			if (rndObsObj == 2) {
				Transform go = Instantiate (rock3Prefab, position, rock3Prefab.rotation);
				go.transform.SetParent (transform,false);
				//go.transform.parent = transform;
			}
		}

		// procedural instantiation of coin objects
		for (int i = 0; i < coinCount; i++) {
			Vector3 position = new Vector3 (Random.Range (-4f, 4f) - transform.position.x, Random.Range (3.5f, 6f) - transform.position.y, Random.Range (-42.0f, 40.0f));

			Transform go = Instantiate (honeyPrefab, position, honeyPrefab.rotation);
			go.transform.SetParent (transform,false);
		}
		//Debug.Log ("GENe OBJs");
//		// procedural instantiation of power up objects
//		for (int i = 0; i < pwCount; i++) {
//			rndPwObj = Random.Range (0, 2); 
//			Vector3 position = new Vector3 (Random.Range (-4.5f, 4.5f), Random.Range (1f, 9f), transform.position.z  + Random.Range (-42.0f, 40.0f));
//
//			if (rndPwObj == 0) {
//				Transform go = Instantiate (BurgerPrefab,  position, BurgerPrefab.rotation) ;
//				go.transform.parent = transform;
//
//			}
//			if (rndPwObj == 1) {
//				Transform go = Instantiate (PizzaPrefab,  position, PizzaPrefab.rotation) ;
//				go.transform.parent = transform;
//			}
//		}
	}
}
