using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeEnemy : MonoBehaviour
{
    public Transform playerDirectionSlap;
    // public Animator explosionAnim;
    private Vector3 launchBack;
    private AudioSource whackFX;
    // Start is called before the first frame update
    void Start()
    {
     whackFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            whackFX.PlayOneShot(whackFX.clip);
            playerInfo pl = other.GetComponent<playerInfo>();
            launchBack = GetComponentInParent<movement>().control;
            pl.launchCapability += 1;
            //explosionAnim.SetTrigger("exploding");
            pl.explosionPower += .4f;
            launchBack = new Vector3(-playerDirectionSlap.forward.x * pl.launchCapability, pl.explosionPower, -playerDirectionSlap.forward.z * pl.launchCapability);
            other.attachedRigidbody.AddForce(launchBack*100);  //AddExplosionForce(pl.launchCapability, 10*launchBack, 20.0f, 10.0f, ForceMode.Impulse);
            other.attachedRigidbody.AddTorque(launchBack * 100);
            Debug.Log(launchBack + ":" + pl.launchCapability);
           
        }
    }
}
