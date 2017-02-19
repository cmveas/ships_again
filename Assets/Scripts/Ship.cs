using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	public float mSpeed;
	public GameObject mCamera;

	float mMaxX;

	float mMinX;

	float mMaxY;

	float mMinY;

	// Use this for initialization
	void Start () {
		mMaxX = 11f;
		mMinX = -11f;
		mMaxY = 10f;
		mMinY = -10f;
	}


	
	// Update is clled once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical,0f );
		var rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.velocity = movement * mSpeed;
		
		rigidBody.position = new Vector3
		(
				Mathf.Clamp(rigidBody.position.x,mMinX,mMaxX) ,
				Mathf.Clamp(rigidBody.position.y,mMinY,mMaxY), 
				0f
		);

	//	GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	 
	}


}
