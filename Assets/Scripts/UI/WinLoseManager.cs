// Boyuan Huang
// Alec Kaxon-Rupp - Debugging and Implementation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;



// WinLoseManager deals with the win scene, and button in the win scene
public class WinLoseManager : MonoBehaviour
{

    [SerializeField]
    private GameObject winCanvas;

    [SerializeField]
    private TextMeshProUGUI textField;

    private static Action disableControlDel;

    // Start is called before the first frame update
    void Start()
    {
        winCanvas = this.gameObject;
        textField = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TeamManager.subscribeToWinDel(win);
        winCanvas.SetActive(false);
    }

    public void win(string playerName)
    {
        textField.text = playerName + " wins!!!";
        winCanvas.SetActive(true);
        disableControlDel();
    }

    public static void subscribeToDisableControl(Action action)
    {
        disableControlDel += action;
    }

    public void OnBackToHomeMenuButtonClick()
    {
        TeamManager.resetAll();
        SceneManager.LoadScene("Menu");
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
