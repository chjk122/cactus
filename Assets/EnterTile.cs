using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class EnterTile : MonoBehaviour {
    [SerializeField] private int startSize = 0;
    private int size;
    private int step = 0;
    const int STEP_SIZE = 3;

    private void Awake()
    {
        size = startSize;
        foreach (Transform child in transform)
        {
            TextMesh sizeDisplay = child.GetComponent<TextMesh>();
            if (sizeDisplay != null)
            {
                sizeDisplay.text = size.ToString();
            }
        }
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.isTrigger)
        {
            //skip the start tile
            GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
            UnityStandardAssets._2D.PlatformerCharacter2D player = p[0].GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
            if (player.hasntMoved)
                return;


            if(size == -1)
            {
                Debug.Log("You beat Williams crappy level XD");
                nextlevel();
            }
            // affect the player
            /*GameObject[] ui = GameObject.FindGameObjectsWithTag("ui");
            for (int x = 0; x < ui.Length; x++)
            {
                if (ui[x].name == "hpBar")
                {
                    Text hp = ui[x].GetComponent<Text>();
                    hp.color = new Color(0, 1, 0);
                    player.damage(damageAmount());
                    hp.text = "HP: " + player.getHp() + "/100";
                    hp.color = new Color(1- player.getHp() / 100.0f, player.getHp()/100.0f, 0);
                }
            }*/
            player.damage(damageAmount());

            //grow last
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
            for (int x = 0; x < tiles.Length; x++)
            {
                tiles[x].GetComponent<EnterTile>().grow();
            }

            Material newMat = Resources.Load("dirt", typeof(Material)) as Material;
            GetComponent<Renderer>().material = newMat;
            //remove the text
            foreach (Transform child in transform)
            {
                TextMesh sizeDisplay = child.GetComponent<TextMesh>();
                if (sizeDisplay != null)
                {
                    sizeDisplay.text = "";
                }
            }
            size = 0;
        }
    }

    public void grow()
    {
        if (size > 0 && size < 10)
        {
            step++;
            if(step%3 == 0)
            {
                step = 0;
                size++;
            }
        }
        //GetComponent<TextMesh>().text = size.ToString();
        foreach(Transform child in transform)
        {
            TextMesh sizeDisplay = child.GetComponent<TextMesh>();
            if(sizeDisplay != null)
            {
                if(sizeDisplay.text != "")
                    sizeDisplay.text = size.ToString();
            }
        }
    }

    public int damageAmount()
    {
        if (size <= 5)
            return size * -5;
        else if (size >= 6)
            return (size - 5) * 10;
        return 0;
    }

    private void nextlevel()
    {
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
        UnityStandardAssets._2D.PlatformerCharacter2D player = p[0].GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
        player.beatLevel();
        player.respawn();
    }
}
