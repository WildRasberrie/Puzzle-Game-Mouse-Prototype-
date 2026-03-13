using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;

public class MoveableObjects : MonoBehaviour
{
    [Header ("Moveable Objects")]
    Vector2 mouseInput;
    bool rightClick, leftClick;

    InputSystem_Actions InputActions;

     void Awake()
    {
        InputActions = new InputSystem_Actions();
        InputActions.Player.Enable();
    }
    void OnDisable(){
        InputActions.Player.Disable();
    }

    void Update()
    {
        leftClick = Mouse.current.leftButton.isPressed;
        rightClick = Mouse.current.rightButton.isPressed;
    }

    
}