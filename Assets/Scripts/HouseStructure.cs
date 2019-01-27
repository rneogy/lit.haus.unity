using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HouesStructure", menuName = "HouseStructure")]

public class HouseStructure : ScriptableObject
{
    public RoomStructure[] roomStructures;
    public int size;
}
