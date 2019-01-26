using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArsonistController : NetworkBehaviour
{

    private int numMatches;

    private RoomController room;

    public Color spriteColor;

    void Awake() {
        numMatches = 0;
    }

    void Update () {
        GetComponentInChildren<SpriteRenderer>().color = spriteColor;
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.LeftShift) && numMatches > 0 && room) {
                CmdSetOnFire();
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                print(isServer + " " + numMatches);
            }
        }
    }

    void setMatches(int matches) {
        numMatches = matches;
    }


    [Command]
    void CmdSetOnFire() {
        if (numMatches > 0 && room.SetOnFire()) {
            setMatches(numMatches - 1);
            TargetSetMatches(connectionToClient, numMatches);
        }
    }

    [TargetRpc]
    public void TargetSetMatches(NetworkConnection target, int matches) {
        setMatches(matches);
    }


    [Command]
    void CmdPickupMatches(GameObject g) {
        Destroy(g);
        setMatches(numMatches + 1);
        TargetSetMatches(connectionToClient, numMatches);
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (isLocalPlayer && this.enabled) {
            if (c.CompareTag("Match")) {
                CmdPickupMatches(c.gameObject);
            }
        }
        if (c.CompareTag("Room")) {
            room = c.GetComponent<RoomController>();
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.CompareTag("Room")) {
            room = null;
        }
    }

    public int getNumMatches() {
        return numMatches;
    }
}
