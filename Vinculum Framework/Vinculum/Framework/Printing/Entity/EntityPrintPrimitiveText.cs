namespace Vinculum.Framework.Printing.Entity
{
    using System;
    using System.Drawing;
    using System.Windows.Forms.VisualStyles;

    public class EntityPrintPrimitiveText : IPrintPrimitive
    {
        private HorizontalAlign _printAlign;
        private Brush _printBrush;
        private Font _printFont;
        private string _text;

        public EntityPrintPrimitiveText(Brush printBrush, Font printFont) : this(printBrush, printFont, HorizontalAlign.Left, string.Empty)
        {
        }

        public EntityPrintPrimitiveText(Brush printBrush, Font printFont, string buffer) : this(printBrush, printFont, HorizontalAlign.Left, buffer)
        {
        }

        public EntityPrintPrimitiveText(Brush printBrush, Font printFont, HorizontalAlign printAlign, string buffer)
        {
            this._printAlign = printAlign;
            this._printBrush = printBrush;
            this._printFont = printFont;
            this._text = buffer;
        }

        public float CalculateHeight(Graphics gc)
        {
            return this.PrintFont.GetHeight(gc);
        }

        public void Draw(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds)
        {
            float x = elementBounds.Left;
            string text = manager.ReplaceTokens(this._text);
            HorizontalAlign var0001 = this.PrintAlign.GetValueOrDefault();
            if (this.PrintAlign.HasValue)
            {
                switch (var0001)
                {
                    case HorizontalAlign.Left:
                        x = elementBounds.Left;
                        break;

                    case HorizontalAlign.Center:
                        x = elementBounds.Left + (((elementBounds.Right - elementBounds.Left) - gc.MeasureString(text, this.PrintFont).Width) / 2f);
                        break;

                    case HorizontalAlign.Right:
                        x = elementBounds.Right - gc.MeasureString(text, this.PrintFont).Width;
                        break;
                }
            }
            gc.DrawString(text, this.PrintFont, this.PrintBrush, (float) ((int) x), yPos, new StringFormat());
        }

        public HorizontalAlign? PrintAlign
        {
            get
            {
                return new HorizontalAlign?(this._printAlign);
            }
            set
            {
                if (!value.HasValue)
                {
                    throw new ArgumentNullException("HorizontalAlign");
                }
                this._printAlign = value.Value;
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

