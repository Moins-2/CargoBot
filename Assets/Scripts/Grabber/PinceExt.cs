using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PinceExt : MonoBehaviour
    {
    private Vector2 originalPos ;
    public int marge; 
    private void Start()
    {
        originalPos = this.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        this.SendMessageUpwards("BoxCollision", collision);

    }
    public bool isAtOriginalPos()
    {
      return this.transform.position.x< originalPos.x +marge && this.transform.position.x > originalPos.x - marge ? true : false;
    }
    
    }
