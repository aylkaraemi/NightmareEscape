using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float horizInput;
    private float vertInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * vertInput * Time.deltaTime * speed);
    }
}
