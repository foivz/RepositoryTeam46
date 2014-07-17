using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Seds.Web.UserControls
{
    public class BaseGrid<TItem> : BaseUserControl
    {
        #region Properties

        private SortDirection? _sortingDirection = null;

        protected GridView _gridControl = null;

        protected GridView GridControl
        {
            get
            {
                if (_gridControl == null)
                {
                    _gridControl = (GridView)this.FindControl(GridControlId);
                }
                return _gridControl;
            }
        }

        protected virtual string GridControlId
        {
            get
            {
                return "gvData";
            }
        }

        protected virtual string InitialSortColumn
        {
            get
            {
                return "Id";  //stavio ID umjesto LastName jer ga nemam u nekim tablicama
            }
        }

        protected virtual bool HideIfNoItems
        {
            get { return false; }
        }

        protected string SortingExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"] = InitialSortColumn;
                }
                return ViewState["SortExpression"].ToString();
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        public SortDirection SortingDirection
        {
            get
            {
                if (_sortingDirection == null)
                {
                    if (ViewState["SortDirection"] == null)
                    {
                        ViewState["SortDirection"] = SortDirection.Ascending;
                    }
                    else
                    {
                        if ((SortDirection)ViewState["SortDirection"] == SortDirection.Descending)
                        {
                            ViewState["SortDirection"] = SortDirection.Ascending;
                        }
                        else
                        {
                            ViewState["SortDirection"] = SortDirection.Descending;
                        }
                    }

                    _sortingDirection = (SortDirection)ViewState["SortDirection"];
                }

                return _sortingDirection.Value;
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        public Func<IQueryable<TItem>> GetItems { get; set; }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {   
                BindGrid(SortingExpression, SortingDirection);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            GridControl.PageSize = 15;
            GridControl.Sorting += new GridViewSortEventHandler(GridControl_Sorting);
            GridControl.PageIndexChanging += new GridViewPageEventHandler(GridControl_PageIndexChanging);

            GridControl.RowCommand += GridControl_RowCommand;

            GridControl.RowDeleted += (s, ea) => { };
            GridControl.RowDeleting += (s, ea) => { };

            base.OnInit(e);
        }

        void GridControl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                Delete(int.Parse(e.CommandArgument.ToString()));
            }
        }

        void GridControl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridControl.PageIndex = e.NewPageIndex;

            BindGrid(SortingExpression, GridControl.SortDirection);
        }

        void GridControl_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortingExpression = e.SortExpression;

            BindGrid(SortingExpression, SortingDirection);
        }

        #endregion

        public void BindGrid()
        {
            BindGrid(SortingExpression, SortingDirection);
        }

        protected virtual void BindGrid(string sortingExpression, SortDirection sortDirection)
        {
            if (GetItems != null)
            {
                List<TItem> list = null;

                var items = GetItems();
                if (items != null)
                {
                    list = items.ToList();
                }

                if (list != null && list.Count > 0)
                {
                    this.Visible = true;

                    GridControl.DataSource = list.OrderBy(sortingExpression, sortDirection).ToList();
                    GridControl.DataBind();
                }
                else
                {
                    if (HideIfNoItems)
                    {
                        this.Visible = false;
                    }
                }
            }
        }

        protected virtual bool Delete(int id)
        {
            return false;
        }
    }
}