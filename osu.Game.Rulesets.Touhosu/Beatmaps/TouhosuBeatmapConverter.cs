﻿using osu.Game.Beatmaps;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Touhosu.Objects;
using osu.Game.Rulesets.Touhosu.Extensions;

namespace osu.Game.Rulesets.Touhosu.Beatmaps
{
    public class TouhosuBeatmapConverter : BeatmapConverter<TouhosuHitObject>
    {
        public TouhosuBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasPosition);

        private int index = -1;
        private int objectIndexInCurrentCombo = 0;

        protected override IEnumerable<TouhosuHitObject> ConvertHitObject(HitObject obj, IBeatmap beatmap)
        {
            var comboData = obj as IHasCombo;
            if (comboData?.NewCombo ?? false)
            {
                objectIndexInCurrentCombo = 0;
                index++;
            }

            List<TouhosuHitObject> hitObjects = new List<TouhosuHitObject>();

            switch (obj)
            {
                case IHasPathWithRepeats curve:
                    hitObjects.AddRange(ProjectileExtensions.ConvertSlider(obj, beatmap, curve, index));
                    break;

                case IHasDuration endTime:
                    hitObjects.AddRange(ProjectileExtensions.ConvertSpinner(obj, endTime, index));
                    break;

                default:
                    hitObjects.AddRange(ProjectileExtensions.ConvertHitCircle(obj, index, objectIndexInCurrentCombo));
                    break;
            }

            objectIndexInCurrentCombo++;

            return hitObjects;
        }

        protected override Beatmap<TouhosuHitObject> CreateBeatmap() => new TouhosuBeatmap();
    }
}
