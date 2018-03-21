using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {
    
    // Destroys anything that exits

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Health>())
            other.gameObject.GetComponent<Health>().Die();
        Destroy(other.gameObject);
    }

}
