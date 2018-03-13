using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentOnAwake : MonoBehaviour
{
    // Happens before Start (I think)
    private void Awake()
    {
        // Appends the name of the previous parent to the end of the name.
        gameObject.name = gameObject.name + " (" + transform.parent.name + ")";

        // Does not alter the transform values
        transform.SetParent(null, false);
    }

}
