using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrisonerUIManager : MonoBehaviour
{
    public PrisonerLogic PL;
    public TMP_InputField prisonerInput;
    public TMP_InputField runInput;
    // Start is called before the first frame update
    void Start()
    {
        PL = GameObject.FindGameObjectWithTag("Logic").GetComponent<PrisonerLogic>();
        prisonerInput.text = "100";
        runInput.text = "1";
    }

    // Update is called once per frame
    void Update()
    {
        if(int.TryParse(prisonerInput.text, out PL.PBAmt))
            PL.PBAmt = int.Parse(prisonerInput.text);
        if (int.TryParse(runInput.text, out PL.runAmt))
            PL.runAmt = int.Parse(runInput.text);
        
    }
}
