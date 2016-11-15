namespace Vinculum.Framework.Printing.Entity
{
    using System;
    using System.Drawing;
    using System.Reflection;

    public interface IPrintPrimitiveTable : IPrintPrimitive
    {
        void AddColumn(float column);
        void AddColumns(params float[] column);
        void AddRow(params IPrintPrimitive[] child);
        void Draw(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds, int rowIndex);
        void DrawFooter(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds);
        void DrawHeader(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds);

        float[] ColumnWidth { get; }

        CellWidthUnit ColumnWidthUnit { get; set; }

        IPrintPrimitiveRow Footer { get; set; }

        IPrintPrimitiveRow Header { get; set; }

        IPrintPrimitive this[int row, int cell] { get; }

        IPrintPrimitive PrimitiveID { get; set; }

        IPrintPrimitiveRow[] Rows { get; }
    }
}

