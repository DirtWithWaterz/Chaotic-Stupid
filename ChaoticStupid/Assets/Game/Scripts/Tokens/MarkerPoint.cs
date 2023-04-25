using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MarkerPoint : MonoBehaviour
{
    Camera mainCamera;
    public Vector2 target;

    void Start(){
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        target = new Vector3(mousePosition.x, mousePosition.y, 0);
        transform.position = new Vector3(target.x, target.y, 1);
    }
}
