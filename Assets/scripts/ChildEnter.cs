using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChildEnter : MonoBehaviour
{


    [SerializeField] TMP_InputField childInput;
    [SerializeField] AudioSource audio_sc;
    [SerializeField] AudioClip[] audio_clip;
    [SerializeField] GameObject[] letter;
    [SerializeField] GameObject prefabWord;
    [SerializeField] Transform spawnPositions;
    [SerializeField] TMP_Text text_Score;
    [SerializeField] Animator crowFly;
    [SerializeField] Animator playerFly;
    [SerializeField] Animator showWord;
    [SerializeField] Image[] lives;
    [SerializeField] Sprite emptyLives;


    GameObject instantiatedWord;



    private int Audioindex;
    private int letterCheckIndex;
    private int score;
    private int typedWordIndex;
    private int livesIndex;
    





    public void Start()
    {

        Audioindex = 0;
        letterCheckIndex = 0;
        score = 0;
        livesIndex = 0;

        typedWordIndex = 0;
        // particle = GetComponent<ParticleSystem>();
        AssignWords();
        StartCoroutine(showWithDelay());
        childInput.onValueChanged.AddListener(delegate { InputCheck(); });
        crowFly.SetTrigger("fly");
         
    }


    public void InputAudio()
    {
        audio_sc.Play();
    }
    public void AppreciateAudio()
    {
        audio_sc.clip = audio_clip[Audioindex];
        audio_sc.Play();
        Audioindex++;
        if (Audioindex == 5)
        {
            Audioindex = 0;
        }

    }

    public void AssignWords()
    {
        //looping through wordlist
        for (int i = 0; i < TeacherEnter.wordList.Count; i++)
        {

            string word = TeacherEnter.wordList[i];

            instantiatedWord = Instantiate(prefabWord, spawnPositions);
            //looping through each letter in word
            for (int j = 0; j < word.Length; j++)
            {
                int index = CheckLetter(word[j]);

                GameObject instantiatedLetter = Instantiate(letter[index], instantiatedWord.transform.GetChild(j));
                instantiatedWord.transform.GetChild(j).gameObject.SetActive(true);
            }
            instantiatedWord.SetActive(false);
           
        }
    }

    public int CheckLetter(char c)
    {
        int returnValue = 0;
        for (int k = 0; k < letter.Length; k++)
            if (c == char.Parse(letter[k].name))
                returnValue = k;

        return returnValue;
    }

    //word show one by one
    IEnumerator showWithDelay()
        {
             
          
       while(typedWordIndex < TeacherEnter.wordList.Count)
        {
             Debug.Log(typedWordIndex);
            showWord.SetTrigger("show");
              yield return new WaitForSeconds(2f);
            playerFly.SetTrigger("playerFly");
          yield return new WaitForSeconds(5f);
         spawnPositions.transform.GetChild(typedWordIndex).gameObject.SetActive(true);
            // for (int l = 0; l < TeacherEnter.wordLength[i]; l++)
            // {
            //     yield return new WaitForSeconds(15f);
            //     Debug.Log(spawnPositions.transform.GetChild(i).gameObject.name);
            //     spawnPositions.transform.GetChild(i).GetChild(l).gameObject.SetActive(true);
            // }
            yield return new WaitForSeconds(15f);
            childInput.text = "";
            

        }

    }

    //each letter hiding
    public void HideLetter()
    {
      //  spawnPositions.GetComponentInChildren<ParticleSystem>().Play();
       spawnPositions.GetComponentInChildren<LetterPrefabController>().gameObject.SetActive(false);
        
        // Invoke("HideLetterAfterParticle", 0.2f);
    }


    public void HideLetterAfterParticle()
    {
        spawnPositions.GetComponentInChildren<LetterPrefabController>().gameObject.SetActive(false);
    }
    //hide prefabword
    public void HideWord()
    {

        spawnPositions.transform.GetChild(typedWordIndex).gameObject.SetActive(false);
        Debug.Log( spawnPositions.transform.GetChild(typedWordIndex).gameObject.name);

        //crowFly.SetTrigger("fly");
         
    }



    public void InputCheck()
    {

        InputAudio();
        if (childInput.text.Length > 0)
        {

            GameObject go = spawnPositions.GetComponentInChildren<LetterPrefabController>().gameObject;

            if (childInput.text[letterCheckIndex] == go.name[0])
            {
                //typing matches with the letter
                
                HideLetter();
                letterCheckIndex++;
                if (childInput.text.Length == TeacherEnter.wordLength[typedWordIndex])
                {
                    //word typed fully
                    HideWord();
                     AppreciateAudio();
                    UpdateScore();

                    typedWordIndex++;
                    Debug.Log("length got");

                    childInput.text = "";
                    letterCheckIndex = 0;

                  StopAllCoroutines();
                    StartCoroutine(showWithDelay());


                }

            }
        }
    }


    public void UpdateScore()
    {
        score++;
        text_Score.text = "Score : " + score;
    }
    public void lossScore()
    {
        score--;
        text_Score.text = "Score : " + score;
    }



   
    public void CollisionDeleteWord()
    {
         HideWord();
        typedWordIndex++;


        childInput.text = "";
        letterCheckIndex = 0;

        if (livesIndex < 3)
        {
            lives[livesIndex].sprite = emptyLives;
            instantiatedWord.transform.position = spawnPositions.position;
            Debug.Log( instantiatedWord.transform.position);
             Debug.Log( spawnPositions.position);
            livesIndex++;
        }

        StopAllCoroutines();
        StartCoroutine(showWithDelay());
    }

}

