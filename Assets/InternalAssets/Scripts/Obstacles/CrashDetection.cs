using UnityEngine;

/// <summary>
/// Gameover logic
/// </summary>
public class CrashDetection : MonoBehaviour
{
    private const string CrashScreenName = "CrashScreen";
    private const string TruckName = "Truck";

    private IChangeGameState _changeGameState;
    private Transform _guiTransform;
    private GameObject _crashScreenGameobject;

    private void Start()
    {
        _changeGameState = GameObject.Find("GameManager").GetComponent<IChangeGameState>();
        _guiTransform = GameObject.Find("GUI").transform;

        AccessCrashScreen();
    }

    private void AccessCrashScreen()
    {
        foreach (Transform child in _guiTransform)
        {
            if (child.name == CrashScreenName)
            {
                _crashScreenGameobject = child.gameObject;
            }
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.name == TruckName)
        {
            ShowGameoverScreen();
        }
    }

    private void ShowGameoverScreen()
    {
        _crashScreenGameobject.SetActive(true);
        _changeGameState.PauseGame();
    }
}
