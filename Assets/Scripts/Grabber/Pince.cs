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
            case move.GRAB:
                if (PinceGauche.transform.position.x < PinceDroite.transform.position.x - 50)  // -100 : nothing is smaller than 100 
                {
                    PinceGauche.transform.Translate(Time.deltaTime * speed, 0, 0);
                    PinceDroite.transform.Translate(-Time.deltaTime * speed, 0, 0);
                }
                else
                {
                    //Nothing Catch
                    endStep();

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
        if (direction == move.DOWN && collision.gameObject.GetComponent<PinceExt>() == null)
        {
            if (collision.gameObject.tag == "Box")
            {
                boxHeight = collision.gameObject.GetComponent<RectTransform>().sizeDelta.y;
                endStep();

                // claw.gameObject.SendMessage("BoxDetected");
            }
            else if (collision.gameObject.tag == "Ground")
            {

                endStep();

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

        endStep();
        nbSideTouching = 0;


    }
    private void endStep()
    {
        nextStep = true;
    }


}

