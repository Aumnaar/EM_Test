using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    //public UI _ui;
    public int _coinsCount;
    public string collisionTag;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(collisionTag))
        {
            UI.instance.OnCrystals();
            Destroy(col.gameObject);
        }

    }

    

}
