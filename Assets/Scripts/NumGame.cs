using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

enum State
{
    NUM_FADE_IN,
    NUM_SHOW,
    NUM_FADE_OUT,
    ANSWERS_FADE_IN,
    ANSWERS_WAIT,
    ANSWERS_SHOW,
    ANSWERS_FADE_OUT
};

public class NumGame : MonoBehaviour
{
    //Variable declaration to hold references to the different buttons and their elements and the text box
    private Text textBox;

    private Text scoreboard;

    private Button[] buttons;
    private Image[] buttonImages;
    private Text[] buttonText;

    //Number to guess and array of numbers to put in the other buttons
    private int guessNum;
    private int[] answerNums;

    //Array position of the button containing the right answer
    private int rightAnswer;

    //Right and wrong answers given throughout the playthrough and the current question
    private int rightScore;
    private int wrongScore;
    private bool[] pressedAnswers;
    int failCounter;

    //In-editor variables to change the scripts behaviour
    [SerializeField]
    private int maxValue = 29;

    //Seconds that the fading animation will last
    [SerializeField]
    private float fadeTime = 2;

    //Seconds that the number to guess will be shown on screen
    [SerializeField]
    private float showTime = 2;

    //Seconds that the answers will bee shown on screen
    [SerializeField]
    private float answerShowTime = 2;

    //State of the game and timer to switch state
    State state;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        //On start the Object finds its children textboxes and buttons and sets their visibility
        Text[] textAux = GetComponentsInChildren<Text>();
        for(int i = 0; i < textAux.Length; i++)
        {
            if(textAux[i].text.ToLower() == "written number")
            {
                textBox = textAux[i];
                textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 0);

            } else if (textAux[i].text.ToLower() == "scoreboard")
            {
                scoreboard = textAux[i];
            }
        }

        buttons = GetComponentsInChildren<Button>();
        buttonImages = new Image[buttons.Length];
        buttonText = new Text[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            buttonImages[i] = buttons[i].gameObject.GetComponent<Image>();
            buttonText[i] = buttons[i].GetComponentInChildren<Text>();

            buttonImages[i].color = new Color(buttonImages[i].color.r, buttonImages[i].color.g, buttonImages[i].color.b, 0);
            buttonText[i].color = new Color(buttonText[i].color.r, buttonText[i].color.g, buttonText[i].color.b, 0);
        }

        //Then initializes the answer array to the appropiate number of buttons
        answerNums = new int[buttons.Length];
        pressedAnswers = new bool[buttons.Length];

        //Finally it sets the right and wrong answers to 0 and updates the scoreboard and initialises the state
        rightScore = 0;
        wrongScore = 0;

        UpdateScoreboard();

        ChangeState(State.NUM_FADE_IN);
    }

    // Update is called once per frame
    void Update()
    {
        //The update function manages the state machine the game runs on
        //Most states just wait for the timer to end to change to the appropiate state. The ChangeState() method manages
        //everything else
        switch (state)
        {
            case State.NUM_FADE_IN:
                timer += Time.deltaTime;
                if(timer >= fadeTime)
                {
                    ChangeState(State.NUM_SHOW);
                }
                break;

            case State.NUM_SHOW:
                timer += Time.deltaTime;
                if (timer >= showTime)
                {
                    ChangeState(State.NUM_FADE_OUT);
                }
                break;

            case State.NUM_FADE_OUT:
                timer += Time.deltaTime;
                if (timer >= fadeTime)
                {
                    ChangeState(State.ANSWERS_FADE_IN);
                }
                break;

            case State.ANSWERS_FADE_IN:
                timer += Time.deltaTime;
                if (timer >= fadeTime)
                {
                    ChangeState(State.ANSWERS_WAIT);
                }
                break;

            case State.ANSWERS_WAIT:
                break;

            case State.ANSWERS_SHOW:
                timer += Time.deltaTime;
                if (timer >= answerShowTime)
                {
                    ChangeState(State.ANSWERS_FADE_OUT);
                }
                break;

            //ANSWERS_FADE_OUT is the special case. It changes its state based on if the right answer was pressed or not.
            //The function that manages button pressing automatically sets the right answer to pressed if all other buttons
            //have been used.
            case State.ANSWERS_FADE_OUT:
                timer += Time.deltaTime;
                if (timer >= fadeTime)
                {
                    if (pressedAnswers[rightAnswer])
                    {
                        ChangeState(State.NUM_FADE_IN);
                    }
                    else
                    {
                        ChangeState(State.ANSWERS_WAIT);
                    }
                }
                break;

            default:
                break;
        }
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
        textBox.text = Utilities.IntToString(guessNum);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonText[i].text = answerNums[i].ToString();
        }
    }

    private void UpdateScoreboard()
    {
        //Simple function that updates the written score
        scoreboard.text = "Respostes correctes: " + rightScore.ToString() + "\nRespostes equivocades: " + wrongScore.ToString();

    }

    private void ChangeState(State s)
    {
        //This function sets the loop to enter the state indicated.
        switch (s)
        {
            //When the number to guess fades in, the numbers are rerolled and all buttons are unpressed.
            case State.NUM_FADE_IN:
                RandomizeValues();
                failCounter = 0;
                StartCoroutine(Utilities.Fade(textBox, 1/fadeTime));
                for (int i = 0; i < buttons.Length; i++){
                    buttonImages[i].color = new Color(1, 1, 1, buttonImages[i].color.a);
                    pressedAnswers[i] = false;
                }
                break;

            case State.NUM_SHOW:
                break;

            case State.NUM_FADE_OUT:
                StartCoroutine(Utilities.Fade(textBox, -1 / fadeTime));
                break;

            case State.ANSWERS_FADE_IN:
                for(int i = 0; i < buttons.Length; i++)
                {
                    StartCoroutine(Utilities.Fade(buttonImages[i], 1 / fadeTime));
                    StartCoroutine(Utilities.Fade(buttonText[i], 1 / fadeTime));
                }
                break;

            //When waiting for the answer, all buttons that have not been pressed yet are enabled
            case State.ANSWERS_WAIT:
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (!pressedAnswers[i])
                    {
                        buttons[i].interactable = true;
                    }
                }
                break;
            
            //Once a button is pressed, the code disables them until the loop enters ANSWERS_WAIT again
            case State.ANSWERS_SHOW:
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].interactable = false;
                }
                break;

            //When fading out buttons, fades out all pressed buttons. This will have no effect on buttons already faded.
            case State.ANSWERS_FADE_OUT:
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (pressedAnswers[i])
                    {
                        StartCoroutine(Utilities.Fade(buttonImages[i], -1 / fadeTime));
                        StartCoroutine(Utilities.Fade(buttonText[i], -1 / fadeTime));
                    }
                }
                break;

            default:
                break;
        }

        //Resets the timer and changes the state accordingly
        timer = 0;
        state = s;
    }

    public void OnButtonClicked()
    {
        //This function is called when a button is pressed, and must be assigned to the buttons that trigger it
        //The function itself identifies the button that calls it, that way it doesn't need any additional parameters
        GameObject caller = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < buttons.Length; i++)
        {
            //Once the button is identified as one of the valid buttons, it checks wether the answer is right or not
            if (caller == buttons[i].gameObject)
            {
                if(i == rightAnswer)
                {
                    //If the answer is right, presses all other buttons to fade them all out
                    rightScore += 1;
                    buttonImages[i].color = new Color(0, 1, 0, buttonImages[i].color.a);
                    for (int j = 0; j < buttons.Length; j++)
                    {
                        pressedAnswers[j] = true;
                    }
                }
                else
                {
                    wrongScore += 1;
                    buttonImages[i].color = new Color(1, 0, 0, buttonImages[i].color.a);

                    //If the answer is wrong, checks whether the only answer left is the right one
                    pressedAnswers[i] = true;
                    failCounter = 0;

                    for (int j = 0; j < buttons.Length; j++)
                    {
                        if (pressedAnswers[j])
                        {
                            failCounter++;
                        }
                    }

                    //If it is, presses it to fade it out to and reroll the numbers
                    if(failCounter >= buttons.Length - 1)
                    {
                        buttonImages[rightAnswer].color = new Color(0, 1, 0, buttonImages[i].color.a);
                        pressedAnswers[rightAnswer] = true;
                    }
                }

                //Regardless of that, it updates the scoreboard and update the state to show the results
                UpdateScoreboard();
                ChangeState(State.ANSWERS_SHOW);
                break;
            }
        }
    }
}


