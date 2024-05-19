using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    Renderer[] colorOfObject = new Renderer[3];
    public GameObject Objects;
    public Transform[] conveyorPoint = new Transform[3];

    bool isWait;
    int[] sid = new int[3];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ; i < sid.Length; i++)
        {
            sid[i] = 0;
        }

        StartCoroutine(SpawnEvery(0, 20f));
        StartCoroutine(SpawnEvery(1, 30f));
        StartCoroutine(SpawnRandom(2));
    }

    void ObjectSpawn(int i)
    {
        GameObject tmp = Instantiate(Objects, conveyorPoint[i].position, Quaternion.identity);
        colorOfObject[i] = tmp.GetComponent<Renderer>();

        if(sid[i] != 0)
        {
            colorOfObject[i].material.color = Color.blue;
        }
        else
        {
            colorOfObject[i].material.color = Color.red;
        }
    }

    IEnumerator Spawner(int i, float time)
    {
        sid[i] = Random.Range(0, 5);

        ObjectSpawn(i);
        yield return new WaitForSeconds(time);
    }

    IEnumerator SpawnerStart(int i, float time)
    {
        sid[i] = Random.Range(0, 5);
        ObjectSpawn(i);
        yield return new WaitForSeconds(time);

    }

    IEnumerator SpawnEvery(int i, float time)
    {
        float elapsedTime = 0f; 
        while (elapsedTime < 180f)
        {
            yield return StartCoroutine(SpawnerStart(i, time));
            elapsedTime += time;
        }
    }

    IEnumerator SpawnRandom(int i)
    {
        float time = Random.Range(45f, 60f);
        float elapsedTime = 0f;
        while (elapsedTime < 180f)
        {
            yield return StartCoroutine(Spawner(i, time));
            elapsedTime += time;
        }
    }
}
