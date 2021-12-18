using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public delegate void IsDead();
    public static event IsDead isDeadEvent;

    int health = 5; //Hits

    public void OnDamageTaken(int damage)
    {
        health -= damage;

        if (damage <= 0)
            isDeadEvent?.Invoke();
    }
}
