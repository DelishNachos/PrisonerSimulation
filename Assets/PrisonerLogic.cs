using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrisonerLogic : MonoBehaviour
{
    private int[] _boxes = new int[100];
    private int[] _prisoners = new int[100];
    public int PBAmt = 100;
    public int runAmt = 1;

    public bool runSimulation = false;
    public bool randomizeSlips = false;
    public bool run100 = false;

    private bool passed;
    private int passedPrisoners;
    private int failedPrisoners;
    private int timesSurvived;
    private int timesRun;

    [SerializeField] private TextMeshProUGUI passedText;
    [SerializeField] private TextMeshProUGUI failedText;
    [SerializeField] private TextMeshProUGUI successText;
    [SerializeField] private TextMeshProUGUI percentageText;
    // Start is called before the first frame update
    void Start()
    {
        //createBoxes(PBAmt);
        //createPrisoners(PBAmt);
    }

    // Update is called once per frame
    void Update()
    {
        /*while(runSimulation)
		{
            simulate();
		}*/

        while(run100)
		{
            for (int i = 0; i < runAmt; i++)
            {
                simulate();
            }
            run100 = false;
        }
        percentageText.text = (((float)timesSurvived / timesRun) * 100).ToString() + "%";
    }

    public void createBoxes(int numBoxes) 
    {
        _boxes = new int[numBoxes];
        for (int i = 0; i < _boxes.Length; i++)
        {
            _boxes[i] = i + 1;
        }
        for (var i = _boxes.Length - 1; i > 0; i--)
        {
            int j = (int)Mathf.Floor(Random.value * (i + 1));
            var temp = _boxes[i];
            _boxes[i] = _boxes[j];
            _boxes[j] = temp;
        }
        PrisonerData.boxes = _boxes;
    }

    public void createPrisoners(int numPrisoners)
	{
        _prisoners = new int[numPrisoners];
		for (int i = 0; i < _prisoners.Length; i++)
		{
            _prisoners[i] = i + 1;
		}
	}

    public void ActivateSimulation()
	{
        runSimulation = true;
	}

    public void Run100()
	{
        run100 = true;
	}

    private void simulate()
	{
        createBoxes(PBAmt);
        createPrisoners(PBAmt);
        for (int i = 0; i < _prisoners.Length; i++)
        {
            passed = false;
            int boxesLeft = PBAmt / 2;
            int nextBox = i;
            while (boxesLeft > 0)
            {
                //Debug.Log("Choosing Boxes");
                int currentBox = nextBox;
                int currentSlip = PrisonerData.boxes[currentBox];
                if (currentSlip == _prisoners[i])
                {
                    passed = true;
                }
                nextBox = currentSlip - 1;
                boxesLeft--;
            }
            if (passed)
            {
                passedPrisoners++;
            }
            else
            {
                failedPrisoners++;
            }
        }
        //Debug.Log("Passed Prisoners: " + passedPrisoners);
        passedText.text = "Passed: " + passedPrisoners;
        //Debug.Log("Failed Prisoners: " + failedPrisoners);
        failedText.text = "Failed: " + failedPrisoners;
        if (passedPrisoners == PBAmt)
        {
            successText.text = "Survived";
            successText.color = Color.green;
            timesSurvived++;
        }
        else
        {
            successText.text = "Failed";
            successText.color = Color.red;
        }
        passedPrisoners = 0;
        failedPrisoners = 0;
        timesRun++;
        runSimulation = false;
    }
}
