using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RoomStructure", menuName = "RoomStructure")]
public class RoomStructure : ScriptableObject
{

    public GameObject[] rooms;
    public int length;
}
