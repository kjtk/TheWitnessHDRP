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
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }

    void OnMouseDown() {
        puzzle.NodeClicked(this);
    }

    void Start() {

        var points = new List<Vector3>();
        var puzzleGridLineOffset = 0.0f;

        var puzzleGridLine = new VectorLine("PuzzlePathLine", points, 0.0f);

        foreach (var neighbor in neighbors) {
            points.Add(gameObject.transform.position += Vector3.back * puzzleGridLineOffset);
            points.Add(neighbor.transform.position += Vector3.back * puzzleGridLineOffset);

            //VectorLine.SetLine3D(Color.red, 
            //    gameObject.transform.position += Vector3.back * puzzlePathLineOffset, 
            //    neighbor.transform.position += Vector3.back * puzzlePathLineOffset);
        }

        puzzleGridLine.material = puzzle.PuzzleGridMaterial;
        puzzleGridLine.lineWidth = 15.0f;
        puzzleGridLine.lineType = LineType.Discrete;
        puzzleGridLine.joins = Joins.Weld;
        puzzleGridLine.Draw3D();



        /*
         * 
         * 
        // LineRenderer not very good for this

        // Create path from node to it's neighbour nodes.

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
        lineRenderer.transform.position += Vector3.back * 0.02f;
        lineRenderer.numCapVertices = 5;
        lineRenderer.numCornerVertices = 5;

        var points = new List<Vector3>();
        foreach (var neighbor in neighbors) {

            // lines get distorded when they "turn back"...!!!
            // case 1
            //points.Add(gameObject.transform.position);
            //points.Add(neighbor.transform.position);
            //points.Add(neighbor.transform.position);

            // case 2
            points.Add(gameObject.transform.position);
            points.Add(neighbor.transform.position);
            points.Add(neighbor.transform.position);
            points.Add(neighbor.transform.position);
            points.Add(neighbor.transform.position);
            points.Add(gameObject.transform.position);

            // case 3
            //points.Add(gameObject.transform.position);
            //points.Add(gameObject.transform.position);
            //points.Add(neighbor.transform.position);
            //points.Add(neighbor.transform.position);

        }
        // Draw the paths from single node.
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());

        // If node is StartNode, create big circle
        if (isStartNode) {
        }

        // If node is EndNode, create small circle
        if (isStartNode) {
        }
        *
        *
        */


    }
}
