using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalibrationRecordingScript : MonoBehaviour
{
    [Header("VR Input Manager")]
    public GameObject VRInput;

    [Header("Other Scripts")]
    public GameObject ScriptOrganizer;

    [Header("Dummies")]
    public GameObject Dummy0;
    public GameObject Dummy1;
    protected Animator dummy0Animator;
    protected Animator dummy1Animator;

    [Header("Instructors")]
    public GameObject InstructorTextCanvas;
    public TMP_Text instructorText;
    public GameObject InformationTextCanvas;
    public TMP_Text timerText;

    private int selectedExercise;
    private int prepLength = 5;
    private float exerciseLength;
    private float calibrationTime = 0;
    private int captures;
    private bool clearedOnce = false;
    private bool initialCapture = false;

    List<string> exerciseNames = new List<string> { "Neck Roll", "Sit Twist", "Push-up", "Horse Pose", "Lunge", "Squat", "Tree Pose", "Child's Pose", "Plank", "Situps", "Back Bridge", "Sit Side Stretch" };
    List<string> stillExcerciseAnimationNames = new List<string> { "Still Armature|NeckRoll", "Still Armature|SitTwist", "Still Armature|PushUp", "HorsePose", "Still Lunge", "Still Squat", "TreePose", "Armature| ChildPose", "Armature|PlankPose", "Still Armature|SitUp", "BackBridge", "Still Armature|SitSideStretch" };
    List<string> exerciseAnimationNames = new List<string> { "Armature|NeckRoll", "Armature|SitTwist", "Armature|PushUp", "HorsePose", "Lunge", "Squat", "TreePose", "Armature| ChildPose", "Armature|PlankPose", "Armature|SitUp", "BackBridge", "Armature|SitSideStretch" };

    //Vector Lists
    public List<Vector3> neckRollHeadVectors;
    public List<Vector3> neckRollRightControllerVectors;
    public List<Vector3> neckRollLeftControllerVectors;

    public List<Vector3> sitTwistHeadVectors;
    public List<Vector3> sitTwistRightControllerVectors;
    public List<Vector3> sitTwistLeftControllerVectors;

    public List<Vector3> pushUpHeadVectors;
    public List<Vector3> pushUpRightControllerVectors;
    public List<Vector3> pushUpLeftControllerVectors;

    public Vector3 horsePoseHeadVector;
    public Vector3 horsePoseRightControllerVector;
    public Vector3 horsePoseLeftControllerVector;

    public List<Vector3> lungeHeadVectors;
    public List<Vector3> lungeRightControllerVectors;
    public List<Vector3> lungeLeftControllerVectors;

    public List<Vector3> squatHeadVectors;
    public List<Vector3> squatRightControllerVectors;
    public List<Vector3> squatLeftControllerVectors;

    public Vector3 treePoseHeadVector;
    public Vector3 treePoseRightControllerVector;
    public Vector3 treePoseLeftControllerVector;

    public Vector3 childPoseHeadVector;
    public Vector3 childPoseRightControllerVector;
    public Vector3 childPoseLeftControllerVector;

    public Vector3 plankPoseHeadVector;
    public Vector3 plankPoseRightControllerVector;
    public Vector3 plankPoseLeftControllerVector;

    public List<Vector3> sitUpHeadVectors;
    public List<Vector3> sitUpRightControllerVectors;
    public List<Vector3> sitUpLeftControllerVectors;

    public Vector3 backBridgeHeadVector;
    public Vector3 backBridgeRightControllerVector;
    public Vector3 backBridgeLeftControllerVector;

    public List<Vector3> sitSideStretchHeadVectors;
    public List<Vector3> sitSideStretchRightControllerVectors;
    public List<Vector3> sitSideStretchLeftControllerVectors;

    // Start is called before the first frame update
    void Start()
    {
        dummy0Animator = Dummy0.GetComponent<Animator>();
        dummy1Animator = Dummy1.GetComponent<Animator>();
        selectedExercise = ScriptOrganizer.GetComponent<CalibrationMenuScript>().exerciseNumber;
    }

    //Neck Roll
    private void NeckRollCalibration()
    {
        if (selectedExercise == 0)
        {
            if (!clearedOnce)
            {
                neckRollHeadVectors.Clear();
                neckRollRightControllerVectors.Clear();
                neckRollLeftControllerVectors.Clear();
                clearedOnce = true;
            }
            Dummy0.SetActive(true);
            Dummy1.SetActive(false);
            exerciseLength = 2.0f;
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy0Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else if (calibrationTime <= exerciseLength + prepLength)
            {
                if (!initialCapture)
                {
                    neckRollHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    neckRollRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    neckRollLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    initialCapture = true;
                }
                if (calibrationTime - prepLength - captures * 0.1f > 0.1f)
                {
                    neckRollHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    neckRollRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    neckRollLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    captures++;
                }
                InstructorTextCanvas.SetActive(false);
                timerText.text = Mathf.Ceil(exerciseLength + prepLength - calibrationTime).ToString();
                dummy0Animator.Play(exerciseAnimationNames[selectedExercise]);
            }
            else
            {
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Sit Twist
    private void SitTwistCalibration()
    {
        if (selectedExercise == 1)
        {
            if (!clearedOnce)
            {
                sitTwistHeadVectors.Clear();
                sitTwistRightControllerVectors.Clear();
                sitTwistLeftControllerVectors.Clear();
                clearedOnce = true;
            }
            Dummy0.SetActive(true);
            Dummy1.SetActive(false);
            exerciseLength = 3.75f;
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy0Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else if (calibrationTime <= exerciseLength + prepLength)
            {
                if (!initialCapture)
                {
                    sitTwistHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    sitTwistRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    sitTwistLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    initialCapture = true;
                }
                if (calibrationTime - prepLength - captures * 0.1f > 0.1f)
                {
                    sitTwistHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    sitTwistRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    sitTwistLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    captures++;
                }
                InstructorTextCanvas.SetActive(false);
                timerText.text = Mathf.Ceil(exerciseLength + prepLength - calibrationTime).ToString();
                dummy0Animator.Play(exerciseAnimationNames[selectedExercise]);
            }
            else
            {
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Push-up
    private void PushUpCalibration()
    {
        if (selectedExercise == 2)
        {
            if (!clearedOnce)
            {
                pushUpHeadVectors.Clear();
                pushUpRightControllerVectors.Clear();
                pushUpLeftControllerVectors.Clear();
                clearedOnce = true;
            }
            Dummy0.SetActive(true);
            Dummy1.SetActive(false);
            exerciseLength = 1.25f;
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy0Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else if (calibrationTime <= exerciseLength + prepLength)
            {
                if (!initialCapture)
                {
                    pushUpHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    pushUpRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    pushUpLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    initialCapture = true;
                }
                if (calibrationTime - prepLength - captures * 0.1f > 0.1f)
                {
                    pushUpHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    pushUpRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    pushUpLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    captures++;
                }
                InstructorTextCanvas.SetActive(false);
                timerText.text = Mathf.Ceil(exerciseLength + prepLength - calibrationTime).ToString();
                dummy0Animator.Play(exerciseAnimationNames[selectedExercise]);
            }
            else
            {
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Horse Pose
    private void HorsePoseCalibration()
    {
        if (selectedExercise == 3)
        {
            Dummy0.SetActive(false);
            Dummy1.SetActive(true);
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy1Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else
            {
                horsePoseHeadVector = VRInput.GetComponent<InputReader>().headPosition;
                horsePoseRightControllerVector = VRInput.GetComponent<InputReader>().rightControllerPosition;
                horsePoseLeftControllerVector = VRInput.GetComponent<InputReader>().leftControllerPosition;
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }
    
    //Lunges
    private void LungesCalibration()
    {
        if (selectedExercise == 4)
        {
            if (!clearedOnce)
            {
                lungeHeadVectors.Clear();
                lungeRightControllerVectors.Clear();
                lungeLeftControllerVectors.Clear();
                clearedOnce = true;
            }
            Dummy0.SetActive(false);
            Dummy1.SetActive(true);
            exerciseLength = 3.33333333333f;
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy1Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else if (calibrationTime <= exerciseLength + prepLength)
            {
                if (!initialCapture)
                {
                    lungeHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    lungeRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    lungeLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    initialCapture = true;
                }
                if (calibrationTime - prepLength - captures * 0.1f > 0.1f)
                {
                    lungeHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    lungeRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    lungeLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    captures++;
                }
                InstructorTextCanvas.SetActive(false);
                timerText.text = Mathf.Ceil(exerciseLength + prepLength - calibrationTime).ToString();
                dummy1Animator.Play(exerciseAnimationNames[selectedExercise]);
            }
            else
            {
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Squats
    private void SquatsCalibration()
    {
        if (selectedExercise == 5)
        {
            if (!clearedOnce)
            {
                squatHeadVectors.Clear();
                squatRightControllerVectors.Clear();
                squatLeftControllerVectors.Clear();
                clearedOnce = true;
            }
            Dummy0.SetActive(false);
            Dummy1.SetActive(true);
            exerciseLength = 1.66666666667f;
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy1Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else if (calibrationTime <= exerciseLength + prepLength)
            {
                if (!initialCapture)
                {
                    squatHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    squatRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    squatLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    initialCapture = true;
                }
                if (calibrationTime - prepLength - captures * 0.1f > 0.1f)
                {
                    squatHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    squatRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    squatLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    captures++;
                }
                InstructorTextCanvas.SetActive(false);
                timerText.text = Mathf.Ceil(exerciseLength + prepLength - calibrationTime).ToString();
                dummy1Animator.Play(exerciseAnimationNames[selectedExercise]);
            }
            else
            {
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Tree Pose
    private void TreePoseCalibration()
    {
        if (selectedExercise == 6)
        {
            Dummy0.SetActive(false);
            Dummy1.SetActive(true);
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy1Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else
            {
                treePoseHeadVector = VRInput.GetComponent<InputReader>().headPosition;
                treePoseRightControllerVector = VRInput.GetComponent<InputReader>().rightControllerPosition;
                treePoseLeftControllerVector = VRInput.GetComponent<InputReader>().leftControllerPosition;
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Child Pose
    private void ChildPoseCalibration()
    {
        if (selectedExercise == 7)
        {
            Dummy0.SetActive(true);
            Dummy1.SetActive(false);
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy0Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else
            {
                childPoseHeadVector = VRInput.GetComponent<InputReader>().headPosition;
                childPoseRightControllerVector = VRInput.GetComponent<InputReader>().rightControllerPosition;
                childPoseLeftControllerVector = VRInput.GetComponent<InputReader>().leftControllerPosition;
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Plank Pose
    private void PlankPoseCalibration()
    {
        if (selectedExercise == 8)
        {
            Dummy0.SetActive(true);
            Dummy1.SetActive(false);
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy0Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else
            {
                plankPoseHeadVector = VRInput.GetComponent<InputReader>().headPosition;
                plankPoseRightControllerVector = VRInput.GetComponent<InputReader>().rightControllerPosition;
                plankPoseLeftControllerVector = VRInput.GetComponent<InputReader>().leftControllerPosition;
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Sit-up
    private void SitUpCalibration()
    {
        if (selectedExercise == 9)
        {
            if (!clearedOnce)
            {
                sitUpHeadVectors.Clear();
                sitUpRightControllerVectors.Clear();
                sitUpLeftControllerVectors.Clear();
                clearedOnce = true;
            }
            Dummy0.SetActive(true);
            Dummy1.SetActive(false);
            exerciseLength = 2.0f;
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy0Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else if (calibrationTime <= exerciseLength + prepLength)
            {
                if (!initialCapture)
                {
                    sitUpHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    sitUpRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    sitUpLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    initialCapture = true;
                }
                if (calibrationTime - prepLength - captures * 0.1f > 0.1f)
                {
                    sitUpHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    sitUpRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    sitUpLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    captures++;
                }
                InstructorTextCanvas.SetActive(false);
                timerText.text = Mathf.Ceil(exerciseLength + prepLength - calibrationTime).ToString();
                dummy0Animator.Play(exerciseAnimationNames[selectedExercise]);
            }
            else
            {
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Back Bridge
    private void BackBridgePoseCalibration()
    {
        if (selectedExercise == 10)
        {
            Dummy0.SetActive(false);
            Dummy1.SetActive(true);
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy1Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else
            {
                backBridgeHeadVector = VRInput.GetComponent<InputReader>().headPosition;
                backBridgeRightControllerVector = VRInput.GetComponent<InputReader>().rightControllerPosition;
                backBridgeLeftControllerVector = VRInput.GetComponent<InputReader>().leftControllerPosition;
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Sit Side Stretch
    private void SitSideStretchCalibration()
    {
        if (selectedExercise == 11)
        {
            if (!clearedOnce)
            {
                sitSideStretchHeadVectors.Clear();
                sitSideStretchRightControllerVectors.Clear();
                sitSideStretchLeftControllerVectors.Clear();
                clearedOnce = true;
            }
            Dummy0.SetActive(true);
            Dummy1.SetActive(false);
            exerciseLength = 2.0f;
            if (calibrationTime <= prepLength)
            {
                InstructorTextCanvas.SetActive(true);
                timerText.text = Mathf.Ceil(prepLength - calibrationTime).ToString();
                instructorText.text = "Get in position:\n" + exerciseNames[selectedExercise];
                dummy0Animator.Play(stillExcerciseAnimationNames[selectedExercise]);
            }
            else if (calibrationTime <= exerciseLength + prepLength)
            {
                if (!initialCapture)
                {
                    sitSideStretchHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    sitSideStretchRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    sitSideStretchLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    initialCapture = true;
                }
                if (calibrationTime - prepLength - captures * 0.1f > 0.1f)
                {
                    sitSideStretchHeadVectors.Add(VRInput.GetComponent<InputReader>().headPosition);
                    sitSideStretchRightControllerVectors.Add(VRInput.GetComponent<InputReader>().rightControllerPosition);
                    sitSideStretchLeftControllerVectors.Add(VRInput.GetComponent<InputReader>().leftControllerPosition);
                    captures++;
                }
                InstructorTextCanvas.SetActive(false);
                timerText.text = Mathf.Ceil(exerciseLength + prepLength - calibrationTime).ToString();
                dummy0Animator.Play(exerciseAnimationNames[selectedExercise]);
            }
            else
            {
                ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted = false;
            }
        }
    }

    //Update all static vectors
    public void UpdateAllStaticVectors()
    {
        MovementVectorsScript.neckRollHeadVectorsStatic = neckRollHeadVectors;
        MovementVectorsScript.neckRollRightControllerVectorsStatic = neckRollRightControllerVectors;
        MovementVectorsScript.neckRollLeftControllerVectorsStatic = neckRollLeftControllerVectors;

        MovementVectorsScript.sitTwistHeadVectorsStatic = sitTwistHeadVectors;
        MovementVectorsScript.sitTwistRightControllerVectorsStatic = sitTwistRightControllerVectors;
        MovementVectorsScript.sitTwistLeftControllerVectorsStatic = sitTwistLeftControllerVectors;

        MovementVectorsScript.pushUpHeadVectorsStatic = pushUpHeadVectors;
        MovementVectorsScript.pushUpRightControllerVectorsStatic = pushUpRightControllerVectors;
        MovementVectorsScript.pushUpLeftControllerVectorsStatic = pushUpLeftControllerVectors;

        MovementVectorsScript.horsePoseHeadVectorStatic = horsePoseHeadVector;
        MovementVectorsScript.horsePoseRightControllerVectorStatic = horsePoseRightControllerVector;
        MovementVectorsScript.horsePoseLeftControllerVectorStatic = horsePoseLeftControllerVector;

        MovementVectorsScript.lungeHeadVectorsStatic = lungeHeadVectors;
        MovementVectorsScript.lungeRightControllerVectorsStatic = lungeRightControllerVectors;
        MovementVectorsScript.lungeLeftControllerVectorsStatic = lungeLeftControllerVectors;

        MovementVectorsScript.squatHeadVectorsStatic = squatHeadVectors;
        MovementVectorsScript.squatRightControllerVectorsStatic = squatRightControllerVectors;
        MovementVectorsScript.squatLeftControllerVectorsStatic = squatLeftControllerVectors;

        MovementVectorsScript.treePoseHeadVectorStatic = treePoseHeadVector;
        MovementVectorsScript.treePoseRightControllerVectorStatic = treePoseRightControllerVector;
        MovementVectorsScript.treePoseLeftControllerVectorStatic = treePoseLeftControllerVector;

        MovementVectorsScript.childPoseHeadVectorStatic = childPoseHeadVector;
        MovementVectorsScript.childPoseRightControllerVectorStatic = childPoseRightControllerVector;
        MovementVectorsScript.childPoseLeftControllerVectorStatic = childPoseLeftControllerVector;

        MovementVectorsScript.plankPoseHeadVectorStatic = plankPoseHeadVector;
        MovementVectorsScript.plankPoseRightControllerVectorStatic = plankPoseRightControllerVector;
        MovementVectorsScript.plankPoseLeftControllerVectorStatic = plankPoseLeftControllerVector;

        MovementVectorsScript.sitUpHeadVectorsStatic = sitUpHeadVectors;
        MovementVectorsScript.sitUpRightControllerVectorsStatic = sitUpRightControllerVectors;
        MovementVectorsScript.sitUpLeftControllerVectorsStatic = sitUpLeftControllerVectors;

        MovementVectorsScript.backBridgeHeadVectorStatic = backBridgeHeadVector;
        MovementVectorsScript.backBridgeRightControllerVectorStatic = backBridgeRightControllerVector;
        MovementVectorsScript.backBridgeLeftControllerVectorStatic = backBridgeLeftControllerVector;

        MovementVectorsScript.sitSideStretchHeadVectorsStatic = sitSideStretchHeadVectors;
        MovementVectorsScript.sitSideStretchRightControllerVectorsStatic = sitSideStretchRightControllerVectors;
        MovementVectorsScript.sitSideStretchLeftControllerVectorsStatic = sitSideStretchLeftControllerVectors;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScriptOrganizer.GetComponent<CalibrationMenuScript>().calibrationStarted)
        {
            calibrationTime = calibrationTime + Time.deltaTime;
            selectedExercise = ScriptOrganizer.GetComponent<CalibrationMenuScript>().exerciseNumber;
            InformationTextCanvas.SetActive(true);
            NeckRollCalibration();
            SitTwistCalibration();
            PushUpCalibration();
            HorsePoseCalibration();
            LungesCalibration();
            SquatsCalibration();
            TreePoseCalibration();
            ChildPoseCalibration();
            PlankPoseCalibration();
            SitUpCalibration();
            BackBridgePoseCalibration();
            SitSideStretchCalibration();
        }
        else
        {
            initialCapture = false;
            clearedOnce = false;
            calibrationTime = 0;
            captures = 0;
            InstructorTextCanvas.SetActive(false);
            InformationTextCanvas.SetActive(false);
            Dummy0.SetActive(false);
            Dummy1.SetActive(false);
        }
    }
}