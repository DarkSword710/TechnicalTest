using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Class containing functions to help convert from a number to its written name and vice versa
public static class Utilities
{
    public static string IntToString(int i)
    {

        //Given a number returns the written form of that number
        string s = "";

        //First the tens are checked.
        switch ((i/10)%10)
        {
            case 0:
                break;

            case 1:
                switch (i%10)
                {
                    case 0:
                        s += "deu";
                        break;

                    case 1:
                        s += "onze";
                        break;

                    case 2:
                        s += "dotze";
                        break;

                    case 3:
                        s += "tretze";
                        break;

                    case 4:
                        s += "catorze";
                        break;

                    case 5:
                        s += "quinze";
                        break;

                    case 6:
                        s += "setze";
                        break;

                    case 7:
                        s += "dis";
                        break;

                    default:
                        s += "di";
                        break;
                }
                break;

            case 2:
                switch (i % 10)
                {
                    case 0:
                        s += "vint";
                        break;

                    default:
                        s += "vint-i-";
                        break;
                }
                break;

        }

        //Then the ones. Result of the previous operation is checked in 1 to 6 to fit catalan number naming conventions
        switch (i)
        {
            case 0:
                //Zero is a special case. It only adds its string when there's no other digits
                if (i == 0)
                {
                    s += "zero";
                }
                break;

            case 1:
                if((i / 10) % 10 != 1)
                {
                    s += "un";
                }
                break;

            case 2:
                if ((i / 10) % 10 != 1)
                {
                    s += "dos";
                }
                break;

            case 3:
                if ((i / 10) % 10 != 1)
                {
                    s += "tres";
                }
                break;

            case 4:
                if ((i / 10) % 10 != 1)
                {
                    s += "quatre";
                }
                break;

            case 5:
                if ((i / 10) % 10 != 1)
                {
                    s += "cinc";
                }
                break;

            case 6:
                if ((i / 10) % 10 != 1)
                {
                    s += "sis";
                }
                break;

            case 7:
                s += "set";
                break;

            case 8:
                s += "vuit";
                break;

            case 9:
                s += "nou";
                break;

            default:
                return null;
        }

        return s;
    }
    /*
    public static int StringToInt(string s)
    {
        //Given the written form of a number returns the number itself
        switch (s.ToLower())
        {
            case "zero":
                return 0;
            case "un":
                return 1;
            case "dos":
                return 2;
            case "tres":
                return 3;
            case "quatre":
                return 4;
            case "cinc":
                return 5;
            case "sis":
                return 6;
            case "set":
                return 7;
            case "vuit":
                return 8;
            case "nou":
                return 9;
            case "deu":
                return 10;
            default:
                return -1;
        }
    }*/

    public static IEnumerator Fade(Text text, float speed)
    {
        //Coroutine that fades a color according to a speed. The code won't loop if speed is 0 to prevent getting stuck
        do
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + speed * Time.deltaTime);
            yield return null;
        } while (text.color.a > 0 && text.color.a < 1 && speed != 0);

        //When the loop ends, sets the alpha to exactly 1 or 0 to prevent the loop being skipped on future calls
        if (speed > 0)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        }
        else if (speed < 0)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
        yield break;
    }

    public static IEnumerator Fade(Image image, float speed)
    {
        //Coroutine that fades a color according to a speed. The code won't loop if speed is 0 to prevent getting stuck
        do
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + speed * Time.deltaTime);
            yield return null;
        } while (image.color.a > 0 && image.color.a < 1 && speed != 0);

        //When the loop ends, sets the alpha to exactly 1 or 0 to prevent the loop being skipped on future calls
        if(speed > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        } else if (speed < 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
        yield break;
    }
}
