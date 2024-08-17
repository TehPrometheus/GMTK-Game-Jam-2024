using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputReader : MonoBehaviour
{
    // NOTE: Make sure to set the Plyaer input component to C# events
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction dashAction;


    public Vector2 Move => moveAction.ReadValue<Vector2>();
    public float Dash => dashAction.ReadValue<float>();

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        dashAction = playerInput.actions["Dash"];

    }


}
