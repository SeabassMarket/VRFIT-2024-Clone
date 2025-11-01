using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExerciseAnimationsScript : MonoBehaviour
{
    [Header("VR Input Manager")]
    public GameObject VRInput;
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
    public TMP_Text accuracyText;

    List<int> selectedExercises = new List<int>();
    List<string> exerciseAnimationNames = new List<string> { "Armature|NeckRoll", "Armature|SitTwist", "Armature|PushUp", "HorsePose", "Lunge", "Squat", "TreePose", "Armature| ChildPose", "Armature|PlankPose", "Armature|SitUp", "BackBridge", "Armature|SitSideStretch" };
    List<string> stillExcerciseAnimationNames = new List<string> { "Still Armature|NeckRoll", "Still Armature|SitTwist", "Still Armature|PushUp", "HorsePose", "Still Lunge", "Still Squat", "TreePose", "Armature| ChildPose", "Armature|PlankPose", "Still Armature|SitUp", "BackBridge", "Still Armature|SitSideStretch" };
    List<string> exerciseNames = new List<string> { "Neck Roll", "Sit Twist", "Push-up", "Horse Pose", "Lunge", "Squat", "Tree Pose", "Child's Pose", "Plank", "Situps", "Back Bridge", "Sit Side Stretch" };
    public int currentExercise;
    private int exercisesDone = 0;
    private int exerciseLength = 10;
    private int prepLength = 5;
    private float timeNotPaused = 0;
    public bool prepping = false;

    // Start is called before the first frame update
    void Start()
    {
        if (SelectedExercisesScript.staticSelectedExercises != null)
        {
            selectedExercises = SelectedExercisesScript.staticSelectedExercises;
        }
        dummy0Animator = Dummy0.GetComponent<Animator>();
        dummy1Animator = Dummy1.GetComponent<Animator>();
        Dummy1.SetActive(false);
        Dummy0.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!VRInput.GetComponent<InputReader>().menuButtonSwitch)
        {
            //Sets the values of variables that control animations
            timeNotPaused = timeNotPaused + Time.deltaTime;
            if ((timeNotPaused / (exerciseLength + prepLength)) >= 1)
            {
                exercisesDone = exercisesDone + 1;
                timeNotPaused = 0;
            }

            //Turn on animations and time them if there are any to be done
            if (exercisesDone < selectedExercises.Count && !ScriptOrganizer.GetComponent<ExerciseColliders>().calibrationError)
            {
                currentExercise = selectedExercises[exercisesDone];
                if ((currentExercise >= 3 && currentExercise <= 6) || currentExercise == 10)
                {
                    Dummy1.SetActive(true);
                    Dummy0.SetActive(false);
                    if (timeNotPaused <= prepLength)
                    {
                        InstructorTextCanvas.SetActive(true);
                        instructorText.text = "Get in position:\n" + exerciseNames[currentExercise];
                        timerText.text = Mathf.Ceil(prepLength - timeNotPaused).ToString();
                        dummy1Animator.Play(stillExcerciseAnimationNames[currentExercise]);
                        prepping = true;
                    }
                    else
                    {
                        InstructorTextCanvas.SetActive(false);
                        timerText.text = Mathf.Ceil(exerciseLength + prepLength - timeNotPaused).ToString();
                        dummy1Animator.Play(exerciseAnimationNames[currentExercise]);
                        prepping = false;
                    }
                } else
                {
                    Dummy0.SetActive(true);
                    Dummy1.SetActive(false);
                    if (timeNotPaused <= prepLength)
                    {
                        InstructorTextCanvas.SetActive(true);
                        instructorText.text = "Get in position:\n" + exerciseNames[currentExercise];
                        timerText.text = Mathf.Ceil(prepLength - timeNotPaused).ToString();
                        dummy0Animator.Play(stillExcerciseAnimationNames[currentExercise]);
                        prepping = true;
                    }
                    else
                    {
                        InstructorTextCanvas.SetActive(false);
                        timerText.text = Mathf.Ceil(exerciseLength + prepLength - timeNotPaused).ToString();
                        dummy0Animator.Play(exerciseAnimationNames[currentExercise]);
                        prepping = false;
                    }
                }
            }
            else
            {
                Dummy1.SetActive(false);
                Dummy0.SetActive(false);
                InstructorTextCanvas.SetActive(true);
                prepping = true;
                timerText.text = "0";
                if (ScriptOrganizer.GetComponent<ExerciseColliders>().calibrationError)
                {
                    instructorText.text = "Calibration Error\nUse menu button to\ngo to calibration";
                } else
                {
                    instructorText.text = "Workout done\nUse menu button to\ngo back to start";
                }
            }
        }
    }
}
