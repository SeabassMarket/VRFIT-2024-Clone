using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalibrationMenuScript : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameObject ScriptOrganizer;

    [Header("Overall Calibration Menu")]
    public GameObject CalibrationMenu;

    [Header("Buttons")]
    public Button backToStartButton;
    public Button startCalibrationButton;
    public Button forwardExerciseButton;
    public Button backExerciseButton;

    [Header("Text")]
    public TMP_Text exerciseText;

    List<string> exerciseNames = new List<string> { "1 Neck Roll", "2 Sit Twist", "3 Push-up", "4 Horse Pose", "5 Lunge", "6 Squat", "7 Tree Pose", "8 Child's Pose", "9 Plank", "A Situps", "B Back Bridge", "C Sit Side Stretch" };
    public int exerciseNumber = 0;
    public bool calibrationStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        backToStartButton.onClick.AddListener(LoadStart);
        startCalibrationButton.onClick.AddListener(StartCalibration);
        forwardExerciseButton.onClick.AddListener(ForwardExercise);
        backExerciseButton.onClick.AddListener(BackExercise);
        exerciseText.text = exerciseNames[exerciseNumber];
    }

    public void LoadStart()
    {
        HideAll();
        ScriptOrganizer.GetComponent<CalibrationRecordingScript>().UpdateAllStaticVectors();
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    public void HideAll()
    {
        CalibrationMenu.SetActive(false);
    }

    public void StartCalibration()
    {
        calibrationStarted = true;
    }

    public void ForwardExercise()
    {
        if (exerciseNumber == exerciseNames.Count - 1)
        {
            exerciseNumber = 0;
        }
        else
        {
            exerciseNumber++;
        }
        exerciseText.text = exerciseNames[exerciseNumber];
    }

    public void BackExercise()
    {
        if (exerciseNumber == 0)
        {
            exerciseNumber = exerciseNames.Count - 1;
        }
        else
        {
            exerciseNumber--;
        }
        exerciseText.text = exerciseNames[exerciseNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if (calibrationStarted)
        {
            CalibrationMenu.SetActive(false);
        } else
        {
            CalibrationMenu.SetActive(true);
        }
    }
}
