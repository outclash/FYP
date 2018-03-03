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
	public float distance;
	private float moveSpeed;
	//for computing to inc lvl
	public static float score;
	private Rigidbody rb;
	private int health;
	public Image heart;
	public Sprite[] spHP;
	public bool isShield;
	public bool isBoost;
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

	public DBManager DB;
	private GameObject speedfx;
	private GameObject shieldfx;

	void Awake ()
	{ //reference and initialise even if the script is not yet enabled.
		rb = GetComponent<Rigidbody> ();
		GameObjectGenerator.coinCount = 1;
		GameObjectGenerator.obsCount = 10;
		DB = GameObject.Find ("DBManager").GetComponent<DBManager> ();
		speedfx = GameObject.Find ("Speedfx");
		shieldfx = GameObject.Find ("Bee").transform.GetChild (2).gameObject;
	}

	// Use this for initialization
	void Start ()
	{
		health = 3;
		healthSetup ();
		speed = 10f;
		moveSpeed = 10.0f;
		HorizontalVelocity = 0;
		VerticalVelocity = 0;
		distance = 0;
		skies = 1;
		obsMaxCount = false;
		lvlMultiplier = 1;
		dis1KMultiplier = 1;
		dis5HMultiplier = 1;
		incSpeed = 1f;
		isShield = false;
		isBoost = false;
		speedfx.SetActive (false);
		shieldfx.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{
		healthSetup ();
		distance = transform.position.z;
		increaseLevel ();
	}

	void FixedUpdate ()
	{

		//user for phone accelerometer
		//float accX = Input.acceleration.x;
		//float accY = Input.acceleration.y;
		//Vector3 movement = new Vector3 (moveHorizontal * moveSpeed , moveVertical * moveSpeed, speed);

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal * moveSpeed, moveVertical * moveSpeed, speed);
		rb.velocity = (movement);

		//Border Collision = bounce back
		int borderRicochet = 5000;
		if (transform.position.y < 0) {
			rb.AddForce (Random.Range (-borderRicochet, borderRicochet), borderRicochet, 0);
			//stop player movement after 1.5 second able to move again
			moveSpeed = 0f; 
			Invoke ("moveAgain", 1.5f);
		}
		if (transform.position.y > 10) {
			rb.AddForce (Random.Range (-borderRicochet, borderRicochet), -borderRicochet, 0);
			moveSpeed = 0f;
			Invoke ("moveAgain", 1.5f);
		}
		if (transform.position.x < -5f) {
			rb.AddForce (borderRicochet, Random.Range (-borderRicochet, borderRicochet), 0);
			moveSpeed = 0f;
			Invoke ("moveAgain", 1.5f);
		}
		if (transform.position.x > 5f) {
			rb.AddForce (-borderRicochet, Random.Range (-borderRicochet, borderRicochet), 0);
			moveSpeed = 0f;
			Invoke ("moveAgain", 1.5f);
		}
	}

	private void moveAgain ()
	{ //make the player move again
		moveSpeed = 5.0f;
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
		int obstacleRicochet = 2000;
//		int borderRicochet = 5000;

		if (other.gameObject.tag == "Danger") {
			if (isShield) {
				Destroy (other.gameObject);
				isShield = false;
				shieldfx.SetActive (false);
				//set false active particle
			} else {
				//bounce back if hit obstacle
				rb.AddForce (Random.Range (-obstacleRicochet, obstacleRicochet), Random.Range (-obstacleRicochet, obstacleRicochet), -obstacleRicochet);
				//turn off collider to remove double damage on the same object
				Collider ot = other.gameObject.GetComponent<Collider> ();
				ot.enabled = !ot.enabled;
				health--;
			}
		}

		if (other.gameObject.name == "Honey(Clone)") {
			DB.coinGain++;
			Destroy (other.gameObject);
		}
	}

	public void pu1Active ()
	{
		isBoost = true;
		speed += 5;
		speedfx.SetActive (true);
		Invoke ("speedNorm", 5);
	}

	public void pu2Active ()
	{
		isShield = true;
		shieldfx.SetActive (true);
	}

	private void speedNorm ()
	{ //return speed to normal after 5 seconds of getting burger power up
		isBoost = false;
		speedfx.SetActive (false);
		speed -= 5;
	}

	//set up hptext HUD
	void healthSetup ()
	{
		if (health >= 3) { //max health cannot exceed 3
			health = 3; 
		}
		if (health <= 0) {
			DB.currScore = distance;
			DB.afterGameUpdate ();		//do update DB and check highscoree and coin gain
			SceneManager.LoadScene (2);
		}
		if (health > 0) {
			heart.sprite = spHP [health - 1];
		}
	}

	void increaseLevel ()
	{
		const int maxSpeed = 50;
		int lvlDistance = 30; //set up for demo, change to 250 later
		int dis1000 = 90; //set for demo, change to 1000 later
		int dis500 = 30; //set for demo. change to 500 later

		if (distance > (lvlDistance * lvlMultiplier)) {
			GameObjectGenerator.coinCount = Random.Range (1, 5); //power up count change every lvlDistance reach
			int lvlDist = lvlDistance * lvlMultiplier;
			speed += incSpeed;
			lvlMultiplier++;

			Debug.Log (lvlDist + "  distance reach inc speed");
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