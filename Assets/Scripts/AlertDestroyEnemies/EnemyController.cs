using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 lastKnown;
    public GameObject alertPrefab;
    public GameObject destroyPrefab;
    public int detected = 0;

    // Use this for initialization
    void Start()
    {
        spawnAlert();
        spawnAlert();
        spawnDestroy();
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
