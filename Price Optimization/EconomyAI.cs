using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ceriumin
{
    public class EconomyAI : MonoBehaviour
    {
        //Version 1.0.2 A
        
        [Header("Price Statistics")]
        [Tooltip("Set your desired price"), SerializeField]   
        private float originalPrice;
        [Tooltip("The price it is adjusted from"), SerializeField]   
        private float beforeAdjustment;
        [Tooltip("The adjusted price after inflation"), SerializeField]  
        private float adjustedPrice;
        [SerializeField]    
        private string inflationRate;
        [Space(5)]

        [Header("Independent Variables")]
        [Range(0, 100), SerializeField]
        private int demand;
        [Range(0, 100), SerializeField]
        private int supply;
        [Space(5)]

        [Header("Debug"), Tooltip("Weight affects how strong the price change is")]
        [SerializeField]
        private float weight;
        [SerializeField]
        private float priceDelay;
        [SerializeField]
        private bool setPrice;

        private float setDelay;
        private float supplyRate = 0f;
        private float demandRate = 0f;

        void Update()
        {
            CalculatePrice();
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

            var inflate = ((adjustedPrice - originalPrice) / originalPrice) * 100;
            inflationRate = ((int)inflate).ToString() + "%";

            //Adjust price to 0 if it is negative
            
            if(adjustedPrice < 0)
                adjustedPrice = 0;

            if(originalPrice < 0)
                originalPrice = 0;
        }

        void CallDelay()
        {
            //Setting new price after specified delay
            
            setDelay -= Time.deltaTime;
            if(setDelay < 0 && setPrice)
            {
                beforeAdjustment = adjustedPrice;
                setDelay = priceDelay;
            }
        }
        
    }
}
