using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGridCreator : MonoBehaviour
{
    public GameObject[] puzzleNodes;
    public GameObject[] puzzleNodeNeighbours;

    LineRenderer lineRenderer;

    private void Start() {
        puzzleNodes = GameObject.FindGameObjectsWithTag("node");
        foreach (var node in puzzleNodes) {
            Debug.Log(node.name);
            


        }
    }

    void Awake() {
        //lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        // Don't use World Space as then the line doesn't follow parent (Puzzle).
        // Scaling issue to fix...
        lineRenderer.useWorldSpace = false;
    }

    // korjattava
    /*
    void DrawLineFromNodeToNeighbours() {
        var points = new List<Vector3>();
        foreach (var n in drawnPath) {
            points.Add(n.transform.position);
        }
        if (points.Count == 1) {
            points.Add(points[0]);
        }
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
    */

}
