using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PinceExt : MonoBehaviour
    {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        this.SendMessageUpwards("BoxCollision", collision);

    }
    
    }
