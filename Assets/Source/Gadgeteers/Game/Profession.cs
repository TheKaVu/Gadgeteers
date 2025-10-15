using System.Collections.Generic;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [CreateAssetMenu(fileName = "New Profession", menuName = "Profession", order = 0)]
    public class Profession : ScriptableObject
    {
        [SerializeField] private EffectUnit _kit;
        [SerializeField] private List<EffectUnit> _perks;
    }
}