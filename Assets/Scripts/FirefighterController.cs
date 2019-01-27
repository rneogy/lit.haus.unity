using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FirefighterController : NetworkBehaviour
{

    public int maxWater = 5;
    private int numWater;

    public Color spriteColor;

    private RoomController room;

    void Awake() {
        numWater = maxWater;
    }

    void Update()
    {
        GetComponentInChildren<SpriteRenderer>().color = spriteColor;
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.LeftShift) && numWater > 0 && room) {
                CmdDouseFire();
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                print("water " + numWater);
            }
        }
    }

    [Command]
    void CmdDouseFire() {
        if (numWater > 0 && room.DouseFire()) {
            numWater = 0;
            TargetSetWater(connectionToClient, 0);
        }
    }

    [TargetRpc]
    public void TargetSetWater(NetworkConnection target, int water) {
        numWater = water;
    }

    [Command]
    void CmdFillWater() {
        numWater = maxWater;
        TargetFillWater(connectionToClient);
    }

    [TargetRpc]
    void TargetFillWater(NetworkConnection target) {
        numWater = maxWater;
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (isLocalPlayer && this.enabled) {
            if (c.CompareTag("Water")) {
                CmdFillWater();
            }
        }
        if (c.CompareTag("Room")) {
            room = c.GetComponent<RoomController>();
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.CompareTag("Room")) {
            if (c.gameObject == room.gameObject) {
                room = null;
            }
        }
    }

    public int getNumWater() {
        return numWater;
    }
}
