using UnityEngine;

public class BehaviorButtons : MonoBehaviour
{
    public DogData dogData;
    private bool isButtonActive;
    private float buttonDelay = 3;

    void Start()
    {
        //Activates buttons at every specified amount of time.
        Invoke("ActivateButton", buttonDelay);

        OnActionClickSubscribe();
    }

    void Update()
    {
        if(dogData.isDogAlive == false)
        {
            DeactivateAllButtons();
        }
    }

    //Method that gets the number of children of the Action Buttons game object, and uses their index to activate a button.
    private void ActivateButton()
    {
        if (isButtonActive == false && dogData.IsDogIdle())
        {
            int childIndex = Random.Range(0, gameObject.transform.childCount);

            GameObject childToActivate = gameObject.transform.GetChild(childIndex).gameObject;
            DogBehavior buttonBehavior = childToActivate.GetComponent<Enums>().behaviorEnum;

            childToActivate.SetActive(true);
            isButtonActive = true;

            //Updates dog animation based on Behavior activated to sit, lay down, or run.
            AnimationUpdateCall(buttonBehavior);
        }
    }

    //Method that deactivates a button after being clicked.
    private void DeactivateButton(DogBehavior buttonBehavior)
    {
        //Identifies button that's active based on behavior and deactivates it.
        foreach (Transform child in transform)
        {
            if(child.gameObject.GetComponent<Enums>().behaviorEnum == buttonBehavior)
            {
                child.gameObject.SetActive(false);
            }
        }

        isButtonActive = false;

        //Updates dog animation back to idle state.
        AnimationUpdateCall(buttonBehavior);

        Invoke("ActivateButton", buttonDelay);
    }

    //Method that iterates through all buttons and deactivates them when the game is over.
    private void DeactivateAllButtons()
    {
        foreach (Transform child in transform)
        {
            OnActionClickUnsubscribe();
            child.gameObject.SetActive(false);
        }
    }

    //Method that deals with all animations that need to be triggered for the dog.
    private void AnimationUpdateCall(DogBehavior buttonBehavior)
    {
        dogData.UpdateDogAnimation(buttonBehavior);
        StartCoroutine(dogData.MoveDog());
    }

    //Method that calls our custom event system to perform an action and subscribe to an event.
    private void OnActionClickSubscribe()
    {
        GameManager.current.OnActionButtonClick += DeactivateButton;
    }

    //Method that calls our custom event system to unsubscribe to an event.
    private void OnActionClickUnsubscribe()
    {
        GameManager.current.OnActionButtonClick -= DeactivateButton;
    }
}