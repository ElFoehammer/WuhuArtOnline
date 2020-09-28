using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatsuSpin : MonoBehaviour
{

    float i = 10;
    float j = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( j <= 20) {
            gameObject.transform.localScale = (new Vector3(20,i,20));
            i++;
        }

        if (j <= 39 && j >= 20)
        {
            gameObject.transform.localScale = (new Vector3(20, i, 20));
            i--;
        }

        if(j == 40)
        {
            j = 0;
        }
        j++;
    }
}
