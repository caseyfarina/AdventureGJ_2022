using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableTrigger : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

}
