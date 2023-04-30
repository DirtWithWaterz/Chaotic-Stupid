using System.Collections;
using System.Collections.Generic;
using FOW;
using Photon.Pun;
using UnityEngine;

public class hideOtherPlayers : FogOfWarHider
{
    PhotonView view;
    void Start(){
        view = GetComponent<PhotonView>();
        if(view.IsMine){GetComponent<hideOtherPlayers>().enabled = false;}
    }
}
