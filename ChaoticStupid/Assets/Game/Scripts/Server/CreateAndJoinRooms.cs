using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public TMP_InputField nickInput;

    public void CreateRoom(){
        if(nickInput.text.Length < 1){
            Debug.Log("Cannot join a room without a nickname.");
            return;
        }
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom(){
        if(nickInput.text.Length < 1){
            Debug.Log("Cannot join a room without a nickname.");
            return;
        }
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = nickInput.text;
        PhotonNetwork.LoadLevel("Game");
    }
}
