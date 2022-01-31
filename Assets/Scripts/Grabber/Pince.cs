using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GrabberTool;


public class Pince : MonoBehaviour
{
        public PinceExt PinceDroite;
        public PinceExt PinceGauche;

        Vector3 tmpPosition;
        move direction;
        bool test;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Pince: Init");
            direction = move.NONE;
            tmpPosition = Vector3.negativeInfinity;
            test = false;

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
                    if (transform.position.y > tmpPosition.y - 2) // TODO: Replace with box detection
                    {
                        // Move the object to the right 1 unit/second.
                        transform.Translate(0, -Time.deltaTime, 0);
                    }
                    else
                    {
                        endStep();
                    }
                    break;
                case move.GRAB:
                    if (PinceGauche.transform.position.x < PinceDroite.transform.position.x - 1)
                    {
                        PinceGauche.transform.Translate(Time.deltaTime, 0, 0);
                        PinceDroite.transform.Translate(-Time.deltaTime, 0, 0);
                    }
                    else
                    endStep();
                    break;
                case move.UP:
                    if (transform.position.y < tmpPosition.y)
                    {
                        transform.Translate(0, Time.deltaTime, 0);
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
            
        test = false;
        }
        return test;
    }


    private void endStep()
    {
        test = true;
    }
        private void reachBottom()
        {
            print("Pince: Bottom Reached");

            test = true;
        } private void reachTop()
        {
            print("Pince: Cieling Reached");

            test = true;
        }
        private void grabbed()
        {
            print("Pince: Grabbed");

            test = true;
        }

   }

