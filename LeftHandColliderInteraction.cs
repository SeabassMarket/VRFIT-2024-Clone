using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandColliderInteraction : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameObject ScriptOrganizer;
    public GameObject VRInput;

    [Header("Info")]
    public float leftHandTimeInCollider;
    public float leftHandTotalTime;

    void OnTriggerEnter(Collider other)
    {
        if (!ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping && !VRInput.GetComponent<InputReader>().menuButtonSwitch)
        {
            if (other.gameObject.tag == "LeftHand")
            {
                leftHandTimeInCollider = leftHandTimeInCollider + Time.deltaTime;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping && !VRInput.GetComponent<InputReader>().menuButtonSwitch)
        {
            if (other.gameObject.tag == "LeftHand")
            {
                leftHandTimeInCollider = leftHandTimeInCollider + Time.deltaTime;
            }
        }
    }

    private void Update()
    {
        if (!ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
        {
            if (!VRInput.GetComponent<InputReader>().menuButtonSwitch)
            {
                leftHandTotalTime = leftHandTotalTime + Time.deltaTime;
            }
        }
        else
        {
            leftHandTimeInCollider = 0;
            leftHandTotalTime = 0;
        }
    }
}
