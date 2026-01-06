using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }

    public bool AttackPressed { get; private set; }
    public bool AttackHeld { get; private set; }
    public bool SecondaryAttackHeld { get; private set; }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector2(h, v);

        // ★ 핵심 수정 부분
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        MouseWorldPosition = mouseWorld;

        AttackPressed = Input.GetMouseButtonDown(0);
        AttackHeld = Input.GetMouseButton(0);
        SecondaryAttackHeld = Input.GetMouseButton(1);
    }
}
