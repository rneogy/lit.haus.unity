using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RoomStructure", menuName = "RoomStructure")]
public class RoomStructure : ScriptableObject
{

    public bool TopDoor;
    public bool RightDoor;
    public bool BottomDoor;
    public bool LeftDoor;
}
