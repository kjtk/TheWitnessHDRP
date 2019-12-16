using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RuleVisitPoints : MonoBehaviour, IRule {
    public List<PuzzleNode> points;
    List<GameObject> visibleSpots;
    public GameObject spotPrefab;

    Puzzle puzzle;

    public bool Check() {
        // Player must go through predefined nodes (black spots/hexagons)
        Debug.Log("CheckBlackSpots");
        foreach (var pn in points) {
            if (!puzzle.drawnPath.Contains(pn))
                return false;
        }
        return true;
    }



    public void ShowFail() {
        print("RuleVisitPoints failed");
    }
    void Awake() {
        puzzle = transform.parent.GetComponent<Puzzle>();
        /*
        if (visibleSpots.Count != 0) {
            Debug.Log("Print spotPrefabs destroyed");
            for (int i = 0; i < visibleSpots.Count - 1; i++) {
                Destroy(visibleSpots[i]);
            }
        }
        */
    }
#if UNITY_EDITOR
    void Update() {
        /*
        if (visibleSpots.Count == 0) {
            foreach (var pn in points) {
                //Instantiate(spotPrefab, pn.transform);
                //visibleSpots.Add(spotPrefab);

            }
            Debug.Log("Print spotPrefabs instantiated");
            for (int i = 0; i < points.Count - 1; i++) {
                visibleSpots[i] = spotPrefab;
                //visibleSpots[i].transform = points[i].transform;
            }
        }
        */
    }
#endif
}
