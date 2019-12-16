using UnityEngine;
using System.Collections;

public interface IRule {
    bool Check();
    void ShowFail();
}

public interface IActivatable {
    void Activate();
}
