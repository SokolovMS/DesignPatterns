﻿using System;
using System.Collections.Generic;

namespace Editor
{
    class CompositeFigure : AFigure
    {
        private List<IFigure> children = new List<IFigure>();

        // Constructor
        public CompositeFigure()
        {
            SetName("CompositeFigure");
        }
        public CompositeFigure(params IFigure[] array)
        {
            children.AddRange(array);
        }

        public override IFigure Clone()
        {
            CompositeFigure res = new CompositeFigure();
            foreach (IFigure f in children)
            {
                res.Add(f.Clone());
            }
            return res;
        }

        public override double Area()
        {
            Double res = 0.0;
            foreach (IFigure pf in children)
            {
                res += pf.Area();
            }
            return res;
        }
        public override double Perimeter()
        {
            Double res = 0.0;
            foreach (IFigure pf in children)
            {
                res += pf.Perimeter();
            }
            return res;
        }


        public void Add(IFigure component)
        {
            children.Add(component.Clone());
        }
        public void Remove(IFigure component)
        {
            children.Remove(component);
        }

        public override void Show(int lvl = 0)
        {
            DrawText(new String('-', lvl * 2) + GetName() + " : P="+Perimeter()+",S=" +Area() + Environment.NewLine);

            foreach (IFigure pf in children)
            {
                pf.Show(lvl + 1);
            }
        }

        public override void SetShower(IShower shower)
        {
            base.SetShower(shower);

            foreach (IFigure pf in children)
            {
                pf.SetShower(shower);
            }
        }

        internal IFigure Get(int ind)
        {
            if (ind < 0 || ind >= children.Count)
                throw new IndexOutOfRangeException("Can't get elem by ind in CompositeFigure.Get");

            return children[ind];
        }

        public override void MoveTo(Point x)
        {
            foreach (IFigure pf in children)
            {
                pf.MoveTo(x);
            }
        }
        public override void MoveOn(Point dx)
        {
            foreach (IFigure pf in children)
            {
                pf.MoveOn(dx);
            }
        }
        public override Point[] GetBorder()
        {
            Double minX = Double.PositiveInfinity, maxX = Double.NegativeInfinity;
            Double minY = Double.PositiveInfinity, maxY = Double.NegativeInfinity;

            Point[] arrP;

            foreach (IFigure pf in children)
            {
                arrP = pf.GetBorder();
                if (arrP.Length != 2)
                    throw new Exception("CompositeFigure : GetBorder : arrP.Length != 2");

                if (arrP[0].X < minX) minX = arrP[0].X;
                if (arrP[1].X > maxX) maxX = arrP[1].X;
                if (arrP[0].Y < minY) minY = arrP[0].Y;
                if (arrP[1].Y > maxY) maxY = arrP[1].Y;
            }

            return new Point[2] { new Point(minX, minY), new Point(maxX, maxY) };
        }
        public override void ShowShadow(int lvl, IShower shower, Point dx)
        {
            DrawText(new String('+', lvl * 2) + "shadow CompositeFigure" + Environment.NewLine);

            foreach (IFigure f in children)
                f.ShowShadow(lvl, shower, dx);
        }
        public override void ShowBorder(int lvl, IShower shower)
        {
            DrawText(new String('+', lvl * 2) + "border CompositeFigure" + Environment.NewLine);

            Point[] border = GetBorder();
            Point[] poligon = new Point[4];
            poligon[0] = new Point(border[0].X, border[0].Y);
            poligon[1] = new Point(border[0].X, border[1].Y);
            poligon[2] = new Point(border[1].X, border[1].Y);
            poligon[3] = new Point(border[1].X, border[0].Y);
            FillPoligon(shower, poligon);
        }

        #region 6_Strategy
        public void Sort(IStrategy strategy)
        {
            if (strategy == null)
                throw new ArgumentNullException("Sort : strategy == null");

            strategy.Sort(children);
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            CompositeFigure f = obj as CompositeFigure;
            if (f == null) return false;

            bool b = true;
            if (children.Count != f.children.Count)
                return false;
            for (int i = 0; i < children.Count; i++)
                b &= children[i].Equals(f.children[i]);

            return b;
        }

        public void RemoveAllChilds()
        {
            children.RemoveRange(0, children.Count);
        }
        public void Replace(IFigure oldF, IFigure newF)
        {
            int i = children.FindIndex(x => x.Equals(oldF));
            if (i < 0 || i >= children.Count)
                throw new IndexOutOfRangeException("Can't find figure to CompositeFigure.Replace");

            children[i] = newF;
        }
    }
}
