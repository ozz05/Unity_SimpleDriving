using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 SteerValue {get; private set;}
    private Controls controls;

    private void Start ()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy() {
        controls.Player.Disable();
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        SteerValue = context.ReadValue<Vector2>();
    }
}
