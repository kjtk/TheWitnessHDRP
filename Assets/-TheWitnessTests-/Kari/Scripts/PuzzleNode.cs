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

    }

    void OnMouseDown() {
        puzzle.NodeClicked(this);
        //Debug.Log("Node Clicked: " + this.name);
    }

    void Start() {

        // =====================================================

        // Line drawing with Vectrosity

        /*
        var points = new List<Vector3>();
        var puzzleGridLineOffset = 0.2f;

        var puzzleGridLine = new VectorLine("PuzzlePathLine", points, 0.0f);

        foreach (var neighbor in neighbors) {
            points.Add(gameObject.transform.position += Vector3.back * puzzleGridLineOffset);
            points.Add(neighbor.transform.position += Vector3.back * puzzleGridLineOffset);

            //VectorLine.SetLine3D(Color.red, 
            //    gameObject.transform.position += Vector3.back * puzzlePathLineOffset, 
            //    neighbor.transform.position += Vector3.back * puzzlePathLineOffset);
        }

        puzzleGridLine.material = puzzle.PuzzleGridMaterial;
        puzzleGridLine.lineWidth = 10.0f;
        puzzleGridLine.lineType = LineType.Discrete;
        puzzleGridLine.endCap = "RoundCap";
        puzzleGridLine.drawTransform = transform;
        puzzleGridLine.Draw3DAuto();
        */


        // ==================================================


        // Line drawing with LineRenderer

        // Create path from node to it's neighbour nodes.

        
        var puzzleGridLineOffset = 0.0075f;

        /*
        // Create new LineRenderer container gameobject as a child of node.
        GameObject newLR = new GameObject("LineRenderer");
        newLR.transform.SetParent(gameObject.transform, true);
        // Add LineRenderer component
        LineRenderer lineRenderer = newLR.AddComponent<LineRenderer>();

        lineRenderer.useWorldSpace = false;
        //lineRenderer.material = new Material(Shader.Find("PuzzleGridMaterial"));
        lineRenderer.material = puzzle.PuzzleGridMaterial;
        lineRenderer.startWidth = 0.06f;
        lineRenderer.endWidth = 0.06f;
        lineRenderer.alignment = LineAlignment.TransformZ;
        lineRenderer.transform.position += Vector3.back * puzzleGridLineOffset;
        lineRenderer.numCapVertices = 5;
        lineRenderer.numCornerVertices = 5;

        var points = new List<Vector3>();

        
        foreach (var neighbor in neighbors) {

            // Line drawing with one LineRenderer per node
            // lines get distorded when they "turn back"...!!!
            // case 1
            points.Add(gameObject.transform.position);
            points.Add(neighbor.transform.position);
            points.Add(neighbor.transform.position);


            // case 2
            //points.Add(gameObject.transform.position);
            //points.Add(gameObject.transform.position);
            //points.Add(gameObject.transform.position);
            //points.Add(neighbor.transform.position);
            //points.Add(gameObject.transform.position);
            //points.Add(gameObject.transform.position);
            //points.Add(gameObject.transform.position);
            

            // case 3
            //points.Add(gameObject.transform.position);
            //points.Add(gameObject.transform.position);
            //points.Add(neighbor.transform.position);
            //points.Add(neighbor.transform.position);

        }
  
        // Draw the paths from single node.
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
        */



        // Line drawing with nultiple LineRenderers per node
        //foreach (var neighbor in neighbors) {
        // Create new LineRenderer container gameobject as a child of node.
        for (int i = 0; i < neighbors.Count; i++) {
            GameObject newLR = new GameObject("LineRenderer");
            newLR.transform.SetParent(gameObject.transform, true);
            
            // Add LineRenderer component
            LineRenderer lineRenderer = newLR.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;

            if (puzzle.puzzleState == Puzzle.PuzzleState.Locked) {
                lineRenderer.material = puzzle.PuzzleGridMaterialLocked;
            } else if (puzzle.puzzleState == Puzzle.PuzzleState.Solvable || puzzle.puzzleState == Puzzle.PuzzleState.Solved) {
                lineRenderer.material = puzzle.PuzzleGridMaterialSolvable;
            }
            lineRenderer.startWidth = 0.06f;
            lineRenderer.endWidth = 0.06f;
            lineRenderer.alignment = LineAlignment.TransformZ;
            lineRenderer.transform.position += Vector3.back * puzzleGridLineOffset;
            lineRenderer.numCapVertices = 5;
            lineRenderer.numCornerVertices = 5;

            lineRenderer.SetPosition(0,gameObject.transform.position);
            lineRenderer.SetPosition(1,neighbors[i].transform.position);
        }
        //}


        // ==============================================



        // If node is StartNode, create big circle
        if (isStartNode) {
            GameObject newLR = new GameObject("LineRenderer");
            newLR.transform.SetParent(gameObject.transform, true);

            // Add LineRenderer component
            LineRenderer lineRenderer = newLR.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;

            if (puzzle.puzzleState == Puzzle.PuzzleState.Locked) {
                lineRenderer.material = puzzle.PuzzleGridMaterialLocked;
            } else if (puzzle.puzzleState == Puzzle.PuzzleState.Solvable || puzzle.puzzleState == Puzzle.PuzzleState.Solved) {
                lineRenderer.material = puzzle.PuzzleGridMaterialSolvable;
            }
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.alignment = LineAlignment.TransformZ;
            lineRenderer.transform.position += Vector3.back * puzzleGridLineOffset;
            lineRenderer.numCapVertices = 5;
            lineRenderer.numCornerVertices = 5;

            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, gameObject.transform.position);
        }

        // If node is EndNode, create small circle
        if (isStartNode) {
        }


    }
}
