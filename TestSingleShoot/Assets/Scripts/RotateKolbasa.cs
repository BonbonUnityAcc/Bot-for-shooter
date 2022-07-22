using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateKolbasa : MonoBehaviour
{
    private Transform trKolb;

    private void Start()
    {
        trKolb = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        trKolb.Rotate(new Vector3(0, 0, 5f));
    }
}
