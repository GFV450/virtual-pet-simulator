using UnityEngine;

public class Dog : MonoBehaviour
{
    public DogData dogData;

    void Start()
    {
        //Sets all the parameters of the instantiated dog to the DogData scriptable object.
        dogData.isDogAlive = true;
        dogData.health = 100;
        dogData.transform = transform;
        dogData.animation = gameObject.GetComponent<Animator>();
        dogData.audio = gameObject.GetComponent<AudioSource>();

        OnActionSubscribe();
    }

    void Update()
    {
        if (dogData.health > 0)
        {
            ReduceHealth();
        }
        else if (dogData.health <= 0 && dogData.isDogAlive)
        {
            DogDeath();
        }
    }

    //Method that identifies which button was pressed, and what health value must be restored based on the DogBehavior enum.
    private void RestoreHealth(DogBehavior buttonBehavior)
    {
        foreach (DogData.Behavior dogBehavior in dogData.behaviorList)
        {
            if(dogBehavior.behavior == buttonBehavior)
            {
                dogData.UpdateHealth(dogBehavior.behaviorHealValue);
            }
        }
    }

    //Method that constantly reduces the dog's health.
    private void ReduceHealth()
    {
        dogData.health -= Time.deltaTime;
    }

    //Method that signals when the dog has died and the game is over.
    private void DogDeath()
    {
        dogData.isDogAlive = false;

        dogData.UpdateDogAnimation(DogBehavior.Dead);
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
