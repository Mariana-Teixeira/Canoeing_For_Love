using UnityEngine;

public class InputManager : Subject
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) NotifyObserver();
    }
}
