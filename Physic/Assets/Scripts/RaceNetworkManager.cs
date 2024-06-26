using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceNetworkManager : NetworkManager
{
    [SerializeField]
    Transform[] spawnPoints;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        Vector3 spawnPoint = spawnPoints[numPlayers].position;

        var player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity) as GameObject;

        NetworkServer.AddPlayerForConnection(conn, player);
    }

}
