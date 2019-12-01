using UnityEngine;
using System.Collections;

public class MouseClickerScript : MonoBehaviour {

    void Start() {
        //Cursor.visible = !Cursor.visible;
        //Cursor.visible = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Debug.Log("Mouse Left Button Down");
            Debug.DrawLine(transform.position, Input.mousePosition, Color.red);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                //var c = hit.collider.GetComponent<IActivatable>();
                //if (c != null)
                //    c.Activate();
                var c = hit.collider.GetComponents<IActivatable>();
                foreach (var a in c) {
                    a.Activate();
                }
            }
            //if (Physics.Raycast(ray, out hit, 100)) {
            //    var rc = hit.collider.gameObject.GetComponent<RiseOnClick>();
            //    if (rc != null)
            //        rc.Activate();
            //    var cc = hit.collider.gameObject.GetComponent<ChangeColorOnClick>();
            //    if (cc != null)
            //        cc.Activate();
            //    var sc = hit.collider.gameObject.GetComponent<ScaleOnClick>();
            //    if (sc != null)
            //        sc.Activate();
            //}

        }
    }
}

