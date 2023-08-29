using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

sealed class GameManager : MonoBehaviour
{
    //Static singleton reference to the script for event handling.
    public static GameManager current;
    public event Action<DogBehavior> OnActionButtonClick;

    public DogData dogData;
    public Button restartButton;

    //Static instance initialization to be called upon from other classes.
    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        if(dogData.isDogAlive == false)
        {
            restartButton.gameObject.SetActive(true);
        }
    }

    //Method that handles our custom action/event system. It's called when a button is clicked, and it's assigned to the buttons onClick() method in the Inspector.
    public void ActionButtonClick()
    {
        DogBehavior buttonBehavior = EventSystem.current.currentSelectedGameObject.GetComponent<Enums>().behaviorEnum;

        OnActionButtonClick?.Invoke(buttonBehavior);
    }

    //Method for restarting the game when the dog dies.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
