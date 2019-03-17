using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public Transform GridsParent;
    public int seed = 0;
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
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            //TouchCell(hit.point);
            GameObject grid = hit.transform.parent.gameObject;
            print(grid.name);
            print(grid.GetComponent<HexGrid>().Serialize());
        }
    }
   
}
