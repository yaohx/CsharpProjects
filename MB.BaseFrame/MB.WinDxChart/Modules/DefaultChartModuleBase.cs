using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.Utils.Menu;
using MB.WinDxChart.Common;
using MB.Util.Model.Chart;
using MB.WinDxChart.Chart;

namespace MB.WinDxChart.Modules
{

    public partial class DefaultChartModuleBase : ChartModuleBase
    {
        protected Series seriesSelected = null;
        protected SeriesPoint pointSelected = null;
        protected object selectedAnotherObject = null;

        protected virtual bool SeriesSelection { get { return false; } }
        public DefaultChartModuleBase()
        {
            InitializeComponent();
        }

        protected override void InitControls()
        {
            ChartControl.RuntimeSelection = true;
            ChartControl.MouseUp += new MouseEventHandler(Chart_MouseUp);
            ChartControl.ObjectHotTracked += new HotTrackEventHandler(Chart_ObjectHotTracked);
            ChartControl.ObjectSelected += new HotTrackEventHandler(Chart_ObjectSelected);

            ChartControl.BoundDataChanged += new BoundDataChangedEventHandler(ChartControl_BoundDataChanged);

        }

        void ChartControl_BoundDataChanged(object sender, EventArgs e)
        {
            
        }
        protected virtual bool AllowSelectAnotherObject(object obj)
        {
            return false;
        }
        protected virtual void Chart_ObjectHotTracked(object sender, HotTrackEventArgs e)
        {
            if (e.Object is Series)
                e.Cancel = !SeriesSelection;
            else
                e.Cancel = !AllowSelectAnotherObject(e.Object);
        }
        protected virtual void Chart_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            if (e.Object is Series)
            {
                e.Cancel = !SeriesSelection;
                if (SeriesSelection)
                {
                    this.seriesSelected = (Series)e.Object;
                    this.pointSelected = e.AdditionalObject as SeriesPoint;
                }
            }
            else
            {
                if (AllowSelectAnotherObject(e.Object))
                {
                    this.selectedAnotherObject = e.Object;
                    e.Cancel = false;
                }
                else
                {
                    this.selectedAnotherObject = null;
                    e.Cancel = true;
                    ChartControl.ClearSelection(false);
                }
                if (SeriesSelection)
                {
                    this.seriesSelected = null;
                    this.pointSelected = null;
                }
            }
            UpdateControls();
        }
        protected virtual DXPopupMenu ConstructPopupMenu(object obj, ChartControl chartControl)
        {
            return DXMenuHelper.ConstructMenu(obj, chartControl);
        }
        void Chart_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            object[] hitObjects = ChartControl.HitTest(e.X, e.Y);
            foreach (object hitObject in hitObjects)
            {
                if (hitObject is Axis || hitObject is Legend || hitObject is ChartControl)
                {
                    DXPopupMenu popupMenu = ConstructPopupMenu(hitObject, ChartControl);
                    MenuManagerHelper.ShowMenu(
                        popupMenu,
                        LookAndFeel,
                        MenuManager,
                        ChartControl,
                        new Point(e.X, e.Y));
                    break;
                }
            }
        }
    }
}
