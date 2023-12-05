using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference flyActionReference;
    [SerializeField] private InputActionReference MovementActionReference;
    [SerializeField] private float speed = 16.6667f;

    private Rigidbody _body;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        flyActionReference.action.performed += MoveVertically;
        MovementActionReference.action.performed += MoveHorizontally;
    }

    private void MoveVertically(InputAction.CallbackContext context)
    {

        float flyValue = context.ReadValue<Vector2>().y;

        Vector3 movement = Vector3.up * speed * flyValue;

        _body.velocity = movement;
    }

    private void MoveHorizontally(InputAction.CallbackContext context)
    {
        float sideways = context.ReadValue<Vector2>().x;
        float forward = context.ReadValue<Vector2>().y;

        Vector3 moveforward = Vector3.forward * speed * forward;
        Vector3 movesideways = Vector3.right * speed * sideways;
        Vector3 movement = moveforward + movesideways;

        _body.velocity = movement;
    }

}