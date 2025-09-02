
using UnityEngine;
public class CirclePatternSpawner : MonoBehaviour
{
    [Header("Pattern Settings")]
    public int numberOfPoints = 10;   //number of points u want to create
    public float radius = 5f;         // distance from the empty object
    public GameObject circlePrefab;   // The prefab to spawn
private GameObject[] spawnedPoints;
    public float dumping;
    public float Stiffness;
    PlayerMovement playerMovement;
    
    private void Start()
    {
        GeneratePattern();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

   
    public void GeneratePattern()
    {


        spawnedPoints = new GameObject[numberOfPoints];
        if (circlePrefab == null || numberOfPoints <= 0) return;

        // Create evenly spaced points around a circle
        float angleStep = 360f / numberOfPoints;
        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad; // Convert to radianti
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                0f // Z is 0 for 2D
            );

            GameObject point =Instantiate(circlePrefab, transform.position + position, Quaternion.identity, transform);
            spawnedPoints[i] = point;
        }
        setupPoint();
    }
    public void setupPoint()
    {
        //in this function we assign hte circle collider and the varius spring joints to every single point
        foreach (Transform child in transform)
        {
            GameObject childpoint = child.gameObject;
            childpoint.AddComponent<CircleCollider2D>();



        }


        for (int i = 0; i < spawnedPoints.Length; i++)
        {
            for (int j = i + 1; j < spawnedPoints.Length; j++)
            {
                SpringJoint2D spring = spawnedPoints[i].AddComponent<SpringJoint2D>();
                spring.connectedBody = spawnedPoints[j].GetComponent<Rigidbody2D>();
                spring.dampingRatio = dumping;
                spring.frequency = Stiffness; 
                //this cicle makes every point connected to each other
            }
            //this joint is connected to their father(the empty object)
            //and every point has it
            SpringJoint2D springCenter = spawnedPoints[i].AddComponent<SpringJoint2D>();
            springCenter.connectedBody = gameObject.GetComponent<Rigidbody2D>();
            springCenter.dampingRatio = dumping;
            springCenter.frequency = Stiffness; 
        }
           
            
    }

}