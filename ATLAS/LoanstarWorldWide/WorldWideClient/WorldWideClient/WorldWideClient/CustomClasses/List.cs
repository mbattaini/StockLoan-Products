using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using C1.Silverlight.Data;

namespace WorldWideClient
{
  public class List
  {
    private string status = "";
	private DataTable dtList;
	
    public List() : this("") {}
    public List(string list)
    {
      dtList = new DataTable("List");
	  dtList.Columns.Add("SecId");
	  dtList.Columns.Add("Quantity");
      dtList.AcceptChanges();
		
      if (!list.Equals(""))
      {
        Parse(list);
      }
    }

    public bool Parse(string list)
    {
      string [] records;
      string [] fields;
      char [] delimiters = new char[5];
    
      ListItem listItem = new ListItem();

      try
      {
        list = list.Replace('/', '.'); // Replace the '/', '\', and '-' operators with '.'.
        list = list.Replace('\\', '.');
        list = list.Replace('-', '.');

        delimiters[0] = (char)10; 
        delimiters[1] = (char)13;
        delimiters[2] = (char)9;
        delimiters[3] = ' ';
        delimiters[4] = ';';



		records = list.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
		
		for (int count = 0; count < records.Length; count += 2)
		{
			try
				{
				    listItem.SecId = records[count].ToUpper();
				    listItem.Quantity = long.Parse(records[count + 1].ToString().Replace(",",""));
					
					DataRow drItem = dtList.NewRow();
					drItem["SecId"] = listItem.SecId;
					drItem["Quantity"] = listItem.Quantity;
					dtList.Rows.Add(drItem);
					dtList.AcceptChanges();
				}
				catch
				{
				}
		}
		
        return true;
      }  
      catch (Exception e)
      {
       	SystemEventWindow.Show(e.Message);
        
		status = "Error parsing list.";
        
		return false;
      }
    }

    public int Count
    {
      get 
      {
        return dtList.Rows.Count;
      }
    }
	
	public DataTable Items
	{
		get
		{
			return dtList;
		}
	}
  }
} 
