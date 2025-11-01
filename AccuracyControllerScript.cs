using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccuracyControllerScript : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameObject ScriptOrganizer;

    [Header("Player Collider Information")]
    public GameObject head;
    public GameObject rightHand;
    public GameObject leftHand;

    [Header("Accuracy Text")]
    public TMP_Text accuracyText;

    private float overallTimeIn;
    private float overallTime;

    //Update is called once per frame
    void Update()
    {
        overallTimeIn = head.GetComponent<HeadColliderInteraction>().headTimeInCollider + rightHand.GetComponent<RightHandColliderInteraction>().rightHandTimeInCollider + leftHand.GetComponent<LeftHandColliderInteraction>().leftHandTimeInCollider;
        overallTime = head.GetComponent<HeadColliderInteraction>().headTotalTime + rightHand.GetComponent<RightHandColliderInteraction>().rightHandTotalTime + leftHand.GetComponent<LeftHandColliderInteraction>().leftHandTotalTime;
        if (overallTime == 0)
        {
            accuracyText.text = "100%";
        } else
        {
            accuracyText.text = Mathf.Round((overallTimeIn / overallTime) * 100).ToString() + "%";
        }
    }
}
