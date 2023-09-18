using Cinemachine;
using Code.Grid;
using UnityEngine;

namespace Code.CameraSystem
{
    public class CameraFollowSettings : MonoBehaviour
    {
        [field: SerializeField] public CinemachineVirtualCamera FollowCamera { get; private set; }
        [field: SerializeField] public GameObject CenterGridPoint { get; private set; }
        [field: SerializeField, Range(1f, 5f)] public float PositionMultiplier { get; private set; }
        
    }
}