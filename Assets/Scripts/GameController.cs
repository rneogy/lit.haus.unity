using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{
    public RoomStructure[] roomStructures;
    public float RoomWidth = 21f;


    // Start is called before the first frame update
    void Start()
    {
        print("Spawning room structure. On server? " + isServer);
        InstantiateRooms(roomStructures[0]);
    }

    void InstantiateRooms(RoomStructure r) {
        for (int i = 0; i < r.length; i++) {
            for (int j = 0; j < r.length; j++) {
                Vector2 pos = new Vector2(j-(int)(r.length/2), (int)(r.length/2)-i) * RoomWidth;
                GameObject roomPrefab = r.rooms[i*r.length + j];
                NetworkServer.Spawn(Instantiate(roomPrefab, pos, roomPrefab.transform.rotation));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
