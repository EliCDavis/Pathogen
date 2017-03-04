using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 lastKnown;
    public GameObject alertPrefab;
    public GameObject destroyPrefab;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void spawnAlert()
    {
        GameObject.Instantiate(alertPrefab, this.transform);
    }

    public void spawnDestroy()
    {
        GameObject.Instantiate(destroyPrefab, this.transform);
    }
}
