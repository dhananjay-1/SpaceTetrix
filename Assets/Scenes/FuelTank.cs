using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    static float fuel = 50f;
    static public int nextRocketLevel = 1;

    public static bool CheckFuel()
    {
        return fuel > 0f;
    }

    public static float GetFuel()
    {
        return fuel;
    }

    public static void SetFuel(float val)
    {
        fuel = val;
    }

    public static void ResetFuel()
    {
        fuel = 50f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
