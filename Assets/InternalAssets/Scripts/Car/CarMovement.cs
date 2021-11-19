using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private int _carSpeed = 12;
    private ICheckTruckLocation _checkTruckLocation;

    Quaternion rotation;

    private void Start()
    {
        _checkTruckLocation = GameObject.Find("Truck").GetComponent<ICheckTruckLocation>();
    }

    private void Update()
    {
        if (_checkTruckLocation.MoveCar == true)
        {
            
            if (transform.position == _checkTruckLocation.NextCarPosition)
            {
                _checkTruckLocation.MoveCar = false;
                rotation = Quaternion.Euler(new Vector3(0, _checkTruckLocation.TruckRotationY, 0));
                transform.rotation = rotation;
            }
            if (Vector3.Distance(transform.position, _checkTruckLocation.NextCarPosition) > 10)
            {
                _carSpeed = 30;
                transform.position = Vector3.MoveTowards(transform.position, _checkTruckLocation.NextCarPosition, _carSpeed * Time.deltaTime);
                rotation = Quaternion.Euler(new Vector3(0, _checkTruckLocation.TruckRotationY, 0));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 2 * Time.deltaTime);
            }
        }
    }
}
