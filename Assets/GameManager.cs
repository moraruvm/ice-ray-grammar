using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Generator> generators;

    private void Awake()
    {
        //Random.InitState(-1610706745);
        Debug.Log("Seed " + Random.seed);

        generators.ForEach(generator => generator.Generate());
    }

}
