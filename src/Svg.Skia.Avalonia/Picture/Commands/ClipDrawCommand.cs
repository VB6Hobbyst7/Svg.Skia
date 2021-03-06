﻿using A = Avalonia;

namespace Svg.Skia.Avalonia
{
    public sealed class ClipDrawCommand : DrawCommand
    {
        public A.Rect Clip { get; }

        public ClipDrawCommand(A.Rect clip)
        {
            Clip = clip;
        }
    }
}
