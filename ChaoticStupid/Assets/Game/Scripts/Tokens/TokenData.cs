using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TokenData : MonoBehaviourPunCallbacks
{
    [SerializeField] public List<string> owners = new List<string>();
    [SerializeField] public string tokenName;
}
