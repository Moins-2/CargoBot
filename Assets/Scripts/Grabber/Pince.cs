using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GrabberTool;


public class Pince : MonoBehaviour
{
    public PinceExt PinceDroite;
    public PinceExt PinceGauche;
    [SerializeField] private int speed = 150;

    Vector3 tmpPosition;
    private Vector3 originalPos;

    move direction;
    bool nextStep;

    private float boxHeight = 0;
    private int nbSideTouching = 0; // To be sure that right and left side touch the box, so the box is grabbed (unused for now)
    private Collider2D box = null;
    // Start is called before the first frame update
    void Start()
    {
        direction = move.NONE;
        tmpPosition = Vector3.negativeInfinity;
        originalPos = transform.position;
        nextStep = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (direction != move.NONE && tmpPosition.Equals(Vector3.negativeInfinity))
        {
            tmpPosition = transform.position;
        }
        switch (direction)
        {
            case move.DOWN:
                /*   if (transform.position.y > tmpPosition.y - 600) // TODO: Replace with box detection
                   {
                       // Move the object to the right 1 unit/second.
                       transform.Translate(0, -Time.deltaTime * speed, 0);
                   }
                   else
                   {
                       endStep();
                   }*/
                transform.Translate(0, -Time.deltaTime * speed, 0);

                break;
            case move.ADJUSTMENT:
              

                if (transform.position.y > tmpPosition.y - boxHeight) // TODO: Replace with box detection
                {
                    // Move the object to the right 1 unit/second.
                    transform.Translate(0, -Time.deltaTime * speed, 0);
                }
                else
                {
                    endStep();
                }

                break;
            case move.UNGRAB:
                if (box)
                {
                    box.gameObject.transform.SetParent(GameObject.Find("boxes").transform);
                }
                if (!PinceDroite.isAtOriginalPos() && !PinceGauche.isAtOriginalPos())
                {
                    PinceGauche.transform.Translate(-Time.deltaTime * speed, 0, 0);
                    PinceDroite.transform.Translate(Time.deltaTime * speed, 0, 0);
                }
                else if (!PinceDroite.isAtOriginalPos())
                {
                    PinceDroite.transform.Translate(Time.deltaTime * speed, 0, 0);
                }
                else if (!PinceGauche.isAtOriginalPos())
                {
                    PinceGauche.transform.Translate(-Time.deltaTime * speed, 0, 0);
                }
                else
                {
                    endStep();
                }

                break;
            case move.GRAB:
                if (PinceGauche.transform.position.x < PinceDroite.transform.position.x - 75)  // -75 : nothing is smaller than 75 
                {
                    PinceGauche.transform.Translate(Time.deltaTime * speed, 0, 0);
                    PinceDroite.transform.Translate(-Time.deltaTime * speed, 0, 0);
                }
                else
                {
                    //Nothing Catched

                    this.SendMessageUpwards("NothingCatched");

                  //  endStep();

                }
                break;
            case move.UP:
                if (transform.position.y < originalPos.y)
                {
                    transform.Translate(0, Time.deltaTime * speed, 0);
                }
                else
                {
                    endStep();
                }

                break;


        }

    }

    /// <summary>
    /// Change the direction of the "Pince" if it's different that the one it already has
    /// </summary>
    /// <param name="type">direction of the movement</param>
    /// <returns>true: the movement is finished | false: the movement is running</returns>
    public bool movePince(move type)
    {

        if (direction != type)
        {
            switch (type)
            {
                case move.DOWN:
                    direction = move.DOWN;

                    break;
                case move.ADJUSTMENT:
                    tmpPosition = transform.position;
                    direction = move.ADJUSTMENT;

                    break;
                case move.UP:
                    direction = move.UP;
                    break;
                case move.GRAB:
                    direction = move.GRAB;
                    break; 
                case move.UNGRAB:
                    direction = move.UNGRAB;
                    break;
                default:
                    direction = move.NONE;
                    break;

            }

            nextStep = false;
        }

        return nextStep;  
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");

        if (direction == move.DOWN)
        {
            if (collision.gameObject.tag == "Box")
            {
                boxHeight = collision.gameObject.GetComponent<RectTransform>().sizeDelta.y;
                this.SendMessageUpwards("BoxTouched");

               // endStep();

                // claw.gameObject.SendMessage("BoxDetected");
            }
            else if (collision.gameObject.tag == "Ground")
            {
                Debug.Log("GROUND!!!!!!!!!!!");
                this.SendMessageUpwards("GroundTouched");

                //endStep();

                //  claw.gameObject.SendMessage("GroundDetected");
            }
            else
            {
                Debug.Log(" tag unknown");
            }
        }
    }
    private void BoxCollision(Collider2D collider)
    {
        /*  nbSideTouching++;
          if (nbSideTouching == 2)
          {
              endStep();
              nbSideTouching = 0;
          }  n*/
        box = collider;
        box.gameObject.transform.SetParent(this.transform);
        box.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

        this.SendMessageUpwards("BoxCatched");
      //  endStep();

    }
    private void endStep()
    {
        nextStep = true;
    }


}

