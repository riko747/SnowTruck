using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    private bool rotationClamped = false;

    private void Update()
    {
        if (transform.eulerAngles.y < 180 && transform.eulerAngles.y > 360)
        {
            rotationClamped = true;
        }
        else
        {
            rotationClamped = false;
        }
        Debug.Log("ROTATION: " + transform.eulerAngles.y);
        MoveTruck();
    }

    private void MoveTruck()
    {
        transform.position += transform.forward * Time.deltaTime * 10;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
            {
                Vector3 rotationVector = new Vector3(0, Screen.width / 2 - touch.position.x, 0);
                if (!rotationClamped)
                {
                    transform.Rotate(rotationVector * -1 / 4 * Time.deltaTime);
                }
            }
            if (touch.position.x < Screen.width / 2)
            {
                Vector3 rotationVector = new Vector3(0, Screen.width / 2 - touch.position.x, 0);
                if (!rotationClamped)
                {
                    transform.Rotate(rotationVector * -1 / 4 * Time.deltaTime);
                }
            }
        }
    }
}
