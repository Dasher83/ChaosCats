using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaosCats
{
    [CreateAssetMenu(fileName = "InteractableObject", menuName = "ScriptableObjects/InteractableObject")]
    public class InteractableObject : ScriptableObject
    {
        public bool canInteract;
        public bool canHide;
    }
}
