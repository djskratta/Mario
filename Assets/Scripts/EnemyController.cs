using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	private Animator anim;
	public float speed;
	public LayerMask isGround;
	public Transform wallHitBox;
	private bool wallHit;
	public float wallHitHeight;
	public float wallHitWidth;

	private AudioSource source;
	public AudioClip goombaDeathClip;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;

	void Start () 
	{
		anim = GetComponent<Animator> ();
	}

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	void FixedUpdate () 
	{
		transform.Translate (speed * Time.deltaTime, 0, 0);

		wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);

		if (wallHit == true)
		{
				speed = speed * -1;
		}
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player") 
		{
			float vol = Random.Range (volLowRange, volHighRange);
			source.PlayOneShot (goombaDeathClip);
			anim.SetBool ("isDead", true);
			Debug.Log ("I loved living");
			Destroy (gameObject, 0.25f);
		}
	}
}
