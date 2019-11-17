using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape: MonoBehaviour
{

    public int sides;
    public List<Vector3> points;
    public List<AdjacentRule> rules;
    public LineRenderer renderer;

    public void Init(List<Vector3> points) {
        this.sides = points.Count;
        this.points = points;
        RefreshView();
    }

    public List<Shape> Split() {
        Rule rule = rules[Random.Range(0, rules.Count)];
        List<Shape> children = rule.split(points);
        children.ForEach(c =>
        {
            c.transform.parent = transform.parent;
            c.transform.localPosition = Vector3.zero;
        });

        return children;
    }

    private void OnValidate()
    {
        RefreshView();
    }

    private void RefreshView() {
        this.renderer.positionCount = points.Count;
        this.renderer.SetPositions(points.ToArray());
    }

}

