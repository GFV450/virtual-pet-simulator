using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DogData : ScriptableObject
{
    [Range (0, 100)] public float health;

    [Serializable]
    public struct Behavior
    {
        public DogBehavior behavior;
        public float behaviorHealValue;
    }

    public Behavior[] behaviorList;

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
}