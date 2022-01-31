using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Play : MonoBehaviour, IPointerDownHandler
{
    public GameEngine engine;


    public void OnPointerDown(PointerEventData eventData)
    {
        engine.execute();
    }
}
