using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Class to control the player object
 * attached to player gameObject
*/

public class Player : MonoBehaviour
{

	//need to change to kinematic use transformations
	//player fields
	public float speed;
	//change to private later
	public float HorizontalVelocity;
	public float VerticalVelocity;
	public double distance;
	//for computing to inc lvl
	public static double score;
	private int startingPoint;
	private Rigidbody rb;
	private int health;
	public Image heart;
	public Sprite[] spHP;
	private bool isSheild;
	//Prefabs
	//fields to create new road, reference to a Prefab object
	public Transform SkyRoadPrefab1;
	public Transform SkyRoadPrefab2;
	private int skies;
	//checks if what sky is gonna be instanstiated

	//level increase fields
	bool obsMaxCount;
	int lvlMultiplier;
	int dis1KMultiplier;
	int dis5HMultiplier;
	float incSpeed;

	void Awake ()
	{ //reference and initialise even if the script is not yet enabled.
		rb = GetComponent<Rigidbody> ();
		GameObjectGenerator.pwCount = 3;
		GameObjectGenerator.obsCount = 10;
	}

	// Use this for initialization
	void Start ()
	{
		health = 3;
		healthSetup ();
		speed = 10f;
		HorizontalVelocity = 0;
		VerticalVelocity = 0;
		distance = 0;
		startingPoint = 47;
		skies = 1;
		obsMaxCount = false;
		lvlMultiplier = 1;
		dis1KMultiplier = 1;
		dis5HMultiplier = 1;
		incSpeed = 1f;
		isSheild = false;
	}

	// Update is called once per frame
	void Update ()
	{

		healthSetup ();
		distance = transform.position.z + startingPoint;
		increaseLevel ();

		rb.velocity = new Vector3 (HorizontalVelocity, VerticalVelocity, speed);

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			HorizontalVelocity = -3f;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			HorizontalVelocity = 3f;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			VerticalVelocity = 3f;
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			VerticalVelocity = -3f;
		}
	}

	//all triggers here
	void OnTriggerEnter (Collider other)
	{

		//create new road
		if (other.gameObject.tag == "CreateRoad") {
			if (skies == 1) {
				Instantiate (SkyRoadPrefab2, new Vector3 (11.49982f, 3.754295f, other.transform.parent.position.z + 100f), SkyRoadPrefab2.rotation);
				skies = 2;
			} else {
				Instantiate (SkyRoadPrefab1, new Vector3 (11.49982f, 3.754295f, other.transform.parent.position.z + 100f), SkyRoadPrefab1.rotation);
				skies = 1;
			}

		}

		//destroy previous road
		if (other.gameObject.tag == "DestroyRoad") {

			Destroy (other.transform.parent.gameObject); //destroy the parent object in which this object is attached
		}
	}

	//add collisions here
	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Danger") {
			if (isSheild) {
				Destroy (other.gameObject);
				isSheild = false;
			} 
			else {
				health--;
			}

		}
		if (other.gameObject.tag == "PowerUps") {
			if (other.gameObject.name == "Burger(Clone)") {
				health++;
				Destroy (other.gameObject);
			}
			if (other.gameObject.name == "Pizza(Clone)") {
				isSheild = true;
				//health = 1;
				Destroy (other.gameObject);
			}
		}
	}

	//set up hptext HUD
	void healthSetup ()
	{
		if (health >= 3) { //max health cannot exceed 3
			health = 3; 
		}
		if (health <= 0) {
			score = distance;
			SceneManager.LoadScene (2);
		}
		heart.sprite = spHP[health-1];
	}

	void increaseLevel ()
	{
		const int maxSpeed = 100;
		long lvlDistance = 30; //set up for demo, change to 250 later
		int dis1000 = 90; //set for demo, change to 1000 later
		int dis500 = 30; //set for demo. change to 500 later

		if (distance > (lvlDistance * lvlMultiplier)) {
			GameObjectGenerator.pwCount = Random.Range (1, 4); //power up count change every lvlDistance reach
			long lvlDist = lvlDistance * lvlMultiplier;
			speed += incSpeed;
			lvlMultiplier++;

			Debug.Log (lvlDist+ "  distance reach inc speed");
			if (speed >= maxSpeed) { //checks if max speed exceed
				speed = maxSpeed;
			}

			if (lvlDist == (dis1000 * dis1KMultiplier)) { //incSpeed increaser per certain lvlDistance reach
				incSpeed += 0.5f;
				dis1KMultiplier++;
				Debug.Log ("Enter inc incSpeed by.5 every +90dis");
			}


			if (lvlDist == (dis500 * dis5HMultiplier) && !obsMaxCount) { //Obstacle count per certain lvlDistance reach
				GameObjectGenerator.obsCount += Random.Range (1, 6);
				dis5HMultiplier++;
				Debug.Log ("enter add obs every +30dis :" + GameObjectGenerator.obsCount + "obs present");
				if (GameObjectGenerator.obsCount >= 30) {
					GameObjectGenerator.obsCount = 30;
					obsMaxCount = true;
				}
			}
		}

	}
}