using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 movDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position+ Vector3.up * playerHeight, 
                                            playerRadius, movDir, moveDistance);
        
        if (!canMove)
        {
            //Cannnot move towards moveDir

            //Attempt only x movement
            Vector3 movDirX = new Vector3(movDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                                            playerRadius, movDirX, moveDistance);
            if (canMove)
            {
                //Can move only on the X
                movDir = movDirX;
            }
            else
            {
                //Cannot move only on the X
                //Attempt only Z movement
                Vector3 movDirZ = new Vector3(0, 0, movDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                                                playerRadius, movDirZ, moveDistance);
                if (canMove)
                {
                    //Can move only on the X
                    movDir = movDirZ;
                }
                else
                {
                   //Cannot move in any direction
                }

            }

        }
        if (canMove)
        {
            transform.position += movDir * moveSpeed * Time.deltaTime;
        }

        isWalking = movDir != Vector3.zero;

        //Rotating
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
        //Debug.Log(inputVector);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

}
