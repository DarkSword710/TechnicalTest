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
        switch (i)
        {
            case 0:
                return "Zero";
            case 1:
                return "Un";
            case 2:
                return "Dos";
            case 3:
                return "Tres";
            case 4:
                return "Quatre";
            case 5:
                return "Cinc";
            case 6:
                return "Sis";
            case 7:
                return "Set";
            case 8:
                return "Vuit";
            case 9:
                return "Nou";
            case 10:
                return "Deu";
            default:
                return null;
        }
    }

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
    }

    public static IEnumerator Fade(Text text, float speed)
    {
        //Coroutine that fades a color according to a speed
        do
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + speed * Time.deltaTime);
            yield return null;
        } while (text.color.a > 0 && text.color.a < 1);
    }

    public static IEnumerator Fade(Image image, float speed)
    {
        //Coroutine that fades a color according to a speed
        do
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + speed * Time.deltaTime);
            yield return null;
        } while (image.color.a > 0 && image.color.a < 1);
    }
}
