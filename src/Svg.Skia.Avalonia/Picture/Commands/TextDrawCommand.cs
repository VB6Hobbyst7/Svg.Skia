﻿using A = Avalonia;
using AM = Avalonia.Media;

namespace Svg.Skia.Avalonia
{
    public sealed class TextDrawCommand : DrawCommand
    {
        public AM.IBrush? Brush { get; }
        public A.Point Origin { get; }
        public AM.FormattedText? FormattedText { get; }

        public TextDrawCommand(AM.IBrush? brush, A.Point origin, AM.FormattedText? formattedText)
        {
            Brush = brush;
            Origin = origin;
            FormattedText = formattedText;
        }
    }
}
