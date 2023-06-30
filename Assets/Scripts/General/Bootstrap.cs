using System;
using System.Collections.Generic;
using General.Initialize;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace General
{
    public class Bootstrap : SerializedMonoBehaviour
    {
        [OdinSerialize] private List<IInitializable> _initializables = new List<IInitializable>();
        
        private void Awake()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            } 
        }
    }
}