using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    private Vector2[] levelLimits = new Vector2[2];
    public GameObject[] items;
    private float timer = 10;

    private void Start()
    {
        timer = Random.Range(10, 30);
        GameObject temp = GameObject.FindGameObjectWithTag("spawnController");
        levelLimits[0] = temp.GetComponent<outOfBounds>().xRespawn;
        levelLimits[1] = temp.GetComponent<outOfBounds>().zRespawn;
    }

    IEnumerator NewItem()
    {
        GameObject gm = Instantiate(items[Random.Range(0,items.Length)], new Vector3(Random.Range(levelLimits[0].x, levelLimits[0].y), 1, Random.Range(levelLimits[1].x, levelLimits[1].y)), Quaternion.identity);
        yield return new WaitForSeconds(0);
    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            StartCoroutine(NewItem());
            timer = Random.Range(10, 60);
        }
    }
 

}
