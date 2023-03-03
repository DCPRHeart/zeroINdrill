using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject pivotObject;
    
    void Start() {}

    void Update()
    {
        transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider collision)
    {
        GameObject gameObj = collision.gameObject;
        if(gameObj.tag == "Bullet")
        {
            Destroy(gameObject);
            Debug.Log("Score");
        }
    }
}
