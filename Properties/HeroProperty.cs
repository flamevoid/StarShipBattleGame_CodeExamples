using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroProperty : ActorProperty
{
    [SerializeField] private Image healthStateProgressBarUI; // Поле отображения здоровья персонажа-героя
    [SerializeField] private GameObject nullHealthEffect; // Эффект при "health <= null" для поля "health". Поле "health" существует для класса всех персонажей "ActorProperty"

    private void Start()
    {
        InitProperties();
    }

    internal void ApplyHealthDamage(float damage) // Принять урон к текущему персонажу. (См. currenthealth в базовом классе)
    {
        currentHealth -= damage;
    }

    override internal void NullHealthValue()
    {
        gameObject.SetActive(false);

        Instantiate(nullHealthEffect, gameObject.transform.position, gameObject.transform.rotation);
    }

    private void ChangeHealthStateUI(float currentHealthValue)
    {
        healthStateProgressBarUI.fillAmount = currentHealthValue * 0.01f;
    }

    private void Update()
    {
        ChangeHealthStateUI(currentHealth);
    }
}
