using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New RoomTilePalette", menuName = "RoomTilePalette")]
public class RoomTilePalette : ScriptableObject
{

    public Tile Floor;
    public Tile RightWall;
    public Tile LeftWall;
    public Tile TopWall;
    public Tile BottomWall;
    public Tile TopRight;
    public Tile BottomRight;
    public Tile BottomLeft;
    public Tile TopLeft;

}
