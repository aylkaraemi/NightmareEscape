using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;

    private float height = 5f;
    private float distance;
    private Vector3 prevPos, moveDir;
    private Vector3 offset = new Vector3(0.0f, 6.0f, -6.0f);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        prevPos = player.transform.position;
        distance = (offset).magnitude;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        moveDir = player.transform.position - prevPos;
        
        if (moveDir != Vector3.zero)
        {
            moveDir.Normalize();
            
            transform.position = player.transform.position - moveDir * distance;
            transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);

            transform.LookAt(player.transform.position);
            // works as long as player is moving. If player rotates than stops, rotation snaps
            // have not yet figured out a solution to smooth this that doesn't make the rotation
            // when moving odd (curving a little too far on turns, like the camera behind a car in
            // a driving game, but really weird for following a person)

            prevPos = player.transform.position;
        }        
    }
}
