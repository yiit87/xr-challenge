using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public string fileNameToLoad;

    public int mapWidth;
    public int mapHeight;

    public GameObject Wall;
    public GameObject EndLevel;

    public GameObject Trap1;

    public GameObject Star;
    public GameObject FasterPlayer;
    public GameObject ExtraHealth;

    private int[,] tiles;

    void Awake()
    {
        string levelSelected = fileNameToLoad + ".txt";
        string combine = Path.Combine(Application.dataPath + "/LevelGenerate/", levelSelected);

        tiles = Load(combine);
        
        BuildMap();
    }

    void BuildMap()
    {
        GameObject map = new GameObject();
        map.name = "Map";
        map.transform.position = Vector3.zero;

        Debug.Log("Building Map...");
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j] == 0)
                {
                    continue;
                }
                else if (tiles[i, j] == 1)
                {
                    GameObject TilePrefab = Instantiate(Wall, new Vector3(j - mapWidth,0, mapHeight - i), Quaternion.identity) as GameObject;
                    TilePrefab.transform.SetParent(map.transform);
                }
                else if (tiles[i, j] == 2)
                {
                    GameObject TilePrefab = Instantiate(EndLevel, new Vector3(j - mapWidth, 0, mapHeight - i), Quaternion.identity) as GameObject;
                    TilePrefab.transform.SetParent(map.transform);
                }
                else if (tiles[i, j] == 3)
                {
                    GameObject TilePrefab = Instantiate(Trap1, new Vector3(j - mapWidth, 0, mapHeight - i), Quaternion.identity) as GameObject;
                    TilePrefab.transform.SetParent(map.transform);
                }
                else if (tiles[i, j] == 4)
                {
                    GameObject TilePrefab = Instantiate(ExtraHealth, new Vector3(j - mapWidth, 0, mapHeight - i), Quaternion.identity) as GameObject;
                    TilePrefab.transform.SetParent(map.transform);
                }
                else if (tiles[i, j] == 5)
                {
                    GameObject TilePrefab = Instantiate(FasterPlayer, new Vector3(j - mapWidth, 0, mapHeight - i), Quaternion.identity) as GameObject;
                    TilePrefab.transform.SetParent(map.transform);
                }
            }
        }
        Debug.Log("Building Completed!");
    }

    private int[,] Load(string filePath)
    {
        try
        {
            Debug.Log("Loading File...");

            using (StreamReader sr = new StreamReader(filePath))
            {
                string input = sr.ReadToEnd();
                string[] lines = input.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

                int[,] tiles = new int[lines.Length, mapWidth];

                Debug.Log("Parsing...");

                for (int i = 0; i < lines.Length; i++)
                {
                    string st = lines[i];
                    string[] nums = st.Split(new[] { ',' });
                    if (nums.Length != mapWidth)
                    {

                    }
                    for (int j = 0; j < Mathf.Min(nums.Length, mapWidth); j++)
                    {
                        int val;
                        if (int.TryParse(nums[j], out val))
                        {
                            tiles[i, j] = val;
                        }
                        else
                        {
                            tiles[i, j] = 1;
                        }
                    }
                }
                Debug.Log("Parsing Completed!");
                return tiles;
            }
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }
}
