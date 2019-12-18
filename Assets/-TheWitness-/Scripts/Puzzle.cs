using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour {
    // PuzzleState:
    // Locked - Solving not yet possible
    // Solvable - Puzzle solving can be started
    // Solved - Puzzle solved
    // Failed - Puzzle solution was wrong (in some cases disables previous puzzle) ??(not needed)
    public enum PuzzleState { None, Locked, Solvable, Solved, Failed };
    public PuzzleState puzzleState = PuzzleState.None;

    public UnityEvent onComplete;
    public UnityEvent onCompleteUndo;

    public List<PuzzleNode> drawnPath;
    public List<IRule> rules;

    public List<ParticleSystem> hints = new List<ParticleSystem>();

    LineRenderer lineRenderer;

    public List<Puzzle> unlockThesePuzzles = new List<Puzzle>();
    public List<Puzzle> unlockedByThesePuzzles = new List<Puzzle>();

    public AudioSource audioPuzzleStart;
    public AudioSource audioPuzzleSolved;
    public AudioSource audioPuzzleFails;
    public AudioSource audioPuzzleActive;
    
    public Material PuzzleGridMaterialSolvable;
    public Material PuzzleGridMaterialLocked;
    public Material PuzzleBlackSpot;

    public GameObject PuzzleNodeParticlesPrefab;

    public bool showPuzzleNodes = false;
    public bool showPuzzleBackground = false;

    void Start() {
        gameObject.GetComponent<MeshRenderer>().enabled = showPuzzleBackground;

    }

    public void NodeClicked(PuzzleNode node) {

        if (puzzleState == PuzzleState.Solvable) {

            //Debug.Log("Node clicked");
            if (drawnPath.Count == 0 && ( node.isStartNode || node.isFakeStartNode ) ) {
                audioPuzzleStart.Play();
                audioPuzzleActive.Play();
                drawnPath.Add(node);
                DrawLineBetweenNodes();
            } else {
                var last = drawnPath[drawnPath.Count - 1];
                var neighbors = last.neighbors;

                // Check if node already in drawn path, node can be visited only once.
                // If it's previous one, then continue... (simplify these ifs...)
                if (!drawnPath.Contains(node) || node == drawnPath[drawnPath.Count - 2]) {

                    if (node == last ||
                            (drawnPath.Count > 1 && node == drawnPath[drawnPath.Count - 2])) {
                        drawnPath.RemoveAt(drawnPath.Count - 1);
                    } else if (neighbors.Contains(node)) {

                        drawnPath.Add(node);

                        if (node.isEndNode) {
                            var brokenRules = CheckRules();
                            var success = brokenRules.Count == 0;
                            print("At end node. Solved? " + success);
                            if (success == false) {
                                audioPuzzleActive.Stop();
                                audioPuzzleFails.Play();
                                foreach (var f in brokenRules) {
                                    f.ShowFail();
                                }
                                onCompleteUndo.Invoke();
                                drawnPath.Clear();
                                DrawLineBetweenNodes();
                            }
                            if (success == true) {
                                audioPuzzleActive.Stop();
                                audioPuzzleSolved.Play();
                                onComplete.Invoke();
                                puzzleState = PuzzleState.Solved;
                                //foreach(var unlockThisPuzzle in unlockThesePuzzles) {
                                //    unlockThisPuzzle.puzzleState = PuzzleState.Solvable;
                                //}
                                UnlockThesePuzzles();
                                //
                            }
                        }
                    }

                }


            }
            if (drawnPath.Count > 1) {
                DrawLineBetweenNodes();
            }
            GiveHint();

        } else if (puzzleState == PuzzleState.Locked) {
            // Not solvale puzzle yet...
            Debug.Log("This puzzle is locked!");
        } else if (puzzleState == PuzzleState.Solved) {
            // Already solved puzzle.
            // Status will be cleared (-> solvable) on click?
            Debug.Log("This puzzle is already solved.");
        }

    }

    public List<IRule> CheckRules() {
        var brokenRules = new List<IRule>();
        foreach (var r in rules) {
            if (!r.Check())
                brokenRules.Add(r);
        }
        return brokenRules;
    }

    void Awake() {
        rules = new List<IRule>(GetComponentsInChildren<IRule>());
        lineRenderer = GetComponentInChildren<LineRenderer>();
        // Don't use World Space as then the line doesn't follow parent (Puzzle).
        // Scaling issue to fix...
        lineRenderer.useWorldSpace = false;
    }

    void DrawLineBetweenNodes() {
        var points = new List<Vector3>();
        foreach(var n in drawnPath) {
            //points.Add(n.transform.position);
            points.Add(n.transform.localPosition);
        }
        if (points.Count == 1) {
            points.Add(points[0]);
        }
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    void UnlockThesePuzzles() {
        if (unlockThesePuzzles.Count > 0) {
            foreach (var unlockThisPuzzle in unlockThesePuzzles) {
                unlockThisPuzzle.puzzleState = PuzzleState.Solvable;
                //unlockThisPuzzle.lineRenderer.material = unlockThisPuzzle.PuzzleGridMaterialSolvable;
                // Update grid colors on unlocked puzzle
                var nodes = unlockThisPuzzle.GetComponentsInChildren<PuzzleNode>();
                foreach (var i in nodes) {
                    //foreach (var j in i)
                    //i.GetComponentInChildren<LineRenderer>().material = unlockThisPuzzle.PuzzleGridMaterialSolvable;
                    var lRends = i.GetComponentsInChildren<LineRenderer>();
                    foreach (var j in lRends) {
                        j.GetComponentInChildren<LineRenderer>().material = unlockThisPuzzle.PuzzleGridMaterialSolvable;
                    }
                }
            }
        }
    }

    void GiveHint() {
        
        foreach (var n in hints) {
            n.Stop();
        }
        hints.Clear();
        if (drawnPath.Count > 0) {
            var last = drawnPath[drawnPath.Count - 1];
            var neighbors = last.neighbors;
            foreach(var n in neighbors) {
                if (!drawnPath.Contains(n)) {
                    var p = n.transform.Find("PuzzleNodeParticles(Clone)").GetComponent<ParticleSystem>();
                    p.Play();
                    hints.Add(p);
                }
            }
        }
    }
}
