using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private Camera m_camera;
    private Camera Camera => m_camera = m_camera != null ? m_camera : GetComponent<Camera>();

    [SerializeField] private InputActionAsset m_input;
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private float m_rotateSpeed = 10f;

    private Vector3 m_move;
    private Vector2 m_look;

    private void OnEnable()
    {
        InputActionMap player = m_input.FindActionMap("Player");
        InputAction move = player.FindAction("Move");
        InputAction moveVertical = player.FindAction("MoveVertical");
        InputAction look = player.FindAction("Look");

        player.Enable();

        move.performed += OnMove;
        move.canceled += OnMove;
        look.performed += OnLook;
        look.canceled += OnLook;
    }

    private void OnDisable()
    {
        InputActionMap player = m_input.FindActionMap("Player");
        InputAction move = player.FindAction("Move");
        InputAction moveVertical = player.FindAction("MoveVertical");
        InputAction look = player.FindAction("Look");

        move.performed -= OnMove;
        move.canceled -= OnMove;
        look.performed -= OnLook;
        look.canceled -= OnLook;

        player.Disable();
    }

    private void LateUpdate()
    {
        // 카메라 이동
        float delta = m_moveSpeed * Time.deltaTime;
        Vector3 right = Camera.transform.right;
        Vector3 forward = Camera.transform.forward;
        Vector3 up = Camera.transform.up;
        Camera.transform.position += delta * (right * m_move.x + forward * m_move.z + up * m_move.y);

        // 카메라 회전
        float rotateDelta = m_rotateSpeed * Time.deltaTime;
        Vector3 angles = Camera.transform.eulerAngles;
        angles.x -= m_look.y * rotateDelta;
        angles.y += m_look.x * rotateDelta;
        Camera.transform.eulerAngles = angles;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        m_move = context.ReadValue<Vector3>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        m_look = context.ReadValue<Vector2>();
    }
}
