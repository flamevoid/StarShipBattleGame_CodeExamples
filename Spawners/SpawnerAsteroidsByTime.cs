using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "Спавнер" для астероидов ко времени. Работает по принципу Object Pull.
public class SpawnerAsteroidsByTime : Level
{
    [SerializeField] private float[] timeFromFirstFrame;
    public GameObject[] arrayAsteroids;

    private int i = 0;

    private void PushAsteroidByList(int asteroidNumber)
    {
        GameObject asteroid = arrayAsteroids[asteroidNumber];
        EnemyProperty asteroidData = asteroid.GetComponent<EnemyProperty>();

        asteroid.SetActive(true);
        asteroid.transform.position = transform.position;
        asteroid.transform.rotation = transform.rotation;

        Rigidbody rbAsteroid = asteroid.GetComponent<Rigidbody>();
        // Debug.Log(asteroidData.currentSpeed + speedForAddEnemy);
        rbAsteroid.velocity = transform.TransformDirection(Vector3.back * (asteroidData.currentSpeed + speedForAddEnemy));
    }

    private void SpawnAsteroidByList(int timeCallOfAsteroid, int maxAsteroid)
    {
        int isGetLimit = timeCallOfAsteroid % maxAsteroid;

        PushAsteroidByList(isGetLimit);
    }

    private void Update()
    {
        if (i == timeFromFirstFrame.Length) {}
        else if (timeFromFirstFrame[i] == (int)Time.timeSinceLevelLoad)
        {
            SpawnAsteroidByList(i, arrayAsteroids.Length);
            i++;
        }
    }
}
