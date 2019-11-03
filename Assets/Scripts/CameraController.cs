using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraController : MonoBehaviour
{
    public bool canMove = false;
    public bool moving = false;
    public FixedJoystick joystick;
    public FixedButton jumpButton;
    public FixedTouchField touchField;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            var fps = GetComponent<RigidbodyFirstPersonController>();
            fps.RunAxis = new Vector2(joystick.Horizontal, joystick.Vertical);
            if (joystick.Horizontal + joystick.Vertical != 0)
                moving = true;
            else
                moving = false;
            fps.jumpAxis = jumpButton.Pressed;
            fps.mouseLook.lookAxis = touchField.TouchDist;
        }
    }
}
