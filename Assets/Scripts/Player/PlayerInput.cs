using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }

    public bool AttackPressed { get; private set; } // 한번 클릭(Down)
    public bool AttackHeld { get; private set; }    // 누르고 있는 중(Hold)

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector2(h, v);

        MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        AttackPressed = Input.GetMouseButtonDown(0);
        AttackHeld = Input.GetMouseButton(0);
    }
}
