using UnityEngine;
using System.Collections;

/**
 * This script defines the playble area, gets the player moving, tilting and shooting.
 * 
 */

// 
// Seralizes the class Boundary to be visible and edited in the inspector,
// as it is not recognized by Unity.
[System.Serializable]

// Boundary class to define the playable area
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	// player movement variables
	public float speed;
	public float tilt;
	public Boundary boundary;

	// shot variables
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float nextFire;

	// Called every frame
	void Update ()
	{
		/**
		 * Condition for shooting: if the button is pushed, a shot is instantiated at the
		 * determined position and triggers the sound associated with the shot.
		 * Also allows us to set the fire rate via the inspector.
		 * 
		 */

		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}

	// Called every fixed framerate frame
	void FixedUpdate ()
	{
		// Get current position, update position and rotation.

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;

		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
	
}