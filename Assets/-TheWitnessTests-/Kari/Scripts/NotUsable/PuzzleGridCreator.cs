using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGridCreator : MonoBehaviour
{
    public GameObject[] puzzleNodes;
    //public GameObject[] puzzleNodeNeighbors;

    LineRenderer lineRenderer;

    void Start() {
        puzzleNodes = GameObject.FindGameObjectsWithTag("node");

        // We'll store point pairs for LineRenderer here (single line from node to it's one neighbour node).  
        var points = new List<Vector3>();

        // Let's go through each node
        foreach (var node in puzzleNodes) {
            Debug.Log("Node: " + node.name);
            Debug.Log(node.GetComponent<PuzzleNode>().neighbors.Count);
            Vector3 lineStart = node.transform.position;
            // Let's go through node's neighbours
            for(int i = 0; i < node.GetComponent<PuzzleNode>().neighbors.Count; i++) {
                Debug.Log("Node: " + node.name + " neighbour: " + node.GetComponent<PuzzleNode>().neighbors[i].name);
                Vector3 lineEnd = node.GetComponent<PuzzleNode>().neighbors[i].transform.position;
                points.Add(lineStart);
                points.Add(lineEnd);
            }
        }
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    void Awake() {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        //lineRenderer = GetComponent<LineRenderer>();
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
