using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontSpawnStuck : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Stuck!!!");
            gameObject.transform.position += new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        }
    }
}
