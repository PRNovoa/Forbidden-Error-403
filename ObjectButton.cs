using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectButton : MonoBehaviour
{
    /*public SpriteRenderer button;
    public BoxCollider2D buttonHit;*/
    public GameObject objectsGrande;

    /// <summary>
    /// Activacion de las opciones del menu
    /// </summary>
    private void OnMouseDown()
    {
        if (this.GetComponent<SpriteRenderer>().enabled)
        {
            objectsGrande.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<SpriteRenderer>().color = Color.red;

        }
    }
}
