using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomTileController : MonoBehaviour
{

    public RoomTilePalette TilePalette;
    public RoomStructure Structure;
    public int RoomSize;

    public Tilemap Walls;
    public Tilemap Floor;

    // Start is called before the first frame update
    void Start()
    {
        int h = RoomSize/2;
        for (int y=0; y < RoomSize; y++) {
            for (int x=0; x < RoomSize; x++) {
                Vector3Int pos = new Vector3Int(x-h,y-h,0);
                Floor.SetTile(pos, TilePalette.Floor);

                if (x == 0) {
                    if (y == 0) {
                        Walls.SetTile(pos, TilePalette.BottomLeft);
                    } else if (y == RoomSize-1) {
                        Walls.SetTile(pos, TilePalette.TopLeft);
                    } else {
                        Walls.SetTile(pos, TilePalette.LeftWall);
                    }
                } else if (y == 0) {
                    if (x == RoomSize-1) {
                        Walls.SetTile(pos, TilePalette.BottomRight);
                    } else {
                        Walls.SetTile(pos, TilePalette.BottomWall);
                    }
                } else if (x == RoomSize-1) {
                    if (y == RoomSize-1) {
                        Walls.SetTile(pos, TilePalette.TopRight);
                    } else {
                        Walls.SetTile(pos, TilePalette.RightWall);
                    }
                } else if (y == RoomSize-1) {
                    Walls.SetTile(pos, TilePalette.TopWall);
                }
            }
        }

        if (Structure.TopDoor) {
            Walls.SetTile(new Vector3Int(0, h-1, 0), null);
            Walls.SetTile(new Vector3Int(-1, h-1, 0), null);
        }
        if (Structure.BottomDoor) {
            Walls.SetTile(new Vector3Int(0, -h, 0), null);
            Walls.SetTile(new Vector3Int(-1, -h, 0), null);
        }
        if (Structure.RightDoor) {
            Walls.SetTile(new Vector3Int(h-1, 0, 0), null);
            Walls.SetTile(new Vector3Int(h-1, -1, 0), null);
        }
        if (Structure.LeftDoor) {
            Walls.SetTile(new Vector3Int(-h, 0, 0), null);
            Walls.SetTile(new Vector3Int(-h, -1, 0), null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
