using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Departments;


namespace LogicUniversity.Departments
{
 
    public partial class CreateRequisition : System.Web.UI.Page
    {
        protected RequisitionController RC = new RequisitionController();


        DropDownList ddlCategory = new DropDownList();
        DropDownList ddlItemDes = new DropDownList();
        Label lblUOM = new Label();
        TextBox tbQty = new TextBox();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                initData();
            }

        }



        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            List<RequisitionViewModel> crViewModelList = (List<RequisitionViewModel>)Session["ReqItemList"];
            RequisitionViewModel newcrViewModel = (RequisitionViewModel)Session["defaultReqItem"];
            getPreviousData();
            setPreviousData();
            crViewModelList.Add(newcrViewModel);

            Session["defaultReqItem"] = newcrViewModel;
            GridViewCreateRequisition.DataSource = crViewModelList;
            GridViewCreateRequisition.DataBind();


        }

        protected void GridViewCreateRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                List<RequisitionViewModel> crViewModelList = (List<RequisitionViewModel>)Session["ReqItemList"];

                getPreviousData();
                setPreviousData();


                crViewModelList.RemoveAt(index);

                Session["ReqItemList"] = crViewModelList;
                GridViewCreateRequisition.DataSource = crViewModelList;
                GridViewCreateRequisition.DataBind();

            }
        }



        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            List<RequisitionItem> reqItemList = new List<RequisitionItem>();
            string type = ddlRequisitiontype.SelectedValue;

            for (int i = 0; i < GridViewCreateRequisition.Rows.Count; i++)
            {

                RequisitionItem reqItem = new RequisitionItem();
                ddlCategory = (DropDownList)GridViewCreateRequisition.Rows[i].Cells[1].FindControl("DropDownListDesc");
                TextBox tbQty = (TextBox)GridViewCreateRequisition.Rows[i].Cells[3].FindControl("TextboxQty");

                reqItem.Item_ID = ddlCategory.SelectedValue;
                reqItem.Required_Qty = Convert.ToInt32(tbQty.Text);
                if (reqItem.Required_Qty > 0)
                {
                    reqItemList.Add(reqItem);
                }

                reqItemList = reqItemList.GroupBy(x => x.Item_ID)
                                        .Select(y => new RequisitionItem { Item_ID = y.Key, Required_Qty = y.Sum(t => t.Required_Qty) })
                                        .ToList();


            }

            if (reqItemList.Count > 0)
            {
                try
                {
                    Staff loginstaff = (Staff)Session["LoginStaff"];


                    RequisitionController.createRequisition(reqItemList, loginstaff.Staff_ID, type, "Submitted");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Requisition Submit Successfully')", true);
                    
                    initData();
                    
                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OOPS! Requisition Submit Failed')", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have Item which Item Quantiy is 0, Pls check!!')", true);
            }



        }

        public void clearPage()
        {
            Session["defaultReqItem"] = "";
            Session["ReqItemList"] = "";
            Session["categoryList"] = "";
            Session["itemIDList"] = "";
            Session["itemDesList"] = "";
        }


        protected void DropDownListCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow r = (sender as DropDownList).NamingContainer as GridViewRow;
            ddlCategory = (DropDownList)r.FindControl("DropDownListCategory");
            ddlItemDes = (DropDownList)r.FindControl("DropDownListDesc");
            lblUOM = (Label)r.FindControl("lblUOM");

            string cateID = ddlCategory.SelectedValue;

            //itemDic = (Dictionary<string, List<Item>>)Session["itemDic"];
            //List<Item> items = itemDic[cateID];

            ddlItemDes.Items.Clear();

            ddlItemDes.DataSource = RC.getItemDesIDByID(cateID);
            ddlItemDes.DataTextField = "Item_Name";
            ddlItemDes.DataValueField = "Item_ID";
            ddlItemDes.DataBind();


            String s = ddlItemDes.SelectedValue;
            lblUOM.Text = RC.getItemUOMByID(s);
        }

        protected void DropDownListDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow r = (sender as DropDownList).NamingContainer as GridViewRow;
            ddlItemDes = (DropDownList)r.FindControl("DropDownListDesc");
            lblUOM = (Label)r.FindControl("lblUOM");


            string itemID = ddlItemDes.SelectedValue;
            Item item = RC.getItemById(itemID);
            ddlCategory.SelectedValue = item.Category_ID;
            lblUOM.Text = item.UOM;

        }




        protected void initData()
        {
            clearPage();
            List<RequisitionViewModel> crViewModelList = new List<RequisitionViewModel>();

            Item ditem = RC.GetDefaultItem();

            List<string> categoryList = RC.getCategoryList();
            List<string> itemIDList = RC.getAllItemIDList();
            List<string> itemDesList = RC.getAllItemDesList();



            RequisitionViewModel newcrViewModel = new RequisitionViewModel();
            newcrViewModel.categoryID = ditem.Category_ID;
            newcrViewModel.ItemID = ditem.Item_ID;
            newcrViewModel.ItemName = ditem.Item_Name;
            newcrViewModel.UOM = ditem.UOM;
            newcrViewModel.RequiredQty = 0;

            crViewModelList.Add(newcrViewModel);
            Session["defaultReqItem"] = newcrViewModel;
            Session["ReqItemList"] = crViewModelList;
            Session["categoryList"] = categoryList;
            Session["itemIDList"] = itemIDList;
            Session["itemDesList"] = itemDesList;

            GridViewCreateRequisition.DataSource = crViewModelList;
            GridViewCreateRequisition.DataBind();
        }


        protected void GridViewCreateRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                List<string> categoryddl = (List<String>)Session["categoryList"];
                List<string> itemIDList = (List<String>)Session["itemIDList"];
                List<string> itemDesList = (List<String>)Session["itemDesList"];

                RequisitionViewModel newcrViewModel = (RequisitionViewModel)Session["defaultReqItem"];

                ddlCategory = (DropDownList)e.Row.FindControl("DropDownListCategory");
                ddlItemDes = (DropDownList)e.Row.FindControl("DropDownListDesc");
                lblUOM = (Label)e.Row.FindControl("lblUOM");
                tbQty = (TextBox)e.Row.FindControl("TextboxQty");


                foreach (string category in categoryddl)
                {
                    ddlCategory.Items.Add(new ListItem(category, category));
                }


                ddlCategory.SelectedValue = newcrViewModel.categoryID;
                ddlCategory.DataBind();

                ddlItemDes.DataSource = RC.getItemDesIDByID(newcrViewModel.categoryID);
                ddlItemDes.DataValueField = "Item_ID";
                ddlItemDes.DataTextField = "Item_Name";
                ddlItemDes.DataBind();

                lblUOM.Text = newcrViewModel.UOM;
                tbQty.Text = "0";


            }
            if (GridViewCreateRequisition.Rows.Count > 1)
            {
                setPreviousData();
            }


        }





        //}
        protected void getPreviousData()
        {
            List<RequisitionViewModel> crViewModelList = (List<RequisitionViewModel>)Session["ReqItemList"];
            crViewModelList.Clear();
            for (int t = 0; t < GridViewCreateRequisition.Rows.Count; t++)
            {

                ddlCategory = (DropDownList)GridViewCreateRequisition.Rows[t].Cells[0].FindControl("DropDownListCategory");
                ddlItemDes = (DropDownList)GridViewCreateRequisition.Rows[t].Cells[1].FindControl("DropDownListDesc");
                tbQty = (TextBox)GridViewCreateRequisition.Rows[t].Cells[2].FindControl("TextboxQty");
                lblUOM = (Label)GridViewCreateRequisition.Rows[t].Cells[3].FindControl("lblUOM");


                RequisitionViewModel crViewModel = new RequisitionViewModel();
                crViewModel.categoryID = ddlCategory.SelectedValue;
                crViewModel.ItemID = ddlItemDes.SelectedValue;
                crViewModel.RequiredQty = Convert.ToInt32(tbQty.Text);
                crViewModel.UOM = lblUOM.Text;
                crViewModelList.Add(crViewModel);
            }

            Session["ReqItemList"] = crViewModelList;
            GridViewCreateRequisition.DataSource = crViewModelList;
            GridViewCreateRequisition.DataBind();

        }

        protected void setPreviousData()
        {
            List<RequisitionViewModel> crViewModelList = (List<RequisitionViewModel>)Session["ReqItemList"];

            for (int i = 0; i < GridViewCreateRequisition.Rows.Count; i++)
            {
                ddlCategory = (DropDownList)GridViewCreateRequisition.Rows[i].Cells[0].FindControl("DropDownListCategory");
                ddlItemDes = (DropDownList)GridViewCreateRequisition.Rows[i].Cells[1].FindControl("DropDownListDesc");
                tbQty = (TextBox)GridViewCreateRequisition.Rows[i].Cells[2].FindControl("TextboxQty");
                lblUOM = (Label)GridViewCreateRequisition.Rows[i].Cells[3].FindControl("lblUOM");



                ddlCategory.SelectedValue = crViewModelList[i].categoryID;

                ddlItemDes.Items.Clear();
                ddlItemDes.DataSource = RC.getItemDesIDByID(ddlCategory.SelectedValue);
                ddlItemDes.DataTextField = "Item_Name";
                ddlItemDes.DataValueField = "Item_ID";
                ddlItemDes.DataBind();

                ddlItemDes.SelectedValue = crViewModelList[i].ItemID;
                tbQty.Text = crViewModelList[i].RequiredQty.ToString();
                lblUOM.Text = crViewModelList[i].UOM;

            }


        }




    }
}