using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;
using System.Windows;
using System.Drawing;
using BL;
using BL.Store;


namespace ADProject_Team1.View.Store
{
    

    public partial class CreateDisbursement : System.Web.UI.Page
    {
        CreateDisbursementListController control = new CreateDisbursementListController();
        int count = 0;
        Dictionary<string, List<RequisitionItem>> RequisitionItemDictionaryByReqiId = new Dictionary<string, List<RequisitionItem>>();
        List<string> selectedReqIdsList = new List<string>();
        List<RequisitionItem> reqItemListByReqId = new List<RequisitionItem>();
        List<RequisitionItem> AllreqItemList = new List<RequisitionItem>();//list of all instance of RequisitionItem selected requisition Ids   
        Dictionary<string, int> itemsDictionary = new Dictionary<string, int>();// //ItemDictionary caontains all items and quantities related to selected Requisition ids
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PostBack();
            }

        }

        protected void PostBack()
        {
            setInitialData();
            Panel1.Visible = false;
            showDisbursementBtn.Visible = false;
            Retreivallabl.Visible = false;
        }
        //setting data on page load
        protected void setInitialData()
        {
            showRetreivalBtn.Visible = false;
            List<string> list = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("requisitionId", typeof(string)));
            dt.Columns.Add(new DataColumn("departmentName", typeof(string)));
            dt.Columns.Add(new DataColumn("orderType", typeof(string)));
            list = control.GetAllReuisitionId();
            foreach (string requisitionId in list)
            {
                string submissionStaffId = control.GetSubmissionStaff_ID(requisitionId);
                string departmentId = control.GetDepartmentIdByStaffId(submissionStaffId);
                string departmentName = control.GetDepartmentNameById(departmentId);
                string orderType = control.GetRequisitionTypeByReqId(requisitionId);
                DataRow dr = dt.NewRow();
                dr["requisitionId"] = requisitionId;
                dr["departmentName"] = departmentName;
                dr["orderType"] = orderType;
                dt.Rows.Add(dr);
            }
            int y = dt.Rows.Count;
            if (y == 0)
            {
                //System.Windows.Forms.MessageBox.Show("There is no Requisition left,Come later!!!");
                LBStatus.Visible = true;
            }
            else
            {
                showRetreivalBtn.Visible = true;
                dt.AcceptChanges();
                ViewState["CurrentTable"] = dt;
                requisitionSummaryView.DataSource = dt;
                requisitionSummaryView.DataBind();
            }

        }





        //creating Reteival list
        protected void showRetreivalBtn_Click(object sender, EventArgs e)
        {
            
            int count = 0;
                showDisbursementBtn.Enabled = true;
                selectedReqIdsList.Clear();
                RequisitionItemDictionaryByReqiId.Clear();
                itemGridView.Visible = false;
                if (requisitionSummaryView.Rows.Count > 0)
                {
                    
                        for (int i = 0; i < requisitionSummaryView.Rows.Count; i++)
                        {
                            CheckBox cbx = (CheckBox)requisitionSummaryView.Rows[i].FindControl("selectChkBox");
                            if (cbx != null)
                            {
                                if (cbx.Checked)
                                {
                                    string SelectedRequisitionId = requisitionSummaryView.Rows[i].Cells[0].Text;
                                    reqItemListByReqId = control.GetreqItemListByReqId(SelectedRequisitionId);
                                    RequisitionItemDictionaryByReqiId.Add(SelectedRequisitionId, reqItemListByReqId);
                                    selectedReqIdsList.Add(SelectedRequisitionId);
                                    count++;
                                }

                            }

                        }
                    
                    

                }
                if (count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Please check at least one requisition.')</script>");
                    Retreivallabl.Visible = false;
                    showDisbursementBtn.Visible = false;

                }
                else
                {
                    Session["ReqItemDicByReqID"] = RequisitionItemDictionaryByReqiId;
                    Session["selectedReqIdsList"] = selectedReqIdsList;
                    Session["reqItemListByReqId"] = reqItemListByReqId;
                   // control.UpdateReqIdStatus(selectedReqIdsList);                               //update requisition status

                    selectedReqIdsList = (List<string>)Session["selectedReqIdsList"];
                    AllreqItemList = control.GetSelectedreqItemList(selectedReqIdsList);
                    Session["allreqItemList"] = AllreqItemList;
                    AllreqItemList = (List<RequisitionItem>)Session["allreqItemList"];
                    foreach (RequisitionItem reqItem in AllreqItemList)
                    {
                        string itemId = reqItem.Item_ID;
                        int qty = reqItem.Required_Qty;
                        if (!itemsDictionary.ContainsKey(itemId))
                        {
                            itemsDictionary.Add(itemId, qty);
                        }
                        else
                        {
                            itemsDictionary[itemId] = itemsDictionary[itemId] + qty;
                        }

                    }
                    Session["ItemsDictionary"] = itemsDictionary;

                    List<string> list = new List<string>();
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("BinNo", typeof(string)));
                    dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                    dt.Columns.Add(new DataColumn("Decription", typeof(string)));
                    dt.Columns.Add(new DataColumn("needed", typeof(Int32)));
                    dt.Columns.Add(new DataColumn("providing", typeof(Int32)));
                    itemsDictionary = (Dictionary<string, int>)Session["ItemsDictionary"];
                    foreach (var ReqItem in itemsDictionary)
                    {
                        string itemcode = ReqItem.Key;
                        int qty = ReqItem.Value;
                        Item item = control.GetItemObjectById(itemcode);
                        string binNo = item.Bin_ID;
                        string desciption = item.Item_Name;
                        int providingQty = qty;
                        DataRow dr = dt.NewRow();
                        dr["BinNo"] = binNo;
                        dr["ItemCode"] = itemcode;
                        dr["Decription"] = desciption;
                        dr["needed"] = qty;
                        dr["providing"] = providingQty;
                        dt.Rows.Add(dr);


                    }
                    itemGridView.Visible = true;
                    int y = dt.Rows.Count;
                    dt.AcceptChanges();
                    ViewState["CurrentTable"] = dt;
                    itemGridView.DataSource = dt;
                    itemGridView.DataBind();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        TextBox provide = (TextBox)itemGridView.Rows[j].FindControl("ProvidingTxtBox");
                        provide.Text = dt.Rows[j]["providing"].ToString();
                    }

                    Retreivallabl.Visible = true;
                    showDisbursementBtn.Visible = true;
                    showRetreivalBtn.Enabled = true;
                }
            }






        protected void itemGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            itemGridView.EditIndex = e.NewEditIndex;

        }


        protected void showDisbursementBtn_Clicks(object sender, EventArgs e)
        {
            List<string>reqIDList  = (List<string>) Session["selectedReqIdsList"];
            control.UpdateReqIdStatus(selectedReqIdsList); 
            for (int i = 0; i < itemGridView.Rows.Count; i++)
            {
                TextBox txtTotal = (TextBox)itemGridView.Rows[i].FindControl("ProvidingTxtBox");
                int qtyInStore = Convert.ToInt32(txtTotal.Text);
                string itemcode = itemGridView.Rows[i].Cells[1].Text;
                int neededQty = Convert.ToInt32(itemGridView.Rows[i].Cells[3].Text);
                if (Convert.ToInt32(txtTotal.Text) > neededQty)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Assigned quanity can not be more than needed quantity!!!')</script>");
                    txtTotal.Text = neededQty.ToString();
                }
            }

            Dictionary<string, DeptLevelReqController> mstrDic_Dept = new Dictionary<string, DeptLevelReqController>();
            Dictionary<string, int> underNeededItem_Dic = new Dictionary<string, int>();
            Dictionary<string, int> NeededItem_Dic = new Dictionary<string, int>();
            Dictionary<string, List<ItemInfobyReqiIdModel>> deptReqDictionary = new Dictionary<string, List<ItemInfobyReqiIdModel>>();//pojo2 dictionary
            List<DeptLevelReqController> deptLevelReqList = new List<DeptLevelReqController>(); //Pojo 2 list
            Dictionary<string, Dictionary<string, int>> depConsolidatedDic = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string, List<ItemInfobyReqiIdModel>> depToItem = new Dictionary<string, List<ItemInfobyReqiIdModel>>();
            Dictionary<string, List<ItemInfobyReqiIdModel>> specialReqDic = new Dictionary<string, List<ItemInfobyReqiIdModel>>();
            Dictionary<string, List<ItemInfobyReqiIdModel>> urgentReqDic = new Dictionary<string, List<ItemInfobyReqiIdModel>>();
            Dictionary<string, List<ItemInfobyReqiIdModel>> normalReqDic = new Dictionary<string, List<ItemInfobyReqiIdModel>>();

            selectedReqIdsList = (List<string>)Session["selectedReqIdsList"];
            // List<string> deptList = control.GetDeptList(selectedReqIdsList);
            //populate Pojo 3
            foreach (string reqiId in selectedReqIdsList)                           //1
            {
                List<RequisitionItem> reqiItemByReqId = control.GetreqItemListByReqId(reqiId);
                List<ItemInfobyReqiIdModel> ItemInfobyReqiIdList = new List<ItemInfobyReqiIdModel>();
                foreach (RequisitionItem reqItem in reqiItemByReqId)
                {
                    ItemInfobyReqiIdModel iteminfoByReq = new ItemInfobyReqiIdModel();
                    iteminfoByReq.ReuisitionId1 = reqiId;
                    iteminfoByReq.ItemCode1 = reqItem.Item_ID;
                    iteminfoByReq.ItemQty = reqItem.Required_Qty;
                    ItemInfobyReqiIdList.Add(iteminfoByReq);
                }
                if (control.GetRequisitionTypeByReqId(reqiId) == "Special")
                {
                    specialReqDic.Add(reqiId, ItemInfobyReqiIdList);
                }
                if (control.GetRequisitionTypeByReqId(reqiId) == "Urgent")
                {
                    urgentReqDic.Add(reqiId, ItemInfobyReqiIdList);
                }
                if (control.GetRequisitionTypeByReqId(reqiId) == "Normal")
                {
                    normalReqDic.Add(reqiId, ItemInfobyReqiIdList);
                }
                deptReqDictionary.Add(reqiId, ItemInfobyReqiIdList);                                    //duplicacy

                Dictionary<string, List<ItemInfobyReqiIdModel>> deptReqDictionary_tmp2PopulateMstrListPojo2 = new Dictionary<string, List<ItemInfobyReqiIdModel>>();//pojo2 dictionary
                deptReqDictionary_tmp2PopulateMstrListPojo2.Add(reqiId, ItemInfobyReqiIdList);
                string dept_Name = control.GetDepName(reqiId);
                DeptLevelReqController deptLevelReq = new DeptLevelReqController();
                deptLevelReq.DeptName = dept_Name;
                deptLevelReq.DeptReqi = deptReqDictionary_tmp2PopulateMstrListPojo2;                          // populate Pojo2 ???
                deptLevelReqList.Add(deptLevelReq);                                 //pojo 2 lis                


            }


            //start   populating undrneeded and needed map
            for (int i = 0; i < itemGridView.Rows.Count; i++)
            {

                TextBox txtTotal = (TextBox)itemGridView.Rows[i].Cells[4].FindControl("ProvidingTxtBox");
                int qtyInStore = Convert.ToInt32(txtTotal.Text);

                string itemcode = itemGridView.Rows[i].Cells[1].Text;
                int neededQty = Convert.ToInt32(itemGridView.Rows[i].Cells[3].Text);
                if (Convert.ToInt32(txtTotal.Text) > neededQty)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Assigned quanity can not be more than needed quantity!!!')</script>");
                    txtTotal.Text = neededQty.ToString();
                }

                NeededItem_Dic[itemcode] = neededQty;
                if (qtyInStore < neededQty)
                {
                    underNeededItem_Dic[itemcode] = qtyInStore;
                }
            }

            //end






            for (int index = 0; index < specialReqDic.Count; index++)
            {
                int y = specialReqDic.Count;
                var item = specialReqDic.ElementAt(index);
                string reqId = item.Key;
                List<ItemInfobyReqiIdModel> reqIdItemList = item.Value;
                string deptName = control.GetDepName(reqId);

                foreach (DeptLevelReqController depLevelReq in deptLevelReqList)                //deplevel final map
                {
                    //deptReqDictionary=Session["deptReqDictionary"];
                    if (depLevelReq.DeptName == deptName)
                    {
                        List<ItemInfobyReqiIdModel> reqIdItemInfo = deptReqDictionary[reqId];
                        foreach (ItemInfobyReqiIdModel itemInfoByreq in reqIdItemInfo)
                        {

                            string itemCode = itemInfoByreq.ItemCode1;
                            int assign = itemInfoByreq.ItemQty;

                            //start  allocating quantity
                            if (underNeededItem_Dic.ContainsKey(itemCode))
                            {
                                if (itemInfoByreq.ItemQty < underNeededItem_Dic[itemCode])
                                {
                                    itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                    underNeededItem_Dic[itemCode] = underNeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                                }
                                else
                                {
                                    itemInfoByreq.Item_assigned1 = underNeededItem_Dic[itemCode];
                                }
                            }
                            else
                            {
                                itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                NeededItem_Dic[itemCode] = NeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                            }

                            ///finish allocation
                        }


                    }

                }
            }





            for (int index = 0; index < urgentReqDic.Count; index++)
            {
                int y = urgentReqDic.Count;
                var item = urgentReqDic.ElementAt(index);
                string reqId = item.Key;
                List<ItemInfobyReqiIdModel> reqIdItemList = item.Value;
                string deptName = control.GetDepName(reqId);

                foreach (DeptLevelReqController depLevelReq in deptLevelReqList)
                {
                    if (depLevelReq.DeptName == deptName)
                    {
                        List<ItemInfobyReqiIdModel> reqIdItemInfo = deptReqDictionary[reqId];
                        foreach (ItemInfobyReqiIdModel itemInfoByreq in reqIdItemInfo)
                        {

                            string itemCode = itemInfoByreq.ItemCode1;
                            int assign = itemInfoByreq.ItemQty;

                            //start  allocating quantity
                            if (underNeededItem_Dic.ContainsKey(itemCode))
                            {
                                if (itemInfoByreq.ItemQty < underNeededItem_Dic[itemCode])
                                {
                                    itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                    underNeededItem_Dic[itemCode] = underNeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                                }
                                else
                                {
                                    itemInfoByreq.Item_assigned1 = underNeededItem_Dic[itemCode];
                                }
                            }
                            else
                            {
                                itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                NeededItem_Dic[itemCode] = NeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                            }

                            ///finish allocation
                        }


                    }

                }
            }










            // selection from screen --- mstr map : req-item          
            for (int index = 0; index < normalReqDic.Count; index++)
            {
                int y = normalReqDic.Count;
                var item = normalReqDic.ElementAt(index);
                string reqId = item.Key;
                List<ItemInfobyReqiIdModel> reqIdItemList = item.Value;
                string deptName = control.GetDepName(reqId);

                foreach (DeptLevelReqController depLevelReq in deptLevelReqList)
                {
                    if (depLevelReq.DeptName == deptName)
                    {
                        List<ItemInfobyReqiIdModel> reqIdItemInfo = deptReqDictionary[reqId];
                        foreach (ItemInfobyReqiIdModel itemInfoByreq in reqIdItemInfo)
                        {

                            string itemCode = itemInfoByreq.ItemCode1;
                            int assign = itemInfoByreq.ItemQty;

                            //start  allocating quantity
                            if (underNeededItem_Dic.ContainsKey(itemCode))
                            {
                                if (itemInfoByreq.ItemQty < underNeededItem_Dic[itemCode])
                                {
                                    itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                    underNeededItem_Dic[itemCode] = underNeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                                }
                                else
                                {
                                    itemInfoByreq.Item_assigned1 = underNeededItem_Dic[itemCode];
                                }
                            }
                            else
                            {                                                /////////new
                                itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                NeededItem_Dic[itemCode] = NeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                            }

                            ///finish allocation
                        }


                    }

                }
            }





            ////for showing on screen populating dictionary depToItem[deptnmae(key),listOfItem(value)]
            foreach (DeptLevelReqController deptLevelReq in deptLevelReqList)
            {
                if (depToItem.ContainsKey(deptLevelReq.DeptName))
                {
                    List<ItemInfobyReqiIdModel> finalAggregatedItemsLis = addNewItemListIntoExistingItemList(depToItem[deptLevelReq.DeptName], getAggregatedItems(deptLevelReq));
                    depToItem.Remove(deptLevelReq.DeptName);
                    depToItem.Add(deptLevelReq.DeptName, finalAggregatedItemsLis);
                }
                else
                {
                    depToItem.Add(deptLevelReq.DeptName, getAggregatedItems(deptLevelReq));
                }

            }


            string s = "";
            for (int index = 0; index < depToItem.Count; index++)
            {
                Dictionary<string, int> itemCodeDic = new Dictionary<string, int>();
                var item = depToItem.ElementAt(index);
                string deptName = item.Key;
                List<ItemInfobyReqiIdModel> consolidateItemList = item.Value;
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Item Code", typeof(string)));
                dt.Columns.Add(new DataColumn("item Category", typeof(string)));
                dt.Columns.Add(new DataColumn("Decription", typeof(string)));
                dt.Columns.Add(new DataColumn("UOM", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantity", typeof(Int32)));


                foreach (ItemInfobyReqiIdModel itemList in consolidateItemList)
                {
                    string itemCode = itemList.ItemCode1;
                    string itemCategory = control.GetItemCategoryByitemId(itemCode);
                    string Desc = control.GetItemDescByitemId(itemCode);
                    string UOM = control.GetItemUomByitemId(itemCode);
                    int quantity = itemList.Item_assigned1;
                    DataRow dr = dt.NewRow();
                    dr["Item Code"] = itemCode;
                    dr["item Category"] = control.GetItemCategoryByitemId(itemCode);
                    dr["Decription"] = control.GetItemDescByitemId(itemCode);
                    dr["UOM"] = control.GetItemUomByitemId(itemCode);
                    dr["Quantity"] = quantity;
                    itemCodeDic.Add(itemCode, quantity);
                    dt.Rows.Add(dr);
                }

                string disbursementId = "D0" + getDisbursementId();
                
                Label lbl2 = new Label();
                lbl2.ForeColor = System.Drawing.Color.Black;
                lbl2.Font.Bold = true;
                lbl2.Font.Size = 12;
                lbl2.Text = "Disbursment ID:  " + disbursementId;
                PlaceHolder1.Controls.Add(lbl2);
                PlaceHolder1.Controls.Add(new LiteralControl("<br />"));

                Label lbl1 = new Label();
                lbl1.ForeColor = System.Drawing.Color.Black;
                lbl1.Font.Bold = true;
                lbl1.Font.Size = 12;
                lbl1.Text = "Department Name:  " + deptName;
                PlaceHolder1.Controls.Add(lbl1);
                GridView gv = new GridView();
                gv.AutoGenerateColumns = true;
                gv.DataSource = dt;
                gv.DataBind();
                gv.Attributes.Add("Class", "table table-striped table-bordered table-hover dataTable no-footer");
                gv.Width = 800;
                PlaceHolder1.Controls.Add(gv);
                Panel1.Visible = true;
                
                if (disbursementId == s)
                {
                    disbursementId = "D0" + (Convert.ToInt64(disbursementId.Substring(2)) + 1).ToString();

                }
                List<string> reqIdByDept = new List<string>();
                selectedReqIdsList = (List<string>)Session["selectedReqIdsList"];
                foreach (string reqId in selectedReqIdsList)
                {

                    string depname = control.GetDepName(reqId);
                    if (depname == deptName)
                    {
                        reqIdByDept.Add(reqId);
                    }
                }
                Disbursement disbursement = new Disbursement();
                disbursement.Disbursement_ID = disbursementId;
                disbursement.Create_Date = DateTime.Now;
                disbursement.Receive_Date = null;
                disbursement.Dept_ID = control.getDeptIdByDeptName(deptName);
                disbursement.RepStaff_ID = control.getRepStaffId(disbursement.Dept_ID);
                disbursement.Status = "Submitted";
                control.CreateDisburseList(reqIdByDept, itemCodeDic, disbursement);     //for each dept disbursement
                s = disbursementId;
            }

            showDisbursementBtn.Enabled = false;
            // requisitionSummaryView.
            //  showDisbursementBtn.ForeColor

            // setInitialData();
        }
        public List<ItemInfobyReqiIdModel> getAggregatedItems(DeptLevelReqController deptLevelReq)
        {

            // Hm [itemcode, POJO3 aggregated]
            Dictionary<string, ItemInfobyReqiIdModel> temp_Dic_AggregatedItems = new Dictionary<string, ItemInfobyReqiIdModel>();


            Dictionary<string, List<ItemInfobyReqiIdModel>> temp_DeptReqi = deptLevelReq.DeptReqi;


            foreach (var key in temp_DeptReqi.Keys)
            {
                ItemInfobyReqiIdModel reqItemInfo = new ItemInfobyReqiIdModel();
                foreach (ItemInfobyReqiIdModel itemInfoReq in temp_DeptReqi[key])
                {

                    if (temp_Dic_AggregatedItems.ContainsKey(itemInfoReq.ItemCode1))
                    {
                        temp_Dic_AggregatedItems[itemInfoReq.ItemCode1].ItemQty = temp_Dic_AggregatedItems[itemInfoReq.ItemCode1].ItemQty + itemInfoReq.ItemQty;
                    }
                    else
                    {
                        temp_Dic_AggregatedItems.Add(itemInfoReq.ItemCode1, itemInfoReq);
                    }
                }

            }


            // make list of items values
            List<ItemInfobyReqiIdModel> temp_Dic_AggregatedItems_List = new List<ItemInfobyReqiIdModel>();
            foreach (var key in temp_Dic_AggregatedItems.Keys)
            {
                temp_Dic_AggregatedItems_List.Add(temp_Dic_AggregatedItems[key]);
            }

            return temp_Dic_AggregatedItems_List;
        }


        public List<ItemInfobyReqiIdModel> addNewItemListIntoExistingItemList(List<ItemInfobyReqiIdModel> existingAggregatedItemsList, List<ItemInfobyReqiIdModel> NewAggregatedItemsList)
        {

            Boolean matchFound = false;
            int indexForMatchFound = 0;

            List<ItemInfobyReqiIdModel> finalAggregatedItemsList = existingAggregatedItemsList;// new List<ItemInfobyReqiId>();

            foreach (ItemInfobyReqiIdModel newItemInfobyReqiId in NewAggregatedItemsList)
            {
                matchFound = false;
                indexForMatchFound = 0;
                foreach (ItemInfobyReqiIdModel existingItemInfobyReqiId in existingAggregatedItemsList)
                {

                    if (existingItemInfobyReqiId.ItemCode1 == newItemInfobyReqiId.ItemCode1)
                    {
                        matchFound = true;
                        finalAggregatedItemsList[indexForMatchFound].Item_assigned1 = newItemInfobyReqiId.Item_assigned1 + existingItemInfobyReqiId.Item_assigned1;
                    }
                    indexForMatchFound++;
                }
                if (!matchFound)
                {
                    finalAggregatedItemsList.Add(newItemInfobyReqiId);
                }


            }
            return finalAggregatedItemsList;
        }

        private String getDisbursementId()
        {
            String timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            return timeStamp;
        }

        protected void requisitionSummaryView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            requisitionSummaryView.PageIndex = e.NewPageIndex;
            requisitionSummaryView.DataSource = dt;
            requisitionSummaryView.DataBind();
        }

      

    }
    }





