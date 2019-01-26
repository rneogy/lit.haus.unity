using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterMover : NetworkBehaviour {

	[Range(0,1)]
	public float speed = 0.3f;


	void FixedUpdate () {
		if (isLocalPlayer == true) {
			float x = Input.GetAxis("Horizontal") * speed;
			float y = Input.GetAxis("Vertical") * speed;

			gameObject.transform.Translate(x,y,0);
		}
	}
}
