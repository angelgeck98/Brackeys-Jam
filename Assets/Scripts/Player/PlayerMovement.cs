using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float vertical;
    public float speed = 6f;
    
    
    
   
    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.up * vertical * speed * Time.deltaTime);
    }

   
}
