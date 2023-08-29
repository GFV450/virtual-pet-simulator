using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public DogData dogData;

    void Update()
    {
        if(dogData.health > 0)
        {
            ModifyHealthBar();
        }
    }

    //Method that updates the health bar based on the remaining amount of HP.
    public void ModifyHealthBar()
    {
        Image health = gameObject.transform.Find("Health").GetComponent<Image>();
        int fillDivide = 100;

        health.fillAmount = dogData.health / fillDivide;
    }
}
