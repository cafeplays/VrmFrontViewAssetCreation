using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;

    public class ParentTargetBone : MonoBehaviour
    {
        [SerializeField] public Transform targetTransform;
        [SerializeField] public Vector3 offsets;
        [SerializeField] public Vector3 rotOffsets;
        public VRIK vrik;
    }
