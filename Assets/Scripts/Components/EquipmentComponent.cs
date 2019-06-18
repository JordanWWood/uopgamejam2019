using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentComponent : MonoBehaviour
{
    [Serializable]
    public class EquipmentMap
    {
        public GameObject GameObject;
        public Boolean equiped;
    }
    
    public EquipmentMap[] enableMap;
}