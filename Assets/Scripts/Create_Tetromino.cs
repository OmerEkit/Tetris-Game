using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Tetromino : MonoBehaviour
{
    public bool create_tetromino;

    private Tetromino_Movement Tetromino_Enabled;// Tetromino hareketini kontrol eden bileþen
    private GameObject Camera;
    string[] colors = { "RedLight", "OrangeLight", "YellowLight", "GreenLight", "CyanLight", "BlueLight", "MagentaLight" };

    private int Current_Rnd_Tetromino;// Þu anki tetromino türü
    private int Next_Rnd_Tetromino;// Bir sonraki tetromino türü

    // Start is called before the first frame update
    void Start()
    {
        create_tetromino = true;

        Camera = GameObject.Find("Main Camera");
        Tetromino_Enabled = Camera.GetComponent<Tetromino_Movement>();
        // Þu anki ve bir sonraki tetromino türlerini rastgele seç
        Current_Rnd_Tetromino = UnityEngine.Random.Range(0, 7);
        Next_Rnd_Tetromino = UnityEngine.Random.Range(0, 7);
    }

    private void Crate_Tetromino(int Rnd_Tetromino, bool next)
    {
        GameObject[] block =new GameObject[4];// 4 bloða sahip bir tetromino oluþtur

        for (int i = 0; i < 4; i++)
        {
            // Her bir bloðu oluþtur ve yapýlandýr
            block[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block[i].gameObject.tag = "Block";
            // Materyali yükle ve uygula
            Material New_Mat = Resources.Load(colors[Rnd_Tetromino], typeof(Material)) as Material;
            block[i].GetComponent<Renderer>().material = New_Mat;

            if (!next)
            {
                Tetromino_Enabled.Add_enable_block(block[i]);// Aktif tetromino bloðu olarak ekle
            }
            else
            {
                Tetromino_Enabled.Add_next_block(block[i]); // Bir sonraki tetromino bloðu olarak ekle
            }
        }
        // Þu anki veya bir sonraki tetromino için bloklarý yerleþtir
        if (!next)
        {
            if (Rnd_Tetromino == 0)//O
            {
                block[0].transform.position = new Vector3(4, 22, 0);
                block[1].transform.position = new Vector3(5, 22, 0);
                block[2].transform.position = new Vector3(5, 21, 0);
                block[3].transform.position = new Vector3(6, 21, 0);
            }
            else if (Rnd_Tetromino == 2)//S
            {
                block[0].transform.position = new Vector3(5, 21, 0);
                block[1].transform.position = new Vector3(6, 21, 0);
                block[2].transform.position = new Vector3(5, 22, 0);
                block[3].transform.position = new Vector3(6, 22, 0);
            }
            else if (Rnd_Tetromino == 1)//L
            {
                block[0].transform.position = new Vector3(4, 21, 0);
                block[1].transform.position = new Vector3(5, 21, 0);
                block[2].transform.position = new Vector3(6, 21, 0);
                block[3].transform.position = new Vector3(6, 22, 0);
            }
            else if (Rnd_Tetromino == 3)//Z
            {
                block[0].transform.position = new Vector3(4, 21, 0);
                block[1].transform.position = new Vector3(5, 21, 0);
                block[2].transform.position = new Vector3(5, 22, 0);
                block[3].transform.position = new Vector3(6, 22, 0);
            }
            else if (Rnd_Tetromino == 4)//I
            {
                block[0].transform.position = new Vector3(4, 21, 0);
                block[1].transform.position = new Vector3(5, 21, 0);
                block[2].transform.position = new Vector3(6, 21, 0);
                block[3].transform.position = new Vector3(7, 21, 0);
            }
            else if (Rnd_Tetromino == 5)//J
            {
                block[0].transform.position = new Vector3(4, 21, 0);
                block[1].transform.position = new Vector3(5, 21, 0);
                block[2].transform.position = new Vector3(6, 21, 0);
                block[3].transform.position = new Vector3(4, 22, 0);
            }
            else if (Rnd_Tetromino == 6)//T
            {
                block[0].transform.position = new Vector3(4, 21, 0);
                block[1].transform.position = new Vector3(5, 21, 0);
                block[2].transform.position = new Vector3(6, 21, 0);
                block[3].transform.position = new Vector3(5, 22, 0);
            }

            Tetromino_Enabled.Set_Enabled_Tetromino(Rnd_Tetromino); // Þu anki tetromino türünü ayarla
            create_tetromino = false;// Yeni tetromino oluþturmayý durdur

        }
        else
        {
            if (Rnd_Tetromino == 0)
            {
                block[0].transform.position = new Vector3(15, 15, 0);
                block[1].transform.position = new Vector3(16, 15, 0);
                block[2].transform.position = new Vector3(16, 14, 0);
                block[3].transform.position = new Vector3(17, 14, 0);
            }
            else if (Rnd_Tetromino == 2)
            {
                block[0].transform.position = new Vector3(16, 14, 0);
                block[1].transform.position = new Vector3(17, 14, 0);
                block[2].transform.position = new Vector3(16, 15, 0);
                block[3].transform.position = new Vector3(17, 15, 0);
            }
            else if (Rnd_Tetromino == 1)
            {
                block[0].transform.position = new Vector3(15, 14, 0);
                block[1].transform.position = new Vector3(16, 14, 0);
                block[2].transform.position = new Vector3(17, 14, 0);
                block[3].transform.position = new Vector3(17, 15, 0);
            }
            else if (Rnd_Tetromino == 3)
            {
                block[0].transform.position = new Vector3(15, 14, 0);
                block[1].transform.position = new Vector3(16, 14, 0);
                block[2].transform.position = new Vector3(16, 15, 0);
                block[3].transform.position = new Vector3(17, 15, 0);
            }
            else if (Rnd_Tetromino == 4)//I
            {
                block[0].transform.position = new Vector3(15, 14, 0);
                block[1].transform.position = new Vector3(16, 14, 0);
                block[2].transform.position = new Vector3(17, 14, 0);
                block[3].transform.position = new Vector3(18, 14, 0);
            }
            else if (Rnd_Tetromino == 5)//J
            {
                block[0].transform.position = new Vector3(15, 14, 0);
                block[1].transform.position = new Vector3(16, 14, 0);
                block[2].transform.position = new Vector3(17, 14, 0);
                block[3].transform.position = new Vector3(15, 15, 0);
            }
            else if (Rnd_Tetromino == 6)//T
            {
                block[0].transform.position = new Vector3(15, 14, 0);
                block[1].transform.position = new Vector3(16, 14, 0);
                block[2].transform.position = new Vector3(17, 14, 0);
                block[3].transform.position = new Vector3(16, 15, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (create_tetromino)
        {
            Crate_Tetromino(Next_Rnd_Tetromino,true);//Bir sonraki tetrominoyu oluþtur
            Crate_Tetromino(Current_Rnd_Tetromino, false);//Þu anki tetrominoyu oluþtur   
            
            //Tetromino türlerini güncelle
            Current_Rnd_Tetromino = Next_Rnd_Tetromino;
            Next_Rnd_Tetromino = UnityEngine.Random.Range(0, 7);
        }
    }

    public void Set_Tetromino_Block(bool value)
    {
        create_tetromino = value;
    }
}
