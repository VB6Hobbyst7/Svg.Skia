﻿using System.Collections.Generic;

namespace Svg.Model
{
    public class AddPolyPathCommand : PathCommand
    {
        public IList<Point>? Points;
        public bool Close;

        public AddPolyPathCommand(IList<Point> points, bool close)
        {
            Points = points;
            Close = close;
        }
    }
}
