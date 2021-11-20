using UnityEngine;

public class HideElementIfInvisible : MonoBehaviour
{
    GameObject truck;

    private void Start()
    {
        truck = GameObject.Find("Truck");
    }

    private void Update()
    {
        if (transform.position.x > truck.transform.position.x + 35 || transform.position.x < truck.transform.position.x - 35)
        {
            gameObject.SetActive(false);
        }
    }
}
