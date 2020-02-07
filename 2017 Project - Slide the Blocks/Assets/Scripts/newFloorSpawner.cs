using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newFloorSpawner : MonoBehaviour {



    public bool DOUNLOCK;
    public bool UNLOCKED = false;
    public int UNLOCKINDEX = 5;
    private Vector3 unlockTargetPos;
    public Transform unlockBlock;
  

    [SerializeField] private Transform player;

    [SerializeField] private GameObject Block; // to remove
    [SerializeField] private GameObject[] Blocks = new GameObject[3];
    public enum BlockType { Normal, LeftWedge, RightWedge, UI}

    private float gapBetweenBlocks;
    private float blockWidth;
  

    [Range(-1,1)]
    public float[] startBlocks;
    int BlockIndex;

    [SerializeField] private Gradient BlocksGradient;
    private float gradientStartOffset;
    [SerializeField] private int GradientSteps = 100;

    public enum SpawnType {Centre, Random, Snake, Left, Right, Test1, Test2}
    public SpawnType nextType;
    [SerializeField] private float randomRange;

    float prevSpawn = 0;



  void Start () {

       

        Initialize();      
       
        
	}
	
	// Update is called once per frame
	void Update () {



        if (DOUNLOCK)
        {

           // print(Vector3.Distance(unlockBlock.localPosition, unlockTargetPos));
            if(unlockBlock)
            if(Vector3.Distance(unlockBlock.localPosition, unlockTargetPos) < 1)
            {
               // print("HERE");
                unlockBlock.position = unlockTargetPos;
                UNLOCKED = true;
                player.GetComponent<NewPlayerController>().MOVE = true;
            }
        }

	}

    void Initialize()
    {

        BlockType[] startTypes = startTypes = new BlockType[startBlocks.Length]; 

        Block.transform.position = Vector3.zero;     
        gapBetweenBlocks = Block.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * Block.transform.lossyScale.y;
       // gapBetweenBlocks *= .95f;
        blockWidth = Block.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * Block.transform.lossyScale.x;

        if (!DOUNLOCK)
        {
            UNLOCKED = true;
            player.GetComponent<NewPlayerController>().MOVE = true;



        }
        else
        {
            for (int i = 0; i < startBlocks.Length; i++)
            {
                startBlocks[i] = 0;

            }


            startBlocks[UNLOCKINDEX] = 0.75f;

           
            startTypes[UNLOCKINDEX] = BlockType.UI;

           
        }

        gradientStartOffset = Random.value;

        spawnBlocks(startBlocks, startTypes);
    }

    void SpawnBlock(float xOffset)
    {
        SpawnBlock(xOffset, BlockType.Normal);
     
    } 


    void SpawnBlock(float xOffset, BlockType type)
    {
        Vector3 spawnPos = new Vector3(xOffset * blockWidth, 0, BlockIndex * gapBetweenBlocks);

        GameObject temp;
        temp = Blocks[(int)type];    

        temp.transform.position = spawnPos;

        GameObject Instance = Instantiate(temp, transform);

        if (type == BlockType.UI)
        {
            unlockTargetPos = new Vector3(0, 0, UNLOCKINDEX * gapBetweenBlocks);           
            unlockBlock = Instance.transform;

        }

        Instance.GetComponent<Renderer>().material.color = getColorFromGradient(BlockIndex);


        prevSpawn = xOffset;
        BlockIndex++;
    }

    Color getColorFromGradient(int index)
    {
        float step01 = gradientStartOffset + ((float)index / (float)GradientSteps);
        while (step01 > 1) step01 -= 1;
        
        return BlocksGradient.Evaluate(step01);

    }

    void spawnBlocks(float[] xOffsets, BlockType[] types)
    {

        for (int i = 0; i < xOffsets.Length; i++)
        {

            if(types.Length > i)
            {
                SpawnBlock(xOffsets[i], types[i]);
            }
            
            else
            {
                SpawnBlock(xOffsets[i],BlockType.Normal);
            }
        }

    
    }

    void spawnBlocks(float[] xOffsets)
    {
       
        foreach (var offset in xOffsets)
        {
            SpawnBlock(offset);

           
        }
    }




    public void Spawn(int amount)
    {

        SpawnType type = SpawnType.Snake;

        if (BlockIndex > 25)
        {
            if (Random.value > 0.9f) type = (Random.value > 0.5) ? SpawnType.Left : SpawnType.Right;
        }

        spawn(amount, type);
    }


    public void spawn(int amount, SpawnType spawnType)
    {
        float[] toSpawn = new float[amount];
        BlockType[] types = new BlockType[amount];

        //print(spawnType.ToString());

        switch (spawnType)
        {
            case SpawnType.Centre:

                for (int i = 0; i < toSpawn.Length; i++)
                {
                    toSpawn[i] = 0;
                    types[i] = BlockType.Normal;
                
                }

                break;
            case SpawnType.Random:
                //add the max and min for random spawning

                for (int i = 0; i < toSpawn.Length; i++)
                {
                    toSpawn[i] = Random.Range(-randomRange, randomRange);
                }

                break;

            case SpawnType.Snake:
                for (int i = 0; i < toSpawn.Length; i++)
                {
                    //print(player.position.x);

                    toSpawn[i] = prevSpawn + Random.Range(-0.3f, 0.3f);
                    toSpawn[i] = Mathf.Clamp(toSpawn[i], player.position.x / blockWidth - 0.75f, player.position.x / blockWidth + 0.75f);
                    // if (toSpawn[i] > 1.2f) toSpawn[i] = 1.2f;
                    // if (toSpawn[i] < -1.2f) toSpawn[i] = -1.2f;

                }
                break;

            case SpawnType.Test1:
                break;
            case SpawnType.Test2:
                break;

            case SpawnType.Left:

                for (int i = 0; i < toSpawn.Length; i++)
                {
                    toSpawn[i] = prevSpawn;
                    types[i] = BlockType.LeftWedge;
                }

                break;

            case SpawnType.Right:

                for (int i = 0; i < toSpawn.Length; i++)
                {
                    toSpawn[i] = prevSpawn;
                    types[i] = BlockType.RightWedge;
                }

                break;

            default:
                break;
        }

        spawnBlocks(toSpawn,types);

    }
}
