using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{
    public RoomTilePalette[] TilePalettes;
    public HouseStructure[] HouseStructures;
    public float RoomWidth = 20f;
    public GameObject roomPrefab;


    // Start is called before the first frame update
    void Start()
    {
        print("Spawning room structure. On server? " + isServer);
        InstantiateRooms(HouseStructures[0]);
    }

    void InstantiateRooms(HouseStructure h) {
        for (int i = 0; i < h.size; i++) {
            for (int j = 0; j < h.size; j++) {
                Vector2 pos = new Vector2(j-(int)(h.size/2), (int)(h.size/2)-i) * RoomWidth;
                RoomStructure rs = h.roomStructures[i*h.size + j];
                GameObject room = Instantiate(roomPrefab, pos, Quaternion.identity);
                RoomTileController rtc = room.GetComponent<RoomTileController>();
                rtc.TilePalette = TilePalettes[Random.Range(0, TilePalettes.Length)];
                rtc.Structure = rs;
                NetworkServer.Spawn(room);
            }
        }
    }
}
