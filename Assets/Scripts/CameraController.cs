using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

	public float mThreshHoldHorizontally;
	public float mThreshHoldVertically;

	private Vector3 offset;

	public float mSpeed;

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
		offset = transform.position - player.transform.position;
	}


	Vector2 reduceSpeedX (Vector2 velocity)
	{
		return new Vector3(velocity.x/2,velocity.y);
	}

	Vector2 reduceSpeedY (Vector2 velocity)
	{
		return new Vector3(velocity.x,velocity.y/2);
	}

	string correctPosition(){
		var rigidBody = GetComponent<Rigidbody2D> ();
		float startX = GetComponent<Camera>().transform.position.x;
		float startY = GetComponent<Camera>().transform.position.y;

		float limit = player.transform.position.x-startX;
		float limitAbs = Mathf.Abs (limit);

		var xvelocity = getXVelocity (startX);
		var yvelocity = getYVelocity (startY);

			rigidBody.velocity = new Vector2(xvelocity.x,yvelocity.y);
			rigidBody.position = new Vector3
		(
				Mathf.Clamp(GetComponent<Camera>().transform.position.x,mMinX,mMaxX) ,
				Mathf.Clamp(GetComponent<Camera>().transform.position.y,mMinY,mMaxY)	, 
				0f
		);

		return rigidBody.position.ToString();
	}

	Vector2 getXVelocity (float startX)
	{	
		Vector2 velocityX;
		var rigidBody = GetComponent<Rigidbody2D> ();
		float limit = player.transform.position.x-startX;
		float limitAbs = Mathf.Abs (limit);
		if (mThreshHoldHorizontally >= limitAbs) {
			
			velocityX = reduceSpeedX (rigidBody.velocity);
		} else {
			Vector3 movement = new Vector3 (limit, 0f,0f );

			velocityX = movement * mSpeed;
		}
		return velocityX;
	}

	Vector2 getYVelocity (float startY)
	{	
		Vector2 velocityY;
		var rigidBody = GetComponent<Rigidbody2D> ();
		float limit = player.transform.position.y-startY;
		float limitAbs = Mathf.Abs (limit);

		if (mThreshHoldVertically >= limitAbs) {
			velocityY = reduceSpeedY(rigidBody.velocity);
		} else {
			Vector3 movement = new Vector3 (0f, limit,0f );
			velocityY = movement * mSpeed;
		}
		return velocityY;
	}


	
	// Update is called once per frame
	void LateUpdate () {
		//transform.position = player.transform.position + offset;

		Camera camera = transform.GetComponent<Camera>();
		print("Object is  " + correctPosition());
	}
}
