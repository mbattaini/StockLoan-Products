using System;
using System.Drawing;

using C1.Win.C1TrueDBGrid;
using C1.Win.C1Command;

using StockLoan.Common;

namespace NorthStarClient
{
	public class ContextMenuExtend
	{

		private C1TrueDBGrid grid = null;
		private C1ContextMenu menu = null;

		public ContextMenuExtend(ref C1TrueDBGrid grid, ref C1ContextMenu menu)
		{
			this.grid = grid;
			this.menu = menu;
		}

		public void AppendContextMenu()
		{
			C1Command command = null;
			C1CommandLink commandLink = null;

			try
			{
				foreach (C1DisplayColumn column in grid.Splits[0].DisplayColumns)
				{
					command = new C1Command();

					// C1Command will autotomatic add _ if the column.Name has space inside
					// For example: [TD Shorts] will be TD_Shorts
					// In event handler, we need convert this back to space
					command.Name = grid.Name + column.Name + "Command";

					command.Text = column.Name;
					command.Click += new ClickEventHandler(ContextMenuGridColumnCommandClick_Event);
					command.Checked = column.Visible;

					commandLink = new C1CommandLink(command);
					commandLink.Text = column.Name;
					menu.CommandLinks.Add(commandLink);
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex.Message + " [ContextMenuExtend.AppendContextMenu]", Log.Error, 1);
			}
		}

		private void ContextMenuGridColumnCommandClick_Event(object sender, ClickEventArgs e)
		{
			try
			{
				int visiableColumns = 0;
				string columnName = "";

				e.CallerLink.Command.Checked = !e.CallerLink.Command.Checked;

				// we need keep at lease one column as visiable.  
				foreach (C1DisplayColumn column in grid.Splits[0].DisplayColumns)
				{
					if (column.Visible)
						visiableColumns++;
				}

				if (visiableColumns == 1 && e.CallerLink.Command.Checked == false)
				{
					e.CallerLink.Command.Checked = true;
				}
				else
				{
					foreach (C1DisplayColumn column in grid.Splits[0].DisplayColumns)
					{
						columnName = e.CallerLink.Command.Name;
						columnName = columnName.Substring(grid.Name.Length, columnName.LastIndexOf("Command") - grid.Name.Length);

						if (column.Name == columnName.Replace('_', ' '))
						{
							column.Visible = e.CallerLink.Command.Checked;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex.Message + " [ContextMenuExtend.ContextMenuGridColumnCommandClick_Event]", Log.Error, 1);
			}
		}

		public void EnableDisableContextMenu(Point mousePoint)
		{
			bool isFound = false;

			try
			{
				foreach (C1CommandLink link in menu.CommandLinks)
				{
					isFound = false;

					foreach (C1DisplayColumn column in grid.Splits[0].DisplayColumns)
					{
						string columnName = link.Command.Name;
						if (columnName.StartsWith(grid.Name) == false)
							continue;

						columnName = columnName.Substring(grid.Name.Length, columnName.LastIndexOf("Command") - grid.Name.Length);

						if (column.Name == columnName.Replace('_', ' '))
						{
							link.Command.Visible = grid.PointAt(mousePoint) == PointAtEnum.AtColumnHeader ? true : false;
							isFound = true;
							break;
						}
					}

					if (isFound == false)
					{
						//DC new	context menu items which is Not in current grid's column will come here
						//20100510	Which includes other grid's column, AND folating menu item: [SendTo] command 
						//			We want to show [SendTo] item if mouse is not in the column header area. 
						// 			All other non-current grid's columns will be set to Visible = False.
						if ((link.Command.Name.StartsWith(grid.Name) == false) && 	//DC new 
							(link.Command.Name.StartsWith("SendTo") == false))
						{
							link.Command.Visible = false;	//DC new: Not current Visible Grid, and Not Send-To menu, then Visible = false
						}
						else
						{	
							link.Command.Visible = grid.PointAt(mousePoint) != PointAtEnum.AtColumnHeader ? true : false;	//Lei's Original Code:  Does Not work for multiple Grids on same WinForm 
						}
					}

				}
			}
			catch (Exception ex)
			{
				Log.Write(ex.Message + " [ContextMenuExtend.EnableDisableContextMenu]", Log.Error, 1);
			}
		}
	}
}
