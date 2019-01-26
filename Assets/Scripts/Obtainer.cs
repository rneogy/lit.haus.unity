using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Obtainer : NetworkBehaviour
{

    void OnTriggerEnter2D(Collider2D c) {
        if (isLocalPlayer == true) {
            if (c.CompareTag("Obtainable")) {
                Destroy(c.gameObject);
            }
        }
    }
}
