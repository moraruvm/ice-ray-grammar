using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Shape parent;
    public int generations;
    public int genCount;

    private List<Shape> currentGeneration = new List<Shape>();

    void Awake()
    {
        currentGeneration.Add(parent);
    }

    // Start is called before the first frame update
    public void Generate()
    {
        while (genCount < generations)
        {
            BuildNextGeneration();
        }
    }

    public void BuildNextGeneration() {
        genCount++;
        List<Shape> newGeneration = new List<Shape>();
        foreach (Shape s in currentGeneration)
        {
            newGeneration.AddRange(s.Split());
        }

        DropGeneration(currentGeneration);
        currentGeneration = newGeneration;
    }

    private void DropGeneration(List<Shape> shapes)
    {
        shapes.ForEach(s => Destroy(s.gameObject));
    }
}
