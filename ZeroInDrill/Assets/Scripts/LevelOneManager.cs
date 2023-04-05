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
    private GameObject targetSix;
    private GameObject targetSeven;
    private GameObject targetEight;
    private GameObject targetNine;
    private GameObject targetTen;
    private GameObject targetEleven;

    void Start() {
        targetOne = Instantiate(Target, new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z), transform.rotation);
        targetOne.GetComponent<Target>().pivotObject = this.gameObject;
        targetOne.GetComponent<Target>().manager = this.manager;
        targetOne.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);

        targetTwo = Instantiate(Target, new Vector3(transform.position.x + 6.5f, transform.position.y, transform.position.z), transform.rotation);
        targetTwo.GetComponent<Target>().pivotObject = this.gameObject;
        targetTwo.GetComponent<Target>().manager = this.manager;
        targetTwo.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);

        targetThree = Instantiate(Target, new Vector3(transform.position.x + 8f, transform.position.y, transform.position.z), transform.rotation);
        targetThree.GetComponent<Target>().pivotObject = this.gameObject;
        targetThree.GetComponent<Target>().manager = this.manager;
        targetThree.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);

        targetFour = Instantiate(Target, new Vector3(transform.position.x + 9.5f, transform.position.y, transform.position.z), transform.rotation);
        targetFour.GetComponent<Target>().pivotObject = this.gameObject;
        targetFour.GetComponent<Target>().manager = this.manager;
        targetFour.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);

        targetFive = Instantiate(Target, new Vector3(transform.position.x + 11f, transform.position.y, transform.position.z), transform.rotation);
        targetFive.GetComponent<Target>().pivotObject = this.gameObject;
        targetFive.GetComponent<Target>().manager = this.manager;
        targetFive.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);

        targetSix = Instantiate(Target, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5f), transform.rotation);
        targetSix.GetComponent<Target>().pivotObject = this.gameObject;
        targetSix.GetComponent<Target>().manager = this.manager;
        targetSix.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);

        targetSeven = Instantiate(Target, new Vector3(transform.position.x, transform.position.y, transform.position.z + 6.5f), transform.rotation);
        targetSeven.GetComponent<Target>().pivotObject = this.gameObject;
        targetSeven.GetComponent<Target>().manager = this.manager;
        targetSeven.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);

        targetEight = Instantiate(Target, new Vector3(transform.position.x, transform.position.y, transform.position.z + 8f), transform.rotation);
        targetEight.GetComponent<Target>().pivotObject = this.gameObject;
        targetEight.GetComponent<Target>().manager = this.manager;
        targetEight.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);

        targetNine = Instantiate(Target, new Vector3(transform.position.x, transform.position.y, transform.position.z - 9.5f), transform.rotation);
        targetNine.GetComponent<Target>().pivotObject = this.gameObject;
        targetNine.GetComponent<Target>().manager = this.manager;
        targetNine.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);

        targetTen = Instantiate(Target, new Vector3(transform.position.x, transform.position.y, transform.position.z - 11f), transform.rotation);
        targetTen.GetComponent<Target>().pivotObject = this.gameObject;
        targetTen.GetComponent<Target>().manager = this.manager;
        targetTen.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);

        targetEleven = Instantiate(Target, new Vector3(transform.position.x, transform.position.y, transform.position.z - 8f), transform.rotation);
        targetEleven.GetComponent<Target>().pivotObject = this.gameObject;
        targetEleven.GetComponent<Target>().manager = this.manager;
        targetEleven.GetComponent<Target>().rotationSpeed = Random.Range(-70, 70);
    }

    void Update() {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    public void deleteLevel() {
        Destroy(targetOne);
        Destroy(targetTwo);
        Destroy(targetThree);
        Destroy(targetFour);
        Destroy(targetFive);
        Destroy(targetSix);
        Destroy(targetSeven);
        Destroy(targetEight);
        Destroy(targetNine);
        Destroy(targetTen);
        Destroy(targetEleven);
    }
}
