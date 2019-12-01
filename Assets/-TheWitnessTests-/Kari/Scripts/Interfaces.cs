using UnityEngine;
using System.Collections;

public interface IRule {
    bool Check();
}

public interface IActivatable {
    void Activate();
}
