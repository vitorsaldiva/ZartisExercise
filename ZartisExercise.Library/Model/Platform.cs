using System;
using System.Collections.Generic;
using System.Text;

namespace ZartisExercise.Library.Model
{
    public class Platform
    {
        public (int x, int y) InitialPosition { get; private set; }
        public int Size { get; private set; }
        public int Unit { get; private set; }

        public Platform((int, int) initialPostion, int size, int unit)
        {
            InitialPosition = initialPostion;
            Size = size;
            Unit = unit;
        }
    }
}
