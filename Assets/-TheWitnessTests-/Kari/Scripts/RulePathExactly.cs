using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulePathExactly : MonoBehaviour, IRule
{
    Puzzle puzzle;
    public List<PuzzleNode> points;

    public bool Check() {
        // Player must go trough predefined path (hinted by shadows/scratches...)
        // node1, node2... (startnode, endnode points not counted)
        Debug.Log("CheckOneCorrectPath");
        for (int i = 1; i < puzzle.drawnPath.Count - 1; i++) {
            if (puzzle.drawnPath[i] != points[i - 1])
                return false;
        }
        return true;
    }

    public void ShowFail() {
        print("RulePathExactly failed");
    }
    void Awake() {
        puzzle = transform.parent.GetComponent<Puzzle>();
    }
}
