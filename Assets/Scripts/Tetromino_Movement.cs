using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Tetromino_Movement : MonoBehaviour
{
    private Create_Tetromino script;
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
    private bool upmove;
    private float upmovetime;

    private int enable_tetromino;
    private int position;

    private int score;
    public Text score_result;

    private List<GameObject> next_blocks;

    public GameObject Game_Over_Panel;



    // Start is called before the first frame update

    // Baþlangýç ayarlarý ve deðiþkenlerin ilk deðerleri atanýyor
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        script = Camera.GetComponent<Create_Tetromino>();
        TimeRemaining = 0.5f;

        enabled_block = new List<GameObject>();
        disabled_block = new List<GameObject>();

        // Hareket kontrollerinin baþlangýç deðerleri
        leftmove = true;
        leftmovetime = 0.3f;
        rightmove = true;
        rightmovetime = 0.3f;
        downmove = true;
        downmovetime = 0.1f;
        upmove = true;
        upmovetime = 0.1f;


        enable_tetromino = -1;
        position = 1;
        score = 0;

        next_blocks = new List<GameObject>();

    }
    // Tetromino'nun aþaðýda daha fazla hareket edip edemeyeceðini kontrol eder
    private bool DownLimit()
    {
        bool limit = false;

        foreach (GameObject enableblock in enabled_block.ToArray())
        {

            if (enableblock.transform.position.y <= 1f)
            {
                limit = true;
                break;
            }
        }
        if (!limit)
        {
            foreach (GameObject disableblock in disabled_block)
            {
                foreach (GameObject enableblock in enabled_block.ToArray())
                {
                    // Sabit bloklar tetromino hareketini engelliyorsa limit true olur
                    if (enableblock.transform.position.x == disableblock.transform.position.x &&
                    enableblock.transform.position.y == disableblock.transform.position.y + 1f)
                    {
                        limit = true;
                        break;
                    }

                    if (limit)
                    {
                        break;
                    }
                }
            }
            
        }
        return limit;
    }

    private bool LeftLimit()
    {
        // Tetromino'nun sola hareket edip edemeyeceðini kontrol eder
        bool limit = false;
        foreach (GameObject enableblock in enabled_block.ToArray())
        {
            if (enableblock.transform.position.x <= 1f)
            {
                limit = true;
            }
        }

        if (!limit)
        {
            foreach (GameObject disableblock in disabled_block)
            {
                foreach (GameObject enableblock in enabled_block.ToArray())
                {
                    if (enableblock.transform.position.y == disableblock.transform.position.y &&
                    enableblock.transform.position.x == disableblock.transform.position.x + 1f)
                    {
                        limit = true;
                        break;
                    }

                }
                if (limit)
                {
                    break;
                }
            }
        }
        return limit;
    }

    private bool RightLimit()
    {
        // Tetromino'nun saða hareket edip edemeyeceðini kontrol eder
        bool limit = false;
        foreach (GameObject enableblock in enabled_block.ToArray())
        {
            if (enableblock.transform.position.x >= 10f)
            {
                limit = true;
            }
        }

        if (!limit)
        {
            foreach (GameObject disableblock in disabled_block)
            {
                foreach (GameObject enableblock in enabled_block.ToArray())
                {
                    if (enableblock.transform.position.y == disableblock.transform.position.y &&
                    enableblock.transform.position.x == disableblock.transform.position.x - 1f)
                    {
                        limit = true;
                        break;
                    }
                }
                if (limit)
                {
                    break;
                }
            }
        }

        return limit;
    }

    private void Check_Fulled_Rows()
    {
        // Tamamen dolu satýrlarý kontrol eder ve temizler
        int cleared_line_count = 0;
        for (int i = 1; i < 21;  i++)
        {
            int fulled_rows = 0;

            foreach(GameObject disableblock in disabled_block.ToArray())
            {
                if(disableblock.transform.position.y == i)
                {
                    fulled_rows++;
                }
            }

            if (fulled_rows > 9)
            {
                // Dolu satýrlarý yok eder ve yukarýdaki bloklarý aþaðý kaydýrýr
                foreach (GameObject disableblock in disabled_block.ToArray())
                {
                    if (disableblock.transform.position.y == i)
                    {
                        disabled_block.Remove(disableblock);
                        Destroy(disableblock);
                    }
                }

                foreach (GameObject disableblock in disabled_block.ToArray())
                {
                    if (disableblock.transform.position.y > i)
                    {
                        disableblock.transform.position += new Vector3(0, -1, 0);
                    }
                }
                cleared_line_count++;

                i--;
            }
        }
        // Temizlenen satýr sayýsýna göre puan verir
        if (cleared_line_count == 1)
        {
            score = score + 100;
        }
        else if (cleared_line_count == 2)
        {
            score = score + 300;
        }
        else if (cleared_line_count == 3)
        {
            score = score + 500;
        }
        else if (cleared_line_count == 4)
        {
            score = score + 1000;
        }
        score_result.text = score.ToString();
    }
    public void Enabled_to_Disabled()
        {
            // Aktif bloklarý sabit bloklara dönüþtürür ve yeni tetromino oluþturur
            string[] colors = { "RedLight", "OrangeLight", "YellowLight", "GreenLight", "CyanLight", "BlueLight", "MagentaLight" };

            foreach(GameObject block in enabled_block.ToArray())
            {
                disabled_block.Add(block);

                Material New_Mat = Resources.Load(colors[enable_tetromino], typeof(Material)) as Material;
                block.GetComponent<Renderer>().material = New_Mat;

            }
            foreach (GameObject block in next_blocks.ToArray())
            {
                Destroy(block);

            }

            bool game_over = false;// Oyun sonu kontrolü

            foreach (GameObject block in disabled_block.ToArray())
            {
                    if(block.transform.position.y > 20 && !game_over)
                {
                    Game_Over_Panel.SetActive(true);
                    game_over = true;   
                }
            }
            if (!game_over)
            {
                enabled_block.Clear();
                Check_Fulled_Rows();
                script.Set_Tetromino_Block(true);// Yeni tetromino oluþturma
                position = 1;
            }
    }
    private void Drop()
    {
        // Tetromino'yu bir hücre aþaðý hareket ettirir
        if (!DownLimit())
        {
            foreach (GameObject block in enabled_block.ToArray())
            {
                block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y - 1f, 0);
            }
        }
        else
        {
            Enabled_to_Disabled();// Eðer daha fazla aþaðý inemezse, sabit blok yapar
        }
    }

    private bool Rotation_Limit(GameObject[] blocks, int x1,  int y1, int x2, int y2, int x3, int y3, int x4,int y4) {
        // Rotasyonun geçerli olup olmadýðýný kontrol eder
        bool my_limit = false;
        float new_x1 = blocks[0].transform.position.x + x1;
        float new_x2 = blocks[1].transform.position.x + x2;
        float new_x3 = blocks[2].transform.position.x + x3;
        float new_x4 = blocks[3].transform.position.x + x4;

        float new_y1 = blocks[0].transform.position.y + y1;
        float new_y2 = blocks[1].transform.position.y + y2;
        float new_y3 = blocks[2].transform.position.y + y3;
        float new_y4 = blocks[3].transform.position.y + y4;

        // Bloklarýn oyun alanýnýn dýþýna çýkmasýný veya sabit bloklarla çakýþmasýný engeller
        if ((new_x1 < 1 || new_x1 > 10) || (new_x2 < 1 || new_x2 > 10) || (new_x3 < 1 || new_x3 > 10) || (new_x4 < 1 || new_x4 > 10))
        {
            my_limit = true;    
        }
        if ((new_y1 < 1 || new_y1 > 20) || (new_y2 < 1 || new_y2 > 20) || (new_y3 < 1 || new_y3 > 20) || (new_y4 < 1 || new_y4 > 20))
        {
            my_limit = true;
        }

        if (!my_limit)
        {
            foreach(GameObject disableblock in disabled_block.ToArray())
            {
                if((disableblock.transform.position.x == new_x1 && disableblock.transform.position.y == new_y1)
                    || (disableblock.transform.position.x == new_x2 && disableblock.transform.position.y == new_y2)
                    || (disableblock.transform.position.x == new_x3 && disableblock.transform.position.y == new_y3)
                    || (disableblock.transform.position.x == new_x4 && disableblock.transform.position.y == new_y4))
                {
                    my_limit = true;
                    break;
                }
            }
        }

        return my_limit;

    }

    private void Set_to_New_Position(GameObject[] blocks, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
        // Bloklarý yeni pozisyonlarýna taþýr
        if (!Rotation_Limit(blocks, x1, y1, x2, y2, x3, y3, x4, y4))
        {
            blocks[0].transform.position = new Vector3(blocks[0].transform.position.x + x1, blocks[0].transform.position.y + y1, 0);
            blocks[1].transform.position = new Vector3(blocks[1].transform.position.x + x2, blocks[1].transform.position.y + y2, 0);
            blocks[2].transform.position = new Vector3(blocks[2].transform.position.x + x3, blocks[2].transform.position.y + y3, 0);
            blocks[3].transform.position = new Vector3(blocks[3].transform.position.x + x4, blocks[3].transform.position.y + y4, 0);
        }
        else {if(position == 0)
            {
                position = 4;
            }
            position--;
        }
    }

    private void Rotate_Tetromino()
    {
        // Tetromino'yu saat yönünde döndürür ve pozisyon deðiþikliðine göre hareket ettirir
        position++;

        GameObject[] blocks = enabled_block.ToArray();
        if (enable_tetromino == 0)//z
        {
            if (position == 2)
            {
                Set_to_New_Position(blocks, 1, 1, 0, 0, -1, 1, -2, 0);

            }
            else if (position == 3)
            {
                Set_to_New_Position(blocks, 1, -1, 0, 0, 1, 1, 0, 2);
            }
            else if (position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -1, -1, 0, 0, 1, -1, 2, 0);

            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1, 1, 0, 0, -1, -1, 0, -2);
            }

        }
        else if (enable_tetromino == 1) //L
        {
            if (position == 2)
            {
                Set_to_New_Position(blocks, 1, 1, 0, 0, -1, -1, 0, -2);

            }
            else if (position == 3)
            {
                Set_to_New_Position(blocks, 1, -1, 0, 0, -1, 1, -2, 0);
            }
            else if (position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -1, -1, 0, 0, 1, 1, 0, 2);

            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1, 1, 0, 0, 1, -1, 2, 0);
            }

        }
        else if (enable_tetromino == 3) //S
        {
            if (position == 2)
            {
                Set_to_New_Position(blocks, 1, 1, 0, 0, 1, -1, 0, -2);

            }
            else if (position == 3)
            {
                Set_to_New_Position(blocks, 1, -1, 0, 0, -1, -1, -2, 0);
            }
            else if (position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -1, -1, 0, 0, -1, 1, 0, 2);

            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1, 1, 0, 0, 1, 1, 2, 0);
            }
        }
        else if (enable_tetromino == 4) //I
        {
            if (position == 2)
            {
                Set_to_New_Position(blocks, 2, -1, 1, 0, 0, 1, -1, 2);

            }
            else if (position == 3)
            {
                Set_to_New_Position(blocks, 1, 2, 0, 1, -1, 0, -2, -1);
            }
            else if (position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -2, 1, -1, 0, 0, -1, 1, -2);

            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1, -2, 0, -1, 1, 0, 2, 1);
            }
        }
        else if (enable_tetromino == 5) //J
        {
            if (position == 2)
            {
                Set_to_New_Position(blocks, 1, 1, 0, 0, -1, -1, 2, 0);

            }
            else if (position == 3)
            {
                Set_to_New_Position(blocks, 1, -1, 0, 0, -1, 1, 0, -2);
            }
            else if (position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -1, -1, 0, 0, 1, 1, -2, 0);

            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1, 1, 0, 0, 1, -1, 0, 2);
            }

        }
        else if (enable_tetromino == 6) //T
        {
            if (position == 2)
            {
                Set_to_New_Position(blocks, 1, 1, 0, 0, -1, -1, 1, -1);

            }
            else if (position == 3)
            {
                Set_to_New_Position(blocks, 1, -1, 0, 0, -1, 1, -1, -1);
            }
            else if (position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -1, -1, 0, 0, 1, 1, -1, 1);

            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1, 1, 0, 0, 1, -1, 1, 1);
            }
        }
    }

    private void Update()
    {
        // Oyun döngüsü, tetromino hareketlerini ve oyun kontrollerini ele alýr
        TimeRemaining -= Time.deltaTime;

        if (TimeRemaining < 0)
        {
            TimeRemaining = 0.5f;
            Drop();// Tetromino'yu bir hücre aþaðý hareket ettirir  
        }

        leftmovetime -= Time.deltaTime;
        if (leftmovetime < 0)
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
        if (downmovetime < 0)
        {
            downmovetime = 0.05f;
            downmove = true;
        }

        upmovetime -= Time.deltaTime;
        if (upmovetime < 0)
        {
            upmovetime = 0.5f;
            upmove = true;
        }

        // Klavye giriþleri ile tetromino'nun hareket ve dönüþlerini kontrol eder
        if (Input.GetKey(KeyCode.LeftArrow) && leftmove && !LeftLimit())
        {
            leftmove = false;
            foreach (GameObject block in enabled_block.ToArray())
            {
                block.transform.position = new Vector3(block.transform.position.x - 1f, block.transform.position.y, 0);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) && rightmove && !RightLimit())
        {
            rightmove = false;
            foreach (GameObject block in enabled_block.ToArray())
            {
                block.transform.position = new Vector3(block.transform.position.x + 1f, block.transform.position.y, 0);

            }
        }

        if (Input.GetKey(KeyCode.DownArrow) && downmove)
        {
            downmove = false;
            Drop();
            score++;
            score_result.text = score.ToString();
        }

        if (Input.GetKey(KeyCode.UpArrow) && upmove)
        {
            upmove = false;
            Rotate_Tetromino();// Tetromino'yu döndürür
        }


    }
    public void Add_enable_block(GameObject value)
    {
        enabled_block.Add(value);// Yeni bir aktif blok ekler
    }
    public void Set_Enabled_Tetromino(int value)
    {
        // Þu anda aktif olan tetromino tipini belirler
        enable_tetromino = value;
    }
    public void Add_next_block(GameObject value)
    {
        // Yeni bir sonraki tetromino bloðu ekler
        next_blocks.Add(value);
    }

    public void New_Game_Button_On_Click()
    {
        // Yeni oyun baþlatýr ve sahneyi yeniden yükler
        UnityEngine.SceneManagement.SceneManager.LoadScene("Deneme");
    }
}
