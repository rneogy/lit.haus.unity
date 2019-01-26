using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomController : NetworkBehaviour
{

    private Animator animator;

    public float burnDuration = 10f;
    [SyncVar]
    public bool burning = false;
    [SyncVar]
    private bool burnt = false;
    [SyncVar]
    private float burnStartTime = 0f;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Update () {
        if (burning && Time.time - burnStartTime > burnDuration) {
            animator.SetBool("Burnt", true);
            burnt = true;
        }
        animator.SetBool("Burning", burning);
        animator.SetBool("Burnt", burnt);
    }

    public bool CanSetOnFire() {
        return !(burning || burnt);
    }


    public bool SetOnFire() {
        print("checking to see if we can set fire to this room");
        if (!(burning || burnt)) {
            print("setting fire to this room");
            burning = true;
            burnStartTime = Time.time;
            return true;
        }
        return false;
    }

    public bool CanDouseFire() {
        return burning;
    }

    public bool DouseFire() {
        if (burning) {
            burning = false;
            return true;
        }
        return false;
    }

}
