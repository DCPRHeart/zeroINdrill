using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float despawnAfter = 5;
    public float speed = 10;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Despawn());
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnAfter);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += this.transform.forward * speed * Time.deltaTime;
    }
}
