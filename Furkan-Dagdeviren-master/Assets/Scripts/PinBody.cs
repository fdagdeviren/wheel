using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBody : MonoBehaviour
{
    public string TriggerText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerText = collision.name;
    }
}
