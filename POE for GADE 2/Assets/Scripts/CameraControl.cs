
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float sensitivity = 2f;
    public float panBorderThickness = 30f;
    public GameObject map;        //Public variable to store a reference to the player game object
    public float followSpeed = 5f;

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
        //transform.position = Vector3.Slerp(transform.position, map.transform.position + offset, followSpeed * Time.deltaTime);
        Vector3 pos = transform.position;
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += followSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <=  panBorderThickness)
        {
            pos.z -= followSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += followSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <=  panBorderThickness)
        {
            pos.x -= followSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * sensitivity * 100f *  Time.deltaTime;

        transform.position = pos;
    }
}
