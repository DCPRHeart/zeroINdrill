using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneManager : MonoBehaviour
{
    public GameObject Target;
    public SceneManager manager;
    public float rotationSpeed;

    private GameObject targetOne;
    private GameObject targetTwo;
    private GameObject targetThree;
    private GameObject targetFour;
    private GameObject targetFive;

    void Start() {
        targetOne = Instantiate(Target, new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z), transform.rotation);
        targetOne.GetComponent<Target>().pivotObject = this.gameObject;
        targetOne.GetComponent<Target>().manager = this.manager;
        targetOne.GetComponent<Target>().rotationSpeed = 30;

        targetTwo = Instantiate(Target, new Vector3(transform.position.x + 6.5f, transform.position.y, transform.position.z), transform.rotation);
        targetTwo.GetComponent<Target>().pivotObject = this.gameObject;
        targetTwo.GetComponent<Target>().manager = this.manager;
        targetTwo.GetComponent<Target>().rotationSpeed = -40;

        targetThree = Instantiate(Target, new Vector3(transform.position.x + 8f, transform.position.y, transform.position.z), transform.rotation);
        targetThree.GetComponent<Target>().pivotObject = this.gameObject;
        targetThree.GetComponent<Target>().manager = this.manager;
        targetThree.GetComponent<Target>().rotationSpeed = 40;

        targetFour = Instantiate(Target, new Vector3(transform.position.x + 9.5f, transform.position.y, transform.position.z), transform.rotation);
        targetFour.GetComponent<Target>().pivotObject = this.gameObject;
        targetFour.GetComponent<Target>().manager = this.manager;
        targetFour.GetComponent<Target>().rotationSpeed = -50;

        targetFive = Instantiate(Target, new Vector3(transform.position.x + 11f, transform.position.y, transform.position.z), transform.rotation);
        targetFive.GetComponent<Target>().pivotObject = this.gameObject;
        targetFive.GetComponent<Target>().manager = this.manager;
        targetFive.GetComponent<Target>().rotationSpeed = 50;
    }

    void Update() {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }
}
