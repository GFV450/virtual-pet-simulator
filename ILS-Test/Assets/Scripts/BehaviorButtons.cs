using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BehaviorButtons : MonoBehaviour
{
    public DogData dogData;

    void Start()
    {
        //Activates buttons at every specified amount of time.
        float repeatRate = 5;
        InvokeRepeating("ActivateButton", repeatRate, repeatRate);

        OnActionSubscribe();
    }

    void Update()
    {
        if(dogData.health <= 0)
        {
            CancelInvoke();
            DeactivateAllButtons();
        }
    }

    //Method that gets the number of children of the Action Buttons game object, and uses their index to activate a button.
    private void ActivateButton()
    {
        int childIndex = gameObject.transform.childCount;
        int activeChild = Random.Range(0, childIndex);

        gameObject.transform.GetChild(activeChild).gameObject.SetActive(true);
    }

    //Method that deactivates a button after being clicked
    private void DeactivateButton()
    {
        EventSystem.current.currentSelectedGameObject.SetActive(false);
    }

    private void DeactivateAllButtons()
    {
        foreach (Transform child in transform)
        {
            OnDisableUnsubscribe();
            child.gameObject.SetActive(false);
        }
    }

    //Method that calls our custom event system to perform an action and subscribe to an event.
    private void OnActionSubscribe()
    {
        GameManager.current.OnActionButtonClick += DeactivateButton;
    }

    //Method that calls our custom event system to unsubscribe to an event.
    private void OnDisableUnsubscribe()
    {
        GameManager.current.OnActionButtonClick -= DeactivateButton;
    }
}