﻿using System;

namespace Editor
{
    public class Circle : AFigure
    {
        public Point Center { get; private set; }
        public double R { get; private set; }

        public Circle(Point center, Double r)
        {
            SetName("Circle");
            this.Center = new Point(center);
            this.R = r;
        }
        public override IFigure Clone() { return new Circle(Center, R); }

        public override Double Area()
        {
            return Math.PI * R * R;
        }
        public override Double Perimeter()
        {
            return 2 * Math.PI * R;
        }

        public override void Show(int lvl = 0)
        {
            DrawText(new String('-', lvl * 2) + GetName() + " : P=" + Perimeter() + ",S=" + Area());
            FillEllipse(Center, R);
        }

        public override void MoveTo(Point x) { Center.MoveTo(x); }
        public override void MoveOn(Point dx) { Center.MoveOn(dx); }

        public override Point[] GetBorder()
        {
            Point[] res = new Point[2];
            res[0] = new Point(Center.X - R, Center.Y - R);
            res[1] = new Point(Center.X + R, Center.Y + R);
            return res;
        }
        public override void ShowShadow(int lvl, IShower shower, Point dx)
        {
            DrawText(new String('+', lvl * 2) + "shadow");

            Point newCenter = Center + dx;
            FillEllipse(shower, newCenter, R);
        }
        public override void ShowBorder(int lvl, IShower shower)
        {
            DrawText(new String('+', lvl * 2) + "border");

            Point[] border = GetBorder();
            Point[] poligon = new Point[4];
            poligon[0] = new Point(border[0].X, border[0].Y);
            poligon[1] = new Point(border[0].X, border[1].Y);
            poligon[2] = new Point(border[1].X, border[1].Y);
            poligon[3] = new Point(border[1].X, border[0].Y);
            FillPoligon(shower, poligon);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Circle f = obj as Circle;
            if (f == null) return false;

            if (R.Equals(f.R) && Center.Equals(f.Center))
                return true;
            return false;
        }
    }
}
