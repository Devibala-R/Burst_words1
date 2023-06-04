using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPrefabController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    public void Enabledynamic()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    

}
