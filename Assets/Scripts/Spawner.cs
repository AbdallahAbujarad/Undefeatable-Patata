using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    void Start()
    {
        InvokeRepeating(nameof(Spawn),2f,2f);
    }
    void Spawn()
    {
        Instantiate(objectToSpawn,transform.position,Quaternion.identity);
    }
}
