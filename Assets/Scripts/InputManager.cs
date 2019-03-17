using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public Transform GridsParent;
    public int seed = 0;
    public float MutationProb = .1f;
    private HexGrid[] Grids;

    void Start()
    {
        Grids = GridsParent.GetComponentsInChildren<HexGrid>();

        if (seed != 0)
        {
            Random.InitState(seed);
            foreach (HexGrid grid in Grids)
                //change the color of each cell according to the given seed.
                for (int i = 0; i < grid.cells.Length; i++)
                    grid.ColorCell(i, HexGrid.colors[(int)Random.Range(0, 4)]);
        }

        Mutator.MutationProb = this.MutationProb;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    string temp;

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            //TouchCell(hit.point);
            GameObject gridObj = hit.transform.parent.gameObject;
            HexGrid grid = gridObj.GetComponent<HexGrid>();

            print(gridObj.name);
            if (temp == null)
            {
                temp = grid.Serialize();
                print(temp);
            }
            else
            {
                temp = Mutator.Mutate(temp);
                print(temp);
                grid.Deserialize(temp);
                temp = null;
            }

        }
    }
    
}
