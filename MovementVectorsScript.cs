using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVectorsScript : MonoBehaviour
{
    //Vector Lists
    public static List<Vector3> neckRollHeadVectorsStatic;
    public static List<Vector3> neckRollRightControllerVectorsStatic;
    public static List<Vector3> neckRollLeftControllerVectorsStatic;

    public static List<Vector3> sitTwistHeadVectorsStatic;
    public static List<Vector3> sitTwistRightControllerVectorsStatic;
    public static List<Vector3> sitTwistLeftControllerVectorsStatic;

    public static List<Vector3> pushUpHeadVectorsStatic;
    public static List<Vector3> pushUpRightControllerVectorsStatic;
    public static List<Vector3> pushUpLeftControllerVectorsStatic;

    public static Vector3 horsePoseHeadVectorStatic;
    public static Vector3 horsePoseRightControllerVectorStatic;
    public static Vector3 horsePoseLeftControllerVectorStatic;

    public static List<Vector3> lungeHeadVectorsStatic;
    public static List<Vector3> lungeRightControllerVectorsStatic;
    public static List<Vector3> lungeLeftControllerVectorsStatic;

    public static List<Vector3> squatHeadVectorsStatic;
    public static List<Vector3> squatRightControllerVectorsStatic;
    public static List<Vector3> squatLeftControllerVectorsStatic;

    public static Vector3 treePoseHeadVectorStatic;
    public static Vector3 treePoseRightControllerVectorStatic;
    public static Vector3 treePoseLeftControllerVectorStatic;

    public static Vector3 childPoseHeadVectorStatic;
    public static Vector3 childPoseRightControllerVectorStatic;
    public static Vector3 childPoseLeftControllerVectorStatic;

    public static Vector3 plankPoseHeadVectorStatic;
    public static Vector3 plankPoseRightControllerVectorStatic;
    public static Vector3 plankPoseLeftControllerVectorStatic;

    public static List<Vector3> sitUpHeadVectorsStatic;
    public static List<Vector3> sitUpRightControllerVectorsStatic;
    public static List<Vector3> sitUpLeftControllerVectorsStatic;

    public static Vector3 backBridgeHeadVectorStatic;
    public static Vector3 backBridgeRightControllerVectorStatic;
    public static Vector3 backBridgeLeftControllerVectorStatic;

    public static List<Vector3> sitSideStretchHeadVectorsStatic;
    public static List<Vector3> sitSideStretchRightControllerVectorsStatic;
    public static List<Vector3> sitSideStretchLeftControllerVectorsStatic;
}
