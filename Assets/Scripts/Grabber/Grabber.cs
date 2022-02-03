using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GrabberTool;





public class Grabber : MonoBehaviour
{


    public Pince pince;
    [SerializeField] private int speed;
    Vector3 tmpPosition;

    private move direction;

    private bool boxCatched = false;
    private bool groundTouched = false;
    private bool boxTouched = false;


    // Start is called before the first frame update
    void Start()
    {
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
                if (transform.position.x < tmpPosition.x + 200)
                {
                    // Move the object to the right 1 unit/second.
                    transform.Translate(Time.deltaTime * speed, 0, 0);
                }
                else
                {
                    tmpPosition = Vector3.negativeInfinity;
                    NextMove();
                }
                break;
            case move.LEFT:

                if (transform.position.x > tmpPosition.x - 200)
                {
                    // Move the object to the left 1 unit/second.
                    transform.Translate(-Time.deltaTime * speed, 0, 0);
                }
                else
                {
                    tmpPosition = Vector3.negativeInfinity;
                    NextMove();
                }
                break;
            

            case move.DOWN:
                if (boxTouched || pince.movePince(direction))
                {
                    tmpPosition = transform.position;
                    direction = move.ADJUSTMENT;
                    boxTouched = false;

                }
                else if (groundTouched || pince.movePince(direction))
                {
                    tmpPosition = transform.position;
                    direction = move.UNGRAB;
                    groundTouched = false;
                }
                break;
      
            case move.ADJUSTMENT:
                if (pince.movePince(direction))
                {
                    tmpPosition = transform.position;
                    direction = move.GRAB;
                }

                break;
            case move.GRAB:
                if (boxCatched || pince.movePince(direction))
                {
                    tmpPosition = transform.position;
                    direction = move.UP;
                    boxCatched = false;

                }
                break;
            case move.UNGRAB:
                if (pince.movePince(direction))
                {
                    tmpPosition = transform.position;
                    direction = move.UP;
                }
                break;
            case move.UP:
                if (pince.movePince(direction))
                {
                    NextMove();
                }
                break;
            default:
                break;
        }



    }

    private void BoxTouched()
    {
        boxTouched = true;
    }  private void BoxCatched()
    {
        boxCatched = true;
    }
    private void GroundTouched()
    {
        groundTouched = true;
    }

    void NextMove()
    {

        direction = move.NONE;
    }
}

