using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;

    private float height = 5f;
    private float distance;
    private Vector3 prevPos, moveDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        prevPos = player.transform.position;       
        distance = (transform.position - player.transform.position).magnitude;               
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = player.transform.position - prevPos;
        if (moveDir != Vector3.zero)
        {
            moveDir.Normalize();
            transform.position = player.transform.position - moveDir * distance;
            transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);

            transform.LookAt(player.transform.position);

            prevPos = player.transform.position;
        }       
    }
}
