using UnityEngine;
using System.Collections.Generic;

public class Symbols : MonoBehaviour
{

    public List<Shape> shapes;

    public static int SHAPES;

    public Shape Generate(List<Vector3> points) {
        SHAPES++;

        Shape template = shapes.Find(s => s.sides == points.Count);
        if (template == null) {
            throw new System.Exception("Cound not find shape for point count " + points.Count);
        }
        Shape instantiated = Instantiate(template);
        instantiated.transform.name = "Shape " + SHAPES;
        instantiated.Init(points);

        return instantiated;
    }

}
