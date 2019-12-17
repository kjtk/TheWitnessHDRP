using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerActionController : MonoBehaviour {

    public enum PlayerState { SolvingPuzzle, MovingAround };
    public PlayerState playerState;
    public bool playerSucceededToSolve = false;

    //public UnityEvent StartSolvingPuzzle;
    //public UnityEvent StartMovingAround;

    // Disabling CharacterController still allows looking around... 
    // ...so let's use FirstPersonController instead.
    // https://answers.unity.com/questions/974094/disable-first-person-controller-script-how.html
    FirstPersonController FPC;

    void Start() {
        playerState = PlayerState.MovingAround;
        FPC = GetComponent<FirstPersonController>();
    }

    public void ActivatePuzzle(PlayerState currentState) {
        // We are already solving puzzle.
        if (currentState == PlayerState.SolvingPuzzle) {
            return;
        // Lets start solving the puzzle.
        } else if(currentState == PlayerState.MovingAround) {
            playerState = PlayerState.SolvingPuzzle;
            FPC.enabled = false;
            FPC.m_MouseLook.SetCursorLock(false);
            //Debug.Log("::ActivatePuzzle::");

        }
    }

    public void QuitPuzzle(PlayerState currentState) {
        // Already moving
        if (currentState == PlayerState.MovingAround) {
            return;
        // We are solving puzzle but want to quit.
        } else if (currentState == PlayerState.SolvingPuzzle) {
            playerState = PlayerState.MovingAround;
            FPC.enabled = true;
            FPC.m_MouseLook.SetCursorLock(true);
            //Debug.Log("::QuitPuzzle::");
        }
    }

    public void FPSC_ReleaseAfterSuccess() {
        
        playerSucceededToSolve = true;
        //Debug.Log("::PlayerSuccessToSolve::" + playerSucceededToSolve);
        QuitPuzzle(PlayerState.SolvingPuzzle);
        //Debug.Log("::PlayerSuccessToSolve::" + playerSucceededToSolve);

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !playerSucceededToSolve) {
            //Debug.Log("::MouseButton0 Pressed::");
            ActivatePuzzle(playerState);
            //Debug.Log(playerState);
        } else {
            playerSucceededToSolve = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            //Debug.Log("::MouseButton1 Pressed::");
            QuitPuzzle(playerState);
            //Debug.Log(playerState);
        }
    }
}
