using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    Text text;
    Canvas canvas;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        text = GetComponentInChildren<Text>();
    }

    public void ChangeHealth(int health)
    {
        text.text = health.ToString(); 
    }
}
