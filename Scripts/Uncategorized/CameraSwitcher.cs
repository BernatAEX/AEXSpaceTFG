using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{

    [SerializeField] private InputActionReference Switcher;

    public Camera firstPersonCamera;
    public Camera overheadCamera;
    bool FPCameraActive = true;

    private void Start()
    {
        // Ensure one of the cameras is active at the start
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
        Switcher.action.performed += SwitchCamera;
    }



    // Call this function to disable FPS camera,
    // and enable overhead camera.
    private void SwitchCamera(InputAction.CallbackContext context)
    {
        FPCameraActive = !FPCameraActive;

        // Activate or deactivate the cameras based on the current state
        firstPersonCamera.enabled = FPCameraActive;
        overheadCamera.enabled = !FPCameraActive;
    }
}
