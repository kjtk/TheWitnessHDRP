using UnityEngine;
using System.Collections;

public interface IRule {
    bool Check();
    bool CheckVisibleSpots();
    void ShowFail();
}

public interface IActivatable {
    void Activate();
}
