using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Blocks : MonoBehaviour
{
    private GameObject block;
    public bool create_block;

    private BlockMovement Block_Enabled;
    private GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        create_block = true;

        Camera = GameObject.Find("Main Camera");
        Block_Enabled = Camera.GetComponent<BlockMovement>();
    }

    private string SetColor()
    {
        string[] colors = { "Red", "Blue", "Magenta", "Orange", "Yellow", "Green", "Cyan" };
        int randomcolor = UnityEngine.Random.Range(0, 7);
        return colors[randomcolor];
    }

    private void Create_Block()
    {
        block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        block.transform.position = new Vector3(5, 15, 0);
        block.gameObject.tag = "Block";

        Material New_Mat = Resources.Load(SetColor(), typeof(Material)) as Material;
        block.GetComponent<Renderer>().material = New_Mat;
        Block_Enabled.Add_enable_block(block);

        create_block = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(create_block)
        {
            Create_Block();
        }
    }

    public void Set_Create_Block(bool value)
    {
        create_block=value;
    }
}

