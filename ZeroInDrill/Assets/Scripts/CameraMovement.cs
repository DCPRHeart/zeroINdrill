using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 1000f;
    public float scrollSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;
    float yRotation = 0f;
    float height;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        yRotation += mouseX;
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        //playerBody.Rotate(Vector3.right * mouseY);

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            height++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            height--;
        }
        transform.position = new Vector3(0, height, 0);
    }
}