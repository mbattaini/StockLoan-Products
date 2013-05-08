using System;
using System.Collections;

namespace StockLoan.Common
{
  public struct DealItem
  {
    public string SecId;
    public string Quantity;
    public string Rate;
  }

  public class DealList
  {
    private string status = "";
    private ArrayList items;

    public DealList() : this("") {}
    public DealList(string list)
    {
      items = new ArrayList();
      
      if (!list.Equals(""))
      {
        Parse(list);
      }
    }

    public string Parse(string list)
    {
      string [] records;
      string [] fields;
      char [] delimiters = new char[5];

      try
      {
        delimiters[0] = (char)10; // Use these delimiters to split list into records.
        delimiters[1] = (char)13;

        records = list.Split(delimiters); // Do the split.

        delimiters[0] = (char)9; // Use these delimiters to split each record into fields.
        delimiters[1] = '=';
        delimiters[2] = ':';
        delimiters[3] = ';';
        delimiters[4] = ' ';

        for (int i = 0; i < records.Length; i++) // Parse list items.
        {
          DealItem dealItem = new DealItem();

          fields = records[i].Split(delimiters, 3);

          // Replace the '/', '\', and '-' operators with '.' in ticker symbol for preferred stock.
          fields[0] = fields[0].Replace('/', '.');
          fields[0] = fields[0].Replace('\\', '.');
          fields[0] = fields[0].Replace('-', '.');

          dealItem.SecId = fields[0];
          
          // Initialize in case no rate or quantity.
          dealItem.Quantity = "";
          dealItem.Rate = "";

          if (fields.Length > 1 && Tools.IsNumeric(fields[1])) // Look for a rate or quantity.
          {
            if (fields[1].IndexOf(".") > -1) // This is a rate.
            {
              dealItem.Rate = fields[1];
            }
            else
            {
              dealItem.Quantity = Tools.ParseLong(fields[1]).ToString();
            }

            if ((fields.Length > 2) && Tools.IsNumeric(fields[2])) // Look again for a rate or quantity.
            {
              if (fields[2].IndexOf(".") > -1) // This is a rate.
              {
                dealItem.Rate = fields[2];
              }
              else
              {
                dealItem.Quantity = Tools.ParseLong(fields[2]).ToString();
              }
            }
          }

          if (dealItem.SecId.Length > 0) // We have a security to add to the list.
          {
            items.Add(dealItem);
          }
        }

        status = "OK";
        return status;
      }  
      catch (Exception e)
      {
        Log.Write(e.Message + " [DealList.Parse]", 2);
        
        status = "Error: Unable to parse your list.";
        return status;
      }
    }

    public DealItem ItemGet(int index)
    {
      return (DealItem)items[index];
    }

    public string Status
    {
      get 
      {
        return status; 
      }
    }

    public int Count
    {
      get 
      {
        return items.Count;
      }
    }
  }
}
