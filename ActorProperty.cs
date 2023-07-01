using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ActorProperty : MonoBehaviour
{
    public float currentSpeed = 0.0f, currentHealth = 100.0f, colliderDamage = 34.0f; // Текущие значения

    protected private float speed, health; // Декларативные значения. Нужны для инициализации, ограничения.

    public float GetMaxHealth()
    {
        return health;
    }

    public float GetMaxSpeed()
    {
        return speed;
    }

    virtual protected private void InitProperties() // Инициализация значений при новой игровой сессии для объекта персонажа.
    {
        speed = currentSpeed;
        health = currentHealth;
    }

    virtual internal void NullHealthValue() // Для значения "0" т.е. смерти поля "currentHealth"
    {
        gameObject.SetActive(false);

        currentHealth = health;
    }

    internal bool IsDeath() // Возвращает состояние персонажа. true = мертв, false = жив.
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
