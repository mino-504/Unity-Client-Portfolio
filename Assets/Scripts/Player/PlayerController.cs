using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotation))]
public class PlayerController : MonoBehaviour
{
    void Awake()
    {
        // 이 오브젝트(Player)에 필수 컴포넌트들이 있는지 보장
        // RequireComponent 덕분에 자동으로 추가되거나 에디터에서 막아줌
    }
}
