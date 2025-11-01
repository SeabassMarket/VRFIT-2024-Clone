using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public class ExerciseColliders : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameObject ScriptOrganizer;

    [Header("VR Input Manager")]
    public GameObject VRInput;
    public GameObject XRRig;

    [Header("Colliders Parent")]
    public GameObject AllColliders;

    [Header("Colliders")]
    public GameObject HeadCollider;
    public GameObject RightHandCollider;
    public GameObject LeftHandCollider;

    [Header("Hitboxes")]
    public GameObject HeadHitbox;
    public GameObject RightHandHitbox;
    public GameObject LeftHandHitbox;

    [Header("Changers")]
    public float hitboxOffset = 1.4f;
    public float colliderScale = 0.6f;

    private float headX;
    private float headY;
    private float headZ;
    private float rightHandX;
    private float rightHandY;
    private float rightHandZ;
    private float leftHandX;
    private float leftHandY;
    private float leftHandZ;
    private float xTranslation;
    private float zTranslation;
    private bool hitboxes = false;
    private bool offsetHitbox = false;
    private int currentExercise;
    private float exerciseTime = 0;
    private int captures = 0;
    private int totalCaptures = 0;
    private bool updatedOnce;
    public bool calibrationError = false;
    private List<Vector3> headRecordings;
    private List<Vector3> rightHandRecordings;
    private List<Vector3> leftHandRecordings;
    private Vector3 headRecording;
    private Vector3 rightHandRecording;
    private Vector3 leftHandRecording;

    // Start is called before the first frame update
    void Start()
    {
        //Set scale
        AllColliders.transform.localScale = new Vector3(colliderScale, colliderScale, colliderScale);
        currentExercise = ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().currentExercise;
        updatedOnce = false;
        calibrationError = false;
    }

    public void NeckRollPlay()
    {
        if (currentExercise == 0 && !calibrationError)
        {
            if (MovementVectorsScript.neckRollHeadVectorsStatic != null && MovementVectorsScript.neckRollRightControllerVectorsStatic != null && MovementVectorsScript.neckRollLeftControllerVectorsStatic != null)
            {
                if (MovementVectorsScript.neckRollHeadVectorsStatic.Count > 0 && MovementVectorsScript.neckRollRightControllerVectorsStatic.Count > 0 && MovementVectorsScript.neckRollLeftControllerVectorsStatic.Count > 0)
                {
                    headRecordings = MovementVectorsScript.neckRollHeadVectorsStatic;
                    rightHandRecordings = MovementVectorsScript.neckRollRightControllerVectorsStatic;
                    leftHandRecordings = MovementVectorsScript.neckRollLeftControllerVectorsStatic;
                }
                else
                {
                    calibrationError = true;
                }
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    if (!updatedOnce)
                    {
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[0].y;
                        headZ = XRRig.transform.position.z + (headRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[0].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[0].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);

                        exerciseTime = 0;
                        totalCaptures = 0;
                        captures = 0;
                        updatedOnce = true;
                    }
                } else
                {
                    exerciseTime += Time.deltaTime;
                    if (exerciseTime - totalCaptures * 0.1f > 0.1f)
                    {
                        totalCaptures++;
                        if (captures == headRecordings.Count - 1)
                        {
                            captures = 0;
                        } else
                        {
                            captures++;
                        }
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[captures].y;
                        headZ = XRRig.transform.position.z + (headRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[captures].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[captures].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                    }
                    updatedOnce = false;
                }
            } else
            {
                calibrationError = true;
            }
        }
    }

    public void SitTwistPlay()
    {
        if (currentExercise == 1 && !calibrationError)
        {
            if (MovementVectorsScript.sitTwistHeadVectorsStatic != null && MovementVectorsScript.sitTwistRightControllerVectorsStatic != null && MovementVectorsScript.sitTwistLeftControllerVectorsStatic != null)
            {
                if (MovementVectorsScript.sitTwistHeadVectorsStatic.Count > 0 && MovementVectorsScript.sitTwistRightControllerVectorsStatic.Count > 0 && MovementVectorsScript.sitTwistLeftControllerVectorsStatic.Count > 0)
                {
                    headRecordings = MovementVectorsScript.sitTwistHeadVectorsStatic;
                    rightHandRecordings = MovementVectorsScript.sitTwistRightControllerVectorsStatic;
                    leftHandRecordings = MovementVectorsScript.sitTwistLeftControllerVectorsStatic;
                }
                else
                {
                    calibrationError = true;
                }
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    if (!updatedOnce)
                    {
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[0].y;
                        headZ = XRRig.transform.position.z + (headRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[0].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[0].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);

                        exerciseTime = 0;
                        totalCaptures = 0;
                        captures = 0;
                        updatedOnce = true;
                    }
                }
                else
                {
                    exerciseTime += Time.deltaTime;
                    if (exerciseTime - totalCaptures * 0.1f > 0.1f)
                    {
                        totalCaptures++;
                        if (captures == headRecordings.Count - 1)
                        {
                            captures = 0;
                            exerciseTime = exerciseTime + 0.05f;
                        }
                        else
                        {
                            captures++;
                        }
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[captures].y;
                        headZ = XRRig.transform.position.z + (headRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[captures].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[captures].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                    }
                    updatedOnce = false;
                }
            }
            else
            {
                calibrationError = true;
            }
        }
    }

    public void PushUpPlay()
    {
        if (currentExercise == 2 && !calibrationError)
        {
            if (MovementVectorsScript.pushUpHeadVectorsStatic != null && MovementVectorsScript.pushUpRightControllerVectorsStatic != null && MovementVectorsScript.pushUpLeftControllerVectorsStatic != null)
            {
                if (MovementVectorsScript.pushUpHeadVectorsStatic.Count > 0 && MovementVectorsScript.pushUpRightControllerVectorsStatic.Count > 0 && MovementVectorsScript.pushUpLeftControllerVectorsStatic.Count > 0)
                {
                    headRecordings = MovementVectorsScript.pushUpHeadVectorsStatic;
                    rightHandRecordings = MovementVectorsScript.pushUpRightControllerVectorsStatic;
                    leftHandRecordings = MovementVectorsScript.pushUpLeftControllerVectorsStatic;
                }
                else
                {
                    calibrationError = true;
                }
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    if (!updatedOnce)
                    {
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[0].y;
                        headZ = XRRig.transform.position.z + (headRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[0].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[0].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);

                        exerciseTime = 0;
                        totalCaptures = 0;
                        captures = 0;
                        updatedOnce = true;
                    }
                }
                else
                {
                    exerciseTime += Time.deltaTime;
                    if (exerciseTime - totalCaptures * 0.1f > 0.1f)
                    {
                        totalCaptures++;
                        if (captures == headRecordings.Count - 1)
                        {
                            captures = 0;
                            exerciseTime = exerciseTime + 0.05f;
                        }
                        else
                        {
                            captures++;
                        }
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[captures].y;
                        headZ = XRRig.transform.position.z + (headRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[captures].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[captures].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                    }
                    updatedOnce = false;
                }
            }
            else
            {
                calibrationError = true;
            }
        }
    }

    public void HorsePosePlay()
    {
        if (currentExercise == 3 && !calibrationError)
        {
            if (MovementVectorsScript.horsePoseHeadVectorStatic != null && MovementVectorsScript.horsePoseRightControllerVectorStatic != null && MovementVectorsScript.horsePoseLeftControllerVectorStatic != null)
            {
                headRecording = MovementVectorsScript.horsePoseHeadVectorStatic;
                rightHandRecording = MovementVectorsScript.horsePoseRightControllerVectorStatic;
                leftHandRecording = MovementVectorsScript.horsePoseLeftControllerVectorStatic;
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    //Set head collider position
                    headX = XRRig.transform.position.x + (headRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    headY = XRRig.transform.position.y + headRecording.y;
                    headZ = XRRig.transform.position.z + (headRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                    //Set right hand collider position
                    rightHandX = XRRig.transform.position.x + (rightHandRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    rightHandY = XRRig.transform.position.y + rightHandRecording.y;
                    rightHandZ = XRRig.transform.position.z + (rightHandRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                    //Set left hand collider position
                    leftHandX = XRRig.transform.position.x + (leftHandRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    leftHandY = XRRig.transform.position.y + leftHandRecording.y;
                    leftHandZ = XRRig.transform.position.z + (leftHandRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                }
            }
        }
    }

    public void LungesPlay()
    {
        if (currentExercise == 4 && !calibrationError)
        {
            if (MovementVectorsScript.lungeHeadVectorsStatic != null && MovementVectorsScript.lungeRightControllerVectorsStatic != null && MovementVectorsScript.lungeLeftControllerVectorsStatic != null)
            {
                if (MovementVectorsScript.lungeHeadVectorsStatic.Count > 0 && MovementVectorsScript.lungeRightControllerVectorsStatic.Count > 0 && MovementVectorsScript.lungeLeftControllerVectorsStatic.Count > 0)
                {
                    headRecordings = MovementVectorsScript.lungeHeadVectorsStatic;
                    rightHandRecordings = MovementVectorsScript.lungeRightControllerVectorsStatic;
                    leftHandRecordings = MovementVectorsScript.lungeLeftControllerVectorsStatic;
                }
                else
                {
                    calibrationError = true;
                }
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    if (!updatedOnce)
                    {
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[0].y;
                        headZ = XRRig.transform.position.z + (headRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[0].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[0].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);

                        exerciseTime = 0;
                        totalCaptures = 0;
                        captures = 0;
                        updatedOnce = true;
                    }
                }
                else
                {
                    exerciseTime += Time.deltaTime;
                    if (exerciseTime - totalCaptures * 0.1f > 0.1f)
                    {
                        totalCaptures++;
                        if (captures == headRecordings.Count - 1)
                        {
                            captures = 0;
                            exerciseTime = exerciseTime + (1 - 0.33333333333f);
                        }
                        else
                        {
                            captures++;
                        }
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[captures].y;
                        headZ = XRRig.transform.position.z + (headRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[captures].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[captures].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                    }
                    updatedOnce = false;
                }
            }
            else
            {
                calibrationError = true;
            }
        }
    }

    public void SquatsPlay()
    {
        if (currentExercise == 5 && !calibrationError)
        {
            if (MovementVectorsScript.squatHeadVectorsStatic != null && MovementVectorsScript.squatRightControllerVectorsStatic != null && MovementVectorsScript.squatLeftControllerVectorsStatic != null)
            {
                if (MovementVectorsScript.squatHeadVectorsStatic.Count > 0 && MovementVectorsScript.squatRightControllerVectorsStatic.Count > 0 && MovementVectorsScript.squatLeftControllerVectorsStatic.Count > 0)
                {
                    headRecordings = MovementVectorsScript.squatHeadVectorsStatic;
                    rightHandRecordings = MovementVectorsScript.squatRightControllerVectorsStatic;
                    leftHandRecordings = MovementVectorsScript.squatLeftControllerVectorsStatic;
                }
                else
                {
                    calibrationError = true;
                }
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    if (!updatedOnce)
                    {
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[0].y;
                        headZ = XRRig.transform.position.z + (headRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[0].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[0].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);

                        exerciseTime = 0;
                        totalCaptures = 0;
                        captures = 0;
                        updatedOnce = true;
                    }
                }
                else
                {
                    exerciseTime += Time.deltaTime;
                    if (exerciseTime - totalCaptures * 0.1f > 0.1f)
                    {
                        totalCaptures++;
                        if (captures == headRecordings.Count - 1)
                        {
                            captures = 0;
                            exerciseTime = exerciseTime + (1 - 0.66666666667f);
                        }
                        else
                        {
                            captures++;
                        }
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[captures].y;
                        headZ = XRRig.transform.position.z + (headRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[captures].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[captures].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                    }
                    updatedOnce = false;
                }
            }
            else
            {
                calibrationError = true;
            }
        }
    }

    public void TreePosePlay()
    {
        if (currentExercise == 6 && !calibrationError)
        {
            if (MovementVectorsScript.treePoseHeadVectorStatic != null && MovementVectorsScript.treePoseRightControllerVectorStatic != null && MovementVectorsScript.treePoseLeftControllerVectorStatic != null)
            {
                headRecording = MovementVectorsScript.treePoseHeadVectorStatic;
                rightHandRecording = MovementVectorsScript.treePoseRightControllerVectorStatic;
                leftHandRecording = MovementVectorsScript.treePoseLeftControllerVectorStatic;
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    //Set head collider position
                    headX = XRRig.transform.position.x + (headRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    headY = XRRig.transform.position.y + headRecording.y;
                    headZ = XRRig.transform.position.z + (headRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                    //Set right hand collider position
                    rightHandX = XRRig.transform.position.x + (rightHandRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    rightHandY = XRRig.transform.position.y + rightHandRecording.y;
                    rightHandZ = XRRig.transform.position.z + (rightHandRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                    //Set left hand collider position
                    leftHandX = XRRig.transform.position.x + (leftHandRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    leftHandY = XRRig.transform.position.y + leftHandRecording.y;
                    leftHandZ = XRRig.transform.position.z + (leftHandRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                }
            }
        }
    }

    public void ChildPosePlay()
    {
        if (currentExercise == 7 && !calibrationError)
        {
            if (MovementVectorsScript.childPoseHeadVectorStatic != null && MovementVectorsScript.childPoseRightControllerVectorStatic != null && MovementVectorsScript.childPoseLeftControllerVectorStatic != null)
            {
                headRecording = MovementVectorsScript.childPoseHeadVectorStatic;
                rightHandRecording = MovementVectorsScript.childPoseRightControllerVectorStatic;
                leftHandRecording = MovementVectorsScript.childPoseLeftControllerVectorStatic;
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    //Set head collider position
                    headX = XRRig.transform.position.x + (headRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    headY = XRRig.transform.position.y + headRecording.y;
                    headZ = XRRig.transform.position.z + (headRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                    //Set right hand collider position
                    rightHandX = XRRig.transform.position.x + (rightHandRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    rightHandY = XRRig.transform.position.y + rightHandRecording.y;
                    rightHandZ = XRRig.transform.position.z + (rightHandRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                    //Set left hand collider position
                    leftHandX = XRRig.transform.position.x + (leftHandRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    leftHandY = XRRig.transform.position.y + leftHandRecording.y;
                    leftHandZ = XRRig.transform.position.z + (leftHandRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                }
            }
        }
    }

    public void PlankPosePlay()
    {
        if (currentExercise == 8 && !calibrationError)
        {
            if (MovementVectorsScript.plankPoseHeadVectorStatic != null && MovementVectorsScript.plankPoseRightControllerVectorStatic != null && MovementVectorsScript.plankPoseLeftControllerVectorStatic != null)
            {
                headRecording = MovementVectorsScript.plankPoseHeadVectorStatic;
                rightHandRecording = MovementVectorsScript.plankPoseRightControllerVectorStatic;
                leftHandRecording = MovementVectorsScript.plankPoseLeftControllerVectorStatic;
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    //Set head collider position
                    headX = XRRig.transform.position.x + (headRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    headY = XRRig.transform.position.y + headRecording.y;
                    headZ = XRRig.transform.position.z + (headRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                    //Set right hand collider position
                    rightHandX = XRRig.transform.position.x + (rightHandRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    rightHandY = XRRig.transform.position.y + rightHandRecording.y;
                    rightHandZ = XRRig.transform.position.z + (rightHandRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                    //Set left hand collider position
                    leftHandX = XRRig.transform.position.x + (leftHandRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    leftHandY = XRRig.transform.position.y + leftHandRecording.y;
                    leftHandZ = XRRig.transform.position.z + (leftHandRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                }
            }
        }
    }

    public void SitUpPlay()
    {
        if (currentExercise == 9 && !calibrationError)
        {
            if (MovementVectorsScript.sitUpHeadVectorsStatic != null && MovementVectorsScript.sitUpRightControllerVectorsStatic != null && MovementVectorsScript.sitUpLeftControllerVectorsStatic != null)
            {
                if (MovementVectorsScript.sitUpHeadVectorsStatic.Count > 0 && MovementVectorsScript.sitUpRightControllerVectorsStatic.Count > 0 && MovementVectorsScript.sitUpLeftControllerVectorsStatic.Count > 0)
                {
                    headRecordings = MovementVectorsScript.sitUpHeadVectorsStatic;
                    rightHandRecordings = MovementVectorsScript.sitUpRightControllerVectorsStatic;
                    leftHandRecordings = MovementVectorsScript.sitUpLeftControllerVectorsStatic;
                } else
                {
                    calibrationError = true;
                }
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    if (!updatedOnce)
                    {
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[0].y;
                        headZ = XRRig.transform.position.z + (headRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[0].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[0].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);

                        exerciseTime = 0;
                        totalCaptures = 0;
                        captures = 0;
                        updatedOnce = true;
                    }
                }
                else
                {
                    exerciseTime += Time.deltaTime;
                    if (exerciseTime - totalCaptures * 0.1f > 0.1f)
                    {
                        totalCaptures++;
                        if (captures == headRecordings.Count - 1)
                        {
                            captures = 0;
                        }
                        else
                        {
                            captures++;
                        }
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[captures].y;
                        headZ = XRRig.transform.position.z + (headRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[captures].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[captures].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                    }
                    updatedOnce = false;
                }
            }
            else
            {
                calibrationError = true;
            }
        }
    }

    public void BackBridgePlay()
    {
        if (currentExercise == 10 && !calibrationError)
        {
            if (MovementVectorsScript.backBridgeHeadVectorStatic != null && MovementVectorsScript.backBridgeRightControllerVectorStatic != null && MovementVectorsScript.backBridgeLeftControllerVectorStatic != null)
            {
                headRecording = MovementVectorsScript.backBridgeHeadVectorStatic;
                rightHandRecording = MovementVectorsScript.backBridgeRightControllerVectorStatic;
                leftHandRecording = MovementVectorsScript.backBridgeLeftControllerVectorStatic;
                if (ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().prepping)
                {
                    //Set head collider position
                    headX = XRRig.transform.position.x + (headRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    headY = XRRig.transform.position.y + headRecording.y;
                    headZ = XRRig.transform.position.z + (headRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                    //Set right hand collider position
                    rightHandX = XRRig.transform.position.x + (rightHandRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    rightHandY = XRRig.transform.position.y + rightHandRecording.y;
                    rightHandZ = XRRig.transform.position.z + (rightHandRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                    //Set left hand collider position
                    leftHandX = XRRig.transform.position.x + (leftHandRecording.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecording.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    leftHandY = XRRig.transform.position.y + leftHandRecording.y;
                    leftHandZ = XRRig.transform.position.z + (leftHandRecording.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecording.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                }
            }
        }
    }

    public void SitSideStretchPlay()
    {
        if (currentExercise == 11 && !calibrationError)
        {
            if (MovementVectorsScript.sitSideStretchHeadVectorsStatic != null && MovementVectorsScript.sitSideStretchRightControllerVectorsStatic != null && MovementVectorsScript.sitSideStretchLeftControllerVectorsStatic != null)
            {
                if (MovementVectorsScript.sitSideStretchHeadVectorsStatic.Count > 0 && MovementVectorsScript.sitSideStretchRightControllerVectorsStatic.Count > 0 && MovementVectorsScript.sitSideStretchLeftControllerVectorsStatic.Count > 0)
                {
                    headRecordings = MovementVectorsScript.sitSideStretchHeadVectorsStatic;
                    rightHandRecordings = MovementVectorsScript.sitSideStretchRightControllerVectorsStatic;
                    leftHandRecordings = MovementVectorsScript.sitSideStretchLeftControllerVectorsStatic;
                }
                else
                {
                    calibrationError = true;
                }
                    if (!updatedOnce)
                    {
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[0].y;
                        headZ = XRRig.transform.position.z + (headRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[0].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[0].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[0].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[0].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[0].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[0].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);

                        exerciseTime = 0;
                        totalCaptures = 0;
                        captures = 0;
                        updatedOnce = true;
                    }
                else
                {
                    exerciseTime += Time.deltaTime;
                    if (exerciseTime - totalCaptures * 0.1f > 0.1f)
                    {
                        totalCaptures++;
                        if (captures == headRecordings.Count - 1)
                        {
                            captures = 0;
                        }
                        else
                        {
                            captures++;
                        }
                        //Set head collider position
                        headX = XRRig.transform.position.x + (headRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + headRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        headY = XRRig.transform.position.y + headRecordings[captures].y;
                        headZ = XRRig.transform.position.z + (headRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - headRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        HeadCollider.transform.position = new Vector3(headX, headY, headZ);

                        //Set right hand collider position
                        rightHandX = XRRig.transform.position.x + (rightHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + rightHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        rightHandY = XRRig.transform.position.y + rightHandRecordings[captures].y;
                        rightHandZ = XRRig.transform.position.z + (rightHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - rightHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

                        //Set left hand collider position
                        leftHandX = XRRig.transform.position.x + (leftHandRecordings[captures].x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + leftHandRecordings[captures].z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        leftHandY = XRRig.transform.position.y + leftHandRecordings[captures].y;
                        leftHandZ = XRRig.transform.position.z + (leftHandRecordings[captures].z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - leftHandRecordings[captures].x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                        LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);
                    }
                    updatedOnce = false;
                }
            }
            else
            {
                calibrationError = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentExercise = ScriptOrganizer.GetComponent<ExerciseAnimationsScript>().currentExercise;
        if (!VRInput.GetComponent<InputReader>().menuButtonSwitch)
        {
            NeckRollPlay();
            SitTwistPlay();
            PushUpPlay();
            HorsePosePlay();
            LungesPlay();
            SquatsPlay();
            TreePosePlay();
            ChildPosePlay();
            PlankPosePlay();
            SitUpPlay();
            BackBridgePlay();
            SitSideStretchPlay();

            hitboxes = VRInput.GetComponent<InputReader>().rightPrimarySwitch;
            if (hitboxes)
            {
                //turn on hitboxes
                HeadHitbox.SetActive(true);
                RightHandHitbox.SetActive(true);
                LeftHandHitbox.SetActive(true);

                //set hitboxes on top of the colliders
                HeadHitbox.transform.position = HeadCollider.transform.position;
                RightHandHitbox.transform.position = RightHandCollider.transform.position;
                LeftHandHitbox.transform.position = LeftHandCollider.transform.position;

                offsetHitbox = VRInput.GetComponent<InputReader>().rightSecondarySwitch;
                //if offset is on, offset hitboxes to better demonstrate
                if (offsetHitbox)
                {
                    xTranslation = (0 * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + hitboxOffset * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    zTranslation = (hitboxOffset * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - 0 * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    HeadHitbox.transform.Translate(xTranslation, 0, zTranslation);
                    RightHandHitbox.transform.Translate(xTranslation, 0, zTranslation);
                    LeftHandHitbox.transform.Translate(xTranslation, 0, zTranslation);
                }
            } else
            {
                HeadHitbox.SetActive(false);
                RightHandHitbox.SetActive(false);
                LeftHandHitbox.SetActive(false);
            }
            Debug.Log(calibrationError.ToString());
        }
    }
}

/*//Set head collider position
headX = XRRig.transform.position.x + (VRInput.GetComponent<InputReader>().headPosition.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + VRInput.GetComponent<InputReader>().headPosition.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
headY = XRRig.transform.position.y + VRInput.GetComponent<InputReader>().headPosition.y;
headZ = XRRig.transform.position.z + (VRInput.GetComponent<InputReader>().headPosition.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - VRInput.GetComponent<InputReader>().headPosition.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
HeadCollider.transform.position = new Vector3(headX, headY, headZ);

//Set right hand collider position
rightHandX = XRRig.transform.position.x + (VRInput.GetComponent<InputReader>().rightControllerPosition.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + VRInput.GetComponent<InputReader>().rightControllerPosition.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
rightHandY = XRRig.transform.position.y + VRInput.GetComponent<InputReader>().rightControllerPosition.y;
rightHandZ = XRRig.transform.position.z + (VRInput.GetComponent<InputReader>().rightControllerPosition.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - VRInput.GetComponent<InputReader>().rightControllerPosition.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
RightHandCollider.transform.position = new Vector3(rightHandX, rightHandY, rightHandZ);

//Set left hand collider position
leftHandX = XRRig.transform.position.x + (VRInput.GetComponent<InputReader>().leftControllerPosition.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + VRInput.GetComponent<InputReader>().leftControllerPosition.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
leftHandY = XRRig.transform.position.y + VRInput.GetComponent<InputReader>().leftControllerPosition.y;
leftHandZ = XRRig.transform.position.z + (VRInput.GetComponent<InputReader>().leftControllerPosition.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - VRInput.GetComponent<InputReader>().leftControllerPosition.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
LeftHandCollider.transform.position = new Vector3(leftHandX, leftHandY, leftHandZ);*/