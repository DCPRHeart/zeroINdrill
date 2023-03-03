using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 1000f;
    public float scrollSensitivity = 0.5f;
    public Transform playerBody;
    public Rigidbody bullet;
    public float cooldown = 1f;
    public bool inCoolDown = false;

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

        if (Input.GetKey(KeyCode.Mouse0) && !inCoolDown)
        {
            shoot();
            inCoolDown = true;
            StartCoroutine(shootCooldown());
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            height += scrollSensitivity;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            height -= scrollSensitivity;
        }
        transform.position = new Vector3(0, height, 0);
    }

    public void shoot()
    {
        Rigidbody clone;
        clone = Instantiate(bullet, transform.position, transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.forward * 10);

    }

    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        inCoolDown = false;

    }
}
