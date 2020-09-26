using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    public float thrust = 10;
    public float RefreshTime = 4;
    //public float StopForce = 10;
    public float LagFrames = 10;
    float currentLag;
    public float techFrames = 30;
    float currentTech;
    public float thrustMultiplier = 1.2F;
    public float verticalTechThrust = 10;
    public CapsuleCollider JumpCollider;
    public Camera cam;
    bool canDash;
    bool inLag;
    bool grounded;
    bool validTech;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (inLag)
        {
            validTech = true;
            currentTech = techFrames;
        }
        grounded = true;
        stopLag();
    }

    private void tickTechOff(){
        
        if (Input.GetKeyDown(KeyCode.Space) && validTech)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 launch = cam.transform.forward;
            launch.y = verticalTechThrust;
            gameObject.GetComponent<Rigidbody>().AddForce(launch * thrust * thrustMultiplier);
            canDash = true;
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
        inLag = true;
        currentLag = LagFrames;
    }

    private void tickLagOff(){
        currentLag--;
        if (currentLag <= 0 && grounded)
        {
            stopLag();
        }
    }

    private void stopLag()
    {
        inLag = false;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && canDash){
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().AddForce(cam.transform.forward * thrust);
            canDash = false;
            enterLagDash();
        }

        if (inLag)
        {
            tickLagOff();
        }
        if (validTech)
        {
            tickTechOff();
        }
    }
}
