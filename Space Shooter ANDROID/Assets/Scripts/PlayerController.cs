using UnityEngine;
using System.Collections;

[System.Serializable]
public class Interval
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private AudioSource sound;
	public float speed;
	public Interval boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	
	private float nextFire;
	
	void Update ()
	{
		if(Input.touchCount == 1)
		if (Input.GetTouch(0).phase == TouchPhase.Began  /*Input.GetButton("Fire1")*/ && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			sound.Play ();
		}
	}

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		sound = GetComponent<AudioSource> ();
	}

	void FixedUpdate()
	{
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");
		Vector3 dir = Vector3.zero;
		dir.x = Input.acceleration.x;
		dir.y = Input.acceleration.y;
		if (dir.sqrMagnitude > 1)
			dir.Normalize ();

		Vector3 movement = new Vector3 (dir.x, 0, dir.y);

		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0,
			Mathf.Clamp(rb.position.z,  boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0, 0, rb.velocity.x * -3);
	}
}