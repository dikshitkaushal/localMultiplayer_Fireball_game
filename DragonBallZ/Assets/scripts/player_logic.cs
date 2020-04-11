using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerid
{
    _P1,
    _P2,
}

public class player_logic : MonoBehaviour
{
    CharacterController m_charactercontroller;
    Vector3 m_horizontalmove;
    Vector3 m_verticalmove;
    Vector3 m_jump;
    float m_jumpheight = 0.25f;
    float m_gravity = 0.97f;
    float m_horizontalaxis;
    float m_verticalaxis;
    bool isjumping = false;
    private float speed = 5f;
    [SerializeField] playerid m_playerid;

    // Start is called before the first frame update
    void Start()
    {
        m_charactercontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalaxis = Input.GetAxis("Horizontal"+m_playerid);
        m_verticalaxis = Input.GetAxis("Vertical"+m_playerid);

        if(Input.GetButtonDown("Jump"+m_playerid) && m_charactercontroller.isGrounded)
        {
            isjumping = true;
        }
    }
    private void FixedUpdate()
    {
        //jumping logic
        if(isjumping)
        {
            isjumping = false;
            m_jump.y = m_jumpheight;
        }
        m_jump.y -= m_gravity * Time.deltaTime;
       
        //moving logic

        m_horizontalmove = transform.right * m_horizontalaxis * speed * Time.deltaTime;
        m_verticalmove = transform.forward * m_verticalaxis * speed * Time.deltaTime;
        if(m_charactercontroller)
        {
            m_charactercontroller.Move(m_horizontalmove + m_verticalmove + m_jump);
        }
        if (m_charactercontroller.isGrounded)
        {
            m_jump.y = 0;
        }

    }
}
