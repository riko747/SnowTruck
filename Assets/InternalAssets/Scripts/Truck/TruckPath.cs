using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckPath : MonoBehaviour
{
    public Vector3[] truckPosition = new Vector3[5];

    IEnumerator PathCheckCoroutine()
    {

        yield return new WaitForSeconds(2);
    }
}
