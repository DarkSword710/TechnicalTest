using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class containing functions to help convert from a number to its written name and vice versa
public static class Conversion
{
    public static string IntToString(int i)
    {
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
                return null;
        }
    }
}
