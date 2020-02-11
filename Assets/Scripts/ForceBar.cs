using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceBar : MonoBehaviour {

    // Percent in the range is ((input - min) * 100) / (max - min)
    //private const float MAX_POWER = 11f;
    private const float MAX_POWER = 131f;
    //private const float MIN_POWER = -60f;
    private const float MIN_POWER = 60f;
    private const float BAR_MAX = 250f;

    public float maxDb = 90f;
    public float minDb = 45f;

    private float defaultY = 0f;
    private float adjustedDbs;
    private float percent;
    private Vector2 currentSize;
    private bool reset = false;

    [SerializeField] private GridLayoutGroup barControl;

	// Use this for initialization
	void Start () {
        defaultY = barControl.cellSize.y;
        currentSize = new Vector2(1f, defaultY);
        barControl.cellSize = currentSize;
        DataStore.barOn = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (DataStore.barOn)
        {
            //Debug.Log("STORED DB VALUE IS " + DataStore.savedDbValue.ToString());
            adjustedDbs = DataStore.savedDbValue + maxDb;
            //Debug.Log("ADJUSTED DB VALUE IS " + adjustedDbs.ToString());
            percent = (((adjustedDbs - minDb)) / (maxDb - minDb));
            currentSize.x =  percent > 1 ? BAR_MAX : percent * BAR_MAX;
            //Debug.Log("BAR IS AT " + currentSize.x.ToString());
            barControl.cellSize = currentSize;
            reset = false;
        }
        else if (!DataStore.barOn && !reset)
        {
            //Debug.Log("RESETTING!");
            currentSize.x = 0f;
            barControl.cellSize = currentSize;
            reset = true;
        }
	}
}
