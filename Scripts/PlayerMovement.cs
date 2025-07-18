using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference moveUpButton;
    public InputActionReference moveDownButton;

    public float speed;

    private void OnEnable()
    {
        moveUpButton.action.Enable();
        moveDownButton.action.Enable();
    }

    private void OnDisable()
    {
        moveUpButton.action.Disable();
        moveDownButton.action.Disable();
    }


    private void Update()
    {
        if (moveUpButton.action.IsPressed())
        {
            gameObject.transform.position += new Vector3(0f, 1f, 0f) * Time.deltaTime * speed;
        }
        
        if (moveDownButton.action.IsPressed())
        {
            gameObject.transform.position += new Vector3(0f, -1f, 0f) * Time.deltaTime * speed;
        }
    }
}
