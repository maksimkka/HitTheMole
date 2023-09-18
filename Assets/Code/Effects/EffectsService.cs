using System.Collections.Generic;
using UnityEngine;

namespace Code.Effects
{
    public class EffectsService : MonoBehaviour
    {
        [field: SerializeField] public List<EffectView> ParticleSystems { get; private set; }
    }
}