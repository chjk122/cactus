using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 5f;                    // The fastest the player can travel in the x axis.

        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private int oldDir = 2;  // For determining which way the player is currently facing.
        private int hp = 100;
        public bool hasntMoved = true;
        private bool playerAlive = true;
        private int levelIndex = 0;

        private void Awake()
        {
            // Setting up references.
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {

        }


        public void Move(int dir)
        {
            if (!playerAlive)
            {
                Stop();
                return;
            }
            hasntMoved = false;
            // Move the character
            int velX = 0;
            int velY = 0;
            if (dir == 0)
            {
                m_Anim.Play("walk_u");
                velY = 1;
            }
            if (dir == 1)
            {
                m_Anim.Play("walk_r");
                velX = 1;
            }
            if (dir == 2)
            {
                m_Anim.Play("walk_d");
                velY = -1;
            }
            if (dir == 3)
            {
                m_Anim.Play("walk_l");
                velX = -1;
            }
             m_Rigidbody2D.velocity = new Vector2(velX*m_MaxSpeed, velY*m_MaxSpeed);

            Flip(dir);
        }
        public void Stop()
        {
            m_Rigidbody2D.velocity = new Vector2(0,0);
            if (!playerAlive)
                return;
            if (oldDir == 0)
            {
                m_Anim.Play("idle_u");
            }
            if (oldDir == 1)
            {
                m_Anim.Play("idle_r");
            }
            if (oldDir == 2)
            {
                m_Anim.Play("idle");
            }
            if (oldDir == 3)
            {
                m_Anim.Play("idle_l");
            }
        }


        private void Flip(int dir)
        {
            if (dir == oldDir)
                return;
            if(dir == 0)
            {
                m_Anim.Play("idle_u");
            }
            if (dir == 1)
            {
                m_Anim.Play("idle_r");
            }
            if (dir == 2)
            {
                m_Anim.Play("idle");
            }
            if (dir == 3)
            {
                m_Anim.Play("idle_l");
            }
            oldDir = dir;
        }

        public int getHp()
        {
            return hp;
        }
        public void damage(int x)
        {
            hp -= x;
            if (hp <= 0)
            {
                hp = 0;
                killPlayer();
            }
            else if (hp > 100)
                hp = 100;

            GameObject[] ui = GameObject.FindGameObjectsWithTag("ui");
            for (int i = 0; i < ui.Length; i++)
            {
                if (ui[i].name == "hpBar")
                {
                    Text health = ui[i].GetComponent<Text>();
                    health.color = new Color(0, 1, 0);
                    health.text = "HP: " + getHp() + "/100";
                    health.color = new Color(1 - getHp() / 100.0f, getHp() / 100.0f, 0);
                }
            }
        }

        private void killPlayer()
        {
            m_Anim.Play("die");
            playerAlive = false;
        }

        public void respawn()
        {
            //only need to save objects when not on level 1
            if (levelIndex != 0)
            {
                DontDestroyOnLoad(transform.gameObject);
                GameObject[] lights = GameObject.FindGameObjectsWithTag("light");
                foreach (GameObject x in lights)
                {
                    DontDestroyOnLoad(x);
                }
                GameObject[] ui = GameObject.FindGameObjectsWithTag("ui");
                foreach (GameObject x in ui)
                {
                    DontDestroyOnLoad(x);
                }
            }
            int levelNum = levelIndex + 1;
            string levelString = levelNum.ToString();
            SceneManager.LoadScene("level" + levelString);
            //DoSomething(levelString);
            GameObject[] p = GameObject.FindGameObjectsWithTag("start");
            playerAlive = true;
            hp = 100;
            for (int x = 0; x< p.Length; x++)
            {
                Debug.Log(p[x].name);
                if (p[x].name == "level" + levelString + "start")
                {
                    Debug.Log("postion to go to: " + p[x].name);
                    transform.position = new Vector3(p[x].transform.position.x, p[x].transform.position.y, -2);
                    Debug.Log("start pos is: " + transform.position);
                    return;
                }
            }
            transform.position = new Vector3(68, 8.0f, -2.0f);

            //didnt find the start
        }

        /*IEnumerator DoSomething(string levelString)
        {
            //SceneManager.LoadScene("level" + levelString);
            yield return new WaitForSeconds(.2f);
            SceneManager.LoadScene("level" + levelString);
        }*/

        public void beatLevel()
        {
            if(levelIndex <= 0)
            {
                levelIndex++;
            }
        }

    }
}
