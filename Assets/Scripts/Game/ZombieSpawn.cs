using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZombieSpawn : MonoBehaviour
{
    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    [Header("Variables to spawn")]
    [SerializeField] private GameObject target = null;
    [SerializeField] private GameObject cemetery;

    [Header("Zombie Related")]
    [SerializeField] private GameObject[] zombiePrefab;
    [SerializeField] private Transform spawnPoints;
    [SerializeField] private float timeRespawn;
    [SerializeField] private int maxZombies;
    [SerializeField] private int maxZombieHP;
    [SerializeField] private Transform zombieListTransform;

    private Transform[] spawnArea;
    private List<GameObject> zombieList = new List<GameObject>();

    private void Awake()
    {
        spawnArea = GetComponentsInChildren<Transform>();

        CreateSpawnZone();

        for (int i = 0; i < maxZombies; i++)
        {

            GameObject go = Instantiate(zombiePrefab[Random.Range(0,zombiePrefab.Length)], zombieListTransform);
            Zombie z = go.GetComponent<Zombie>();
            go.name = "Zombie " + Zombie.GetZombieCount();

            ResetZombie(z);

            z.OnZombieDeath += PrepareZombieRespawn;
            zombieList.Add(go);
        }
    }
    private void PrepareZombieRespawn(Zombie z)
    {
        StartCoroutine(SetUpZombie(z));
    }
    IEnumerator SetUpZombie(Zombie z)
    {
        //Debug.Log("Respawneando "+z.name);

        float time = 0.0f;

        while (time < timeRespawn)
        {
            time += Time.deltaTime;
            //Debug.Log(time);
            yield return null;
        }

        ResetZombie(z);
        //Debug.Log("Respawnie al zombie " + z.name);
    }

    private void ResetZombie(Zombie z)
    {
        z.transform.position = GetRandomPosition();
        z.SetCementery(cemetery);
        z.SetActiveState(true);
        z.health = maxZombieHP;
        z.ResetAnimatorVariables();
        z.SetTarget(target);
    }

    void Update()
    {
        /*
        toSpawn += Time.deltaTime;

        if(toSpawn >= timeRespawn && zombieCounter < maxZombies) 
        { 
            toSpawn = 0;

            SpawnZombie();
        }
        foreach (var zomby in zombieList)
        {
            //zomby.SetTarget(target);
        }
        */
    }

    private void CreateSpawnZone()
    {
        maxX = spawnArea.Max(spawn => spawn.position.x);
        minX = spawnArea.Min(spawn => spawn.position.x);
        maxY = spawnArea.Max(spawn => spawn.position.y);
        minY = spawnArea.Min(spawn => spawn.position.y);
    }

    private Vector2 GetRandomPosition() => new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

    public void SetZombieTarget(GameObject target)
    {
        this.target = target;
    }
}
