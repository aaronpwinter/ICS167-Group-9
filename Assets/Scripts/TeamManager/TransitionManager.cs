// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// We probably not gonna need this if we are using movement range

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject transitionCanvas;
    private TextMeshProUGUI textField;
    private Action lookAtPlayerDel;

    void Start()
    {
        transitionCanvas.SetActive(false);
        textField = transitionCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        textField.text = "";
    }

    public void showTransitionCanvas(string nextPlayerName)
    {
        textField.text = "Now it is " + nextPlayerName + "'s turn, get ready!";
        transitionCanvas.SetActive(true);
    }

    public void onImReadyButtonClick()
    {
        transitionCanvas.SetActive(false);
        lookAtPlayerDel();
    }

    public void subscribeToLookAtPlayerDel(Action del)
    {
        lookAtPlayerDel += del;
    }
}
