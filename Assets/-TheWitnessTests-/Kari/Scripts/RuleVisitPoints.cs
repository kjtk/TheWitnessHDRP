using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RuleVisitPoints : MonoBehaviour, IRule {
    public List<PuzzleNode> points;
    //public List<GameObject> visibleSpots;
    public List<PuzzleNode> visibleSpots;
    public GameObject spotPrefab;

    Puzzle puzzle;

    public bool Check() {
        // Player must find the exact route
        foreach (var pn in points) {
            if (!puzzle.drawnPath.Contains(pn))
                return false;
        }
        return true;
    }

    public bool CheckVisibleSpots() {
        // Player must go trough black spots
        Debug.Log("Checking visibleSpots");
        Debug.Log("Length of visibleSpots " + visibleSpots.Count);
        foreach (var vs in visibleSpots) {
            if (!puzzle.drawnPath.Contains(vs))
                return false;
        }
        return true;
    }

    public void ShowFail() {

    }
    void Awake() {
        puzzle = transform.parent.GetComponent<Puzzle>();
    }
#if UNITY_EDITOR
    void Update() {
        foreach (var pn in points) {
            if (!puzzle.drawnPath.Contains(pn)) {
                //Debug.Log("ok");
            }
        }
    }
#endif
}
