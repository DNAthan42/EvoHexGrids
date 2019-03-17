using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    public Transform GridsParent;
    public int seed = 0;
    public float MutationProb = .1f;
    public int Crossover = 10;
    private HexGrid[] Grids;

    void Start()
    {
        Grids = GridsParent.GetComponentsInChildren<HexGrid>();

        if (seed != 0)
        {
            Random.InitState(seed);
            foreach (HexGrid grid in Grids)
                //change the color of each cell according to the given seed.
                grid.Randomize();
        }

        Mutator.MutationProb = this.MutationProb;
        Mutator.CrossoverPoint = this.Crossover;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    string firstGrid;
    string secondGrid;

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            //TouchCell(hit.point);
            GameObject gridObj = hit.transform.parent.gameObject;
            HexGrid grid = gridObj.GetComponent<HexGrid>();
            //print(gridObj.name);

            secondGrid = firstGrid;
            firstGrid = grid.Serialize();
        }
    }

    public void NextGeneration(Text text)
    {
        if (firstGrid == null || secondGrid == null)
        {
            text.text = "Must Select Two Grids";
        }
        else
        {
            text.text = "Next Generation";

            //copy
            Grids[0].Deserialize(firstGrid);
            Grids[1].Deserialize(secondGrid);

            //mutations
            Grids[2].Deserialize(Mutator.Mutate(firstGrid));
            Grids[3].Deserialize(Mutator.Mutate(secondGrid));

            //crossovers
            string[] crosses = Mutator.Crossover(firstGrid, secondGrid);
            Grids[4].Deserialize(crosses[0]);
            Grids[5].Deserialize(crosses[1]);

            //random
            Grids[6].Randomize();
            Grids[7].Randomize();

            firstGrid = secondGrid = null;
        }
    }
    
}
