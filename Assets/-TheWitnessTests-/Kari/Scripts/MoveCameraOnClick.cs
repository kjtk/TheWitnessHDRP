using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraOnClick : MonoBehaviour, IActivatable {
    public Camera mainCamera;
    public Transform newCamTarget;

    public void Activate() {
        //transform.Translate(0, -3, 0);
        Debug.Log("Puzzle activated.");
        mainCamera.gameObject.transform.position = newCamTarget.position + new Vector3(0, 0, -5);
    }
}
