using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUntilEnter : MonoBehaviour
{

    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D c) {
        print("hi");
        if (c.gameObject.CompareTag("Player")) {
            animator.SetBool("Hidden", false);
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.gameObject.CompareTag("Player")) {
            animator.SetBool("Hidden", true);
        }
    }
}
