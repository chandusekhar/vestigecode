namespace Vinculum.Framework.Printing.Entity
{
    using System;
    using System.Drawing;
    using System.Windows.Forms.VisualStyles;

    public interface IPrintPrimitive
    {
        float CalculateHeight(Graphics gc);
        void Draw(EntityPrintManager manager, float yPos, Graphics gc, Rectangle elementBounds);

        HorizontalAlign? PrintAlign { get; set; }

        Brush PrintBrush { get; set; }

        Font PrintFont { get; set; }
    }
}

