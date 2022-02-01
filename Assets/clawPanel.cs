using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clawPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(GameObject.Find("screen").GetComponent<RectTransform>().sizeDelta.x - GameObject.Find("instruction_panel").GetComponent<RectTransform>().sizeDelta.x
            , this.GetComponent<RectTransform>().sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
