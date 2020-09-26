using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Light>().type = LightType.Spot;
        GetComponent<Light>().range = 60;
        GetComponent<Light>().spotAngle = 80;

    }

    // Update is called once per frame
    void Update()
    {
    
    if (Input.GetKeyDown("f")) {
  
        if (GetComponent<Light>().intensity == 0){
         GetComponent<Light>().intensity = (float)1.5;
         }else{
         GetComponent<Light>().intensity = 0;
        }
     }
    
    }
}
