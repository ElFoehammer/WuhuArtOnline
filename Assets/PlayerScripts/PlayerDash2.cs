using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash2 : MonoBehaviour
{

    public float thrust = 10000;
    public float yDashNerf = 0.6F;
    public float LagFrames = 20;
    float currentLag;
    public float LagJFrames = 30;
    float currentJLag;
    public float techFrames = 20;
    float currentTech;
    public float framesFromDash = 30;
    float currentFromDash;
    public float framesWaveDash = 20;
    float currentWaveDash;
    public float thrustMultiplier = 1.2F;
    public float thrust2Multiplier = 1.2F;
    public float verticalTechThrust = 2;
    public CapsuleCollider JumpCollider;
    public CapsuleCollider noFriction;
    public Camera cam;
    bool canDash;
    bool inLag;
    bool inDashWin;
    bool grounded;
    bool validTech;
    bool inJLag;
    bool inWaveDash;
    bool spaceDown;
    public bool secondTech;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (inDashWin)
        {
            validTech = true;
            currentTech = techFrames;
        } else {
            validTech = false;
        }
        grounded = true;
        stopLag();
    }

    private void tickTechOff(){
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (validTech && !inJLag) {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Vector3 launch = cam.transform.forward;
                launch.y = verticalTechThrust;
                gameObject.GetComponent<Rigidbody>().AddForce(launch * thrust * thrustMultiplier);
                gameObject.GetComponent<AudioSource>().Play();
                canDash = true;
            }
        }

        if (validTech)
        {
            currentTech--;
            if(currentTech <= 0)
            {
                validTech = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        grounded = false;
    }

    private void enterLagDash()
    {
        canDash = false;
        inLag = true;
        currentLag = LagFrames;
    }

    private void exitWaveDash()
    {
        noFriction.enabled = false;
        currentWaveDash = 0;
        inWaveDash = false;
    }

    private void enterWaveDash()
    {
        noFriction.enabled = true;
        currentWaveDash = framesWaveDash;
        inWaveDash = true;
    }

    private void tickJLagOff(){
        currentJLag--;
        if (currentJLag <= 0 )
        {
            inJLag = false;
        }
    }

    private void stopLag()
    {
        inLag = false;
        canDash = true;
    }

    private void tickLagOff()
    {
        currentLag--;
        if (currentLag <= 0 && grounded)
        {
            stopLag();
        }
    }

    private void tickTechWinOff()
    {
        currentFromDash--;
        if (currentFromDash <= 0)
        {
            inDashWin = false;
        }
    }

    private void tickWaveDashOff()
    {
        currentWaveDash--;

        if (currentWaveDash <= techFrames && secondTech)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Vector3 launch = cam.transform.forward;
                launch.y = verticalTechThrust;
                gameObject.GetComponent<Rigidbody>().AddForce(launch * thrust * thrust2Multiplier);
                gameObject.GetComponent<AudioSource>().Play();
                canDash = true;
            }
        }
        if (currentWaveDash <= 0)
        {
            exitWaveDash();
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceDown = true;
            if (!grounded)
            {
                currentJLag = LagJFrames;
                inJLag = true;
                if (spaceDown)
                {
                    enterWaveDash();
                }
            }
            
        }


        if (Input.GetKeyDown("q") && canDash)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 dash = cam.transform.forward;
            dash.y *= yDashNerf;
            gameObject.GetComponent<Rigidbody>().AddForce(dash * thrust);
            enterLagDash();
            currentFromDash = framesFromDash;
            inDashWin = true;
        }

        if (inLag)
        {
            tickLagOff();
        }
        if (validTech)
        {
            tickTechOff();
        }
        if (inJLag)
        {
            tickJLagOff();
        }
        if (inDashWin)
        {
            tickTechWinOff();
        }
        if (inWaveDash)
        {
            tickWaveDashOff();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            exitWaveDash();
        }
    }
}
