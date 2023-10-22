using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateClouds : MonoBehaviour
{
    public int rotateSpeed = 10,xforJoe = 0, zforJoe = 0;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(xforJoe, rotateSpeed, zforJoe) * Time.deltaTime);
    }
}
