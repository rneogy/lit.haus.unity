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

    public GameObject[] Layouts;
    public GameObject LayoutCenter;
    public int LayoutIndex;

    public EdgeCollider2D TopDoor;
    public EdgeCollider2D RightDoor;
    public EdgeCollider2D BottomDoor;
    public EdgeCollider2D LeftDoor;

    // Start is called before the first frame update
    void Start()
    {
        DrawTiles();
        if (transform.position == Vector3.zero) {
            Instantiate(LayoutCenter, transform.position, Quaternion.identity);
        } else {
            Instantiate(Layouts[LayoutIndex], transform.position, Quaternion.identity);
        }
    }

    public void DrawTiles() {
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
            TopDoor.enabled = false;
            Walls.SetTile(new Vector3Int(0, h-1, 0), null);
            Walls.SetTile(new Vector3Int(-1, h-1, 0), null);
        }
        if (Structure.BottomDoor) {
            BottomDoor.enabled = false;
            Walls.SetTile(new Vector3Int(0, -h, 0), null);
            Walls.SetTile(new Vector3Int(-1, -h, 0), null);
        }
        if (Structure.RightDoor) {
            RightDoor.enabled = false;
            Walls.SetTile(new Vector3Int(h-1, 0, 0), null);
            Walls.SetTile(new Vector3Int(h-1, -1, 0), null);
        }
        if (Structure.LeftDoor) {
            LeftDoor.enabled = false;
            Walls.SetTile(new Vector3Int(-h, 0, 0), null);
            Walls.SetTile(new Vector3Int(-h, -1, 0), null);
        }
    }


}
