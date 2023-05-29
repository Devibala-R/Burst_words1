using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionControl : MonoBehaviour
{
    [SerializeField] ChildEnter refChildEnter;
    [SerializeField] AudioSource audioData;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        audioData = GetComponent<AudioSource>();
        audioData.Play();
        refChildEnter.CollisionDeleteWord();

        
      refChildEnter.lossScore();
        /* if (livesIndex < 3)
      {
          lives[livesIndex].sprite = emptyLives;
          instantiatedWord.transform.position = spawnPositions.position;
          livesIndex++;
      }*/

    }

}
