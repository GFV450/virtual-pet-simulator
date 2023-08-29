using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class DogData : ScriptableObject
{
    //Data of the dog to be stored in the scriptable object and updated across classes.
    [Range (0, 100)]
    public float health;
    public Transform transform;
    public Animator animation;
    public AudioSource audio;
    public bool isDogAlive;

    [Serializable]
    public struct Behavior
    {
        public DogBehavior behavior;
        public float behaviorHealValue;
    }

    public Behavior[] behaviorList;

    //This method is called to ensure that health never goes over 100%.
    public void UpdateHealth(float healValue)
    {
        float maxHealth = 100;

        if (health + healValue <= maxHealth)
        {
            health += healValue;
        }
        else
        {
            health = maxHealth;
        }
    }

    //Method that updates the animation of the dog.
    public void UpdateDogAnimation(DogBehavior buttonBehavior)
    {
        if (IsDogIdle() == false && buttonBehavior != DogBehavior.Dead)
        {
            //These animation states are triggered when the dog should go back to an idle state.
            if (animation.GetBool("IsRunning"))
            {
                animation.SetBool("IsRunning", false);
            }
            else
            {
                animation.SetTrigger("GetUp");
            }
        }
        else if (buttonBehavior == DogBehavior.Feed)
        {
            animation.SetTrigger("Sit");
        }
        else if (buttonBehavior == DogBehavior.Pet)
        {
            animation.SetTrigger("LayDown");
        }
        else if (buttonBehavior == DogBehavior.Play)
        {
            animation.SetBool("IsRunning", true);
        }
        else if (buttonBehavior == DogBehavior.Dead)
        {
            animation.SetTrigger("Die");
        }
    }

    //This method rotates the dog so he can run and fetch when the Play state is triggered.
    public IEnumerator MoveDog()
    {
        Quaternion startPosition = transform.rotation;
        Quaternion endPosition;
        float lerpDuration = 1;
        float timeElapsed = 0;

        if (animation.GetBool("IsRunning"))
        {
            endPosition = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            endPosition = Quaternion.Euler(0, 180, 0);
        }

        while (transform.rotation != endPosition)
        {
            transform.rotation = Quaternion.Lerp(startPosition, endPosition, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endPosition;
    }

    //This method returns if the dog is in an idle animation state or not.
    public bool IsDogIdle()
    {
        return animation.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }
}