using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class enablePageOnMovement : MonoBehaviour
{
    public GameObject enableUI;
    public void onPressed(InputAction.CallbackContext context)
    {
        enableUI.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
