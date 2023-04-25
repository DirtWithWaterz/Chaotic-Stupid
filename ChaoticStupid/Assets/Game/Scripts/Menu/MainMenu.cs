using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SelectedStart(){
        SceneManager.LoadScene("Loading");
    }
    public void SelectedQuit(){
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
