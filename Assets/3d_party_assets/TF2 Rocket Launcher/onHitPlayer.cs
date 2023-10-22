using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHitPlayer : MonoBehaviour
{
    public GameObject effect;
    public Transform playerDirectionSlap;
    // public Animator explosionAnim;
    private Vector3 launchBack;
    public int bulletSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
        //if no collision found just blow up
        Destroy(gameObject,10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }



    IEnumerator Explode(Vector3 trns)
    {       //NOTE DOES NOT REACH THE YIELD DUE TO OBJECT BEING DESTROYED
        //THIS WAS PROGRAMMED REALLY POORLY BUT ITS 1 AM AND MY EYES HURT
        GameObject gm = Instantiate(effect, trns, Quaternion.identity);
        Destroy(gm, 3);
        yield return new WaitForSeconds(3.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
            //Shake(10,10000);
            Debug.Log("Help");
            playerInfo pl = other.GetComponent<playerInfo>();
            pl.launchCapability += 3;
            pl.explosionPower += 1f;

            launchBack = new Vector3(-playerDirectionSlap.forward.x * pl.launchCapability, pl.explosionPower, -playerDirectionSlap.forward.z * pl.launchCapability);
            other.attachedRigidbody.AddForce(launchBack * 100);  //AddExplosionForce(pl.launchCapability, 10*launchBack, 20.0f, 10.0f, ForceMode.Impulse);
            other.attachedRigidbody.AddTorque(launchBack * 100);
            Debug.Log(launchBack + ":" + pl.launchCapability);


            StartCoroutine(Explode(other.transform.position));
            //add explosion animation on hit
            Destroy(gameObject);

        }
        else
        {
            StartCoroutine(Explode(other.transform.position));
            Destroy(gameObject);
        }
    }
}
