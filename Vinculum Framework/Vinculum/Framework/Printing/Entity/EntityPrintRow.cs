namespace Vinculum.Framework.Printing.Entity
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms.VisualStyles;

    public class EntityPrintRow : IPrintPrimitiveRow, IPrintPrimitive
    {
        private float[] _cellWidth;
        private bool _gridLines;
        private HorizontalAlign _printAlign;
        private Brush _printBrush;
        private IPrintPrimitive[] _printCell;
        private Font _printFont;
        private CellWidthUnit _widthUnit;

        public EntityPrintRow(HorizontalAlign printAlign, Brush printBrush, Font printFont, float[] cellWidth, CellWidthUnit widthUnit)
        {
            this._gridLines = false;
            this._printAlign = printAlign;
            this._printBrush = printBrush;
            this._printFont = printFont;
            this._cellWidth = cellWidth;
            this._widthUnit = widthUnit;
            this._printCell = new IPrintPrimitive[this._cellWidth.Length];
        }

        public EntityPrintRow(HorizontalAlign printAlign, Brush printBrush, Font printFont, float[] cellWidth, CellWidthUnit widthUnit, IPrintPrimitive[] printCell) : this(printAlign, printBrush, printFont, cellWidth, widthUnit)
        {
            if (cellWidth.Length != printCell.Length)
            {
                throw new ArgumentOutOfRangeException("printCell", "Column definition - Data Cell count mismatch");
            }
            this._printCell = printCell;
        }

        public float CalculateContentHeight(Graphics gc)
        {
            float height = 0f;
            float theight = 0f;
            for (int i = 0; i < this._printCell.Length; i++)
            {
                theight = this._printCell[i].CalculateHeight(gc);
                height += (theight > height) ? theight : 0f;
            }
            return height;
        }

        public float CalculateHeight(Graphics gc)
        {
            return (this.CalculateContentHeight(gc) + (this.GridLines ? ((float) 6) : ((float) 0)));
        }

        public void Draw(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds)
        {
            float x = elementBounds.Left;
            EntityPrintPrimitiveRule tRule = new EntityPrintPrimitiveRule(this._printBrush, this._printFont);
            if (this.GridLines)
            {
                tRule.Draw(manager, yPos, gc, elementBounds);
                yPos += tRule.CalculateHeight(gc);
            }
            for (int i = 0; i < this._printCell.Length; i++)
            {
                Rectangle cellBounds;
                switch (this._widthUnit)
                {
                    case CellWidthUnit.Percentage:
                        cellBounds = new Rectangle((int) x, (int) yPos, (int) (((elementBounds.Right - elementBounds.Left) * this._cellWidth[i]) / 100f), elementBounds.Height);
                        x += cellBounds.Right - cellBounds.Left;
                        break;

                    default:
                        cellBounds = new Rectangle((int) x, (int) yPos, (int) this._cellWidth[i], elementBounds.Height);
                        x += cellBounds.Right - cellBounds.Left;
                        break;
                }
                this._printCell[i].Draw(manager, yPos, gc, cellBounds);
            }
            yPos += this.CalculateContentHeight(gc);
            if (this.GridLines)
            {
                tRule.Draw(manager, yPos, gc, elementBounds);
                yPos += tRule.CalculateHeight(gc);
            }
        }

        public void EditCell(params IPrintPrimitive[] cell)
        {
            try
            {
                if (cell.Length != this._cellWidth.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                for (int i = 0; i < cell.Length; i++)
                {
                    this._printCell[i] = cell[i];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GridLines
        {
            get
            {
                return this._gridLines;
            }
            set
            {
                this._gridLines = value;
            }
        }

        public IPrintPrimitive this[int cellindex]
        {
            get
            {
                return this._printCell[cellindex];
            }
            set
            {
                this._printCell[cellindex] = value;
            }
        }

        public HorizontalAlign? PrintAlign
        {
            get
            {
                return new HorizontalAlign?(this._printAlign);
            }
            set
            {
                HorizontalAlign? var0000 = value;
                this._printAlign = var0000.HasValue ? var0000.GetValueOrDefault() : HorizontalAlign.Left;
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

