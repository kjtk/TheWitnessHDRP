using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RuleVisitPoints : MonoBehaviour, IRule {
    public List<PuzzleNode> points;
    public List<GameObject> visibleSpots;
    public GameObject spotPrefab;

    Puzzle puzzle;

    public bool Check() {
        foreach (var pn in points) {
            if (!puzzle.drawnPath.Contains(pn))
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

    }
#endif
}
