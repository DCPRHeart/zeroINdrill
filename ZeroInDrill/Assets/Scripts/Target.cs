using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool destroyed = false;
    private bool respawning = false;
    public float rotationSpeed;
    public GameObject pivotObject;
    public GameObject indicator;
    public SceneManager manager;

    public AudioSource breakSound;
    public ParticleSystem breakEffect;
    
    public Material none;
    public Material green;
    public Material red;
    public Material box;
    public float despawnSpeed = 0.1f;
    public float respawnSpeed = 0.1f;

    private Vector3 initScale;
    void Start() {
        initScale = indicator.transform.localScale;
    }

    void Update()
    {
        transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
        if (respawning)
        {
            if (indicator.transform.localScale.x < initScale.x)
            {
                float s = respawnSpeed * Time.deltaTime;
                indicator.transform.localScale += new Vector3(s, s, s);
            }
            else
            {
                respawning = false;
                indicator.transform.localScale = initScale;
                ReEnable();

            }
        } else if (destroyed)
        {
            float s = despawnSpeed * Time.deltaTime;
            if (indicator.transform.localScale.x > s)
            {
                indicator.transform.localScale -= new Vector3(s, s, s);
            }
            else
            {
                respawning = true;
            }
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        GameObject gameObj = collision.gameObject;
        if(gameObj.tag == "Bullet" && !destroyed)
        {
            Debug.Log(manager);
            manager.updateScore(10);
            Destroyed();
        }
    }
    void Destroyed()
    {
        Debug.Log("Destroyed");
        destroyed = true;
        if (!breakEffect.isPlaying)
            breakEffect.Play();
        if (!breakSound.isPlaying)
            breakSound.Play();
        indicator.transform.localScale = initScale / 2;
        MeshRenderer r = GetComponent<MeshRenderer>();
        r.material = none;
        MeshRenderer r2 = indicator.GetComponent<MeshRenderer>();
        r2.material = red;
    }
    void ReEnable()
    {
        Debug.Log("ReEnable");
        destroyed = false;
        MeshRenderer r = GetComponent<MeshRenderer>();
        r.material = box;
        MeshRenderer r2 = indicator.GetComponent<MeshRenderer>();
        r2.material = green;
    }
}
