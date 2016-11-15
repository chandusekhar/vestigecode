namespace Vinculum.Framework.Printing.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms.VisualStyles;

    public class EntityPrintTable : IPrintPrimitiveTable, IPrintPrimitive
    {
        private CellWidthUnit _cellWidthUnit;
        private IPrintPrimitive _primitiveID;
        private HorizontalAlign _printAlign;
        private Brush _printBrush;
        private List<float> _printColumn;
        private Font _printFont;
        private IPrintPrimitiveRow _printFooter;
        private IPrintPrimitiveRow _printHeader;
        private List<IPrintPrimitiveRow> _printRow;

        public EntityPrintTable(HorizontalAlign printAlign, Brush printBrush, Font printFont, CellWidthUnit columnWidthUnit)
        {
            this._printAlign = printAlign;
            this._printBrush = printBrush;
            this._printFont = printFont;
            this._printRow = new List<IPrintPrimitiveRow>();
            this._printColumn = new List<float>();
            this._cellWidthUnit = columnWidthUnit;
            this._printHeader = null;
            this._printFooter = null;
            this._primitiveID = null;
        }

        public EntityPrintTable(HorizontalAlign printAlign, Brush printBrush, Font printFont, CellWidthUnit columnWidthUnit, IPrintPrimitiveRow header, IPrintPrimitiveRow footer) : this(printAlign, printBrush, printFont, columnWidthUnit)
        {
            this._printHeader = header;
            this._printFooter = footer;
        }

        public void AddColumn(float column)
        {
            this._printColumn.Add(column);
        }

        public void AddColumns(params float[] column)
        {
            this._printColumn.AddRange(column);
        }

        public void AddRow(params IPrintPrimitive[] cells)
        {
            if (cells.Length != this._printColumn.Count)
            {
                throw new ArgumentOutOfRangeException("cells", "Column definition - Data Cell count mismatch");
            }
            HorizontalAlign? var0000 = this.PrintAlign;
            this._printRow.Add(new EntityPrintRow(var0000.HasValue ? var0000.GetValueOrDefault() : HorizontalAlign.Left, this.PrintBrush, this.PrintFont, this.ColumnWidth, this.ColumnWidthUnit, cells));
        }

        public float CalculateHeight(Graphics gc)
        {
            float rowHSum = 0f;
            for (int index = 0; index < this._printRow.Count; index++)
            {
                rowHSum += this._printRow[index].CalculateHeight(gc);
            }
            return (((((this._primitiveID == null) ? 0f : this._primitiveID.CalculateHeight(gc)) + ((this._printHeader == null) ? 0f : this._printHeader.CalculateHeight(gc))) + rowHSum) + ((this._printFooter == null) ? 0f : this._printFooter.CalculateHeight(gc)));
        }

        public void Draw(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds)
        {
            if (this._primitiveID != null)
            {
                this._primitiveID.Draw(manager, yPos, gc, elementBounds);
                yPos += this._primitiveID.CalculateHeight(gc);
            }
            if (this._printHeader != null)
            {
                this._printHeader.Draw(manager, yPos, gc, elementBounds);
                yPos += this._printHeader.CalculateHeight(gc);
            }
            for (int index = 0; index < this._printRow.Count; index++)
            {
                this._printRow[index].Draw(manager, yPos, gc, elementBounds);
                yPos += this._printRow[index].CalculateHeight(gc);
            }
            if (this._printFooter != null)
            {
                this._printFooter.Draw(manager, yPos, gc, elementBounds);
            }
        }

        public void Draw(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds, int rowIndex)
        {
            if (this._printRow != null)
            {
                this._printRow[rowIndex].Draw(manager, yPos, gc, elementBounds);
            }
        }

        public void DrawFooter(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds)
        {
            if (this._printFooter != null)
            {
                this._printFooter.Draw(manager, yPos, gc, elementBounds);
            }
        }

        public void DrawHeader(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds)
        {
            if (this._printHeader != null)
            {
                this._printHeader.Draw(manager, yPos, gc, elementBounds);
            }
        }

        public float[] ColumnWidth
        {
            get
            {
                return this._printColumn.ToArray();
            }
        }

        public CellWidthUnit ColumnWidthUnit
        {
            get
            {
                return this._cellWidthUnit;
            }
            set
            {
                this._cellWidthUnit = value;
            }
        }

        public IPrintPrimitiveRow Footer
        {
            get
            {
                return this._printFooter;
            }
            set
            {
                this._printFooter = value;
            }
        }

        public IPrintPrimitiveRow Header
        {
            get
            {
                return this._printHeader;
            }
            set
            {
                this._printHeader = value;
            }
        }

        public IPrintPrimitive this[int row, int cell]
        {
            get
            {
                if ((row >= this._printRow.Count) || (cell >= this._printColumn.Count))
                {
                    throw new ArgumentOutOfRangeException();
                }
                return this._printRow[row][cell];
            }
        }

        public IPrintPrimitive PrimitiveID
        {
            get
            {
                return this._primitiveID;
            }
            set
            {
                this._primitiveID = value;
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

        public IPrintPrimitiveRow[] Rows
        {
            get
            {
                return this._printRow.ToArray();
            }
        }
    }
}

