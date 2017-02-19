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

	string modifyObjectXPosition (Camera camera)
	{
		float width = camera.pixelWidth;
		float height = camera.pixelHeight;

		float startX = camera.transform.position.x;
		float startY = camera.transform.position.y;
		float endX = startX + width;
		float endY = startY + height;

		return correctHorizontally(startX);

	
	}

	string modifyObjectYPosition (Camera camera)
	{
		float width = camera.pixelWidth;
		float height = camera.pixelHeight;

		float startX = camera.transform.position.x;
		float startY = camera.transform.position.y;
		float endX = startX + width;
		float endY = startY + height;

		return correctVertically(startX);

	
	}

	Vector2 reduceSpeedX (Vector2 velocity)
	{
		return new Vector3(velocity.x/2,velocity.y);
	}

	Vector2 reduceSpeedY (Vector2 velocity)
	{
		return new Vector3(velocity.x,velocity.y/2);
	}

	string correctHorizontally (float startX)
	{
		float limit = player.transform.position.x-startX;
		float limitAbs = Mathf.Abs (limit);

		if (mThreshHoldHorizontally >= limitAbs) {
			var rigidBody = GetComponent<Rigidbody2D> ();
			rigidBody.velocity = reduceSpeedX(rigidBody.velocity);
			return "centered";
		} else {
			Vector3 movement = new Vector3 (limit, 0f,0f );
			var rigidBody = GetComponent<Rigidbody2D> ();
			rigidBody.velocity = movement * mSpeed;

			rigidBody.position = new Vector3
		(
					Mathf.Clamp(GetComponent<Camera>().transform.position.x,mMinX,mMaxX) ,
					Mathf.Clamp(GetComponent<Camera>().transform.position.y,mMinY,mMaxY), 
				0f
		);

			return "not centered correcting" + limit;
		}
	}

	string correctVertically (float startY)
	{
		float limit = player.transform.position.y-startY;
		float limitAbs = Mathf.Abs (limit);

		if (mThreshHoldVertically >= limitAbs) {
			var rigidBody = GetComponent<Rigidbody2D> ();
			rigidBody.velocity = reduceSpeedY(rigidBody.velocity);
			return "centered";
		} else {
			Vector3 movement = new Vector3 (0f, limit,0f );
			var rigidBody = GetComponent<Rigidbody2D> ();
			rigidBody.velocity = movement * mSpeed;

			rigidBody.position = new Vector3
		(
				Mathf.Clamp(rigidBody.position.x,mMinX,mMaxX) ,
				Mathf.Clamp(rigidBody.position.y,mMinY,mMaxY), 
				0f
		);

			return "not centered correcting " + limit;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//transform.position = player.transform.position + offset;

		Camera camera = transform.GetComponent<Camera>();
		print("Object is X " + modifyObjectXPosition(camera));
		print("Object is Y " + modifyObjectYPosition(camera));
	}
}
