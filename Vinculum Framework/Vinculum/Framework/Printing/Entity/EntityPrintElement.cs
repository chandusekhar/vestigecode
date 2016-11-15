namespace Vinculum.Framework.Printing.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms.VisualStyles;

    public class EntityPrintElement : IPrintPrimitive
    {
        private HorizontalAlign? _printAlign;
        private Brush _printBrush;
        private Font _printFont;
        private List<IPrintPrimitive> _printPrimitives;

        public EntityPrintElement(Brush printBrush, Font printFont) : this(printBrush, printFont, new HorizontalAlign?(HorizontalAlign.Left))
        {
        }

        public EntityPrintElement(Brush printBrush, Font printFont, HorizontalAlign? printAlign)
        {
            this._printBrush = printBrush;
            this._printFont = printFont;
            this._printAlign = printAlign;
            this._printPrimitives = new List<IPrintPrimitive>();
        }

        public void AddBlankLine()
        {
            this.AddText();
        }

        public void AddData(string dataName, string dataValue)
        {
            this.AddData(dataName, dataValue, HorizontalAlign.Left);
        }

        public void AddData(string dataName, string dataValue, HorizontalAlign align)
        {
            this.AddText(string.Format("{0} : {1}", dataName, dataValue), align);
        }

        public void AddHeader(string buffer)
        {
            this.AddHeader(buffer, HorizontalAlign.Left);
        }

        public void AddHeader(string buffer, HorizontalAlign align)
        {
            this.AddText(buffer, align);
            this.AddHorizontalRule();
        }

        public void AddHorizontalRule()
        {
            this._printPrimitives.Add(new EntityPrintPrimitiveRule(this.PrintBrush, this.PrintFont));
        }

        public void AddHorizontalRule(float height)
        {
            this._printPrimitives.Add(new EntityPrintPrimitiveRule(this.PrintBrush, this.PrintFont, height));
        }

        public void AddText()
        {
            this.AddText(string.Empty);
        }

        public void AddText(string buffer)
        {
            this.AddText(buffer, HorizontalAlign.Left);
        }

        public void AddText(string buffer, HorizontalAlign align)
        {
            this._printPrimitives.Add(new EntityPrintPrimitiveText(this.PrintBrush, this.PrintFont, align, buffer));
        }

        public float CalculateHeight(Graphics gc)
        {
            float height = 0f;
            foreach (IPrintPrimitive primitive in this._printPrimitives)
            {
                if (!this._printAlign.HasValue)
                {
                    height = (height >= primitive.CalculateHeight(gc)) ? height : primitive.CalculateHeight(gc);
                }
                else
                {
                    height += primitive.CalculateHeight(gc);
                }
            }
            return height;
        }

        public void Draw(EntityPrintManager manager, float yPos, Graphics gc, Rectangle printBounds)
        {
            float height = this.CalculateHeight(gc);
            Rectangle elementBounds = new Rectangle(printBounds.Left, (int) yPos, printBounds.Right - printBounds.Left, (int) height);
            foreach (IPrintPrimitive primitive in this._printPrimitives)
            {
                primitive.Draw(manager, yPos, gc, elementBounds);
                yPos += !this._printAlign.HasValue ? 0f : primitive.CalculateHeight(gc);
            }
        }

        public static EntityPrintElement operator +(EntityPrintElement parent, IPrintPrimitive child)
        {
            parent._printPrimitives.Add(child);
            return parent;
        }

        public static EntityPrintElement operator -(EntityPrintElement parent, IPrintPrimitive child)
        {
            parent._printPrimitives.Remove(child);
            return parent;
        }

        public HorizontalAlign? PrintAlign
        {
            get
            {
                return this._printAlign;
            }
            set
            {
                this._printAlign = value;
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

