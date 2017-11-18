using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* not using right now
 * Class that will contain all trigger collisions
 * attached to player object
 */
public class GameTriggers : MonoBehaviour {

	//variable to create of new road, reference to a Prefab object
	public Transform SkyRoadPrefab1;
	public Transform SkyRoadPrefab2;
	private int skies; //checks if what sky is gonna be instanstiated 

	// Use this for initialization
	void Start () {
		skies = 1;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){

		//create new road
		if (other.gameObject.tag == "CreateRoad" ) {
			if(skies == 1){
				Instantiate(SkyRoadPrefab2,new Vector3(11.49982f, 3.754295f, other.transform.parent.position.z + 100f),SkyRoadPrefab2.rotation);
				skies = 2;
			}
			else {
				Instantiate(SkyRoadPrefab1,new Vector3(11.49982f, 3.754295f, other.transform.parent.position.z + 100f),SkyRoadPrefab1.rotation);
				skies = 1;
			}

		}

		//destroy previous road
		if (other.gameObject.tag == "DestroyRoad") {

			Destroy(other.transform.parent.gameObject);
		}
	}
}
