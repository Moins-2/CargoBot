using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEngine : MonoBehaviour
{
    public Instructions program;
    public Grabber grabber;
    private bool executing;
    private void Start()
    {
        executing = false;
    }
    public void execute()
    {
        executing = true;
    }
    private void Update()
    {
        if (executing)
        {
          StartCoroutine( Play(1, 1));
            executing = false;
        }
    }
    public IEnumerator Play(int line, int step)
    {
        if (grabber.isReady())
        {
            switch (program.getInstruction(line, step))
            {
                case 0:
                    //Nothing
                    yield return null;
                    break;
                case 1:
                    grabber.Down();
                    break;
                case 2:
                    grabber.Up();
                    break;
                case 3:
                    grabber.Left();
                    break;
                case 4:
                    grabber.Right();
                    break;
                case 10:
                case 20:
                case 30:
                case 40:
                    //Play the line concerned
                   StartCoroutine( Play(program.getInstruction(line, step) / 10, 1));

                    break;
                case -1:
                    //Step doesn't exist, end of the line, return
                    
                    yield break;

                case -2:
                    //Line doesn't exist
                    Debug.Log("End");
         
                    executing = false;
                    yield break;

                default:
                    throw new System.Exception("Unknown instruction");

            }

        }
        
        while (!grabber.isReady())
        {
            yield return null;
        }
          StartCoroutine( Play(line, step+1));
    }
}
