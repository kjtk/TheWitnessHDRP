using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNode : MonoBehaviour {
    public bool isStartNode;
    public bool isEndNode;
    public List<PuzzleNode> neighbors;
    Puzzle puzzle;

    void Awake() {
        //puzzle = transform.parent.GetComponent<Puzzle>();
        puzzle = transform.parent.parent.GetComponent<Puzzle>();
    }

    void OnMouseDown() {
        puzzle.NodeClicked(this);
    }
}
