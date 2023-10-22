using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class movement : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject smokeSprint, playerRing, playerRocketLauncher, missile;
    public Animator joeAnim;
    private Animator swingAnim;
    //public Rigidbody rb;
    public int speed = 5, sprintSpeed = -500;
    public Vector3 control;
    //private float vertInput = 0, horizInput = 0;
    private Rigidbody playerRB;
    //public string playerHorizontal, playerVertical, playerSlap, playerSprint, playerWoof;
    public float rotateSpeed = 500;
    private bool canSprint = true, canSlap = true, hasLauncher = false, canBark = true;
    private ParticleSystem woofingPart;
    public AudioSource dashFX, barkFX, fire;
    
    //NEW INPUT VARIABLES
    private Vector2 newMovementInput = Vector2.zero;
    private bool hasSprinted = false, hasSlapped, hasBarked;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject.GetComponent<dontSpawnStuck>());
        playerRB = GetComponent<Rigidbody>();
        woofingPart = gameObject.GetComponentInChildren<ParticleSystem>();
        var e = woofingPart.emission;
        e.enabled = false;
        swingAnim = GetComponentInChildren<Animator>();
        Debug.Log(swingAnim.name);
        //sb.enabled = false;
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        newMovementInput = context.ReadValue<Vector2>();

    }

    public void OnDash(InputAction.CallbackContext context)
    {
        hasSprinted =context.action.triggered;
    }

    public void OnSlap(InputAction.CallbackContext context)
    {
        Debug.Log("bals");
        hasSlapped =context.action.triggered;
     
    }
    public void OnBark(InputAction.CallbackContext context)
    {
        hasBarked = context.action.triggered;
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Slap();
        Sprint();
        woofing();
  
        control = new Vector3(newMovementInput.x, 0, newMovementInput.y).normalized;
        
        if (control.x != 0 || control.z != 0)
        { joeAnim.SetBool("Walking", true); }
        else
        {
            joeAnim.SetBool("Walking", false);
        }

        
        //rb.MovePosition(rb.position + (-1*movement) * speed * Time.fixedDeltaTime);
        if (control != Vector3.zero)
        {
            transform.Translate((-1 * control) * speed * Time.fixedDeltaTime, Space.World);
            Quaternion toRotation = Quaternion.LookRotation(control, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.fixedDeltaTime);
        }
       
      
    }

    void Slap()
    {
         if(!hasLauncher)
        {
            //STOP SIGN SLAP
             if (hasSlapped && canSlap)
            {
                swingAnim.SetTrigger("swinging");
                StartCoroutine(slapCooldown());
            }
            else
            {
                swingAnim.ResetTrigger("swinging");

            }
        } 
        else
        {
            //ROCKET LAUNCHER
            if (hasSlapped)
            {
                fire.Play();
                GameObject proj = Instantiate(missile, firingPoint.position, firingPoint.rotation);
                hasLauncher = false;
                playerRocketLauncher.SetActive(hasLauncher);

            }
     
        }

    }
    void woofing()
    {
        ParticleSystem.EmissionModule emit = woofingPart.emission;
        if (hasBarked && canBark)
        {
            canBark = false;
            barkFX.PlayOneShot(barkFX.clip);
            emit.enabled = true;
            StartCoroutine(barking(emit));
            
            //Debug.Log("bals");
        }
        else
        {  }
    }

    void Sprint()
    {
        if(hasSprinted && canSprint)
        {
            hasSprinted = false;
            dashFX.Play();
            playerRing.SetActive(false);
            smokeSprint.SetActive(true);
            playerRB.AddForce(transform.forward * sprintSpeed);
            StartCoroutine(sprintCooldown());
        }
    }

    IEnumerator sprintCooldown()
    {
        canSprint = false;
        yield return new WaitForSeconds(3f);
        canSprint = true;
        smokeSprint.SetActive(false);
        playerRing.SetActive(true);
        
        

    }
        IEnumerator slapCooldown()
    {
        hasSlapped = false;
        yield return new WaitForSeconds(3f);
        canSlap = true;
    }

        IEnumerator barking(ParticleSystem.EmissionModule bork)
    {
        hasBarked = false;
        yield return new WaitForSeconds(.5f);
        canBark = true;
        bork.enabled = false;
        
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            //Debug.Log("bruh");
            Destroy(other.gameObject);
            hasLauncher = true;
            playerRocketLauncher.SetActive(hasLauncher);
            
        }
    }
}
