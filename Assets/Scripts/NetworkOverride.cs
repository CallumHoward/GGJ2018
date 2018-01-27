using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkOverride : NetworkManager {

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        if (conn.playerControllers.Count > 0)
        {
            //foreach(NetworkClient nc in NetworkClient.allClients[].c)
            print(NetworkServer.connections.Count);
            print(NetworkServer.connections[NetworkServer.connections.Count - 1].playerControllers[0].gameObject.GetInstanceID());
            print(NetworkServer.connections[NetworkServer.connections.Count - 1].playerControllers[0].gameObject);
            //print(conn.playerControllers[0].gameObject.GetInstanceID());
            int counter = NetworkServer.connections.Count;
            for (int nc = 0; nc <= counter; nc++)
            {
                NetworkServer.connections[NetworkServer.connections.Count - 1].playerControllers[0].gameObject.GetComponentInChildren<Renderer>().material.color = NetworkServer.connections[NetworkServer.connections.Count - 1].playerControllers[0].gameObject.GetComponent<PlayerController>().Test();
            }
            
        }
    }
    
}
