using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {

        }


        private void FixedUpdate()
        {
            // Read the inputs.
            int dir = -1;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                dir = 0;
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                dir = 1;
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                dir = 2;
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                dir = 3;
            else if (Input.GetKey(KeyCode.Space))
                m_Character.respawn();
            // Pass all parameters to the character control script.
            if (dir != -1)
                m_Character.Move(dir);
            else
                m_Character.Stop();
        }
    }
}
