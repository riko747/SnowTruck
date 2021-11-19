using UnityEngine;
using System.Collections;

public class TruckMovement : MonoBehaviour
{
    private void Update()
    {
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
                //Debug.Log("Touched right side of a screen" + (Screen.width / 2 - touch.position.x));
                Vector3 rotationVector = new Vector3(0, Screen.width / 2 - touch.position.x, 0);
                transform.Rotate(rotationVector * -1 * Time.deltaTime);
            }
            if (touch.position.x < Screen.width / 2)
            {
                //Debug.Log("Touched left side of a screen" + (Screen.width / 2 - touch.position.x));
                Vector3 rotationVector = new Vector3(0, Screen.width / 2 - touch.position.x, 0);
                transform.Rotate(rotationVector * -1 * Time.deltaTime);
            }
        }
    }
}
