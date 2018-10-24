using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;
	private bool facingRight = true;

	public float speed;
	public float jumpforce;

	//ground check
	private bool isOnGround;
	public Transform groundcheck;
	public float checkRadius;
	public LayerMask allGround;

	public Text countText;
	private int count;

	//audio stuff
	private AudioSource source;
	public AudioClip jumpClip;
	public AudioClip coinClip;
	public AudioClip endOfLevelClip;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		count = 0;
		SetCountText();
	}

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	void FixedUpdate () {

		float moveHorizontal = Input.GetAxis("Horizontal");

		rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

		isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

		Debug.Log(isOnGround);

		if(facingRight == false && moveHorizontal > 0)
		{
			Flip();
		}
		else if(facingRight == true && moveHorizontal < 0)
		{
			Flip();
		}
		if (Input.GetKey ("escape")) 
		{
			Application.Quit ();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("EndOfLevel"))
		{
			float vol = Random.Range (volLowRange, volHighRange);
			source.PlayOneShot (endOfLevelClip);
			other.gameObject.SetActive (false);
		}
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
		if (other.gameObject.CompareTag ("coinBox")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector2 Scaler = transform.localScale;
		Scaler.x = Scaler.x * -1;
		transform.localScale = Scaler;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground" && isOnGround)
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				// rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
				rb2d.velocity = Vector2.up * jumpforce;
				float vol = Random.Range (volLowRange, volHighRange);
				source.PlayOneShot (jumpClip);
			}
		}
	}

	void SetCountText ()
	{
		countText.text = "COINS: " + count.ToString ();
		float vol = Random.Range (volLowRange, volHighRange);
		source.PlayOneShot (coinClip);
	}
}
