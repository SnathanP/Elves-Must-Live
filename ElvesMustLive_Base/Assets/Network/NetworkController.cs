﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkController : Photon.PunBehaviour
{

	// Use this for initialization
	void Start () {
        PhotonNetwork.Instantiate("Perso", new Vector3(0f, 5f, 0f), Quaternion.identity,0);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("n"))
        {
            Debug.Log(PhotonNetwork.room.PlayerCount + "Players in this room");
        }
	}

    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

    }

    public override void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected
        }
    }


    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName); // seen when other disconnects


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected
        }
    }

}