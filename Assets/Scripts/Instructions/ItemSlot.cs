using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");


        if (eventData.pointerDrag != null)
        {
            DragDrop item = eventData.pointerDrag.GetComponent<DragDrop>();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            item.lastPos = GetComponent<RectTransform>().position;
            item.used = true;

        }
    }

    public int getInstruction()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(GetComponent<RectTransform>().position, .5f);
       
        if( hitColliders.Length == 0)
        {
            //There is nothing
            return 0;
        }
        else if (hitColliders.Length > 1)
        {
            throw new System.Exception("Too much instruction on the slot");
        }

        
           return hitColliders[0].GetComponent<DragDrop>().getInstruction();
            
        

    }
}
