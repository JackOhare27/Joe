using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class outOfBounds : MonoBehaviour
{
    public AudioSource explosionFX;
    public GameObject effect;
    public Vector2 xRespawn, zRespawn;
    IEnumerator Explode(Vector3 trns)
    {
        explosionFX.Play();
        GameObject gm = Instantiate(effect, trns, Quaternion.identity);
        yield return new WaitForSeconds(3.5f);
        Destroy(gm);
    }
    IEnumerator Respawn(GameObject player)
    {
        
        player.gameObject.GetComponent<playerInfo>().meshEnabled(true);
        player.gameObject.GetComponent<movement>().enabled = false;
        player.transform.position = new Vector3(Random.Range(xRespawn.x,xRespawn.y), 10, Random.Range(zRespawn.x, zRespawn.y));
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(3f);
        player.gameObject.GetComponent<playerInfo>().meshEnabled(false);
        player.gameObject.GetComponent<movement>().enabled = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().AddForce(Vector3.down*10*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInfo healthInfo = other.gameObject.GetComponent<playerInfo>();
            StartCoroutine(Explode(other.transform.position));

            //perm death
            if (healthInfo.playerLives <= 1)
            {

                healthInfo.playerIcon.color = new Color(healthInfo.playerIcon.color.r, healthInfo.playerIcon.color.g, healthInfo.playerIcon.color.b, .5f);
                healthInfo.playerHealthText.text = "";
                Destroy(other.gameObject);
            }
            else
            {   //respawn
                healthInfo.launchCapability = 0;
                healthInfo.explosionPower = 1;

                GameObject joe = other.gameObject;
                
                StartCoroutine(Respawn(joe));
            }
            healthInfo.playerLives--;
        }
    }

}
