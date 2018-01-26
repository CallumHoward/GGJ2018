using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkOverride : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        if (conn.playerControllers.Count > 0)
        {
            GameObject player = conn.playerControllers[0].gameObject;
            switch (player.GetComponent<PlayerController>()._PLAYER)
            {
                case PLAYER.Player_1:
                    player.GetComponent<PlayerController>().Variables.childModel.GetComponent<Renderer>().material.color = Color.red;
                    break;
                case PLAYER.Player_2:
                    player.GetComponent<PlayerController>().Variables.childModel.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case PLAYER.Player_3:
                    player.GetComponent<PlayerController>().Variables.childModel.GetComponent<Renderer>().material.color = Color.green;
                    break;
                case PLAYER.Player_4:
                    player.GetComponent<PlayerController>().Variables.childModel.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
            }
        }
    }
}