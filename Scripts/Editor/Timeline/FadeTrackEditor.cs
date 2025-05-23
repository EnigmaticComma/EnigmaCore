﻿using UnityEditor.Timeline;
using UnityEngine.Timeline;

namespace EnigmaCore.Timeline {
    [CustomTimelineEditor(typeof(FadeData))]
    public class FadeDataEditor : ClipEditor {

        public override ClipDrawOptions GetClipOptions(TimelineClip clip)
        {
            var fade = (FadeData) clip.asset;
            var clipOptions = base.GetClipOptions(clip);
            if (fade != null) {
                var c = fade.data.Color;
                c.a = fade.data.MaxAlpha;
                clipOptions.highlightColor = c;
            }
            return clipOptions;
        }

    }
}
