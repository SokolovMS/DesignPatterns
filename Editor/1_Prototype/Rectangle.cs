﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    class Rectangle : AFigure
    {
        public Point a1 { get; private set; }
        public Point a2 { get; private set; }
        public Point a3 { get; private set; }
        public Point a4 { get; private set; }

        public Rectangle(Point a1, Point a2, Point a3, Point a4)
        {
            SetName("Rectangle");
            this.a1 = a1;
            this.a2 = a2;
            this.a3 = a3;
            this.a4 = a4;
        }

        public override double Area()
        {
            return
                Point.TriangleArea(a1, a2, a3) +
                Point.TriangleArea(a1, a4, a3);
        }
        public override double Perimeter()
        {
            return 
                Point.DistanceBetween(a1, a2) + Point.DistanceBetween(a2, a3) +
                Point.DistanceBetween(a3, a4) + Point.DistanceBetween(a4, a1);
        }

        public override void Show(int lvl)
        {
            DrawText(new String('-', lvl * 2) + GetName() + " : P=,S=");
            DrawPoligon(a1, a2, a3, a4);
        }

        public override void MoveTo(Point x)
        {
            a1.MoveTo(x);
            a2.MoveTo(x);
            a3.MoveTo(x);
            a4.MoveTo(x);
        }
        public override void MoveOn(Point dx)
        {
            a1.MoveOn(dx);
            a2.MoveOn(dx);
            a3.MoveOn(dx);
            a4.MoveOn(dx);
        }
    }
}
