using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

    static public Color[] colors = { Color.white, Color.blue, Color.green, Color.yellow, Color.red };
    public int width = 6;
	public int height = 6;

	public Color defaultColor = Color.white;

	public HexCell cellPrefab;
	public Text cellLabelPrefab;

    [HideInInspector]
	public HexCell[] cells;

	Canvas gridCanvas;
	HexMesh hexMesh;

	void Awake () {
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();

		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++)
			{
                Color c = Helper.RandomColor();
				CreateCell(x, z, i++, c );
			}
		}
	}

	void Start () {
		hexMesh.Triangulate(cells);
	}

	public void ColorCell (Vector3 position, Color color) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        ColorCell(index, color);
	}

    public void ColorCell(int index, Color color)
    {
		HexCell cell = cells[index];
		cell.color = color;
		hexMesh.Triangulate(cells);
    }

	void CreateCell (int x, int z, int i, Color c) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
		cell.color = c;

		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();
	}

    public string Serialize()
    {
        string result = "";
        for (int i = 0; i < cells.Length; i++)
        {
            //don't prepend a comma to the start of the list.
            result += (i == 0) ? "" : ",";

            result += $"{Helper.ColorToString[cells[i].color]}";
            //add other features of a gridspace here.
        }
        return result;
    }

    public void Deserialize(string source)
    {
        string[] data = source.Split(',');
        if (data.Length != cells.Length) print("Source length does not match grid size.");
        for (int i = 0; i < cells.Length; i++)
            //If you want to add new features to a cell, you'll need to do additional processign to string pieces here.
            ColorCell(i, Helper.StringToColor[data[i]]);
    }

    public void Randomize()
    {
        for (int i = 0; i < cells.Length; i++)
            ColorCell(i, Helper.RandomColor());
    }
}