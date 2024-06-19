using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class VRInteractionButton : MonoBehaviour, IRayItem
{
    [SerializeField]
    private UnityEvent onButtonPressed;
    private bool isHover;
    private void Start()
    {
        isHover = false;
    }
    private void Update()
    {
        if (isHover && Input.GetMouseButtonDown(0))
        {
            onButtonPressed.Invoke();
        }
        if (isHover && Gamepad.current != null && Gamepad.current.selectButton.wasPressedThisFrame)
        {
            onButtonPressed.Invoke();
        }
    }
    public void OnPointerEnter()
    {
        transform.localScale = Vector3.one * 2f;
        isHover = true;
    }

    public void OnPointerExit()
    {
        transform.localScale = Vector3.one;
        isHover = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPointerEnter();
    }
    private void OnTriggerExit(Collider other)
    {
        OnPointerExit();
    }
}

