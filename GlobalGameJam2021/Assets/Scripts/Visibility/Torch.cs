using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private List<GameObject> SoulsInArea = new List<GameObject>();
    private bool isOn = false;
    [SerializeField] private GameObject fire;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if ((other.CompareTag("Soul") || other.CompareTag("Player")) && !isOn)
        {
            SoulsInArea.Add(other.gameObject);
            TurnOn();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Soul") || other.CompareTag("Player")) && isOn)
        {
            var s = other.gameObject;
            if (SoulsInArea.Contains(s))
            {
                SoulsInArea.Remove(s);
                if (SoulsInArea.Count == 0)
                    TurnOff();
            }
        }
    }

    private void TurnOn()
    {
        isOn = true;
        Debug.Log("ON");
        fire.SetActive(true);
    }

    private void TurnOff()
    {
        isOn = false;
        Debug.Log("OFF");
        fire.SetActive(false);
    }
}
