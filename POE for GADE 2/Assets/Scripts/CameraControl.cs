using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float minFov = 15f;
    public float maxFov  = 90f;
    public float sensitivity = 10f;
    public GameObject map;        //Public variable to store a reference to the player game object
    public float followSpeed = 0.5f;

    private Vector3 offset;            //Private variable to store the offset distance between the player and camera


    // Start is called before the first frame update
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - map.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = Vector3.Slerp(transform.position, map.transform.position + offset, followSpeed * Time.deltaTime);
       
    }
}
