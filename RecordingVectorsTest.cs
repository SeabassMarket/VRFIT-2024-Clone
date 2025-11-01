using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingVectorsTest : MonoBehaviour
{
    public GameObject VRInput;
    private bool inputInitialized;
    List<Vector3> rightControllerVectors = new List<Vector3>();
    List<float> timesList = new List<float>();
    private float time = 0.0f;
    private int captures = 0;
    private bool finished = false;
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
            time = time + Time.deltaTime;
            if (rightControllerVectors.Count < 20 && time - captures * 0.2f > 0.2f)
            {
                rightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                timesList.Add(time);
                captures++;
            } else if (rightControllerVectors.Count >= 20 && !finished)
            {
                for (int i = 0; i < rightControllerVectors.Count; i++)
                {
                    //Debug.Log("Vector: " + rightControllerVectors[i].ToString() + "\n" + "Time: " + timesList[i].ToString());
                }
                finished = true;
            }
        }
    }
}
