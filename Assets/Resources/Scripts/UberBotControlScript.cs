using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
public class UberBotControlScript : MonoBehaviour
{			
	public float animSpeed = 1.5f;				    // a public setting for overall animator animation speed
	//public float lookSmoother = 3f;				    // unused at the moment
	//public bool useCurves;						// a setting for teaching purposes to show use of curves
    public float runSpeed = 3.0f;               
    public float rotationSpeed = 7.0f;
    public float jumpSpeed = 0.5f;
    public float jumpIncrement = 0.2f;
    public float maxJumpSpeed = 8.0f;

	private Animator anim;							// a reference to the animator on the character
	private AnimatorStateInfo currentBaseState;	    // a reference to the current state of the animator, used for base layer
	private AnimatorStateInfo layer2CurrentState;	// a reference to the current state of the animator, used for layer 2
	private CapsuleCollider col;					// a reference to the capsule collider of the character
    private Transform CamPos;                       // a reference to the CamPos object child of the character
    private Transform LookAtPos;
    private bool movingBackwards;                   // used to control if the character is moving backwards or not
    private bool buildMode;
    private bool selectMode;
    private Vector3 curNormal;                      // unused at the moment
    private UberCamera cameraScript;                // a reference to the main camera script
    private HexTileMap tileMapScript;               // a reference to the hex tilemap script
    private GameObject jetpack;
    private Transform buildMenu;
    private UberBuildMenu buildMenuScript;
    private float currentJumpSpeed;

	static int jumpState = Animator.StringToHash("Base Layer.Jump");				// and are used to check state for various actions to occur

	void Start ()
	{
		// initialising reference variables
		anim = GetComponent<Animator>();					  
		col = GetComponent<CapsuleCollider>();
        CamPos = GameObject.Find("CamPos").transform;
        LookAtPos = GameObject.Find("LookAtPos").transform;
        jetpack = GameObject.Find("jetpack");
        tileMapScript = GameObject.Find("HexEditor").GetComponent<HexTileMap>();
        buildMenu = GameObject.Find("BuildMenu").transform;
        buildMenuScript = buildMenu.GetComponent<UberBuildMenu>();
        buildMenu.gameObject.SetActive(false);
        jetpack.SetActive(false);
        cameraScript = Camera.main.GetComponent<UberCamera>();      
        movingBackwards = false;
        buildMode = false;
        selectMode = false;
        curNormal = Vector3.up;
        currentJumpSpeed = 0.0f;
		//enemy = GameObject.Find("Enemy").transform;	
		if(anim.layerCount ==2)
			anim.SetLayerWeight(1, 1);
	}

    void OnBuildControl()
    {
        if(buildMode)
        {
            //probably can be optimised
            Hex intersectedTile = tileMapScript.RuntimeHexFromWorld();
            if(intersectedTile != null)
            {
                buildMenu.gameObject.SetActive(true);
                if (selectMode)
                {
                    buildMenuScript.goRegular();
                }
                else buildMenu.position = intersectedTile.getBuildPosition();               
                //Camera.main.GetComponent<UberCamera>().SetLookatBuild(true);

            }
        }
    }

    void OnAnimatorMove()
    {
        if (anim)
        {
            var step = rotationSpeed * Time.deltaTime;

            //Move backwards
            if (anim.GetFloat("Speed") < 0)
            {
                if(!movingBackwards)
                {                   
                    Vector3 backwards = -transform.forward;

                    //Update control variables
                    movingBackwards = true;
                    cameraScript.lookForward = false;

                    //Then rotate
                    transform.rotation = Quaternion.LookRotation(backwards.normalized);
                }
                //Set speed
                anim.SetFloat("Speed", -anim.GetFloat("Speed"));
            }
            else if (anim.GetFloat("Speed") == 0)
            {
                /*if(movingBackwards)
                {
                    movingBackwards = false;
                    cameraScript.lookForward = true;
                }*/
            }
            else
            {
                if (movingBackwards)
                {
                    Vector3 backwards = -transform.forward;
                    transform.rotation = Quaternion.LookRotation(backwards.normalized);
                    movingBackwards = false;
                    cameraScript.lookForward = true;
                }
            }

            /*RaycastHit hit;
 
            if (Physics.Raycast(transform.position, -curNormal, out hit)) //- [ out hit ? ]
            {
    
                curNormal = Vector3.Lerp(curNormal, hit.normal, 9 * Time.deltaTime);
                Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, curNormal);
                transform.rotation = grndTilt * Quaternion.Euler(0, curDir, 0);
                //transform.rotation = grndTilt * Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }*/
               
            //Direction alteration
            Quaternion newRotation = transform.rotation;
            //Test if this is userfriendly or maybe add it as an option
            if(movingBackwards)
            {
                if (anim.GetFloat("Direction") < 0)
                    newRotation *= Quaternion.FromToRotation(transform.forward, transform.right);
                else if (anim.GetFloat("Direction") > 0)
                    newRotation *= Quaternion.FromToRotation(transform.forward, -transform.right);
            }
            else
            {
                if (anim.GetFloat("Direction") > 0)
                    newRotation *= Quaternion.FromToRotation(transform.forward, transform.right);
                else if (anim.GetFloat("Direction") < 0)
                    newRotation *= Quaternion.FromToRotation(transform.forward, -transform.right);
            }


            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, step);

            //Movement
            Vector3 newPosition = transform.position;
            newPosition += transform.forward * anim.GetFloat("Speed")*2.0f * runSpeed * Time.deltaTime;
            transform.position = newPosition;

            //Jump
            if (anim.GetBool("Jump"))
            {
                if (currentJumpSpeed <= maxJumpSpeed)
                {
                    currentJumpSpeed += jumpSpeed;
                    jumpSpeed += jumpIncrement;
                }
            }
            else
            {
                /*RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit)) //- [ out hit ? ]
                {
                    if (hit.distance < 1.75f)
                        currentJumpSpeed = 0.0f;
                    else
                    {
                        currentJumpSpeed -= jumpIncrement;
                        if (currentJumpSpeed < 0.0f)
                            currentJumpSpeed = 0.0f;
                    }
                }
                else
                {*/
                    currentJumpSpeed -= jumpSpeed;
                    if (currentJumpSpeed < 0.0f)
                        currentJumpSpeed = 0.0f;
                //}
            }
            newPosition += transform.up * currentJumpSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }
	
	void Update ()
    {
        #region Animation Input
            float h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
		    float v = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
		    anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
		    anim.SetFloat("Direction", h); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis		
		    anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'
		    currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

		    if(anim.layerCount ==2)		
			layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);	// set our layer2CurrentState variable to the current state of the second Layer (1) of animation
        #endregion

        #region keyboard
        // STANDARD JUMPING 

            if (Input.GetButtonDown("Jump"))
            {
                jetpack.SetActive(true);
                anim.SetBool("Jump", true);
                cameraScript.jumping = true;
            }

            if (Input.GetButtonUp("Jump"))
            {
                jetpack.SetActive(false);
                anim.SetBool("Jump", false);
                cameraScript.jumping = false;
            }

            // BUILD MODE

            if(Input.GetButtonDown("Build"))
            {
                if(buildMode)
                {
                    buildMode = false;
                    buildMenu.gameObject.SetActive(false);
                    selectMode = false;
                    Camera.main.GetComponent<UberCamera>().SetLookatBuild(false);
                }
                else
                {
                    buildMode = true;
                }              
            }

            if(buildMode)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    if (selectMode)
                    {
                        selectMode = false;
                        buildMenuScript.goHidden();
                    }
                    else selectMode = true;
                }
            }
        #endregion 

           OnBuildControl();
    }
}
