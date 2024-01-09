using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRandom : MonoBehaviour
{
    public Transform Terrain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BuildGenerator()
    {
        InitialiseMap();
        Generate(5, 5);
        DrawMap();
    }

    public List<MapLocation> directions = new List<MapLocation>() {
                                            new MapLocation(1,0),
                                            new MapLocation(0,1),
                                            new MapLocation(-1,0),
                                            new MapLocation(0,-1) };
    public int width = 7;
    public int depth = 7;
    public byte[,] map;
    public int scale = 6;


   

    void InitialiseMap()
    {
        map = new byte[width, depth];
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, z] = 1;  // 1�� ��, 0�� ���
            }
        }
    }

    public void Generate(int x, int z)
    {
        //for (int z = 0; z < depth; z++)
        //    for (int x = 0; x < width; x++)
        //    {
        //       //if(Random.Range(0,100) < 50)
        //         map[x, z] = 0;     //1 = wall  0 = corridor
        //    }

        // 4���� �� 2���� �̻��� �����ϰ�� || �ڽ��� ������ ���� �߰�
        if (CountSquareNeighbours(x, z) >= 2 || map[x, z] == 0)
        {
            return;
        }

        map[x, z] = 0;

        directions.Shuffle();

        Generate(x + directions[0].x, z + directions[0].z);  // 6,5
        Generate(x + directions[1].x, z + directions[1].z);  // 5,6
        Generate(x + directions[2].x, z + directions[2].z);  // 4,5 
        Generate(x + directions[3].x, z + directions[3].z);  // 5,4
    }

    void Shuffle(List<MapLocation> list)
    {
        int n = list.Count;
        // ����Ʈ�� ũ�⸸ŭ �ݺ�
        while (n > 1)
        {
            n--;
            System.Random random = new System.Random();
            // 0 ���� ũ�ų� ���� int32.MaxValue ���� ���� 32��Ʈ ��ȣ �ִ� ���� �Դϴ�.
            int k = random.Next(n + 1);
            MapLocation value = list[k];
            list[k] = list[n];
            list[n] = value;
            //T value = list[k];
            //list[k] = list[n];
            //list[n] = value;
        }
    }

    void DrawMap()
    {
        for (int z = 0; z < depth; z++)
            for (int x = 0; x < width; x++)
            {
                if (map[x, z] == 1)
                {
                    Vector3 pos = new Vector3(x * scale, 0, z * scale);
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.localScale = new Vector3(scale, scale, scale);
                    wall.transform.position = pos;
                    wall.transform.parent = Terrain;
                    wall.name = x + "," + z;
                }
            }
    }

    /// <summary>
    /// 4���� ������ �˻��Ѵ�.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x - 1, z] == 0) count++;
        if (map[x + 1, z] == 0) count++;
        if (map[x, z + 1] == 0) count++;
        if (map[x, z - 1] == 0) count++;
        return count;
    }

    /// <summary>
    /// �밢�� ������ �˻��Ѵ�.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public int CountDiagonalNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x - 1, z - 1] == 0) count++;
        if (map[x + 1, z + 1] == 0) count++;
        if (map[x - 1, z + 1] == 0) count++;
        if (map[x + 1, z - 1] == 0) count++;
        return count;
    }

    /// <summary>
    /// ���� �ִ� ��ü ���� �˻��Ѵ�.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public int CountAllNeighbours(int x, int z)
    {
        return CountSquareNeighbours(x, z) + CountDiagonalNeighbours(x, z);
    }
}
