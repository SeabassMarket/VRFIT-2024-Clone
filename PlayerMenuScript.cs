using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuScript : MonoBehaviour
{
    [Header("VR Input Manager")]
    public GameObject VRInput;
    public GameObject XRRig;

    [Header("Overall Player Menu")]
    public GameObject PlayerMenu;

    [Header("UI Pages")]
    public List<GameObject> UIPages;

    [Header("Main Player Menu Buttons")]
    public Button ControlsButton;
    public Button CalibrateButton;
    public Button BackToStartButton;

    [Header("Control Screen Buttons")]
    public Button BackControlButton;

    //Variables private to function for calculations
    [Header("Calculation Modifiers")]
    public float playerMenuDistance;
    public float playerMenuScale;
    private float playerXPosition;
    private float playerYPosition;
    private float playerZPosition;
    private float playerMenuXPosition;
    private float playerMenuYPosition;
    private float playerMenuZPosition;
    private bool playerMenuOff = false;
    private bool updatedOnce = false;
    private bool turnedOffOnce = false;
    private int screenToDisplay = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Turn it off
        PlayerMenu.SetActive(false);
        PlayerMenu.transform.localScale = new Vector3(playerMenuScale, playerMenuScale, playerMenuScale);
        PlayerMenu.transform.Rotate(PlayerMenu.transform.rotation.eulerAngles.x * -1, PlayerMenu.transform.rotation.eulerAngles.y * -1, PlayerMenu.transform.rotation.eulerAngles.z * -1, Space.Self);

        //Main Player Menu Buttons
        ControlsButton.onClick.AddListener(EnableControlsScreen);
        CalibrateButton.onClick.AddListener(GoToCalibration);
        BackToStartButton.onClick.AddListener(BackToStart);

        //Control Screen Buttons
        BackControlButton.onClick.AddListener(EnableMainMenu);
    }

    public void EnableControlsScreen()
    {
        screenToDisplay = 1;
    }

    public void EnableMainMenu()
    {
        screenToDisplay = 0;
    }

    public void TurnOnUIPage(int page)
    {
        for (int i = 0; i < UIPages.Count; i++)
        {
            if (i == page)
            {
                UIPages[i].gameObject.SetActive(true);
            }
            else
            {
                UIPages[i].gameObject.SetActive(false);
            }
        }
    }

    public void GoToCalibration()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(4);
    }

    public void BackToStart()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    public void HideAll()
    {
        playerMenuOff = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (VRInput.GetComponent<InputReader>().leftPrimarySwitch)
        {
            //If menu switch is on...
            if (VRInput.GetComponent<InputReader>().menuButtonSwitch && !playerMenuOff)
            {
                //Turn it on
                PlayerMenu.SetActive(true);
                TurnOnUIPage(screenToDisplay);

                //Set location
                playerXPosition = XRRig.transform.position.x + (VRInput.GetComponent<InputReader>().headPosition.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + VRInput.GetComponent<InputReader>().headPosition.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                playerYPosition = XRRig.transform.position.y + VRInput.GetComponent<InputReader>().headPosition.y;
                playerZPosition = XRRig.transform.position.z + (VRInput.GetComponent<InputReader>().headPosition.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - VRInput.GetComponent<InputReader>().headPosition.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                playerMenuXPosition = playerXPosition + playerMenuDistance * Mathf.Sin((VRInput.GetComponent<InputReader>().eulerHeadRotation.y + XRRig.transform.rotation.eulerAngles.y) * Mathf.Deg2Rad);
                playerMenuYPosition = playerYPosition;
                playerMenuZPosition = playerZPosition + playerMenuDistance * Mathf.Cos((VRInput.GetComponent<InputReader>().eulerHeadRotation.y + XRRig.transform.rotation.eulerAngles.y) * Mathf.Deg2Rad);
                PlayerMenu.transform.position = new Vector3(playerMenuXPosition, playerMenuYPosition, playerMenuZPosition);

                //Set rotation
                PlayerMenu.transform.Rotate(PlayerMenu.transform.rotation.eulerAngles.x * -1, PlayerMenu.transform.rotation.eulerAngles.y * -1, PlayerMenu.transform.rotation.eulerAngles.z * -1, Space.Self);
                PlayerMenu.transform.Rotate(0.0f, VRInput.GetComponent<InputReader>().eulerHeadRotation.y + XRRig.transform.rotation.eulerAngles.y, 0.0f, Space.Self);
            }
            else //If menu switch is off
            {
                //Turn it off
                PlayerMenu.SetActive(false);
            }
        }
        else
        {
            //If menu switch is on...
            if (VRInput.GetComponent<InputReader>().menuButtonSwitch && !playerMenuOff)
            {
                //turn it on
                PlayerMenu.SetActive(true);
                TurnOnUIPage(screenToDisplay);

                if (!updatedOnce)
                {
                    //Set location
                    playerXPosition = XRRig.transform.position.x + (VRInput.GetComponent<InputReader>().headPosition.x * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) + VRInput.GetComponent<InputReader>().headPosition.z * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    playerYPosition = XRRig.transform.position.y + VRInput.GetComponent<InputReader>().headPosition.y;
                    playerZPosition = XRRig.transform.position.z + (VRInput.GetComponent<InputReader>().headPosition.z * Mathf.Cos(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) - VRInput.GetComponent<InputReader>().headPosition.x * Mathf.Sin(XRRig.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
                    playerMenuXPosition = playerXPosition + playerMenuDistance * Mathf.Sin((VRInput.GetComponent<InputReader>().eulerHeadRotation.y + XRRig.transform.rotation.eulerAngles.y) * Mathf.Deg2Rad);
                    playerMenuYPosition = playerYPosition;
                    playerMenuZPosition = playerZPosition + playerMenuDistance * Mathf.Cos((VRInput.GetComponent<InputReader>().eulerHeadRotation.y + XRRig.transform.rotation.eulerAngles.y) * Mathf.Deg2Rad);
                    PlayerMenu.transform.position = new Vector3(playerMenuXPosition, playerMenuYPosition, playerMenuZPosition);

                    //Set rotation
                    PlayerMenu.transform.Rotate(PlayerMenu.transform.rotation.eulerAngles.x * -1, PlayerMenu.transform.rotation.eulerAngles.y * -1, PlayerMenu.transform.rotation.eulerAngles.z * -1, Space.Self);
                    PlayerMenu.transform.Rotate(0.0f, VRInput.GetComponent<InputReader>().eulerHeadRotation.y + XRRig.transform.rotation.eulerAngles.y, 0.0f, Space.Self);

                    //Tell code its been updated
                    updatedOnce = true;
                    turnedOffOnce = false;
                }
            }
            else //If menu switch is off
            {
                if (!turnedOffOnce)
                {
                    //Turn it off
                    PlayerMenu.SetActive(false);
                    turnedOffOnce = true;
                    updatedOnce = false;
                }
            }
        }
    }
}
