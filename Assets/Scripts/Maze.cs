using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class MapLocation
{
    public int x;
    public int z;

    public MapLocation(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public Vector2 ToVector()
    {
        return new Vector2(x, z);
    }

    public static MapLocation operator +(MapLocation a, MapLocation b)
       => new MapLocation(a.x + b.x, a.z + b.z);

    public bool Equals(MapLocation other)
    {
        if(x == other.x && z == other.z)
        {
            return true;
        }
        return false;
    }

    //public override bool Equals(object obj)
    //{
    //    if ((obj == null) || !this.GetType().Equals(obj.GetType()))
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return x == ((MapLocation)obj).x && z == ((MapLocation)obj).z;
    //    }
    //}

    //public override int GetHashCode()
    //{
    //    return 0;
    //}

}

public class Maze : MonoBehaviour
{
    public List<MapLocation> directions = new List<MapLocation>() {
                                            new MapLocation(1,0),
                                            new MapLocation(0,1),
                                            new MapLocation(-1,0),
                                            new MapLocation(0,-1) };
    public int width = 30;
    public int depth = 30;
    public byte[,] map;
    public int scale = 6;


    // Start is called before the first frame update
    void Start()
    {
        InitialiseMap();
        Generate(5,5);
        DrawMap();
    }

    void InitialiseMap()
    {
        map = new byte[width, depth];
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, z] = 1;  // 1은 벽, 0은 통로
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

        // 4방위 중 2방위 이상이 복도일경우 || 자신이 복도일 때를 추가
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
        // 리스트의 크기만큼 반복
        while (n > 1)
        {
            n--;
            System.Random random = new System.Random();
            // 0 보다 크거나 같고 int32.MaxValue 보다 작은 32비트 부호 있는 정수 입니다.
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
                }
            }
    }

    /// <summary>
    /// 4방위 복도를 검색한다.
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
    /// 대각선 복도를 검색한다.
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
    /// 갈수 있는 전체 길을 검색한다.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public int CountAllNeighbours(int x, int z)
    {
        return CountSquareNeighbours(x, z) + CountDiagonalNeighbours(x, z);
    }
}
