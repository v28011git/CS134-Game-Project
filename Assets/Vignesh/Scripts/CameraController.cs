using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // it is a reference for the GameObject player
    public GameObject player;

    //Distance between camera and player
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Calculates the offset
        offset = transform.position - player.transform.position; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Camera follows the player while maintaining same distance
        transform.position = player.transform.position + offset; 
    }
}
