using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RuleVisitPoints : MonoBehaviour, IRule {
    public List<PuzzleNode> points;
    //public List<GameObject> visibleSpots;
    public GameObject spotPrefab;

    Puzzle puzzle;

    public bool CheckBlackSpots() {
        // Player must go through predefined nodes (black spots/hexagons)
        Debug.Log("CheckBlackSpots");
        foreach (var pn in points) {
            if (!puzzle.drawnPath.Contains(pn))
                return false;
        }
        return true;
    }

    public bool CheckOneCorrectPath() {
        // Player must go trough predefined path (hinted by shadows/scratches...)
        // node1, node2... (startnode, endnode points not counted)
        Debug.Log("CheckOneCorrectPath");
        for (int i = 1; i < puzzle.drawnPath.Count-1; i++) {
            if (puzzle.drawnPath[i] != points[i-1])
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
