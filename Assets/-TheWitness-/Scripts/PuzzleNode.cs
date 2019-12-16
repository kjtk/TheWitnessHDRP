using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vectrosity;

public class PuzzleNode : MonoBehaviour {
    public bool isStartNode;
    public bool isEndNode;
    public List<PuzzleNode> neighbors;
    Puzzle puzzle;

    //public VectorObject3D puzzlePathLine;

    void Awake() {
        //puzzle = transform.parent.GetComponent<Puzzle>();
        puzzle = transform.parent.parent.GetComponent<Puzzle>();

        // Disable mesh renderer when in play mode
        gameObject.GetComponent<MeshRenderer>().enabled = puzzle.showPuzzleNodes;

        // Instantiate particle system
        if (puzzle.PuzzleNodeParticlesPrefab != null && !isStartNode && !isEndNode) {
            Instantiate(puzzle.PuzzleNodeParticlesPrefab, transform);
        }

    }

    void OnMouseDown() {
        puzzle.NodeClicked(this);
        //Debug.Log("Node Clicked: " + this.name);
    }

    void Start() {

        // Line drawing with LineRenderer

        // Create path from node to it's neighbour nodes.

        var puzzleGridLineOffset = 0.0075f;

        // Line drawing with nultiple LineRenderers per node
        //foreach (var neighbor in neighbors) {
        // Create new LineRenderer container gameobject as a child of node.
        for (int i = 0; i < neighbors.Count; i++) {
            GameObject newLR = new GameObject("LineRenderer");
            newLR.transform.SetParent(gameObject.transform, true);
            newLR.transform.localPosition = Vector3.zero;
            newLR.transform.localRotation = Quaternion.identity;
            newLR.transform.localScale = Vector3.one;
            // Add LineRenderer component
            LineRenderer lineRenderer = newLR.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            //lineRenderer.useWorldSpace = true;

            if (puzzle.puzzleState == Puzzle.PuzzleState.Locked) {
                lineRenderer.material = puzzle.PuzzleGridMaterialLocked;
            } else if (puzzle.puzzleState == Puzzle.PuzzleState.Solvable || puzzle.puzzleState == Puzzle.PuzzleState.Solved) {
                lineRenderer.material = puzzle.PuzzleGridMaterialSolvable;
            }
            lineRenderer.startWidth = 0.6f;
            lineRenderer.endWidth = 0.6f;
            // Rotation issue solved?
            //lineRenderer.alignment = LineAlignment.Local;
            lineRenderer.alignment = LineAlignment.TransformZ;
            lineRenderer.transform.position += Vector3.back * puzzleGridLineOffset;
            lineRenderer.numCapVertices = 5;
            lineRenderer.numCornerVertices = 5;

            //lineRenderer.SetPosition(0, gameObject.transform.localPosition);
            //lineRenderer.SetPosition(1, neighbors[i].transform.localPosition);
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(neighbors[i].transform.position));
        }

        // If node is StartNode, create big circle
        if (isStartNode) {
            GameObject newLR = new GameObject("LineRenderer");
            newLR.transform.SetParent(gameObject.transform, true);
            
            newLR.transform.localPosition = Vector3.zero;
            newLR.transform.localRotation = Quaternion.identity;
            newLR.transform.localScale = Vector3.one;
            // Add LineRenderer component
            LineRenderer lineRenderer = newLR.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;

            if (puzzle.puzzleState == Puzzle.PuzzleState.Locked) {
                lineRenderer.material = puzzle.PuzzleGridMaterialLocked;
            } else if (puzzle.puzzleState == Puzzle.PuzzleState.Solvable || puzzle.puzzleState == Puzzle.PuzzleState.Solved) {
                lineRenderer.material = puzzle.PuzzleGridMaterialSolvable;
            }
            lineRenderer.startWidth = 1f;
            lineRenderer.endWidth = 1f;
            lineRenderer.alignment = LineAlignment.TransformZ;
            lineRenderer.transform.position += Vector3.back * puzzleGridLineOffset;
            lineRenderer.numCapVertices = 5;
            lineRenderer.numCornerVertices = 5;

            //lineRenderer.SetPosition(0, gameObject.transform.position);
            //lineRenderer.SetPosition(1, gameObject.transform.position);
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);

        }

    }
}
