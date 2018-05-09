using System;
using System.Collections;
using UnityEngine;


public class SimManager : MonoBehaviour
{

    public int NumberUnit;                              //Amount of units to create
    public GameObject[] UnitsBlue;                      //Array that holds the units
    public GameObject unitPrefabB;                      //UnitBlue prefab
    public GameObject[] UnitsRed;                       //array to hold red units
    public GameObject UnitPrefabR;                      //Red Prefab
    public GameObject[] UnitsGreen;                     //Array to hold Green units
    public GameObject UnitPrefabG;                      //Green Prefab
    public Vector2 spawnDistance = new Vector2(20, 20); //default spawn distance
    public float neighbourLimit = 1;                    //default neighbour limit
    public Vector2 goal = new Vector2(0, 0);            //default goal location
    public GameObject[] Blocks;
    public GameObject UnitPrefabBlock;
    public static int NumberBlocks;                     //amount of obstacles initialised
    public static int wantedblocks;                     //blocks that the user wants to create
    public GameObject Obstacle;                         //Obstacle prefab
    public Stack Squares;                               //Stack to hold the Obstacles
    public GameObject manager;
    public GameObject player;

	public System.Object pubOnject;

	public GameObject[] units;

    private void Awake()
    {
        //CheckNumbers();
        //CheckCreation();
        Squares = new Stack();
    }
    // Use this for initialization
    void Start()
    {
        CheckNumbers();
        CheckCreation();
    }
    // Update is called once per frame

    public void CheckCreation()         //checks which boolean values the user wants
    {
        
        bool bluetrue = Settings.blue;
        bool greentrue = Settings.green;
        bool redtrue = Settings.red;
        if (bluetrue == false)
        {
			var uBlue = new UnitBlue();
			CreateUnits(unitPrefabB, uBlue.GetType());
            Debug.Log("created blue");
        }
        else
        {
            Destroy(unitPrefabB);
        }
        if (greentrue == false)
        {
			var uGreen = new UnitGreen();
			CreateUnits(UnitPrefabG, uGreen.GetType());
        }
        else
        {
            Destroy(UnitPrefabG);
        }
        if (redtrue == false)
        {
            var uRed = new UnitRed();
			CreateUnits(UnitPrefabR, uRed.GetType()) ;
        }
        else
        {
            Destroy(UnitPrefabR);
        }
        if (Settings.player == true)
        {
            Destroy(player);
        }
    }
    public void CheckNumbers()          //assigns the integer settings from the menu scene
    {
        NumberUnit = Settings.intFlockMembers;
        wantedblocks = Settings.intamountObstacles;
    }
    /// <summary>
    /// next three methods create the units for each of the desired colour groups
    /// </summary>
    
	public void CreateUnits(GameObject unitsPrefab, Type givenType) {
		units = new GameObject[NumberUnit];
		for (int count = 0; count < NumberUnit - 1; count ++) {
			Vector3 unitPosition = new Vector2(UnityEngine.Random.Range(spawnDistance.x, spawnDistance.y), UnityEngine.Random.Range(spawnDistance.x, spawnDistance.y)); units[count] =
				Instantiate(unitsPrefab, this.transform.position + unitPosition, Quaternion.identity);
			pubOnject = Activator.CreateInstance(givenType);
			Type diamondType = givenType;
			//units[count].GetComponent<gameObject.GetComponent(givenType)>().manager = this.gameObject;
		}
	}
} 