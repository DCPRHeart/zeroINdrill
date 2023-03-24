using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 1000f;
    public float scrollSensitivity = 0.5f;
    public Transform playerBody;
    public Transform playerCam;
    public GameObject bullet;
    public float cooldown = 1f;
    public bool inCoolDown = false;
    public GameObject point1;
    public GameObject point2;

    private bool isMoving = false;
    float xRotation = 0f;
    float yRotation = 0f;
    float height;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.transform.position = point1.transform.position;
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

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && isMoving != true) //move up
        {
            moveUp();
       }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && isMoving != true)
       {
            moveDown();
       }
        
        //transform.position = new Vector3(0, height, 0);
    }

    public void moveUp()
    {
        if(this.transform.position == point2.transform.position)
        {
            isMoving = false;

        }
        else
        {
            isMoving = true;
            this.transform.position = Vector3.MoveTowards(this.transform.position, point2.transform.position, scrollSensitivity * Time.deltaTime);
        }
    }

    public void moveDown()
    {
        if (this.transform.position == point1.transform.position)
        {
            isMoving = false;

        }
        else
        {
            isMoving = true;
            this.transform.position = Vector3.MoveTowards(this.transform.position, point1.transform.position, scrollSensitivity * Time.deltaTime);
        }
    }

    public void shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, transform.forward, out hit, 10f))
        {
            Destroy(hit.transform.gameObject);

        }
    }

    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        inCoolDown = false;

    }

    IEnumerator movingTime()
    {
        yield return new WaitForSeconds(scrollSensitivity * Time.deltaTime);
        isMoving = false;
    }
}
