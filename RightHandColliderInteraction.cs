using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandColliderInteraction : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameObject ScriptOrganizer;
    public GameObject VRInput;

    [Header("Info")]
    public float rightHandTimeInCollider;
    public float rightHandTotalTime;

    void OnTriggerEnter (Collider other)
    {
        if (!ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping && !VRInput.GetComponent<InputReader>().menuButtonSwitch)
        {
            if (other.gameObject.tag == "RightHand")
            {
                rightHandTimeInCollider = rightHandTimeInCollider + Time.deltaTime;
            }
        }
    }

    void OnTriggerStay (Collider other)
    {
        if (!ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping && !VRInput.GetComponent<InputReader>().menuButtonSwitch)
        {
            if (other.gameObject.tag == "RightHand")
            {
                rightHandTimeInCollider = rightHandTimeInCollider + Time.deltaTime;
            }
        }
    }

    private void Update()
    {
        if (!ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
        {
            if (!VRInput.GetComponent<InputReader>().menuButtonSwitch)
            {
                rightHandTotalTime = rightHandTotalTime + Time.deltaTime;
            }
        }
        else
        {
            rightHandTotalTime = 0;
            rightHandTimeInCollider = 0;
        }
    }
}
