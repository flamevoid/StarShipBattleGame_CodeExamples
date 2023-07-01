using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс режима управления для персонажа космического корабля.
public class InputFromAxisPoint : InputMode
{
    [SerializeField] private InputDisplay inputDisplay; // Объект джостика, что появляется на экране при касании

    internal Vector2 lastDirection; // Последнее направление заданное движущим касанием
    internal bool isInputManageable = true; // Доступен для управления?


    private Touch touch; // касание

    private Vector2 positionWithTouch = Vector2.zero; // позиция touch.start
    private Vector2 direction = Vector2.zero; // позиция движения пальца относительно positionWithTouch. От него и просходит движение корабля.

    private float doubleReduction;


    private void MoveRotationByDirection(Vector2 direction, float degree) // Вращает вперед, назад, вправо, влево по значению "degree" в зависимости от направления пальца "direction"
    {
        float directionZ = (System.MathF.Abs(direction.x) / direction.x) * degree;
        float directionX = (System.MathF.Abs(direction.y) / direction.y) * degree;

        MoveRot(directionZ, 0, directionX);
    }

    internal Vector2 GetDirection()
    {
        return direction;
    }

    internal void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    internal void AddDirection(Vector2 additionalDirection)
    {
        direction += additionalDirection;
    }

    internal void SetNullDirection()
    {
        lastDirection = direction;
        SetDirection(Vector2.zero);
    }

    internal void ResetPhysicVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    internal void ResetTouchPosition()
    {
        positionWithTouch = touch.position;
    }

    internal void InvertDirection(float invertRatio = -1.0f) // инвертирует или умножает направление движения корабля.
    {
        direction *= invertRatio;
    }

    override protected void MovePos(Vector2 addToVector, float speed)
    {
        moveVector.x = addToVector.x;
        moveVector.z = addToVector.y;

        rb.MovePosition(transform.position + moveVector * speed);
    }

    override protected internal void InputUpdate() // Input mode для игровой сессии.
    {
        if (Input.touchCount > 0 & isInputManageable)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    ResetPhysicVelocity();
                    positionWithTouch = touch.position;

                    inputDisplay.SetDisplayPosition(positionWithTouch);
                    inputDisplay.gameObject.SetActive(true);
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    SetDirection(touch.position - positionWithTouch);
                    MoveRotationByDirection(direction, 19);
                    MovePos(direction, speedByMove);

                    inputDisplay.SetDirectionPosition(touch.position - positionWithTouch);
                    break;

                case TouchPhase.Stationary:
                    MovePos(direction, speedByMove);
                    MoveRot(0, 0, 0);
                    break;

                case TouchPhase.Ended:
                    // direction[0] = 0;
                    // direction[1] = 0;
                    MovePos(direction, speedByMove);
                    MoveRot(0, 0, 0);

                    inputDisplay.gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            if (isInputManageable)
                MovePos(direction, speedByMove);
            else 
                ResetTouchPosition();
        }
    }
}
