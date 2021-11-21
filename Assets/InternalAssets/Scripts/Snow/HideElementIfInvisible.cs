using UnityEngine;

/// <summary>
/// Deactivation behind the screen snow logic
/// </summary>
public class HideElementIfInvisible : MonoBehaviour
{
    private const int VisibleDistance = 35;
    GameObject truck;

    private void Start()
    {
        truck = GameObject.Find("Truck");
    }

    private void Update()
    {
        DeactivateFarSnowWay();
    }

    private void DeactivateFarSnowWay()
    {
        if (transform.position.x > truck.transform.position.x + VisibleDistance && transform.position.x < truck.transform.position.x - VisibleDistance)
        {
            gameObject.SetActive(false);
        }
    }
}
