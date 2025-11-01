using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestingTextControl : MonoBehaviour
{
    public GameObject VRInput;
    private bool inputInitialized;
    public TMP_Text testText;
    private string testTextString;
    // Start is called before the first frame update
    void Start()
    {
        inputInitialized = VRInput.GetComponent<InputReader>().initialized;
    }

    // Update is called once per frame
    void Update()
    {
        inputInitialized = VRInput.GetComponent<InputReader>().initialized;
        if (inputInitialized)
        {
            //testText.text = VRInput.GetComponent<InputReader>().rightPrimarySwitch.ToString() + "\n" + VRInput.GetComponent<InputReader>().rightSecondarySwitch.ToString() + "\n" + VRInput.GetComponent<InputReader>().leftPrimarySwitch.ToString() + "\n" + VRInput.GetComponent<InputReader>().leftSecondarySwitch.ToString();
            //testText.text = VRInput.GetComponent<InputReader>().eulerHeadRotation.ToString() + "\n" + VRInput.GetComponent<InputReader>().headPosition.ToString();
            testTextString = "";
            for (int i = 0; i < SelectedExercisesScript.staticSelectedExercises.Count; i++)
            {
                testTextString = testTextString + SelectedExercisesScript.staticSelectedExercises[i].ToString();
            }
            testText.text = testTextString;
        } else
        {
            testText.text = "Please make sure all devices are connected";
        }
    }
}
