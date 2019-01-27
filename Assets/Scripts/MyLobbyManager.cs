using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyLobbyManager : NetworkLobbyManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.GetComponent<CorePlayerController>().SetFirefighter(true);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnLobbyStartServer() {
        print("starting!");
    }
}
