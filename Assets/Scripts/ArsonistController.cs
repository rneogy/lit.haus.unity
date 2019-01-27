using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArsonistController : NetworkBehaviour
{

    private int numMatches;

    private RoomController room;

    public Color spriteColor;

    private bool dead = false;

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

    public void Kill() {
        CmdKill();
    }

    [Command]
    void CmdKill() {
        dead = true;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        RpcKill();
    }

    [ClientRpc]
    void RpcKill() {
        dead = true;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
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
        if (dead) {
            return;
        }
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
        if (dead) {
            return;
        }
        if (c.CompareTag("Room")) {
            if (room != null && c.gameObject == room.gameObject) {
                room = null;
            }
        }
    }

    public int getNumMatches() {
        return numMatches;
    }
}
