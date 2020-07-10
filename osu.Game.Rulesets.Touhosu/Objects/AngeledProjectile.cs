﻿using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Touhosu.Judgements;
using osu.Game.Rulesets.Judgements;

namespace osu.Game.Rulesets.Touhosu.Objects
{
    public class AngeledProjectile : Projectile
    {
        public float Angle { get; set; }

        public double SpeedMultiplier { get; set; } = 1;

        public double DeltaMultiplier { get; set; } = 1;

        public double? CustomTimePreempt { get; set; }

        public override Judgement CreateJudgement() => new TouhosuJudgement();

        protected override void ApplyDefaultsToSelf(ControlPointInfo controlPointInfo, BeatmapDifficulty difficulty)
        {
            base.ApplyDefaultsToSelf(controlPointInfo, difficulty);
            SpeedMultiplier = controlPointInfo.DifficultyPointAt(StartTime).SpeedMultiplier;

            if (CustomTimePreempt.HasValue)
                TimePreempt = CustomTimePreempt.Value;
        }
    }
}