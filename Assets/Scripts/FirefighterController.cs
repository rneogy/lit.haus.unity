using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FirefighterController : NetworkBehaviour
{

    public int maxWater = 5;
    private int numWater;

    private RoomController room;

    public GameObject WaterBulletPrefab;
    private Vector3 lastPos;
    private Vector3 facingDirection;
    public float WaterBulletSpeed = 15f;

    void Awake() {
        numWater = maxWater;
    }

    void Update()
    {
        if (isLocalPlayer) {
            if (lastPos != transform.position) {
                facingDirection = (transform.position - lastPos).normalized;
            }
            lastPos = transform.position;

            if (Input.GetKeyDown(KeyCode.LeftShift) && numWater > 0 && room) {
                CmdDouseFire();
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                CmdShoot(facingDirection);
            }
        }
    }

    [Command]
    void CmdShoot(Vector3 dir) {
        if (numWater > 0) {
            numWater -= 1;
            TargetSetWater(connectionToClient, numWater);
            GameObject wb = Instantiate(WaterBulletPrefab, transform.position, Quaternion.identity);
            wb.GetComponent<Rigidbody2D>().velocity = dir * WaterBulletSpeed;
            NetworkServer.Spawn(wb);
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

    [Command]
    void CmdPickupMatches(GameObject g) {
        Destroy(g);
    }
    
    void OnTriggerEnter2D(Collider2D c) {
        if (isLocalPlayer && this.enabled) {
            if (c.CompareTag("Water")) {
                CmdFillWater();
            }
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
        if (!this.enabled) {
            return;
        }
        if (c.CompareTag("Room")) {
            if (room != null && c.gameObject == room.gameObject) {
                room = null;
            }
        }
    }

    public int getNumWater() {
        return numWater;
    }
}
