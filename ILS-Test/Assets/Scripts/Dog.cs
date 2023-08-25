using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dog : MonoBehaviour
{
    public DogData dogData;
    private bool isDogAlive = true;

    void Start()
    {
        dogData.health = 100;
        OnActionSubscribe();
    }

    void Update()
    {
        if(dogData.health > 0)
        {
            ReduceHealth();
        }
        else if (dogData.health <= 0 && isDogAlive)
        {
            DogDeath();
        }
    }

    //Method that identifies which button was pressed, and what health value must be restored based on the DogBehavior enum.
    private void RestoreHealth()
    {
        DogBehavior buttonBehaviorName = EventSystem.current.currentSelectedGameObject.GetComponent<Enums>().dogBehavior;

        for (int index = 0; index < dogData.behaviorList.Length; index++)
        {
            DogBehavior dogBehaviorName = dogData.behaviorList[index].behavior;
            float healValue = dogData.behaviorList[index].behaviorHealValue;

            if (dogBehaviorName == buttonBehaviorName)
            {
                dogData.UpdateHealth(healValue);
            }
        }
    }

    //Method that constantly reduces the dog's health.
    private void ReduceHealth()
    {
        dogData.health -= Time.deltaTime;
    }

    //Method that signals when the dog has died and the game us over.
    private void DogDeath()
    {
        GameObject.Destroy(gameObject);
        isDogAlive = false;
        OnDisableUnsubscribe();
    }

    //Method that calls our custom event system to perform an action and subscribe to an event.
    private void OnActionSubscribe()
    {
        GameManager.current.OnActionButtonClick += RestoreHealth;
    }

    //Method that calls our custom event system to unsubscribe to an event.
    private void OnDisableUnsubscribe()
    {
        GameManager.current.OnActionButtonClick -= RestoreHealth;
    }
}
