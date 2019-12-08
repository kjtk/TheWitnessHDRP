using UnityEngine;
using System.Collections;

public interface IRule {
    bool CheckBlackSpots();
    bool CheckOneCorrectPath();
    void ShowFail();
}

public interface IActivatable {
    void Activate();
}
