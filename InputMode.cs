using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputMode : MonoBehaviour
{
    private protected float speedByMove;

    private protected Rigidbody rb; // Rigidbody контролируемого для управления объекта.
    private protected Vector3 moveVector = new Vector3(0, 0, 0);

    virtual protected internal void Init(Rigidbody controlObject, float speedByMove)
    {
        this.rb = controlObject;

        this.speedByMove = speedByMove;
    }

    virtual protected void MovePos(Vector2 addToVector, float speed)
    {
        moveVector.x = addToVector.x;
        moveVector.z = addToVector.y;

        rb.MovePosition(transform.position + moveVector * speed);
    }

    virtual protected void MoveRot(float rotationX, float rotationY, float rotationZ)
    {
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

    virtual protected internal void InputUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            int screenSize = Screen.width;

            if (touch.position[0] < (screenSize / 2))
            {
                MovePos( Vector3.right, speedByMove);
                MoveRot(transform.rotation.x, -180, -19);
            }
            if ((screenSize / 2) < touch.position[0])
            {
                MovePos(Vector3.left, speedByMove);
                MoveRot(transform.rotation.x, -180, 19);
            }
        }
        else
        {
            MoveRot(transform.rotation.x, -180, 0);
        }
    }
}
