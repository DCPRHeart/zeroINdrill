using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Vector3 stage; //used to change stages #Determines the world space of the following
    public int levels = 1; //max distance from yLevel
    public int level = 0; //point from yLevel
    public float levelDiff = 2;
    public float lerpConstant = 0.2f;
    public float speed = 0.5f;
    private bool is_moving = false;
    // Start is called before the first frame update
    void Start()
    {
        stage = this.transform.position;
    }
    public bool AtLevel()
    {
        if (Mathf.Abs(transform.position.y - ProperPos().y) < 0.1)
        {
            this.transform.position = ProperPos();
            return true;
        }
        return false;
    }
    public IEnumerator moveUp()
    {
        level++;
        is_moving = true;
        yield return new WaitUntil(AtLevel);
        is_moving = false;
    }

    public IEnumerator moveDown()
    {
        level--;
        is_moving = true;
        yield return new WaitUntil(AtLevel);
        is_moving = false;
    }
    private Vector3 ProperPos()
    {
        return new Vector3(stage.x, stage.y + level * levelDiff, stage.z);
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = Vector3.Lerp(this.transform.position, ProperPos(), lerpConstant);
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && level < levels)
        {
            Debug.Log(level);
            //Vector3 currentPosition = transform.position;
            //transform.position = Vector3.Lerp(currentPosition, new Vector3(0, 10, 0), scrollSensitivity/10);
            //height += scrollSensitivity;
            //isMoving = true;
            //StartCoroutine(movingTime());
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 10, 0), scrollSensitivity * Time.deltaTime);
            level++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && level > -levels)
        {
            //Vector3 currentPosition = transform.position;
            // transform.position = Vector3.Lerp(currentPosition, Vector3.zero, scrollSensitivity / 10);
            //height -= scrollSensitivity;
            //isMoving = true;
            //StartCoroutine(movingTime());
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), scrollSensitivity * Time.deltaTime);
            level--;
        }
    }
}
