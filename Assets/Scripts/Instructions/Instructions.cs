using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
     public Canvas   canvas;
     public  DragDrop  prefabDown   ;
     public  DragDrop  prefabUp     ;
     public  DragDrop  prefabLeft   ;
     public  DragDrop  prefabRight  ;
    public ItemSlot prefabSlot;
    private ItemSlot[][] program;
    // Start is called efore the first frame update
    void Start()       
    {
        createAvailableInstructions();
        createSlot();
    }
    void createAvailableInstructions()
    {
        DragDrop down = Instantiate(prefabDown, new Vector2(3f, 2.5f), Quaternion.identity) as DragDrop;
        DragDrop up = Instantiate(prefabUp, new Vector2(4.2f, 2.5f), Quaternion.identity) as DragDrop;
        DragDrop left = Instantiate(prefabLeft, new Vector2(5.4f, 2.5f), Quaternion.identity) as DragDrop;
        DragDrop right = Instantiate(prefabRight, new Vector2(6.6f, 2.5f), Quaternion.identity) as DragDrop;
        DragDrop[] instructions = { down, up, left, right };
        down.setInstruction(1);
        up.setInstruction(2);
        left.setInstruction(3);
        right.setInstruction(4);
        foreach (DragDrop instruction in instructions)
        {
            instruction.transform.SetParent(GameObject.Find("Available").transform);
            instruction.canvas = canvas;
        }
    }
    void createSlot()
    {

        int[] nb = new int[4] {8,5,2,8};
        program = new ItemSlot[4][];
        for (int j = 0; j < 4; j++)
        {
            program[j] = new ItemSlot[nb[j]];

            for (int i = 0; i < nb[j]; i++)
            {
                program[j][i] = Instantiate(prefabSlot, new Vector2(2f + (i * .85f), 1.5f - (j*1)), Quaternion.identity);
            program[j][i].transform.SetParent(GameObject.Find("Program").transform);
            }
        }
    }
    public int getInstruction(int line, int step)
    {
        line--;
        step--;

        // Line doesn't exist
        if (line<0 || line >= program.Length)
        {
            return -2; 
        }
        // Step doesn't exist
        else if( step < 0 || step >= program[line].Length)
        {
            return -1;
        }

        return program[line][step].getInstruction();

    }
  
}
