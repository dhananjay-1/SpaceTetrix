using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
    static TextMesh scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = GetComponent<TextMesh>();
        ShowFuelLevel();
    }

    static private void ShowFuelLevel()
    {
        float fuel = FuelTank.GetFuel();
        scoreBoard.text = string.Format("Fuel : {0:#.0}%", fuel);
    }

    static public void AddFuel() // fuel is added when tetrix line is completed
    {
        float fuel = FuelTank.GetFuel();
        fuel += 10f;
        if (fuel > 100f)
        {
            fuel = 100f;
        }

        FuelTank.SetFuel(fuel);
        ShowFuelLevel();
    }

    static public bool DecreaseFuel()  // fuel is decreased when rocket thrust is used, this function returns false if fuel is finished and returns true otherwise
    {
        float fuel = FuelTank.GetFuel();
        fuel -= 0.1f;

        bool isFuelLeft = true;

        if (fuel <= 0f)
        {
            fuel = 0f;
            isFuelLeft = false;
        }

        FuelTank.SetFuel(fuel);
        ShowFuelLevel();

        return isFuelLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
