using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;

    
    void Update()
    {
       transform.position = player.position + new Vector3(0,8,-9.5f);                    
    }   
}
