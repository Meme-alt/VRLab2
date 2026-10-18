using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimate : MonoBehaviour
{
    public InputActionProperty gripAction;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (gripAction.action != null) gripAction.action.Enable();
    }

    void Update()
    {
        animator.SetFloat("Grip", gripAction.action.ReadValue<float>());
    }
}
