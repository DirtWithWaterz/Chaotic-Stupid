using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class TokenMovement : MonoBehaviourPun
{
    [SerializeField] private MarkerPoint marker;
    [SerializeField] private Vector2 targetPos;

    PhotonView view;

    void Start(){
        view = GetComponent<PhotonView>();
    }

    void OnMouseDown(){
        if(!view.IsMine){return;}

        marker.gameObject.SetActive(true);
    }
    void OnMouseUp(){
        if(!view.IsMine){return;}

        marker.gameObject.SetActive(false);
        targetPos = marker.target;
        transform.position = targetPos;
        marker.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
    }
}
