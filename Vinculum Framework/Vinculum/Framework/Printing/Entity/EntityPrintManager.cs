namespace Vinculum.Framework.Printing.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class EntityPrintManager : PrintDocument
    {
        private int _pageNum;
        private PageSetupDialog _pagesetupDlg;
        private PrintPreviewDialog _previewDlg;
        private int _printBodyIndex;
        private PrintDialog _printDlg;
        private int _printIndex;
        private EntityPrintElement _printPageFooter;
        private EntityPrintElement _printPageHeader;
        private List<EntityPrintTable> _printReportBody;
        private EntityPrintElement _printReportFooter;
        private EntityPrintElement _printReportHeader;

        public EntityPrintManager()
        {
            this._pagesetupDlg = new PageSetupDialog();
            this._pagesetupDlg.PageSettings = base.DefaultPageSettings;
            this._previewDlg = new PrintPreviewDialog();
            this._previewDlg.Document = this;
            this._printDlg = new PrintDialog();
            this._printDlg.Document = this;
            this._printIndex = 0;
            this._pageNum = 0;
            this._printPageHeader = null;
            this._printPageFooter = null;
            this._printReportHeader = null;
            this._printReportFooter = null;
            this._printReportBody = null;
        }

        public EntityPrintManager(EntityPrintElement pageHeader, EntityPrintElement pageFooter, EntityPrintElement reportHeader, EntityPrintElement reportFooter, List<EntityPrintTable> reportBody) : this()
        {
            this._printPageHeader = pageHeader;
            this._printPageFooter = pageFooter;
            this._printReportHeader = reportHeader;
            this._printReportFooter = reportFooter;
            this._printReportBody = reportBody;
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            this._pageNum = 0;
            this._printIndex = 0;
            this._printBodyIndex = 0;
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            bool morePages = false;
            int elementsOnPage = 0;
            float pageHeaderHeight = 0f;
            float pageFooterHeight = 0f;
            float reportHeaderHeight = 0f;
            float yPos = 0f;
            float height = 0f;
            this._pageNum++;
            if (this._printPageHeader != null)
            {
                pageHeaderHeight = this._printPageHeader.CalculateHeight(e.Graphics);
                this._printPageHeader.Draw(this, (float) e.MarginBounds.Top, e.Graphics, e.MarginBounds);
            }
            if (this._printPageFooter != null)
            {
                pageFooterHeight = this._printPageFooter.CalculateHeight(e.Graphics);
                this._printPageFooter.Draw(this, e.MarginBounds.Bottom - pageFooterHeight, e.Graphics, e.MarginBounds);
            }
            if ((this._printReportHeader != null) && (this._pageNum == 1))
            {
                reportHeaderHeight = this._printReportHeader.CalculateHeight(e.Graphics);
                this._printReportHeader.Draw(this, e.MarginBounds.Top + pageHeaderHeight, e.Graphics, e.MarginBounds);
            }
            Rectangle pageBounds = new Rectangle(e.MarginBounds.Left, (int) ((e.MarginBounds.Top + pageHeaderHeight) + reportHeaderHeight), e.MarginBounds.Width, (int) (((e.MarginBounds.Height - pageHeaderHeight) - pageFooterHeight) - reportHeaderHeight));
            yPos = pageBounds.Top;
            while (this._printBodyIndex < this._printReportBody.Count)
            {
                if (elementsOnPage == 0)
                {
                    height = ((this._printReportBody[this._printBodyIndex].Header == null) ? 0f : this._printReportBody[this._printBodyIndex].Header.CalculateHeight(e.Graphics)) + ((this._printReportBody[this._printBodyIndex].PrimitiveID == null) ? 0f : this._printReportBody[this._printBodyIndex].PrimitiveID.CalculateHeight(e.Graphics));
                    if ((yPos + height) > pageBounds.Bottom)
                    {
                        morePages = true;
                        break;
                    }
                    if (height > 0f)
                    {
                        if (this._printReportBody[this._printBodyIndex].PrimitiveID != null)
                        {
                            this._printReportBody[this._printBodyIndex].PrimitiveID.Draw(this, yPos, e.Graphics, pageBounds);
                            yPos += this._printReportBody[this._printBodyIndex].PrimitiveID.CalculateHeight(e.Graphics);
                        }
                        if (this._printReportBody[this._printBodyIndex].Header != null)
                        {
                            this._printReportBody[this._printBodyIndex].DrawHeader(this, yPos, e.Graphics, pageBounds);
                            yPos += this._printReportBody[this._printBodyIndex].Header.CalculateHeight(e.Graphics);
                        }
                    }
                }
                while (this._printIndex < (this._printReportBody[this._printBodyIndex].Rows.Length + ((this._printReportBody[this._printBodyIndex].Footer == null) ? 0 : 1)))
                {
                    if (this._printIndex < this._printReportBody[this._printBodyIndex].Rows.Length)
                    {
                        height = this._printReportBody[this._printBodyIndex].Rows[this._printIndex].CalculateHeight(e.Graphics);
                    }
                    else
                    {
                        height = (this._printReportBody[this._printBodyIndex].Footer == null) ? 0f : this._printReportBody[this._printBodyIndex].Footer.CalculateHeight(e.Graphics);
                    }
                    if (((yPos + height) > pageBounds.Bottom) && (elementsOnPage != 0))
                    {
                        morePages = true;
                        break;
                    }
                    if (height > 0f)
                    {
                        if (this._printIndex < this._printReportBody[this._printBodyIndex].Rows.Length)
                        {
                            this._printReportBody[this._printBodyIndex].Draw(this, yPos, e.Graphics, pageBounds, this._printIndex);
                        }
                        else
                        {
                            this._printReportBody[this._printBodyIndex].DrawFooter(this, yPos, e.Graphics, pageBounds);
                        }
                    }
                    yPos += height;
                    this._printIndex++;
                    elementsOnPage++;
                }
                this._printBodyIndex++;
                this._printIndex = 0;
                elementsOnPage = 0;
            }
            if ((this._printReportFooter != null) && !morePages)
            {
                if (this._printReportFooter.CalculateHeight(e.Graphics) <= (pageBounds.Height - yPos))
                {
                    this._printReportFooter.Draw(this, yPos, e.Graphics, e.MarginBounds);
                }
                else
                {
                    morePages = true;
                }
            }
            e.HasMorePages = morePages;
        }

        public string ReplaceTokens(string buffer)
        {
            buffer = buffer.Replace("[pagenum]", this._pageNum.ToString());
            return buffer;
        }

        public void ShowPageSettings()
        {
            if (this._pagesetupDlg.ShowDialog() == DialogResult.OK)
            {
                base.DefaultPageSettings = this._pagesetupDlg.PageSettings;
            }
        }

        public void ShowPreview()
        {
            this._previewDlg.ShowDialog();
        }

        public void ShowPrintDialog()
        {
            this._printDlg.PrinterSettings = base.PrinterSettings;
            if (this._printDlg.ShowDialog() == DialogResult.OK)
            {
                base.PrinterSettings = this._printDlg.PrinterSettings;
                base.Print();
            }
        }

        public EntityPrintElement PageFooter
        {
            get
            {
                return this._printPageFooter;
            }
            set
            {
                this._printPageFooter = value;
            }
        }

        public EntityPrintElement PageHeader
        {
            get
            {
                return this._printPageHeader;
            }
            set
            {
                this._printPageHeader = value;
            }
        }

        public EntityPrintTable[] ReportBody
        {
            get
            {
                return this._printReportBody.ToArray();
            }
        }

        public EntityPrintElement ReportFooter
        {
            get
            {
                return this._printReportFooter;
            }
            set
            {
                this._printReportFooter = value;
            }
        }

        public EntityPrintElement ReportHeader
        {
            get
            {
                return this._printReportHeader;
            }
            set
            {
                this._printReportHeader = value;
            }
        }
    }
}

