using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 lastKnown;
    public GameObject alertPrefab;
    public GameObject destroyPrefab;
    public int detected = 0;

    private GameObject alertCell;
    private GameObject destroyCell;
    private float coinflip;
    private float spawnX;
    private float spawnY;
    private float spawnZ;

    // Use this for initialization
    void Start()
    {
        for(int i=0; i < 10; i++)
        {
            spawnAlert();
        }

        for(int i=0; i < 3; i++)
        {
            spawnDestroy();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void spawnAlert()
    {

        alertCell = GameObject.Instantiate(alertPrefab);
        alertCell.transform.position = generateSpawnPosition();
    }

    public void spawnDestroy()
    {

        destroyCell = GameObject.Instantiate(destroyPrefab);
        destroyCell.transform.position = generateSpawnPosition();
    }
    private Vector3 generateSpawnPosition()
    {
        spawnX = Random.Range(-1000f, 1000f);
        
        spawnZ = Random.Range(-1000f, 1000f);
        if (spawnX > -415 && spawnX < 525 && spawnZ < 825 && spawnZ > -500)
        {
            coinflip = Random.Range(1f, 10f);
            if (coinflip % 2 == 0)
            {
                spawnY = Random.Range(5f, 100f);
            }

            else
            {
                spawnY = Random.Range(860f, 1000f);
            }

        }

        else
        {
            spawnY = Random.Range(5f, 1000f);
        }

        return new Vector3(spawnX, spawnY, spawnZ);
    }
}
