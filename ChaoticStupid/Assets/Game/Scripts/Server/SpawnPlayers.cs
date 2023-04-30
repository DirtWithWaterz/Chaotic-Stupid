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
    class UtilsClass{
        public static Vector3 GetVectorFromAngle(float angle){
            // angle = 0 -> 360
            float angleRad = angle * (Mathf.PI/180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }
        public static float GetAngleFromVectorFloat(Vector3 dir){
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if(n < 0) n += 360;

            return n;
        }
    }
}
