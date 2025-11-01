using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadColliderInteraction : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameObject ScriptOrganizer;
    public GameObject VRInput;

    [Header("Info")]
    public float headTimeInCollider;
    public float headTotalTime;

    void OnTriggerEnter(Collider other)
    {
        if (!ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping && !VRInput.GetComponent<InputReader>().menuButtonSwitch)
        {
            if (other.gameObject.tag == "Head")
            {
                headTimeInCollider = headTimeInCollider + Time.deltaTime;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping && !VRInput.GetComponent<InputReader>().menuButtonSwitch)
        {
            if (other.gameObject.tag == "Head")
            {
                headTimeInCollider = headTimeInCollider + Time.deltaTime;
            }
        }
    }

    private void Update()
    {
        if (!ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
        {
            if (!VRInput.GetComponent<InputReader>().menuButtonSwitch)
            {
                headTotalTime = headTotalTime + Time.deltaTime;
            }
        }
        else
        {
            headTimeInCollider = 0;
            headTotalTime = 0;
        }
    }
}
