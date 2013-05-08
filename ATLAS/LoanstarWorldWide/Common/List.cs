using System;
using System.Collections;

namespace StockLoan.Common
{
  public struct ListItem
  {
    public string SecId;
    public long Quantity;
  }

  public class List
  {
      private bool status = false;
    private ArrayList items;

    public List() : this("") {}
    public List(string list)
    {
      items = new ArrayList();
      
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

          delimiters[0] = (char)10; // Use these delimiters to first split list into records.
          delimiters[1] = (char)13;

          records = list.Split(delimiters);

          delimiters[0] = (char)9; // Use these delimiters to split each record into fields.
          delimiters[1] = '=';
          delimiters[2] = ':';
          delimiters[3] = ';';
          delimiters[4] = ' ';

          for (int i = 0; i < records.Length; i++) // First test to see if entire list can be parsed.
          {
              fields = records[i].Split(delimiters, 2);

              if (fields.Length.Equals(2))
              {
                  listItem.SecId = fields[0].Trim();
                  listItem.Quantity = Tools.ParseLong(fields[1].Trim());

                  if ((listItem.SecId.Length > 0) && (listItem.Quantity == 0)) // Try it the other way around.
                  {
                      listItem.SecId = fields[1].Trim();
                      listItem.Quantity = Tools.ParseLong(fields[0].Trim());

                      if ((listItem.SecId.Length > 0) && (listItem.Quantity == 0))
                      {
                          status = false;
                          return status;
                      }
                  }
              }
              else if (fields[0].Trim().Length > 0)
              {
                  status = false;
                  return status;
              }
          }

          for (int i = 0; i < records.Length; i++)
          {
              listItem = new ListItem();

              fields = records[i].Split(delimiters, 2);

              if (fields.Length.Equals(2))
              {
                  listItem.SecId = fields[0].Trim();
                  listItem.Quantity = Tools.ParseLong(fields[1].Trim());

                  if ((listItem.SecId.Length > 0) && (listItem.Quantity == 0)) // Try it the other way around.
                  {
                      listItem.SecId = fields[1].Trim();
                      listItem.Quantity = Tools.ParseLong(fields[0].Trim());

                      if ((listItem.SecId.Length > 0) && !(listItem.Quantity == 0))
                      {
                          items.Add(listItem);
                      }
                  }
                  else
                  {
                      items.Add(listItem);
                  }
              }
          }

          status = true;
          return status;
      }
      catch (Exception e)
      {
          Log.Write(e.Message + " [List.Parse]", Log.Error, 1);

          status = false;
          return status;
      }
    }

    public ListItem ItemGet(int index)
    {
      return (ListItem)items[index];
    }

    public bool Status
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
