using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "Спавнер" случайных астероидов. Работает по принципу Object Pull.
public class SpawnerAsteroidsByRandom : MonoBehaviour
{
    [SerializeField] private GameObject[] arrayAsteroids;
    [SerializeField] float speedAsteroid;

    [SerializeField] private float probability = 0.5f;
    [SerializeField] private float timeOutBetweenPush = 3.0f;

    private int pullNumberCallOfAsteroid = 0;

    private void PushAsteroidByList(int asteroidNumber)
    {
        // Debug.Log("Called PushAsteroid");

        GameObject asteroid = arrayAsteroids[asteroidNumber];
        EnemyProperty asteroidData = asteroid.GetComponent<EnemyProperty>();

        asteroid.SetActive(true);
        asteroid.transform.position = transform.position;
        asteroid.transform.rotation = transform.rotation;

        Rigidbody rbAsteroid = asteroid.GetComponent<Rigidbody>();
        rbAsteroid.velocity = transform.TransformDirection(Vector3.back * asteroidData.currentSpeed);
    }

    private void SpawnAsteroidByList(int maxAsteroid)
    {
        if (pullNumberCallOfAsteroid == maxAsteroid)
        {
            pullNumberCallOfAsteroid = 0;
        }
        else
        {
            int isGetLimit = pullNumberCallOfAsteroid % maxAsteroid;

            PushAsteroidByList(isGetLimit);
        }
    }

    private bool IsRandomSpawn(float probability)
    {
        bool randSpawnTime = UnityEngine.Random.value <= probability;

        return randSpawnTime;
    }

    private bool IsCompletedTimeout(float currentTime, float timeOut)
    {
        if (currentTime % timeOut < Time.deltaTime)
            return true;
        else
            return false;
    }

    private void Update()
    {
        switch (IsCompletedTimeout(Time.timeSinceLevelLoad, timeOutBetweenPush) && IsRandomSpawn(probability))
        {
            case true:
                SpawnAsteroidByList(arrayAsteroids.Length);
                pullNumberCallOfAsteroid++;

                // Debug.Log(Time.timeSinceLevelLoad);
                // Debug.Log("Case true was called");

                break;
            case false:
                break;
        }
    }
}
