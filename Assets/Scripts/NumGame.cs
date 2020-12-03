using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumGame : MonoBehaviour
{

    //Variable declaration to hold references to the different buttons and the text box
    private Text textBox;
    private Button[] buttons;

    //Number to guess and array of numbers to put in the other buttons
    private int guessNum;
    private int[] answerNums;

    //Array position of the button containing the right answer
    private int rightAnswer;

    //In-editor variables to change the scripts behaviour
    [SerializeField]
    private int maxValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        //On start the Object finds its children textbox and buttons
        textBox = GetComponentInChildren<Text>();
        Debug.Log(textBox.name + "attached succesfully");

        buttons = GetComponentsInChildren<Button>();
        for(int i = 0; i < buttons.Length; i++)
        {
            Debug.Log(buttons[i].name + "attached succesfully");
        }

        //Then initializes the answer array to the appropiate number of buttons and randomizes the values
        answerNums = new int[buttons.Length];

        RandomizeValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomizeValues()
    {
        //The written number to show and to be guessed is randomized first
        guessNum = Mathf.RoundToInt(Random.value * maxValue);

        //First we generate a right answer at one of the buttons at random
        rightAnswer = Mathf.RoundToInt(Random.value * (buttons.Length - 1));

        //Then every answer is generated ensuring there is no repeats and putting the right answer in its corresponding button
        for (int i = 0; i < buttons.Length; i++)
        {
            if(i == rightAnswer)
            {
                answerNums[i] = guessNum;
            }
            else
            {
                bool repeat;
                do
                {
                    repeat = false;

                    answerNums[i] = Mathf.RoundToInt(Random.value * maxValue);

                    if (answerNums[i] == guessNum)
                    {
                        repeat = true;
                    }
                    else
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (answerNums[i] == answerNums[j])
                            {
                                repeat = true;
                            }
                        }
                    }
                } while (repeat && i < maxValue);
                //This ensures that the loop doesn't become infinite if there are more buttons than possible values
            }
        }

        //Then values are assigned to their respective Objects
        textBox.text = Conversion.IntToString(guessNum);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = answerNums[i].ToString();
        }
    }

}


