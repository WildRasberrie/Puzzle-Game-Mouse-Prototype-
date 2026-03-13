using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class MouseInput : MonoBehaviour
{
    InputSystem_Actions InputActions;
    GameObject piece;
    GameObject currentPiece;
    bool pieceHeld;
    float timePassed = 0f;
    MoveableObjects moveableObjects = null;


    void Awake()
    {
        //cursor is invisble
        Cursor.visible = false;
        //enable input system
        InputActions = new InputSystem_Actions();
        InputActions.Player.Enable();
    }

    void OnDisable()
    {
        InputActions.Player.Disable();
    }

    void Update()
    {
          //get the mouse position
        Vector2 mousePOS = Mouse.current.position.ReadValue();

      // Convert mouse coordinates to world coordinates
        Vector3 worldPosition = ScreenToWorld(mousePOS);
        transform.position = worldPosition;

        // set position of object to mouse position
        MouseBoudaries();

        var leftClick = Mouse.current.leftButton.isPressed;
        var rightClick = Mouse.current.rightButton.wasPressedThisFrame;

        if (leftClick && piece != null && !pieceHeld){
            pieceHeld = true;
            print("Piece held: " +pieceHeld + " | Current piece: " + currentPiece.name);
        }else if (!leftClick && pieceHeld){
            pieceHeld = false;
            piece = null;
        }
        if (rightClick && !leftClick) StartCoroutine(Rotate90(currentPiece));

        if (pieceHeld) MovePiece(piece);
    }

    Vector3 ScreenToWorld(Vector2 screenPosition){
        // Convert screen position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));
        worldPosition.z = 0; // Set z to 0 for 2D games
        return worldPosition;
    }

    void MouseBoudaries(){
        if (transform.position.x < -13)
        {
            transform.position = new Vector3(-13, transform.position.y, transform.position.z);
        } else if (transform.position.x > 13){
            transform.position = new Vector3(13, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -7){
            transform.position = new Vector3(transform.position.x, -7, transform.position.z);
        } else if (transform.position.y > 7){
            transform.position = new Vector3(transform.position.x, 7, transform.position.z);
        }
        
    }
            
    void OnTriggerEnter2D(Collider2D col){     
        if (col.gameObject.CompareTag("Moveable")){
            if (piece == null) piece = col.gameObject;
            currentPiece = col.gameObject;
        }

    }

    void MovePiece(GameObject piece){
        //move piece to mouse position
        piece.transform.position = transform.position;
    }
    
    IEnumerator Rotate90(GameObject gameObj){
        float duration = 1f; 
        Quaternion startRotation = gameObj.transform.rotation;
        var targetRotation =startRotation * Quaternion.Euler(0, 0, 90);
        while (timePassed < duration){
            gameObj.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timePassed / duration);
            timePassed += Time.deltaTime;
            yield return null;
        }
        gameObj.transform.rotation = targetRotation;
        timePassed = 0f;
    }

}
