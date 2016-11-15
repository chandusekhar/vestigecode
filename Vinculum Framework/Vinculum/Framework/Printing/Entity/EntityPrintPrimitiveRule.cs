namespace Vinculum.Framework.Printing.Entity
{
    using System;
    using System.Drawing;
    using System.Windows.Forms.VisualStyles;

    public class EntityPrintPrimitiveRule : IPrintPrimitive
    {
        private float _height;
        private HorizontalAlign? _printAlign;
        private Brush _printBrush;
        private Font _printFont;

        public EntityPrintPrimitiveRule(Brush printBrush, Font printFont) : this(printBrush, printFont, 3f)
        {
        }

        public EntityPrintPrimitiveRule(Brush printBrush, Font printFont, float height)
        {
            this._printBrush = printBrush;
            this._printFont = printFont;
            this._height = height;
            this._printAlign = new HorizontalAlign?(HorizontalAlign.Left);
        }

        public float CalculateHeight(Graphics gc)
        {
            return this.Height;
        }

        public void Draw(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds)
        {
            Pen p = new Pen(this.PrintBrush, 1f);
            gc.DrawLine(p, (float) elementBounds.Left, yPos + 2f, (float) elementBounds.Right, yPos + 2f);
        }

        public float Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        public HorizontalAlign? PrintAlign
        {
            get
            {
                return this._printAlign;
            }
            set
            {
                this._printAlign = new HorizontalAlign?(HorizontalAlign.Left);
            }
        }

        public Brush PrintBrush
        {
            get
            {
                return this._printBrush;
            }
            set
            {
                this._printBrush = value;
            }
        }

        public Font PrintFont
        {
            get
            {
                return this._printFont;
            }
            set
            {
                this._printFont = value;
            }
        }
    }
}

