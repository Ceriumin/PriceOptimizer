using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyAI : MonoBehaviour
{
    [Header("Price Statistics")]
    [Tooltip("Set your desired price")]   
    public float originalPrice;
    [Tooltip("The price it is adjusted from")]   
    public float beforeAdjustment;
    [Tooltip("The adjusted price after inflation")]  
    public float adjustedPrice;
    public string inflationRate;
    [Space(5)]

    [Header("Independent Variables")]
    [Range(0, 100)]
    public int demand;
    [Range(0, 100)]
    public int supply;
    [Space(5)]

    [Header("Debug"), Tooltip("Weight affects how strong the price change is")]
    public float weight;
    public float priceDelay;
    private float setDelay;
    public bool setPrice;
    public float supplyRate = 0f;
    public float demandRate = 0f;

    void Update()
    {
        CalculatePrice();
        Debugging();
        CallDelay();
    }

    void Start()
    {
        beforeAdjustment = originalPrice;
        setDelay = priceDelay;
    }

    void CalculatePrice()
    {
        //Setting the Adjusted Equillibrium price
        demandRate = demand / weight;
        supplyRate = supply / weight;
        adjustedPrice = beforeAdjustment * (1f + -supplyRate + demandRate);
        adjustedPrice = Mathf.Round(adjustedPrice * 100f) / 100.0f;

        //Calculating Inflation Rate
        float inflate = ((adjustedPrice - originalPrice) / originalPrice) * 100;
        inflationRate = (((int)inflate).ToString() + "%");
    }

    void CallDelay()
    {
        //Setting new price after specified delay
        setDelay -= Time.deltaTime;
        if(setDelay < 0 && setPrice)
        {
            Debug.Log("Price has been Changed");
            beforeAdjustment = adjustedPrice;
            setDelay = priceDelay;
        }


    }

    void Debugging()
    {
        if(adjustedPrice < 0)
            adjustedPrice = 0;
        if(originalPrice < 0)
        {
            originalPrice = 0;
            Debug.Log("Set a price equal to or higher than 0");
        }
    }
}
