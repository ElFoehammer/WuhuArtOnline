using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketWall3 : MonoBehaviour
{
    public float scrollSpeed = 0.2f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
