using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    private void Update()
    {
        MoveTruck();
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Clamp(transform.eulerAngles.y, 175, 355), transform.eulerAngles.z);
    }

    private void MoveTruck()
    {
        transform.position += transform.forward * Time.deltaTime * 5;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
            {
                Vector3 rotationVector = new Vector3(0, Screen.width / 2 - touch.position.x, 0);
                transform.Rotate(rotationVector * -1 / 4 * Time.deltaTime);
            }
            if (touch.position.x < Screen.width / 2)
            {
                Vector3 rotationVector = new Vector3(0, Screen.width / 2 - touch.position.x, 0);
                transform.Rotate(rotationVector * -1 / 4 * Time.deltaTime);
            }
        }
    }
}
