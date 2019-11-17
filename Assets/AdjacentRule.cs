using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AdjacentRule : MonoBehaviour, Rule
{
    public Symbols symbols;
    public int sideGap;

    public List<Shape> split(List<Vector3> points)
    {
        if (sideGap >= points.Count)
            throw new System.Exception("Side gap " + sideGap + " cannot be larger than the number of sides " + points.Count);

        var splitShapePoints = splitShape(points);
        return splitShapePoints.Select(symbols.Generate).ToList();
    }

    private List<List<Vector3>> splitShape(List<Vector3> points)
    {
        int side1 = Random.Range(0, points.Count);
        int side2 = (side1 + sideGap) % points.Count;

        var left = Mathf.Min(side1, side2);
        var right = Mathf.Max(side1, side2);
        Debug.Log("Side gap " + sideGap);
        Debug.Log("left " + left);
        Debug.Log("right " + right);

        List<Vector3> slicePoints = new List<Vector3> {
            getPointOnSide(left, points),
            getPointOnSide(right, points)
        };

        return createSplit(left, right, slicePoints, points);
    }

    private List<List<Vector3>> createSplit(int left, int right, List<Vector3> slicePoints, List<Vector3> originalShape) {
        List<Vector3> shape2 = originalShape.GetRange(0, left);
        shape2.AddRange(slicePoints);
        shape2.AddRange(originalShape.GetRange(right, originalShape.Count - right));

        slicePoints.Reverse();
        List<Vector3> shape1 = originalShape.GetRange(left, right - left);
        shape1.AddRange(slicePoints);

        return new List<List<Vector3>> {
            shape1, shape2
        };
    }

    private Vector3 getPointOnSide(int side, List<Vector3> points) {
        var sideBorders = getSide(side, points);
        return sliceRandom(sideBorders[0], sideBorders[1]);
    }

    private Vector3[] getSide(int sideIdx, List<Vector3> points) {
        Vector3[] sidePoints = new Vector3[2];
        int sidePointIdx = sideIdx > 0 ? sideIdx - 1 : points.Count - 1;
        Debug.Log("Side points " + sidePointIdx + " " + sideIdx);

        sidePoints[0] = points[sidePointIdx];
        sidePoints[1] = points[sideIdx];
        return sidePoints;
    }

    private Vector3 sliceRandom(Vector3 start, Vector3 end) {
        // float val = Random.Range(.3f, .7f);
        float val = Random.Range(.3f, .7f);
        return Vector3.Lerp(start, end, val);
    }

}

public interface Rule {
    List<Shape> split(List<Vector3> points);
}