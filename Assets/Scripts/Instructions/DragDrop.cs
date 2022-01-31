using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTranform;
    public Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector3 originalPos;
    public Vector3 lastPos;
    public bool used;
 private int instruc;

   
    public int getInstruction()
    {
        return instruc;
    }
    public void setInstruction(int i)
    {
        instruc = i;
    }
    private void Awake()
    {
        rectTranform = GetComponent<RectTransform>();
        originalPos = transform.position;
        lastPos = transform.position;
        used = false;
        canvasGroup = GetComponent<CanvasGroup>();
        instruc = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTranform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        transform.position = used ? lastPos: originalPos;
        used = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Error Alredy taken");
        
    }
}
