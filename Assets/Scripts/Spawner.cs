using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;
    private GameObject[] bag = new GameObject[7];
    private GameObject tempGO;
    private int index = 0;

    private void Start()
    {
        generateBag();
    }
    
    public void spawnNext()
    {
        // Random Index
        // int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
       Instantiate(groups[index], transform.position, Quaternion.identity);
        
        
        index++;
        if (index > groups.Length - 1)
        {
            index = 0;
            generateBag();
        }
    }

    private void generateBag()
    {
        for (int i = 0; i < groups.Length - 1; i++)
        {
            int rng = Random.Range(0, groups.Length);
            tempGO = groups[rng];
            groups[rng] = groups[i];
            groups[i] = tempGO;
        }
    }
}
