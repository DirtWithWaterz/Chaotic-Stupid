using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerData : MonoBehaviourPun
{
    public bool isGM;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        isGM = PhotonNetwork.PlayerList.Length <= 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
