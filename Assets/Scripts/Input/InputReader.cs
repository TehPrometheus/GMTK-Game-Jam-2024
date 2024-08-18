using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputReader : MonoBehaviour
{
    // NOTE: Make sure to set the Plyaer input component to C# events
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction dashAction;
    InputAction growAction;
    InputAction shrinkAction;
    InputAction InfoScreenAction;


    public Vector2 Move => moveAction.ReadValue<Vector2>();
    public float Dash => dashAction.ReadValue<float>();
    public float Grow => growAction.ReadValue<float>();
    public float Shrink => shrinkAction.ReadValue<float>();
    public float InfoScreen => InfoScreenAction.ReadValue<float>();

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        dashAction = playerInput.actions["Dash"];
        growAction = playerInput.actions["Grow"];
        shrinkAction = playerInput.actions["Shrink"];
        InfoScreenAction = playerInput.actions["InfoScreen"];

    }


}
