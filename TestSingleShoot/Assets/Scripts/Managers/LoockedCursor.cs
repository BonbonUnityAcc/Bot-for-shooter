using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoockedCursor : MonoBehaviour
{
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
    }
}
