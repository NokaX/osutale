﻿using osu.Framework.Input.StateChanges;
using osu.Framework.Utils;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using osuTK;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Touhosu.Replays
{
    public class TouhosuFramedReplayInputHandler : FramedReplayInputHandler<TouhosuReplayFrame>
    {
        public TouhosuFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(TouhosuReplayFrame frame) => true;

        protected Vector2? Position
        {
            get
            {
                var frame = CurrentFrame;

                if (frame == null)
                    return null;

                return NextFrame != null ? Interpolation.ValueAt(CurrentTime.Value, frame.Position, NextFrame.Position, frame.Time, NextFrame.Time) : frame.Position;
            }
        }

        public override List<IInput> GetPendingInputs()
        {
            if (!Position.HasValue) return new List<IInput>();

            return new List<IInput>
            {
                new TouhosuReplayState
                {
                    PressedActions = CurrentFrame?.Actions ?? new List<TouhosuAction>(),
                    Position = Position.Value
                },
            };
        }

        public class TouhosuReplayState : ReplayState<TouhosuAction>
        {
            public Vector2? Position { get; set; }
        }
    }
}
