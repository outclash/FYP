using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to control the player object
 * attached to player object
*/

public class Player : MonoBehaviour {

	//player fields
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public KeyCode moveUp;
	public KeyCode moveDown;
	public float speed;
	public float HorizontalVelocity;
	public float VerticalVelocity;
	public double distance;
	private int startingPoint;
	private Rigidbody rb;

	//Prefabs
	//fields to create new road, reference to a Prefab object
	public Transform SkyRoadPrefab1;
	public Transform SkyRoadPrefab2;
	private int skies; //checks if what sky is gonna be instanstiated 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		speed = 10f;
		HorizontalVelocity = 0;
		VerticalVelocity = 0;
		distance = 0;
		startingPoint = 47;
		skies = 1;
	}

	// Update is called once per frame
	void Update () {

		distance =  transform.position.z + startingPoint;

		rb.velocity = new Vector3 (HorizontalVelocity, VerticalVelocity, speed);

		if (Input.GetKeyDown(moveLeft)) {
			HorizontalVelocity = -5f;
		}

		if (Input.GetKeyDown (moveRight)) {
			HorizontalVelocity = 5f;
		}

		if (Input.GetKeyDown (moveUp)) {
			VerticalVelocity = 5f;
		}

		if (Input.GetKeyDown (moveDown)) {
			VerticalVelocity = -5f;
		}
	}

	//all triggers here
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

	//add collisions here
	void OnCollisionEnter(Collision other ){
		if(other.gameObject.tag == "Danger"){
		}
		if(other.gameObject.tag == "PowerUps"){
		}
	}
}