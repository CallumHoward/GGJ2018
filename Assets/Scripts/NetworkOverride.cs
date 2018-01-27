using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


public class NetworkOverride : NetworkManager {

    public static int playerCount;
<<<<<<< HEAD

=======
>>>>>>> 54a13a0d149b0e203aba18503d73b5aef03bb47e
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
