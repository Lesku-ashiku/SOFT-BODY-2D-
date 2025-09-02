using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SoftBodyBlob2D : MonoBehaviour
{
    public Transform[] points; // Blob points (assign children)
    private Mesh mesh;

    [Header("Rendering")]
    public Color blobColor = Color.green;
    public string sortingLayer = "Foreground";
    public int sortingOrder = 10;

    [Header("Shape Smoothing")]
    public float tangentOffset = 0.2f; // How far to push along tangent
    //the tangent is a bit off set so it doesnt give a levitating effect

    void Start()
    {
        mesh = new Mesh();
        mesh.name = "SoftBodyBlob2D Mesh";
        GetComponent<MeshFilter>().mesh = mesh;

        // Mesh Renderer setup
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Sprites/Default"));
        mr.material.color = blobColor;
        mr.sortingLayerName = sortingLayer;
        mr.sortingOrder = sortingOrder;

        StartCoroutine(FillPointsNextFrame());
    }

    void LateUpdate()
    {
        UpdateMesh();
    }

    void UpdateMesh()
    {
        if (points == null || points.Length < 3) return;
        //a polygon must have more than 3 points if not the game will crash

        Vector3[] vertices = new Vector3[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            // Current, previous, and next points in local space
            Vector3 prev = points[(i - 1 + points.Length) % points.Length].localPosition;
            Vector3 curr = points[i].localPosition;
            Vector3 next = points[(i + 1) % points.Length].localPosition;

            // Tangent direction = normalized vector from prev to next
            Vector3 tangent = (next - prev).normalized;

            // Perpendicolare to tangent in 2D, Z up
            Vector3 normal = new Vector3(-tangent.y, tangent.x, 0f);

            // Push vertex along normal for smoothing
            vertices[i] = curr + normal * tangentOffset;
        }

        int[] triangles = TriangulateConvex(vertices);

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
    }

    int[] TriangulateConvex(Vector3[] verts)
    {
        int[] tris = new int[(verts.Length - 2) * 3];
        for (int i = 0; i < verts.Length - 2; i++)
        {
            tris[i * 3] = 0;
            tris[i * 3 + 1] = i + 1;
            tris[i * 3 + 2] = i + 2;
        }
        return tris;
    }

    private IEnumerator FillPointsNextFrame()
    {
        yield return null; // Wait one frame for children to initialize
        points = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            points[i] = transform.GetChild(i);
    }
}
