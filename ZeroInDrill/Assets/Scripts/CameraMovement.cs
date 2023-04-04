using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 1000f;
    public float scrollSensitivity = 0.5f;
    public Transform playerBody;
    public GameObject bullet;
    public SceneManager manager;
    public float cooldown = 1f;
    public bool inCoolDown = false;

    private bool isMoving = false;
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

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        //playerBody.Rotate(Vector3.right * mouseY);

        if (Input.GetKey(KeyCode.Mouse0) && !inCoolDown && manager.getInGame())
        {
            shoot();
            manager.decrementShot();
            inCoolDown = true;
            StartCoroutine(shootCooldown());
        }
    }

    public void moveUp()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 10, 0), scrollSensitivity * Time.deltaTime);
    }

    public void moveDown()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), scrollSensitivity * Time.deltaTime);

    }

    public void shoot()
    {
        GameObject clone;
        clone = Instantiate(bullet, transform.position, transform.rotation);
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
