using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterMover : NetworkBehaviour {

	public float speed = 2f;

	private Rigidbody2D rb2d;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}


	void FixedUpdate () {
		if (isLocalPlayer == true) {
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");

			rb2d.velocity = new Vector2(x,y) * speed;
		}
	}
}
