using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    CapsuleCollider capcol;
    float heightplayer;

    bool forward_Key;
    bool back_Key;
    bool right_Key;
    bool left_Key;
    bool run_Key;
    bool jump_Key;
    bool crouch_Key;


    private Vector3 playerInput;
    private Vector3 lastCommand;
    Vector3 inputMov;
    Vector3 character_velocity;
    [SerializeField] private float walk_velocity;
    [SerializeField] private float run_velocity;
    float walk_run_velocity;
    [SerializeField] private float jump_force;
    [SerializeField] private float modif_gravity;


    bool moveing;
    bool over_head; //funcionando
    bool stepping_on;
    bool crouched;  
    bool falling; //funcionando
    bool jumping; //dudoso
    bool standing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capcol = GetComponent<CapsuleCollider>();
        heightplayer = capcol.height = 1f;
        Physics.gravity *= modif_gravity;
        standing = true;

    }


    void Update()
    {
        float walk_run_velocity = Input.GetKey(KeyCode.LeftShift) ? run_velocity : walk_velocity;//Operador in line if
        rb.velocity =
            transform.forward * walk_run_velocity * inputMov.z
            + transform.right * walk_run_velocity * inputMov.x
            + new Vector3(0, rb.velocity.y, 0);

        //Debug.Log(walk_run_velocity);

        PlayerCheckers();
        PlayerSkills();
        Move();
    }

    private void Move()
    {
        //Ejes X/
        inputMov.x = Input.GetAxis("Horizontal");
        inputMov.y = Input.GetAxis("Vertical");

        playerInput = new Vector3(inputMov.x, 0, inputMov.y);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        //Inputs
        forward_Key = Input.GetKey(KeyCode.W); 
        back_Key = Input.GetKey(KeyCode.S);
        right_Key = Input.GetKey(KeyCode.D);
        left_Key = Input.GetKey(KeyCode.A);
        run_Key = Input.GetKey(KeyCode.LeftShift);
        jump_Key = Input.GetButtonDown("Jump");
        crouch_Key = Input.GetKey(KeyCode.LeftControl);

        //WASD_Keys
        if (forward_Key)
            rb.transform.position += (playerInput + character_velocity) * Time.deltaTime;

        if (back_Key)
            rb.transform.position += (playerInput + character_velocity) * Time.deltaTime;

        if (right_Key)
            rb.transform.position += (playerInput + character_velocity) * Time.deltaTime;

        if (left_Key)
            rb.transform.position += (playerInput + character_velocity) * Time.deltaTime;

    }

    public void PlayerCheckers()
    {
        //Raycast para detectar el techo
        Vector3 roof = transform.TransformDirection(Vector3.up);

        if (Physics.Raycast(transform.position, roof, 0.40f))
        {
            over_head = true;
            //Debug.Log(cabeceando);
        }
        else
        {
            over_head = false;
        }
        //Chequeando si hay suelo
        Vector3 floor = transform.TransformDirection(Vector3.down);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, floor, out hit, 0.55f))
        {
            stepping_on = true;
            jumping = false;

        }
        else
        {
            stepping_on = false;

        }
        Debug.Log(stepping_on);

        //Chequeando caida del Player
        if (rb.velocity.y <= 0 && !stepping_on)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }
    }

    public void PlayerSkills()
    {
        //Salto
        if (jump_Key && stepping_on && !crouched)
        {
            rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
            jumping = true;

        }
        //Debug.Log(jumping);

        //Correr
        if (run_Key && !crouched)
        {
            rb.velocity = transform.forward * run_velocity * inputMov.y
                + transform.right * run_velocity * inputMov.x
                + new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = transform.forward * walk_velocity * inputMov.y
                + transform.right * walk_velocity * inputMov.x
                + new Vector3(0, rb.velocity.y, 0);
        }

        //Agacharse
        if (crouch_Key)
        {
            if (!crouched && standing)
            {
                capcol.center = new Vector3(0f, -0.25f, 0f);
                capcol.height = heightplayer / 2;
                crouched = true;
                standing = false;
            }
        }
        else
        {
            if (crouched && !over_head)
            {
                capcol.center = new Vector3(0f, 0f, 0f);
                capcol.height = heightplayer;
                crouched = false;
                standing = true;
            }

        }
        //Debug.Log(stepping_on);
    }
}
