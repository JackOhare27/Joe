using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnJoes : MonoBehaviour
{
    public GameObject[] joes;
    float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator spawnJoe()
    {
        Vector3 randRot = new Vector3(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
        GameObject joe = Instantiate(joes[Random.Range(0,4)], new Vector3(Random.Range(-9,10), 15,Random.Range(-3,1)), Quaternion.Euler(randRot));
        yield return new WaitForSeconds(3.5f);
        Destroy(joe,30);
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            StartCoroutine(spawnJoe());
            timer = 1f;
        }
    }
}
