using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Statistics
{
    public class CharacterScheme
    {
        public const float VITALITY_PTS = 20;
        public const float SUSTAIN_PTS = 10;
        public const float CONDITION_PTS = 18;
        public const float CHARGEFLOW_PTS = 20;

        private float _vitalityIndex;
        private float _sustainIndex;
        private Vector2 _conditionIndex;
        private float _chargeflowIndex;

        public List<Stat> CreateStatsList()
        {
            List<Stat> list = new()
            {
                new(StatType.Vitality, VITALITY_PTS),
                new(StatType.MaxHealth, 50, new(StatType.Vitality, _vitalityIndex * 10)),
                new(StatType.HealthRegen, 0.2f, new(StatType.Vitality, (1 - _vitalityIndex) * 0.1f)),
                new(StatType.Chargeflow, CHARGEFLOW_PTS),
                new(StatType.MaxEnergy, 100, new(StatType.Chargeflow, _chargeflowIndex * 20)),
                new(StatType.EnergyRegen, 0.1f, new(StatType.Chargeflow, (1 - _chargeflowIndex) * 0.1f)),
                new(StatType.Sustain, SUSTAIN_PTS),
                new(StatType.Resistance, 3, new(StatType.Sustain, _sustainIndex)),
                new(StatType.Immunity, 1, new(StatType.Sustain, 1 - _sustainIndex)),
                new(StatType.Condition, CONDITION_PTS),
                new(StatType.Strength, 2, new(StatType.Condition, _conditionIndex.x)),
                new(StatType.Movespeed, 5, new(StatType.Condition, _conditionIndex.y * 0.8f)),
                new(StatType.Accuracy, 0, new(StatType.Condition, 1 - _conditionIndex.x - _conditionIndex.y))
            };

            return list;
        }
    }
}
