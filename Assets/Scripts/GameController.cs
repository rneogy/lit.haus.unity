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
        InstantiateRooms(HouseStructures[Random.Range(0, HouseStructures.Length)]);
    }

    void InstantiateRooms(HouseStructure h) {
        for (int i = 0; i < h.size; i++) {
            for (int j = 0; j < h.size; j++) {
                Vector2 pos = new Vector2(j-(int)(h.size/2), (int)(h.size/2)-i) * new Vector2(RoomWidth, RoomWidth-1);
                RoomStructure rs = h.roomStructures[i*h.size + j];
                GameObject room = Instantiate(roomPrefab, pos, Quaternion.identity);
                RoomTileController rtc = room.GetComponent<RoomTileController>();
                rtc.TilePalette = TilePalettes[Random.Range(0, TilePalettes.Length)];
                rtc.TilePaletteName = rtc.TilePalette.name;
                rtc.Structure = rs;
                rtc.RoomStructureName = rs.name;
                rtc.LayoutIndex = Random.Range(0, rtc.Layouts.Length);
                NetworkServer.Spawn(room);
            }
        }
    }
}
