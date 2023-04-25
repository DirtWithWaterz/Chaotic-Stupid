using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using mudz;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    Dick dic;
    public Dictionary<int, string> playerIds = new Dictionary<int, string>();

    [PunRPC]
    public void ListPlayerIds(string name, int id){
        playerIds.Add(id, name);
        Debug.Log($"{name} : {id}");
    }

    void Start(){
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(0, 0), Quaternion.identity);
        PhotonView view = player.GetComponent<PhotonView>();
        if(PhotonNetwork.LocalPlayer.NickName == "" || PhotonNetwork.LocalPlayer.NickName == null){
            PhotonNetwork.LocalPlayer.NickName = "Unknown";
        }
        photonView.RPC(nameof(ListPlayerIds), RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName, view.ViewID);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int id = dic.TryGetKey(playerIds, otherPlayer.NickName);
        playerIds.Remove(id);
    }
    
}
namespace mudz
{
    using System.Collections.Generic;
    class Dick{
        public int TryGetKey(Dictionary<int, string> dic, string str){
        foreach(KeyValuePair<int, string> pair in dic){
                if(pair.Value == str){
                    return pair.Key;
                }
            } return 0;
        }
    }
}
