using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;

// tutorial https://www.youtube.com/watch?v=m139Reb3hmQ
public class Script_MultiL : MonoBehaviour
{
        private void Awake()
        {
                // read csv file -- from GoogleSheets
                LocalizationManager.Read();
    
                // autocompleted -- throw new NotImplementedException();
                
                // switch between Eng/Span automatically
                switch (Application.systemLanguage)
                {
                        case SystemLanguage.English:
                                LocalizationManager.Language = "English";
                                break;
                        case SystemLanguage.Spanish:
                                LocalizationManager.Language = "Spanish";
                                break;
                }
        }

        
}
