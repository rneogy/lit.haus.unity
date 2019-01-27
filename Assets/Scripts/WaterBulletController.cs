using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WaterBulletController : NetworkBehaviour
{

    public float TravelTime = 2f;
    private float SpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > SpawnTime + TravelTime) {
            NetworkServer.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D c) {
        
        ArsonistController ac = c.gameObject.GetComponent<ArsonistController>();
        if (ac && ac.enabled) {
            ac.Kill();
        }

        if (c.CompareTag("Room") || c.CompareTag("Match") || (ac && !ac.enabled)) {
            return;
        }

        NetworkServer.Destroy(gameObject);
    }

}
