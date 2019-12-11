using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class VectorsityTest : MonoBehaviour {
    
    public VectorObject2D line; Vector2 point0;

    void Start() {
        point0 = line.vectorLine.points2[0];
    } 
    
    void Update() { 
        var point = line.vectorLine.points2[0]; 
        point.x = Mathf.Sin(Time.time * 2.0f) * 50.0f + point0.x; point.y = Mathf.Cos(Time.time * 2.0f) * 50.0f + point0.y; 
        line.vectorLine.points2[0] = point; 
        line.vectorLine.Draw(); 
    }
}
