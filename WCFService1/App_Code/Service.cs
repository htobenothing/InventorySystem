using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BL;
using BL.Departments;
using BL.Store;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    public WCF_Item[] itemList()
    {
       // return ItemManager.getAllItem().ToArray<WCF_Item>();
        CreatePurchaseOrderControl cpoc = new CreatePurchaseOrderControl();
        ItemService itemService = new ItemService();
        List<WCF_Item> wf = new List<WCF_Item>();
        wf=itemService.ConvertToWCFItemList(cpoc.GetAllItem());

      
        //foreach (Item f in ItemManager_M.getAllItem())
        //{

        //    wf.Add(new WCF_Item(f.Item_ID, f.Category_ID, f.Item_Name, f.Reorder_Level, f.Reorder_Qty, f.UOM, f.Bin_ID, f.FirstSupplier_ID, f.SecondSupplier_ID, f.ThirdSupplier_ID, f.Inventory, f.Status));
        //}
        return wf.ToArray<WCF_Item>();

    }

    //public string[] List()
    //{
    //    string[] arr2 = { "one", "two", "three" };

    //    return arr2;
    //}
}
