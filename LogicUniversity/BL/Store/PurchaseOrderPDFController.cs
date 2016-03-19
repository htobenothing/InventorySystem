using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using System.Data;
using BL;

namespace BL.Store
{
    public class PurchaseOrderPDFController
    {
        // Template for document elements
        private Template template = new Template();
        // Page Dimensions of pages
        private static PageDimensions pageDimensions = new PageDimensions(PageSize.Letter, PageOrientation.Portrait, 54.0f);
        // Current page that elements are being added to
        private ceTe.DynamicPDF.Page currentPage = null;
        // Top Y coordinate for the body of the report
        private float bodyTop = 38;
        // Bottom Y coordinate for the body of the report
        private float bodyBottom = pageDimensions.Body.Bottom - pageDimensions.Body.Top;
        // Current Y coordinate where elements are being added
        private float currentY = 0;
        // Used to control the alternating background
        private bool alternateBG = false;
        // Connection string to the Northwind database
        InventorySysDBEntities context = new InventorySysDBEntities();
        // Initializing the DataGrid
        public void MakePurchaseOrderPDF(string sup,string PoID,string remark,string clerk,List<ItemWithQtyModel> ItemList)
        {
            // Create a document and set it's properties
            Document document = new Document();
            document.Creator = "Logic University Store Department";
            document.Author = "Logic University Store Department";
            document.Title = "Logic University Purchase Order: "+PoID;
            // Adds elements to the header template
            SetTemplate(sup,PoID,remark,clerk);
            document.Template = template;

            // Establises connection to the database
            

            GridView GridView1 = new GridView();
            GridView1.DataSource = ItemList.ToList();
            GridView1.DataBind();
            AddNewPage(document);
            // Iterating through each row of the DataGrid.
            currentY = 170;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                AddRecord(document, GridView1.Rows[i]);

            }

            // Cleans up database connections
            
            // Outputs the SimpleReport to the current web page
            //document.DrawToWeb("Logic University Purchase Order: "+PoID+".pdf");
            document.Draw("C:\\inetpub\\wwwroot\\LogicUniversity\\PDF\\Logic University Purchase Order_" + PoID + ".pdf");
        }

        private void SetTemplate(string sup, string PoID, string remark, string clerk)
        {
            
            // Adds elements to the header template
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Logic University", 0, 0, 504, 16, Font.HelveticaBold, 16, ceTe.DynamicPDF.TextAlign.Center));
            PageNumberingLabel pageNumLabel = new PageNumberingLabel("Page %%CP%% of %%TP%%", 0, 0, 504, 12,
Font.HelveticaBold, 8, ceTe.DynamicPDF.TextAlign.Right);
            template.Elements.Add(pageNumLabel);
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Stationery Purchase Order", 0, 18, 504, 12, Font.TimesBold, 8, ceTe.DynamicPDF.TextAlign.Center));
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("PO No.: "+PoID, 402, 40, 100, 11, Font.TimesBold, 11, ceTe.DynamicPDF.TextAlign.Right));
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Supplier: "+sup, 2, 60, 236, 11, Font.TimesBold, 11));
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Deliever to: LUW", 402, 60, 100, 11, Font.TimesBold, 11, ceTe.DynamicPDF.TextAlign.Right));
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Order by: "+clerk, 2, 80, 236, 11, Font.TimesBold, 11));
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Date: "+DateTime.Now.ToString("yyyy-MM-dd"), 402, 80, 100, 11, Font.TimesBold, 11,ceTe.DynamicPDF.TextAlign.Right));
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Please supply the following items by "+remark, 2, 100, 236, 11, Font.TimesBold, 11));


            //template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Dept ID", 2, 40, 236, 11, Font.TimesBold, 11));
            //template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Dept Name", 242, 40, 156, 11, Font.TimesBold, 11));
            //template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("CP ID", 402, 40, 100, 11, Font.TimesBold, 11, ceTe.DynamicPDF.TextAlign.Right));
            template.Elements.Add(new Line(0, 120, 504, 120));

            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Category", 2, 140, 236, 11, Font.TimesBold, 11));
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Item ID", 102, 140, 156, 11, Font.TimesBold, 11));
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Description", 202, 140, 156, 11, Font.TimesBold, 11));
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("Quantity", 302, 140, 156, 11, Font.TimesBold, 11));
            template.Elements.Add(new ceTe.DynamicPDF.PageElements.Label("UOM", 402, 140, 100, 11, Font.TimesBold, 11, ceTe.DynamicPDF.TextAlign.Right));
            template.Elements.Add(new Line(0, 160, 504, 160));
            // Uncomment the line below to add a layout grid to each page
            //template.Elements.Add( new LayoutGrid() );
            // Uncomment the line below to add a layout grid to each page
            //template.Elements.Add( new LayoutGrid() );
        }
        private void AddRecord(Document document, GridViewRow row)
        {
            // Adds a new page to the document if needed
            if (currentY > bodyBottom) AddNewPage(document);

            // Adds alternating background to document if needed
            if (alternateBG)
            {
                currentPage.Elements.Add(new Rectangle(0, currentY, 504, 18, RgbColor.Black, new WebColor("E0E0FF"), 0.0F));
            }

            // Adds Labels to the document with data from current record
            currentPage.Elements.Add(new ceTe.DynamicPDF.PageElements.Label(row.Cells[1].Text, 2, currentY + 3, 236, 11, Font.TimesRoman, 11));
            currentPage.Elements.Add(new ceTe.DynamicPDF.PageElements.Label(row.Cells[0].Text, 102, currentY + 3, 156, 11, Font.TimesRoman, 11));
            currentPage.Elements.Add(new ceTe.DynamicPDF.PageElements.Label(row.Cells[2].Text.Split('&')[0], 202, currentY + 3, 156, 11, Font.TimesRoman, 11));
            currentPage.Elements.Add(new ceTe.DynamicPDF.PageElements.Label(row.Cells[12].Text, 302, currentY + 3, 156, 11, Font.TimesRoman, 11));
            currentPage.Elements.Add(new ceTe.DynamicPDF.PageElements.Label(row.Cells[5].Text, 402, currentY + 3, 100, 11, Font.TimesRoman, 11, ceTe.DynamicPDF.TextAlign.Right));
            
            // Toggles alternating background
            alternateBG = !alternateBG;

            // Increments the current Y position on the page
            currentY += 18;
        }

        private void AddNewPage(Document document)
        {
            // Adds a new page to the document
            currentPage = new ceTe.DynamicPDF.Page(pageDimensions);
            currentY = bodyTop;
            alternateBG = false;
            document.Pages.Add(currentPage);
        }
    }
}
    

