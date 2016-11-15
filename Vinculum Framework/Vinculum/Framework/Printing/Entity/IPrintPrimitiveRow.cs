namespace Vinculum.Framework.Printing.Entity
{
    using System;
    using System.Reflection;

    public interface IPrintPrimitiveRow : IPrintPrimitive
    {
        void EditCell(params IPrintPrimitive[] cell);

        bool GridLines { get; set; }

        IPrintPrimitive this[int cellindex] { get; set; }
    }
}

