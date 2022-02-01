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
        move direction;
        bool nextStep;
    private float boxHeight = 0;
        // Start is called before the first frame update
        void Start()
        {
            direction = move.NONE;
            tmpPosition = Vector3.negativeInfinity;
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
                    if (PinceGauche.transform.position.x < PinceDroite.transform.position.x - 100)
                    {
                        PinceGauche.transform.Translate(Time.deltaTime * speed, 0, 0);
                        PinceDroite.transform.Translate(-Time.deltaTime * speed, 0, 0);
                    }
                    else
                    endStep();
                    break;
                case move.UP:
                    if (transform.position.y < tmpPosition.y)
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
        
        if(direction != type)
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
    private void OnCollisionEnter2D(Collision2D collision)
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
    }

    private void endStep()
    {
        nextStep = true;
    }
     

   }

