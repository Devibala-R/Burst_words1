using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TeacherEnter : MonoBehaviour
{

    [SerializeField] TMP_InputField input;
    [SerializeField] Button playButton;
    [SerializeField] Button submitButton;
    [SerializeField] GameObject Teacher;
    [SerializeField] GameObject Child;
   [SerializeField] Animator crowFly;

    public int maxWordLimit = 5;
    private int count;
    public static List<string> wordList;
    public static List<int> wordLength;
    


    public void Start()
    {
        input.onValueChanged.AddListener(delegate { WordCheck(); });
        count = 0;
        wordList = new List<string>();
        wordLength = new List<int>();
    crowFly.SetTrigger("fly");
    }
    public void WordCheck()
    {
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        if (input.text.Length > 5)
        {
            submitButton.gameObject.SetActive(false);
            yield return new WaitForSeconds(seconds: 5f);
            input.text = "";
            yield return new WaitForSeconds(seconds: 3f);

            submitButton.gameObject.SetActive(true);
        }
    }
    public void onclicksubmit()
    {

        //Debug.Log("hi");
        wordList.Add(input.text);
        wordLength.Add(input.text.Length);


        input.text = "";
        count++;

        if (count == maxWordLimit)
        {
            playButton.gameObject.SetActive(true);
        }
    }
    public void OnclickPlay()
    {
        GameObjectdisable();
    }
    public void GameObjectdisable()
    {
        Teacher.gameObject.SetActive(false);
        Child.gameObject.SetActive(true);
    }

}
