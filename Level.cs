using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

// Класс наследуемый для большинства объектов являющихся частью уровня.
public class Level : MonoBehaviour
{
    [SerializeField] protected internal int levelNumber;
    [SerializeField] private bool isNeedLoadGameData = false; // Загружать данные сохранения на уровень?
    
    protected internal static int score; // Текущие очки, заработанные на уровне. Они же отображаются через UI.
    protected bool isCompleted = false; // Уровень уже был пройден до запуска?
    protected float isTime = 1.0f; // Стартовый timeScale уровня.

    [SerializeField] private float[] timeScaleWave; // timeScale для следующей волны.
    [SerializeField] private float[] timeStartWaveOnLevel; // время начала следующей волны.
    [SerializeField] private float[] speedForAddEnemyOfWave; // Дополнительная скорость по началу следующей волны.
    internal static float speedForAddEnemy = 0.0f; // Дополнительная скорость за каждую волну

    private int waveCount = 0; // Счетчик волн


    protected static SaverYG currentSaver = new();

    private void OnEnable() => YandexGame.GetDataEvent += currentSaver.GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= currentSaver.GetData;

    /*
    private Loader loadManager = new();
    protected static Saver saveManager = new();
    protected static GameData gameDataOfGame = new();
    */

    /*
    private void LoadGameData()
    {
        if (isNeedLoadGameData)
        {
            gameDataOfGame = loadManager.LoadFromFile();
        }
    }
    */

    public void SetTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }

    private void Start()
    {
        // LoadGameData();

        if (YandexGame.SDKEnabled == true)
        {
            currentSaver.GetData();
        }

        score = 0;
        speedForAddEnemy = 0.0f;

        SetTimeScale(isTime);
    }

    virtual protected bool IsWinLevel(int timeForWin) // Не уничтожен корабль к текущему времени?
    {
        if ((int)Time.timeSinceLevelLoad == timeForWin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    virtual protected void LevelProgressDataChange() // Изменение игрового прогресса.
    {
        isCompleted = YandexGame.savesData.IsNewLevel(currentSaver.GameData, levelNumber);

        currentSaver.GameData.lastCompletedLevel = levelNumber + 1;
    }

    virtual protected void RecordProgressDataOfLevelChange(int newScoreRecord)
    {
        currentSaver.GameData.scoreLevelRecords[levelNumber] = newScoreRecord;
    }

    private void FixedUpdate()
    {
        if (waveCount < timeStartWaveOnLevel.Length && timeStartWaveOnLevel[waveCount] == Time.timeSinceLevelLoad)
        {
            SetTimeScale(timeScaleWave[waveCount]);
            speedForAddEnemy = speedForAddEnemyOfWave[waveCount];

            waveCount++;
        }
    }
}
