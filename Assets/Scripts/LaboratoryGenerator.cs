using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaboratoryGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public int numFloors = 2;
    public int numRoomsPerFloor = 4;
    public float roomWidth = 5f;
    public float roomHeight = 5f;
    public float floorHeight = 3f;

    private GameObject[,] floors;
    private GameObject[,] walls;
    private GameObject[,] doors;

    void Start()
    {
        // Generate floors
        floors = new GameObject[numFloors, numRoomsPerFloor];
        for (int i = 0; i < numFloors; i++)
        {
            for (int j = 0; j < numRoomsPerFloor; j++)
            {
                floors[i, j] = Instantiate(floorPrefab, new Vector3(j * roomWidth, i * floorHeight, 0), Quaternion.identity);
            }
        }

        // Generate walls
        walls = new GameObject[numFloors, numRoomsPerFloor];
        for (int i = 0; i < numFloors; i++)
        {
            for (int j = 0; j < numRoomsPerFloor; j++)
            {
                walls[i, j] = Instantiate(wallPrefab, new Vector3(j * roomWidth, i * floorHeight + roomHeight / 2f, 0), Quaternion.identity);
            }
        }

        // Generate doors
        doors = new GameObject[numFloors - 1, numRoomsPerFloor];
        for (int i = 0; i < numFloors - 1; i++)
        {
            for (int j = 0; j < numRoomsPerFloor; j++)
            {
                doors[i, j] = Instantiate(doorPrefab, new Vector3(j * roomWidth, i * floorHeight + floorHeight, 0), Quaternion.identity);
            }
        }
    }
}
