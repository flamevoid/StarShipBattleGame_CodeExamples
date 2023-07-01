using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProperty : ActorProperty
{
    public int enemyScore, money;

    [SerializeField] private ScoreDisplay scoreCounter;
    [SerializeField] private StateDisplayUI stateDisplay;

    [SerializeField] private DestroyByPosition destroyerByPosition;

    private void Start()
    {
        InitProperties();

        stateDisplay.InitStateProperties(GetMaxHealth());
    }

    private bool IsFullHealth()
    {
        if (currentHealth == GetMaxHealth())
            return true;
        else
            return false;
    }

    override internal void NullHealthValue()
    {
        gameObject.SetActive(false);

        scoreCounter.CountScore(enemyScore);
        currentHealth = health;
    }

    private void Update()
    {
        switch (IsDeath())
        {
            case true:
                NullHealthValue();
                break;
            case false:
                if (destroyerByPosition.IsGettedDestroyPosition())
                    gameObject.SetActive(false);
                break;
        }

        stateDisplay.ShowDisplay(true != IsFullHealth());
        stateDisplay.ChangeHealthStateUI(currentHealth);
    }
}
