using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


public class NetworkOverride : NetworkManager {

    public static int playerCount;

    public static UnityEvent OnNewPlayer;

    private void Start()
    {
        if (OnNewPlayer == null)
        {
            OnNewPlayer = new UnityEvent();
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        if (conn.playerControllers.Count > 0)
        {
            Tester();
        }
    }

    public void Tester()
    {
        print(NetworkServer.connections.Count);
        print(NetworkServer.connections[NetworkServer.connections.Count - 1].playerControllers[0].gameObject.GetInstanceID());
        print(NetworkServer.connections[NetworkServer.connections.Count - 1].playerControllers[0].gameObject);
        playerCount = NetworkServer.connections.Count;
    }
    
}
