using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class xCharacterController2dAbridged : MonoBehaviour
{

    public PlayerIndex playerIndex;
    public float moveSpeed = 8f;
    public float jumpSpeed = 6f;
    public bool enableMoveJoyJump;

    public float moveSpeed2 = 6f;
    public float moveSpeed3 = 8f;

    public Transform aimTransform;

    GamePadState previousState;
    GamePadState currentState;

    Vector3 moveJoy;
    Vector3 aimJoy;
    bool jump = false;

    float timeElapsedRunning = 0f;

    void Update()
    {
        HandleXInput();
    }

    void HandleXInput()
    {
        currentState = GamePad.GetState(playerIndex);

        if (!currentState.IsConnected)
        {
            return;
        }

        moveJoy.x = currentState.ThumbSticks.Left.X;
        moveJoy.y = currentState.ThumbSticks.Left.Y;

        aimJoy.x = currentState.ThumbSticks.Right.X;
        aimJoy.y = currentState.ThumbSticks.Right.Y;

        //jump by pushing A
        if (previousState.Buttons.A == ButtonState.Released && currentState.Buttons.A == ButtonState.Pressed)
        {
            jump = true;
        }
        

        //aiming
        if(aimJoy.sqrMagnitude > 0)
        	aimTransform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0,0,90) * new Vector3( aimJoy.x, aimJoy.y, 0));

        previousState = currentState;
    }

    void FixedUpdate()
    {
        Vector3 velocity = GetComponent<Rigidbody>().velocity;

        float joySpeed = moveSpeed;

        if (currentState.Triggers.Left > 0.6f || currentState.Triggers.Right > 0.6f)
        {
            joySpeed += moveSpeed3;
        }
        if (currentState.Triggers.Left > 0.25f || currentState.Triggers.Right > 0.25f)
        {
            joySpeed += moveSpeed2;
        }

        velocity.x = moveJoy.x * joySpeed;
        velocity.z = moveJoy.y * joySpeed;

        if (jump)
        {
            velocity.y = jumpSpeed;
            jump = false;
        }

        GetComponent<Rigidbody>().velocity = velocity;

        // Set vibration according to triggers
        GamePad.SetVibration(playerIndex, currentState.Triggers.Left / 5, currentState.Triggers.Right / 5);
        /*
        // Make the current object turn
        transform.localRotation *= Quaternion.Euler(0.0f, currentState.ThumbSticks.Right.X * 25.0f * Time.deltaTime, 0.0f);
        */
    }

    void OnDrawGizmos()
    {
        if (aimTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(aimTransform.position, aimTransform.right);
        }
    }

    void OnGUI()
    {
        string text = "Use left stick to turn the cube, hold A to change color\n";
        text += string.Format("IsConnected {0} Packet #{1}\n", currentState.IsConnected, currentState.PacketNumber);
        text += string.Format("\tTriggers {0} {1}\n", currentState.Triggers.Left, currentState.Triggers.Right);
        text += string.Format("\tD-Pad {0} {1} {2} {3}\n", currentState.DPad.Up, currentState.DPad.Right, currentState.DPad.Down, currentState.DPad.Left);
        text += string.Format("\tButtons Start {0} Back {1}\n", currentState.Buttons.Start, currentState.Buttons.Back);
        text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", currentState.Buttons.LeftStick, currentState.Buttons.RightStick, currentState.Buttons.LeftShoulder, currentState.Buttons.RightShoulder);
        text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", currentState.Buttons.A, currentState.Buttons.B, currentState.Buttons.X, currentState.Buttons.Y);
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", currentState.ThumbSticks.Left.X, currentState.ThumbSticks.Left.Y, currentState.ThumbSticks.Right.X, currentState.ThumbSticks.Right.Y);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);


        Vector3 screenPos = Camera.main.WorldToScreenPoint(GetComponent<Renderer>().bounds.max);
        GUI.color = Color.black;
        GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y, 100, 100), playerIndex.ToString());
    }
}
