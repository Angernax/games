using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public class KEYMAP
    {
        Dictionary<string, bool> keyboard = new Dictionary<string, bool>()
        {
            { "FORWARD", Input.GetKey(KeyCode.W)},
            { "LEFT", Input.GetKey(KeyCode.S)},
            { "RIGHT", Input.GetKey(KeyCode.D)},
            { "BACK", Input.GetKey(KeyCode.A)},
            { "RUN", Input.GetKey(KeyCode.LeftShift)},
            { "JUMP", Input.GetKey(KeyCode.Space)},
            { "CROUCH", Input.GetKey(KeyCode.LeftControl)}

        };
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
