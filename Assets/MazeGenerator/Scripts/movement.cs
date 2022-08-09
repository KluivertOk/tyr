using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class movement : MonoBehaviour
{

	//this creates options in the ball component to has a camera, sounds and texts
	public GameObject ViewCamera = null;
	public AudioClip JumpSound = null;
	public AudioClip CoinSound = null;
	public TextMeshProUGUI scoreText;

	private Rigidbody mRigidBody = null;
	private int score;
	private AudioSource PlayAudio = null;

	//here i am calling some of the objects created above
	void Start()
	{
		mRigidBody = GetComponent<Rigidbody>();
		PlayAudio = GetComponent<AudioSource>();
		score = 0;
		UpdateScore(0);
		
	}
	
	//here i make the movement of the ball and add the jump sound
	void FixedUpdate()
	{
		if (mRigidBody != null)
		{
			if (Input.GetButton("Horizontal"))
			{
				mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal") * 10);
			}
			if (Input.GetButton("Vertical"))
			{
				mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical") * 10);
			}
			if (Input.GetButtonDown("Jump"))
			{
				{
					PlayAudio.PlayOneShot(JumpSound);
				}
				mRigidBody.AddForce(Vector3.up * 50);
			}

		}
		//here is where i adust the camera setting
		if (ViewCamera != null)
		{
			Vector3 direction = (Vector3.up * 2 + Vector3.back) * 2;
			RaycastHit hit;
			
			if (Physics.Linecast(transform.position, transform.position + direction, out hit))
			{
				ViewCamera.transform.position = hit.point;
			}
			else
			{
				ViewCamera.transform.position = transform.position + direction;
			}
			ViewCamera.transform.LookAt(transform.position);
		}
	}

	//this is where i make the coin have sound and make it a trigger
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Coin"))
		{
			PlayAudio.PlayOneShot(CoinSound, 1.0f);
			Destroy(other.gameObject);
			UpdateScore(1);
		}
	}

	//this is how i update the score for the coin
	void UpdateScore(int scoreAdd)
	{
		score += scoreAdd;
		scoreText.text = "Score: " + score;
	}

}
