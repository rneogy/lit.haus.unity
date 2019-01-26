using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HideUntilEnter : MonoBehaviour
{

    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.CompareTag("Player") && c.gameObject.GetComponent<NetworkBehaviour>().isLocalPlayer) {
            animator.SetBool("Hidden", false);
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.gameObject.CompareTag("Player") && c.gameObject.GetComponent<NetworkBehaviour>().isLocalPlayer) {
            animator.SetBool("Hidden", true);
        }
    }
}
