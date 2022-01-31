using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GrabberTool;





public class Grabber : MonoBehaviour
    {


        public Pince pince;

        Vector3 tmpPosition;

        private move direction;


        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Grabber: Init");
            tmpPosition = Vector3.negativeInfinity;
            direction = move.NONE;

        }
    public void Down()
    {
        direction = move.DOWN;  
    }
    public void Left()
    {
        direction = move.LEFT;  
    }
    public void Up()
    {
        direction = move.UP;  
    }
    public void Right()
    {
        direction = move.RIGHT;  
    }
    public bool isReady()
    {
        Debug.Log("Is ready :");
         Debug.Log(((direction == move.NONE) ? true : false));
        return direction == move.NONE ? true : false;
    }

        // Update is called once per frame
        void Update()
        {
            Move();


        }
        void Move()
        {

            if (direction != move.NONE && tmpPosition.Equals(Vector3.negativeInfinity))
            {
                tmpPosition = transform.position;


            }
            switch (direction)
            {
                case move.RIGHT:
                    if (transform.position.x < tmpPosition.x + 2)
                    {
                        // Move the object to the right 1 unit/second.
                        transform.Translate(Time.deltaTime, 0, 0);


                    }
                    else
                    {
                        tmpPosition = Vector3.negativeInfinity;
                        NextMove();
                    }
                    break;
                case move.LEFT:

                    if (transform.position.x > tmpPosition.x - 2)
                    {
                        // Move the object to the left 1 unit/second.
                        transform.Translate(-Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        tmpPosition = Vector3.negativeInfinity;
                        NextMove();
                    }
                    break;
                case move.UP:
                    if (pince.movePince(direction))
                    {
                        NextMove();
                    }
                    break;

                case move.DOWN:
                    if (pince.movePince(direction))
                    {
                        tmpPosition = transform.position;
                        direction = move.GRAB;
                    }

                    break;
                case move.GRAB:
                    if (pince.movePince(direction))
                    {
                        tmpPosition = transform.position;
                        direction = move.UP;
                    }
                    break;
                default:
                    break;
            }



        }
        void NextMove()
        {
        
            direction = move.NONE;
        }
    }

