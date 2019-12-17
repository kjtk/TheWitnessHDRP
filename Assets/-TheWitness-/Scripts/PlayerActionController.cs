using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerActionController : MonoBehaviour {

    public enum PlayerState { SolvingPuzzle, MovingAround };
    public PlayerState playerState;

    public UnityEvent StartSolvingPuzzle;
    public UnityEvent StartMovingAround;

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
        if(currentState == PlayerState.SolvingPuzzle) {
            return;
        // Lets start solving the puzzle.
        } else if(currentState == PlayerState.MovingAround) {
            playerState = PlayerState.SolvingPuzzle;
            FPC.enabled = false;
            FPC.m_MouseLook.SetCursorLock(false);
            //start solving
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
            //start solving
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            ActivatePuzzle(playerState);
            Debug.Log(playerState);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            QuitPuzzle(playerState);
            Debug.Log(playerState);
        }
    }
}
