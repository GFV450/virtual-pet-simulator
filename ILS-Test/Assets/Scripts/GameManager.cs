using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class manages game state and action events
sealed class GameManager : MonoBehaviour
{
    //Static singleton reference to the script for event handling.
    public static GameManager current;
    public event Action OnActionButtonClick;


    //Static instance initialization to be called upon from other classes.
    private void Awake()
    {
        current = this;
    }

    //Method that handles our custom action/event system. It's called when a button is clicked, and it's assigned to the buttons onClick() method in the Inspector.
    public void ActionButtonClick()
    {
        OnActionButtonClick?.Invoke();
    }
}
