using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public static MainMenu mainMenu;
    public TMP_InputField NameInputFied;
    public TMP_InputField AgeInputFied;
    public Toggle maleToggle;
    public Toggle femaleToggle;
    public Toggle otherToggle;
    public static string name="";
    public static string character = "";
    public static int age = 0;
    public static string gender="";
    public void PlayGame()
    {
        string genderWithToggle="";
        if(maleToggle.isOn){
            genderWithToggle="male";
        }
        if(femaleToggle.isOn){
            genderWithToggle="female";
        }
        if(otherToggle.isOn){
            genderWithToggle="other";
        }
        if (int.Parse(AgeInputFied.text) < 50 && maleToggle.isOn)
        {
            character = "young man";
        }
        else if (int.Parse(AgeInputFied.text) < 50 && femaleToggle.isOn)
        {
            character = "young woman";
        }
        else if (int.Parse(AgeInputFied.text) > 50 && maleToggle.isOn)
        {
            character = "middle aged man";
        }
        else if (int.Parse(AgeInputFied.text) > 50 && femaleToggle.isOn)
        {
            character = "middle aged woman";
        }
        else if (int.Parse(AgeInputFied.text) < 50 && otherToggle.isOn)
        {
            character = "young other";
        }
        else if (int.Parse(AgeInputFied.text) > 50 && otherToggle.isOn)
        {
            character = "middle aged other";
        }
        else
        {
            character = "error";
        }
        name=NameInputFied.text;
        age=int.Parse(AgeInputFied.text);
        gender=genderWithToggle;
        print("name is " + name);
        print("gender is " + gender);
        print("age is "+ age);
        print("character is " + character);

        SceneManager.LoadSceneAsync("Lev1_House");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
