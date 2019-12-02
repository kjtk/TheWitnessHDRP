﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour {
    // PuzzleState:
    // Locked - Solving not yet possible
    // Solvable - Puzzle solving can be started
    // Solved - Puzzle solved
    // Failed - Puzzle solution was wrong (in some cases disables previous puzzle) 
    public enum PuzzleState { None, Locked, Solvable, Solved, Failed };
    PuzzleState puzzleState = PuzzleState.None;

    public UnityEvent onComplete;
    public UnityEvent onCompleteUndo;

    public List<PuzzleNode> drawnPath;
    public List<IRule> rules;

    //public bool solvable;
    //public bool solved;
    //public GameObject unlocksPuzzle; // Unlock this puzzle when solved

    //public Vector3 renderedLineStart;
    //public Vector3 renderedLineEnd;

    LineRenderer lineRenderer;

    public List<Puzzle> unlockThesePuzzles = new List<Puzzle>();
    public List<Puzzle> unlockedByThesePuzzles = new List<Puzzle>();

    void Start() {
        // List about puzzles this puzzle opens
        //public List<Puzzle> unlockThesePuzzles = new List<Puzzle>();
        // List about puzzles which unlock this puzzle (neded?)
        //public List<Puzzle> unlockedByThesePuzzles = new List<Puzzle>();

    }


    public void NodeClicked(PuzzleNode node) {
        if (drawnPath.Count == 0 && node.isStartNode) {
            drawnPath.Add(node);
        } else {
            var last = drawnPath[drawnPath.Count - 1];
            var neighbors = last.neighbors;
            if (node == last ||
                (drawnPath.Count > 1 && node == drawnPath[drawnPath.Count-2])) {
                drawnPath.RemoveAt(drawnPath.Count - 1);
            } else if (neighbors.Contains(node)) {
                drawnPath.Add(node);
                if (node.isEndNode) {
                    var success = CheckRules();
                    print("At end node. Solved? " + success);
                    if (success == false) {
                        drawnPath.Clear();
                        DrawLineBetweenNodes();
                    }
                    if (success == true) {
                        puzzleState = PuzzleState.Solved;
                        foreach(var unlockThisPuzzle in unlockThesePuzzles) {
                            unlockThisPuzzle.puzzleState = ;
                        }
                    }
                }
            }
        }
        if (drawnPath.Count > 1) {
            DrawLineBetweenNodes();
        }
    }

    public bool CheckRules() {
        // instead of bool, return list of broken Rules?
        foreach (var r in rules) {
            if (!r.Check())
                return false;
        }
        return true;
    }

    void Awake() {
        rules = new List<IRule>(GetComponentsInChildren<IRule>());
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    void DrawLineBetweenNodes() {
        var points = new List<Vector3>();
        foreach(var n in drawnPath) {
            points.Add(n.transform.position);
        }
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
