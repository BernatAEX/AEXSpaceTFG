using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{

    [SerializeField] private InputActionReference Switcher;

    public Camera firstPersonCamera;
    public Camera overheadCamera;
    public GameObject FPHud;
    public GameObject ThirdPHud;
    [HideInInspector] public bool FPCameraActive = true;
    [HideInInspector] public bool Available = true;

    private void Start()
    {
        // Ensure one of the cameras is active at the start
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
        FPHud.SetActive(true);
        ThirdPHud.SetActive(false);
        Switcher.action.performed += SwitchCamera;
    }



    // Call this function to disable FPS camera,
    // and enable overhead camera.
    private void SwitchCamera(InputAction.CallbackContext context)
    {
        if (Available)
        {
            FPCameraActive = !FPCameraActive;

            // Activate or deactivate the cameras based on the current state
            firstPersonCamera.enabled = FPCameraActive;
            overheadCamera.enabled = !FPCameraActive;
            FPHud.SetActive(firstPersonCamera.enabled);
            ThirdPHud.SetActive(overheadCamera.enabled);
        }
    }
    public void ExternalSwitch()
    {
        if (Available)
        {
            FPCameraActive = !FPCameraActive;

            // Activate or deactivate the cameras based on the current state
            firstPersonCamera.enabled = FPCameraActive;
            overheadCamera.enabled = !FPCameraActive;
            FPHud.SetActive(firstPersonCamera.enabled);
            ThirdPHud.SetActive(overheadCamera.enabled);
        }
    }
}
