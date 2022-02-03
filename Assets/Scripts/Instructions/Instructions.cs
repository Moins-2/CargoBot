using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] private DragDrop prefabDown;
    [SerializeField] private DragDrop prefabLeft;
    [SerializeField] private DragDrop prefabRight;
    [SerializeField] private ItemSlot prefabSlot;
    private ItemSlot[][] program;
    private float slotSize;
    private float instructionSize;
    private GameObject programPanel;
    private GameObject availablePanel;
    public int[] nb = new int[4] { 8, 5, 2, 8 };

    private void Awake()
    {
        GameObject screen = GameObject.Find("screen");
        canvas = screen.GetComponent<Canvas>();

        programPanel = GameObject.Find("program_panel");
        availablePanel = GameObject.Find("available_panel");

        Vector2 size = programPanel.GetComponent<RectTransform>().sizeDelta;
        slotSize = size.x / 10;
        instructionSize = slotSize * 0.8f;
    }
    // Start is called efore the first frame update
    void Start()
    {

        createAvailableInstructions();
        createSlot();
    }
    void createAvailableInstructions()
    {
        DragDrop down = Instantiate(prefabDown, new Vector2(3f, 3.5f), Quaternion.identity) as DragDrop;
        DragDrop left = Instantiate(prefabLeft, new Vector2(5.4f, 3.5f), Quaternion.identity) as DragDrop;
        DragDrop right = Instantiate(prefabRight, new Vector2(6.6f, 3.5f), Quaternion.identity) as DragDrop;
        DragDrop[] instructions = { down, left, right };
        down.setInstruction(1);
        left.setInstruction(3);
        right.setInstruction(4);


        int index = 1;
        foreach (DragDrop instruction in instructions)
        {
            instruction.GetComponent<RectTransform>().position = new Vector2(availablePanel.GetComponent<RectTransform>().position.x - (((instructions.Length + 1) / 2) * instructionSize * 1.5f) + index * instructionSize * 1.5f, availablePanel.GetComponent<RectTransform>().position.y - (availablePanel.GetComponent<RectTransform>().sizeDelta.y) / 2);
            instruction.GetComponent<RectTransform>().sizeDelta = new Vector2(instructionSize, instructionSize);
            instruction.transform.SetParent(availablePanel.transform);
            instruction.canvas = canvas;
            index++;
        }
    }

    void createSlot()
    {
        float x = programPanel.GetComponent<RectTransform>().position.x - programPanel.GetComponent<RectTransform>().rect.size.x / 2 + slotSize;
        float y = programPanel.GetComponent<RectTransform>().position.y + (programPanel.GetComponent<RectTransform>().sizeDelta.y / 2) - slotSize;
        float varX, varY;
        program = new ItemSlot[4][];
        for (int j = 0; j < 4; j++)
        {
            program[j] = new ItemSlot[nb[j]];
            varY = y - j * 1.3f * slotSize;
            for (int i = 0; i < nb[j]; i++)
            {
                varX = x + i * 1.1f * slotSize;

                program[j][i] = Instantiate(prefabSlot, new Vector2(varX, varY), Quaternion.identity);
                program[j][i].GetComponent<RectTransform>().sizeDelta = new Vector2(slotSize, slotSize);

                program[j][i].transform.SetParent(programPanel.transform);
            }
        }
    }
    public int getInstruction(int line, int step)
    {
        line--;
        step--;

        // Line doesn't exist
        if (line < 0 || line >= program.Length)
        {
            return -2;
        }
        // Step doesn't exist
        else if (step < 0 || step >= program[line].Length)
        {
            return -1;
        }

        return program[line][step].getInstruction();

    }

}
