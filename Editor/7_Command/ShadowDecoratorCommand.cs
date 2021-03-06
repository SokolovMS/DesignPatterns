﻿using System;

namespace Editor
{
    internal class ShadowDecoratorCommand : ACommand
    {
        private IFigure f;
        private CompositeFigure cf;

        public ShadowDecoratorCommand(CompositeFigure figures, IFigure f)
        {
            this.f = f;
            this.cf = figures;
        }

        public override void Execute()
        {
            IFigure tmp = new ShadowDecorator(f);
            cf.Replace(f, tmp);
        }
    }
}