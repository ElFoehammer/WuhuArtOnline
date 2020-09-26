using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndDrop : MonoBehaviour
{

    public Camera cam;
    public float thrust = (float)1.0;
    GameObject grabbedObject; 
    float grabbedObjectSize;
    bool crouched;
    
    // Start is called before the first frame update

     GameObject GetMouseHoverObject(float range)
     {
         Vector3 position = gameObject.transform.position;
         RaycastHit raycastHit;
         Vector3 target = position + cam.transform.forward * range;
         if (Physics.Linecast(position,target,out raycastHit))
             {
             return raycastHit.collider.gameObject;
             }
         return null;
     }

    void TryGrabObject(GameObject grabObject)
    {
        if(grabObject == null || !CanGrab(grabObject))
            {
            return;
            }
        grabbedObject = grabObject;
        grabbedObjectSize = grabObject.GetComponent<Renderer>().bounds.size.magnitude;

    }
    
    bool CanGrab(GameObject candidate)
    {
        return candidate.GetComponent<Rigidbody>() != null;
    }
    
    void DropObject()
    {
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if(grabbedObject == null)
        {
            return;
        }

        if (rb!=null)
        {
            rb.velocity = Vector3.zero;
            if (!crouched)
            {
                rb.AddForce(cam.transform.forward * thrust);
            }
        }

        grabbedObject = null;
    }

    // Update is called once per frame
    void Update() {
        Debug.DrawRay(gameObject.transform.position, cam.transform.forward * 10, Color.red);

        // Input detection
        if (Input.GetKeyDown("e")){
            if (grabbedObject == null) {
                TryGrabObject(GetMouseHoverObject(10));
            }
            else {
                DropObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouched = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouched = false;
        }

        // Move grabbed object
        if (grabbedObject != null) {
            Vector3 newPosition = gameObject.transform.position + cam.transform.forward * grabbedObjectSize;
            grabbedObject.transform.position = newPosition;
        }
    }
}