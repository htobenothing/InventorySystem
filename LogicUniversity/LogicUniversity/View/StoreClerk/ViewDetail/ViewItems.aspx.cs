using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Store;
using BL.Store.TableView;
using BL;


namespace LogicUniversity.StoreClerk.View
{
   
    public partial class ViewItems : System.Web.UI.Page
    {
        ViewItemController VIC = new ViewItemController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginStaff"] != null)
            {
                if (!IsPostBack)
                {
                    ListItem firstItem = new ListItem("--select all item--", "all");
                    List<string> allCate = VIC.GetAllItemCategory();
                    List<string> allItemID = VIC.GetAllItemCode();
                    List<string> allDesc = VIC.GetAllItemDesc();

                    //add first
                    ddlCate.Text = "--select item--";
                    ddlDesc.Items.Add(firstItem);
                    ddlItemID.Items.Add(firstItem);
                    foreach (string category in allCate)
                    {
                        ddlCate.Items.Add(new ListItem(category, category));
                    }
                    foreach (string itemcode in allItemID)
                    {
                        ddlItemID.Items.Add(new ListItem(itemcode, itemcode));
                    }
                    foreach (string desc in allDesc)
                    {
                        ddlDesc.Items.Add(new ListItem(desc, desc));
                    }
                }
            }
            
        }

        protected void ddlCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem firstItem = new ListItem("--select all item--", "all");
            ddlItemID.Items.Clear();
            ddlItemID.Items.Add(firstItem);
            ddlDesc.Items.Clear();
            ddlDesc.Items.Add(firstItem);

            List<string> allItemID = VIC.GetItemIDByCategory(ddlCate.SelectedValue.ToString());
            List<string> allDesc = VIC.GetDescByCategory(ddlCate.SelectedValue.ToString());

            foreach (string itemcode in allItemID)
            {
                ddlItemID.Items.Add(new ListItem(itemcode, itemcode));
            }
            foreach (string desc in allDesc)
            {
                ddlDesc.Items.Add(new ListItem(desc, desc));
            }
        }

        protected void ddlItemID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemID.SelectedValue != "all")
            {
                string id = ddlItemID.SelectedValue.ToString();
                ddlCate.SelectedValue = VIC.GetCategoryByItemID(id);

                Item item = VIC.GetItemByItemID(id);
                ddlDesc.SelectedValue = item.Item_Name;
            }
            else
            {
                ddlDesc.SelectedValue = "all";
            }
        }

        protected void ddlDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDesc.SelectedValue != "all")
            {
                string desc = ddlDesc.SelectedValue.ToString();
                ddlCate.SelectedValue = VIC.GetCategoryByDesc(desc);

                Item item = VIC.GetItemByDesc(desc);
                ddlItemID.SelectedValue = item.Item_ID;
            }
            else
            {
                ddlItemID.SelectedValue = "all";
            }
        }

        protected void BtnShow_Click(object sender, EventArgs e)
        {
            List<Item> ItemList = new List<Item>();
            PanelDetail.Visible = false;
            if (ddlItemID.SelectedValue == "all" && ddlDesc.SelectedValue == "all")
            {
                string category = ddlCate.SelectedValue.ToString();
                ItemList = VIC.GetItemByCategory(category);
                PanelTable.Visible = true;
                Session["ItemList"] = ItemList;
                GridViewItems.DataSource = ItemList;
                GridViewItems.DataBind();
            }
            else
            {
                string id = ddlItemID.SelectedValue.ToString();
                Item i = VIC.GetItemByItemID(id);
                PanelTable.Visible = true;
                ItemList.Add(i);
                Session["ItemList"] = ItemList;
                GridViewItems.DataSource = ItemList;
                GridViewItems.DataBind();
            }
        }

        protected void GridViewItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewItems.PageIndex = e.NewPageIndex;
            GridViewItems.DataSource = Session["ItemList"];
            GridViewItems.DataBind();
        }

        protected void GridViewItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewdetail")
            {
                PanelDetail.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                string id = GridViewItems.Rows[index].Cells[1].Text;
                List<ItemTransaction> ITList = VIC.GetItemTransactionByItemID(id);
                Session["ITList"] = ITList;
                GridViewItemDetail.DataSource = ITList;
                GridViewItemDetail.DataBind();
                foreach (GridViewRow r in GridViewItemDetail.Rows)
                {
                    if (r.Cells[1].Text.Contains("Sent to"))
                    {
                        r.BackColor = System.Drawing.Color.Crimson;
                        r.ForeColor = System.Drawing.Color.White;
                    }
                    else if (r.Cells[1].Text.Contains("Received from"))
                    {
                        r.BackColor = System.Drawing.Color.MediumSpringGreen;
                    }
                    else if (r.Cells[1].Text.Contains("Adjustment"))
                    {
                        r.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
        }

        protected void BtnShowAll_Click(object sender, EventArgs e)
        {
            PanelDetail.Visible = false;
            PanelTable.Visible = true;
            Session["ItemList"] = VIC.GetAllItem();
            GridViewItems.DataSource = VIC.GetAllItem();
            GridViewItems.DataBind();
        }

        protected void GridViewItemDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewItemDetail.PageIndex = e.NewPageIndex;
            GridViewItemDetail.DataSource = Session["ITList"];
            GridViewItemDetail.DataBind();
            foreach (GridViewRow r in GridViewItemDetail.Rows)
            {
                if (r.Cells[1].Text.Contains("Sent to"))
                {
                    r.BackColor = System.Drawing.Color.Crimson;
                    r.ForeColor = System.Drawing.Color.White;
                }
                else if (r.Cells[1].Text.Contains("Received from"))
                {
                    r.BackColor = System.Drawing.Color.MediumSpringGreen;
                }
                else if (r.Cells[1].Text.Contains("Adjustment"))
                {
                    r.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }

  

    }
}