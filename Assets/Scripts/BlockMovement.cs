using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    private Create_Blocks script;
    private GameObject Camera;

    private float TimeRemaining;

    private List<GameObject> enabled_block;
    private List<GameObject> disabled_block;

    private bool leftmove;
    private float leftmovetime;
    private bool rightmove;
    private float rightmovetime;
    private bool downmove;
    private float downmovetime;


    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        script = Camera.GetComponent<Create_Blocks>();
        TimeRemaining = 0.5f;
        
        enabled_block = new List<GameObject>();
        disabled_block = new List<GameObject>();

        leftmove = true;
        leftmovetime = 0.3f;
        rightmove = true;
        rightmovetime = 0.3f;
        downmove = true;
        downmovetime = 0.1f;

    }

    private bool DownLimit(GameObject enableblock)
    {
        bool limit = false;

        if (enableblock.transform.position.y <= 1f) {
            limit = true;}

        if (!limit)
        {
            foreach (GameObject disableblock in disabled_block) { 
                if(enableblock.transform.position.x == disableblock.transform.position.x && 
                    enableblock.transform.position.y == disableblock.transform.position.y + 1f)
                {
                    limit = true;
                }
            }
        }
        return limit;
    }

    private bool LeftLimit(GameObject enableblock)
    {
        bool limit = false;

        if (enableblock.transform.position.x <= 1f)
        {
            limit = true;
        }

        if (!limit)
        {
            foreach (GameObject disableblock in disabled_block)
            {
                if (enableblock.transform.position.y == disableblock.transform.position.y && 
                    enableblock.transform.position.x == disableblock.transform.position.x + 1f)
                {
                    limit = true;
                }
            }
        }
        return limit;
    }

    private bool RightLimit(GameObject enableblock)
    {
        bool limit = false;

        if (enableblock.transform.position.x >= 10f)
        {
            limit = true;
        }

        if (!limit)
        {
            foreach (GameObject disableblock in disabled_block)
            {
                if (enableblock.transform.position.y == disableblock.transform.position.y &&
                    enableblock.transform.position.x == disableblock.transform.position.x - 1f)
                {
                    limit = true;
                }
            }
        }
        return limit;
    }

    private void Drop()
    {
        foreach(GameObject block in enabled_block.ToArray())
        {
            block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y - 1f, 0);

            if (DownLimit(block))
            {
                enabled_block.Clear();
                disabled_block.Add(block);
                script.Set_Create_Block(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        TimeRemaining -= Time.deltaTime;

        if(TimeRemaining < 0)
        {
            TimeRemaining = 0.5f;
            Drop();
        }

        leftmovetime -= Time.deltaTime;
        if(leftmovetime < 0)
        {
            leftmovetime = 0.3f;
            leftmove = true;
        }

        rightmovetime -= Time.deltaTime;
        if (rightmovetime < 0)
        {
            rightmovetime = 0.3f;
            rightmove = true;
        }

        downmovetime -= Time.deltaTime;
        if(downmovetime < 0)
        {
            downmovetime = 0.05f;
            downmove = true;
        }

        if(Input.GetKey(KeyCode.LeftArrow) && leftmove)
        {
            leftmove = false;
            foreach (GameObject block in enabled_block.ToArray()) {
                
                if (!LeftLimit(block))
                {
                    block.transform.position = new Vector3(block.transform.position.x - 1f, block.transform.position.y, 0);
                }
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) && rightmove)
        {
            rightmove = false;
            foreach (GameObject block in enabled_block.ToArray())
            {

                if (!RightLimit(block))
                {
                    block.transform.position = new Vector3(block.transform.position.x + 1f, block.transform.position.y, 0);
                }
            }
        }

        if (Input.GetKey(KeyCode.DownArrow) && downmove)
        {
            downmove = false;
            Drop();
        }
    }

    public void Add_enable_block(GameObject value)
    {
        enabled_block.Add(value);  
    }
}
