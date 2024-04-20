﻿using SAIN.Attributes;

namespace SAIN.Preset.GlobalSettings
{
    public class CoverSettings
    {
        [Name("Enhanced Cover Finding")]
        [Description("CAN REDUCE PERFORMANCE. Improves bot reactions in a fight by decreasing the time it takes to find cover, can help with bots standing still occasionally before running for cover. Comes at the cost of some reduced performance overall.")]
        [Default(true)]
        public bool EnhancedCoverFinding = true;

        [Default(6f)]
        [MinMax(1f, 30f, 1f)]
        [Advanced]
        public float ShiftCoverChangeDecisionTime = 6f;

        [Default(30f)]
        [MinMax(2f, 120f, 1f)]
        [Advanced]
        public float ShiftCoverTimeSinceSeen = 30f;

        [Default(30f)]
        [MinMax(1f, 120f, 1f)]
        [Advanced]
        public float ShiftCoverTimeSinceEnemyCreated = 30f;

        [Default(10f)]
        [MinMax(1f, 120f, 1f)]
        [Advanced]
        public float ShiftCoverNoEnemyResetTime = 10f;

        [Default(10f)]
        [MinMax(1f, 120f, 1f)]
        [Advanced]
        public float ShiftCoverNewCoverTime = 10f;

        [Default(10f)]
        [MinMax(1f, 120f, 1f)]
        [Advanced]
        public float ShiftCoverResetTime = 10f;

        [Default(0.75f)]
        [MinMax(0.5f, 1.5f, 100f)]
        [Advanced]
        public float CoverMinHeight = 0.75f;

        [Default(8f)]
        [MinMax(0f, 30f, 1f)]
        [Advanced]
        public float CoverMinEnemyDistance = 8f;

        [Default(0.25f)]
        [MinMax(0.01f, 1f, 100f)]
        [Advanced]
        public float CoverUpdateFrequency = 0.25f;

        [Default(false)]
        [Advanced]
        public bool DebugCoverFinder = false;
    }
}