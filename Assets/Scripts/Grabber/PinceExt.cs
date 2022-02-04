using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PinceExt : MonoBehaviour
    {
    private float originalPos ;
    public int marge; 
    private void Start()
    {
        //originalPos =  this.transform.position;
        originalPos = GameObject.Find("grab_panel").GetComponent<RectTransform>().transform.position.x - this.transform.position.x;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        this.SendMessageUpwards("BoxCollision", collision);
    }
    
    public bool isAtOriginalPos()
    {
        float parentX = GameObject.Find("grab_panel").GetComponent<RectTransform>().transform.position.x;

        if (parentX - this.transform.position.x < originalPos + marge
            && parentX - this.transform.position.x >  originalPos - marge)
            return true;
        else return false;




    //  return this.transform.position.x< originalPos.x +marge && this.transform.position.x > originalPos.x - marge ? true : false;
    }
    
    }
