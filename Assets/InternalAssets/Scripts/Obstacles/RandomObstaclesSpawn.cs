using UnityEngine;

/// <summary>
/// Spawn cars-obstacles logic
/// </summary>
public class RandomObstaclesSpawn : MonoBehaviour
{
    private const byte ObstaclesNumber = 40;
    private const int CarMinXPostition = 126;
    private const int CarMaxXPosition = -180;
    private const int CarMinZPosition = -20;
    private const int CarMaxZPosition = 20;

    private void Start()
    {
        InstantiateCarsRandomly();
    }

    private static void InstantiateCarsRandomly()
    {
        GameObject carObstaclePrefab = Resources.Load<GameObject>("Prefabs/CarObstacle");
        Transform carObstaclesParentTransform = GameObject.Find("Obstacles").transform;

        for (int i = 0; i <= ObstaclesNumber; i++)
        {
            SpawnCarRandomly(carObstaclePrefab, carObstaclesParentTransform);
        }
    }

    private static void SpawnCarRandomly(GameObject carObstaclePrefab, Transform carObstaclesParentTransform)
    {
        int xCarPosition = Random.Range(CarMinXPostition, CarMaxXPosition);
        int yCarPosition = 0;
        int zCarPosition = Random.Range(CarMinZPosition, CarMaxZPosition);
        Quaternion CarRotation = Random.rotation;

        Vector3 obstaclePosition = new Vector3(xCarPosition, yCarPosition, zCarPosition);
        GameObject carInstance = Instantiate(carObstaclePrefab, obstaclePosition, CarRotation, carObstaclesParentTransform);
    }
}
