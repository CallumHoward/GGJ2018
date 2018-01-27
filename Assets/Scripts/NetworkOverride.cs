using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkOverride : NetworkManager {

    public delegate void NewPlayer();
    public static event NewPlayer OnNewPlayer;

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
        //foreach(NetworkClient nc in NetworkClient.allClients[].c)
        print(NetworkServer.connections.Count);
        print(NetworkServer.connections[NetworkServer.connections.Count - 1].playerControllers[0].gameObject.GetInstanceID());
        print(NetworkServer.connections[NetworkServer.connections.Count - 1].playerControllers[0].gameObject);

        if (OnNewPlayer != null)
            OnNewPlayer();
        
        //print(conn.playerControllers[0].gameObject.GetInstanceID());
        
    }
    
}
