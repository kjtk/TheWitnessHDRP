using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleVisitPoints : MonoBehaviour, IRule {
    public List<PuzzleNode> points;
    Puzzle puzzle;

    public bool Check() {
        foreach (var pn in points) {
            if (!puzzle.drawnPath.Contains(pn))
                return false;
        }
        return true;
    }
    void Awake() {
        puzzle = transform.parent.GetComponent<Puzzle>();
    }

}
