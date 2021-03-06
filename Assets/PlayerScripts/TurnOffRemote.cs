﻿using Mirror;
using UnityEngine;
using UnityEngine.Networking;

public class TurnOffRemote : NetworkBehaviour
{

public Camera cam;
public AudioListener ears;


    private void Start()
    {
        string id = string.Format("{0}", this.netId);
        UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController scr = this.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
        
        if(this.isLocalPlayer == true){
            scr.enabled = true;
            cam.enabled = true;
            ears.enabled = true;
        }
        else 
        {
            scr.enabled = false;
            cam.enabled = false;
            ears.enabled = false;

        }
    }

}
