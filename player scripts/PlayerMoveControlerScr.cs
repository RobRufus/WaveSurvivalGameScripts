using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControlerScr : MonoBehaviour
{

    Animator animator;
    public float velocityX = 0.0f;
    public float velocityZ = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;
    public bool isGrounded;
    private float verticalVel;
    private Vector3 moveVector;
    private CharacterController controller;

    //for better performance
    int VelocityZHash, VelocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();

        //for better performance
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }
    


    void changeVelocity(bool wPressed, bool aPressed, bool sPressed, bool dPressed, bool shiftPressed, float currentMaxVelocity)
    {
        //walk anim velocity
        if (wPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (sPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (aPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (dPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        //decellerate
        if (!wPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }


    }


    void lockOrResetVelocity(bool wPressed, bool aPressed, bool sPressed, bool dPressed, bool shiftPressed, float currentMaxVelocity)
    {

        //reset velocity
        if (!wPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        if (!sPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        if (!aPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (!dPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }



        //Center velocity when in low bounds
        if (!aPressed && !dPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
        if (!sPressed && !wPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }




        //velocity lock
        if (wPressed && shiftPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        //decelerate back to max walk speed when let go of run key
        else if (wPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            //round to maxvelocity if within offset
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (wPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }


        //velocityZ Back lock
        if (sPressed && shiftPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        //decelerate back to max walk speed when let go of run key
        else if (sPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            //round to max velocity if within offset
            if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05))
            {
                velocityZ = -currentMaxVelocity;
            }
        }//round to max velocity if within offset
        else if (sPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
        {
            velocityZ = -currentMaxVelocity;
        }





        //velocityX Left lock
        if (aPressed && shiftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        //decelerate back to max walk speed when let go of run key
        else if (aPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            //round to maxvelocity if within offset
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05))
            {
                velocityX = -currentMaxVelocity;
            }
        }//round to max velocity if within offset
        else if (aPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }


        //velocityX Right lock
        if (dPressed && shiftPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        //decelerate back to max walk speed when let go of run key
        else if (dPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            //round to maxvelocity if within offset
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05))
            {
                velocityX = currentMaxVelocity;
            }
        }//round to maxvelocity if within offset
        else if (dPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
    }


    void PlayerMoveAndRotation()
    {

        //anim.SetFloat("Velocity X", AnimTrackerX);
        //anim.SetFloat("Velocity Z", AnimTrackerZ);

        var camera = Camera.main;
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * velocityZ + right * velocityX;

        this.GetComponent<CharacterController>().Move(desiredMoveDirection * Time.deltaTime * 3);

        this.transform.forward = forward;
    }


    // Update is called once per frame
    void Update()
    {
        bool wPressed = Input.GetKey(KeyCode.W);
        bool aPressed = Input.GetKey(KeyCode.A);
        bool sPressed = Input.GetKey(KeyCode.S);
        bool dPressed = Input.GetKey(KeyCode.D);
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);


        //change max velocity based on key press, this is a turnary opperator
        float currentMaxVelocity = shiftPressed ? maximumRunVelocity : maximumWalkVelocity;

        changeVelocity(wPressed, aPressed, sPressed, dPressed, shiftPressed, currentMaxVelocity);
        lockOrResetVelocity(wPressed, aPressed, sPressed, dPressed, shiftPressed, currentMaxVelocity);

        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);

        PlayerMoveAndRotation();


        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 2;
        }

        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);
    }
}
