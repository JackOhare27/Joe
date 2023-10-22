using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class playerWatch : MonoBehaviour
{
    public AudioClip celebrateFX;
    private AudioSource ingameMusic;
    public Transform UIWinnerPlacement;
    public List<Transform> targets;
    public GameObject blurCameraPlane, winnerText, resetButton;
    public GameObject JoeWinnerUI;
    public GameObject JoeWinner;

    public MeshRenderer JoeWinnerUIBody;
    public GameObject[] JoeWinnerUIAcces = new GameObject[5];
    private playerInfo playerInf;
    //blur ui for winner
    public Material mat;
    public Vector3 offset;
    public float smoothTime = .5f;

    public float maxZoom = 70f;
    public float minZoom = 10f;
    public float zoomLimiter = 50f;
    private bool activateWinnerUI = true;
    private Vector3 velocity;
    private Camera cam;

//https://www.gamedeveloper.com/business/different-ways-of-shaking-camera-in-unity
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        ingameMusic = GetComponent<AudioSource>();
        GameObject[] grabPlayers;
        grabPlayers = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < grabPlayers.Length;i++)
        {
            targets.Add(grabPlayers[i].transform);
        }
    }

    void LateUpdate()
    {
        if (targets.Count <= 1)
        {   //TO DO
            Move();
            Zoom();
           
            
            if (activateWinnerUI)
            {
                ingameMusic.PlayOneShot(celebrateFX);
                maxZoom = 20;
                StartCoroutine(IncreaseDistortion());
                activateWinnerUI = false;
            }
            return;
        }
        else
        {
            //if player does not exist remove from tracking
            for (int i = 0; i < targets.Count; i++)
            {
                if (!targets[i])
                {

                    targets.Remove(targets[i]);
                }
            }
            //Debug.Log(targets.Count);
            Move();
            Zoom();
        }
    }

    IEnumerator IncreaseDistortion()
    {
        JoeWinner = GameObject.FindGameObjectWithTag("Player");
        playerInf  = JoeWinner.GetComponent<playerInfo>();
        JoeWinnerUIBody.material = playerInf.bodyMats[playerInf.currJoeMat];
        foreach(GameObject part in JoeWinnerUIAcces)
        {
            part.GetComponent<MeshRenderer>().material = playerInf.accesMats[playerInf.currJoeMat];
        }   

        winnerText.SetActive(true);
        resetButton.SetActive(true);
        yield return new WaitForSeconds(5f);
        blurCameraPlane.SetActive(true);
        JoeWinnerUI.SetActive(true);
        float i = 0;
        while(mat.GetFloat("DistortionFloat") <= .10f)
        {
            i += .005f;
            mat.SetFloat("DistortionFloat", i);
            yield return new WaitForSeconds(1f);
        }
        //UIWinners[e].transform.position = UIWinnerPlacement.position; UIWinners[e].transform.localScale = UIWinnerPlacement.localScale;

    }
    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);

        //ortho way
       // float newZoom2 = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance2() / zoomLimiter);
       // cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom2, Time.deltaTime);
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = newPosition;
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.y;

    }

    /*
    float GetGreatestDistance2()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;

    }
    */
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

}
