using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.DXperience.Demos;
using MB.WinDxChart.Modules;
using DevExpress.Utils.Menu;
using System.Windows.Forms;
using System.Drawing.Imaging;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Wizard;
using DevExpress.XtraPrinting;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts.Printing;
using MB.WinBase.Common;
using MB.Util.Model.Chart;
using MB.WinDxChart.Template;
using System.IO;
using MB.WinBase.IFace;

namespace MB.WinDxChart.Common
{
    public class ChartModulesInfo : ModulesInfo
    {
        public static void InitChartControl(IClientRuleQueryBase clientRule, object datasource, ChartTemplateInfo template)
        {
            if (Instance.CurrentModuleBase != null)
            {
                ChartModuleBase chartModule = Instance.CurrentModuleBase.TModule as ChartModuleBase;
                if (chartModule != null)
                {
                    chartModule.Template = template;
                    chartModule.InitChartControl(clientRule);
                    //绑定数据源
                    chartModule.BindingChartControl(datasource);
                }
            }
        }

        public static ChartTemplateInfo GetChartTemplate()
        {
            if (Instance.CurrentModuleBase != null)
            {
                ChartModuleBase chartModule = Instance.CurrentModuleBase.TModule as ChartModuleBase;
                if (chartModule != null)
                {
                    return chartModule.Template;
                   
                }
            }

            return null;
        }

        public static void SaveChartTemplate(ChartTemplateInfo template)
        {
            if (Instance.CurrentModuleBase != null)
            {
                ChartModuleBase chartModule = Instance.CurrentModuleBase.TModule as ChartModuleBase;
                if (chartModule != null)
                {
                    chartModule.SaveChartControl(template);
                }
            }
        }
    
        public static string SetAppearanceName(string appearanceName)
        {
            if (Instance.CurrentModuleBase != null)
            {
                ChartModuleBase chartModule = Instance.CurrentModuleBase.TModule as ChartModuleBase;
                if (chartModule != null)
                {
                    chartModule.AppearanceName = appearanceName;
                    return chartModule.PaletteName;
                }
            }
            return String.Empty;
        }
        public static string SetPaletteName(string paletteName)
        {
            if (Instance.CurrentModuleBase != null)
            {
                ChartModuleBase chartModule = Instance.CurrentModuleBase.TModule as ChartModuleBase;
                if (chartModule != null)
                {
                    chartModule.PaletteName = paletteName;
                    return chartModule.AppearanceName;
                }
            }
            return String.Empty;
        }

        public static void ShowModule(string name, DevExpress.XtraEditors.PanelControl panel, ChartAppearanceMenu chartAppearanceMenu, IDXMenuManager menuManager, DevExpress.Utils.Frames.ApplicationCaption caption)
        {
            ModuleInfo item = ChartModulesInfo.GetItem(name);
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {               
                TutorialControlBase tutorialBase = item.TModule as TutorialControlBase;
                tutorialBase.Bounds = panel.DisplayRectangle;
                Instance.CurrentModuleBase = item;
                tutorialBase.Visible = false;
                panel.Controls.Add(tutorialBase);
                tutorialBase.Dock = DockStyle.Fill;

                //-----Init----
                //-----Set----
                tutorialBase.TutorialName = name;

                TutorialControl tutorial = tutorialBase as TutorialControl;
                if (tutorial != null)
                    tutorial.MenuManager = menuManager;

                ChartModuleBase chartDemo = tutorialBase as ChartModuleBase;

                chartAppearanceMenu.UpdateAppearanceAndPalette();

                bool wizardAndPrintAndAppearancesMenuEnabled = chartDemo != null;
                chartAppearanceMenu.EnableWizardAndPrintAndAppearancesMenu(wizardAndPrintAndAppearancesMenuEnabled);
                //------------

                tutorialBase.Visible = true;
                item.WasShown = true;
            }
            finally
            {
                Cursor.Current = currentCursor;
            }
            RaiseModuleChanged();
        }


        public static void ShowModule(string name, DevExpress.XtraEditors.GroupControl group, ChartAppearanceMenu chartAppearanceMenu, IDXMenuManager menuManager, DevExpress.Utils.Frames.ApplicationCaption caption)
        {
            ModuleInfo item = ChartModulesInfo.GetItem(name);
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Control oldTutorial = null;
                if (Instance.CurrentModuleBase != null)
                {
                    if (Instance.CurrentModuleBase.Name == name) return;
                    oldTutorial = Instance.CurrentModuleBase.TModule;
                }

                TutorialControlBase tutorialBase = item.TModule as TutorialControlBase;
                tutorialBase.Bounds = group.DisplayRectangle;
                Instance.CurrentModuleBase = item;
                tutorialBase.Visible = false;
                group.Controls.Add(tutorialBase);
                tutorialBase.Dock = DockStyle.Fill;

                //-----Init----
                //-----Set----
                tutorialBase.TutorialName = name;
                tutorialBase.Caption = caption;

                TutorialControl tutorial = tutorialBase as TutorialControl;
                if (tutorial != null)
                    tutorial.MenuManager = menuManager;

                ChartModuleBase chartDemo = tutorialBase as ChartModuleBase;
                chartAppearanceMenu.UpdateAppearanceAndPalette();

                bool wizardAndPrintAndAppearancesMenuEnabled = chartDemo != null;
                chartAppearanceMenu.EnableWizardAndPrintAndAppearancesMenu(wizardAndPrintAndAppearancesMenuEnabled);
                //------------

                tutorialBase.Visible = true;
                item.WasShown = true;
                if (oldTutorial != null)
                    oldTutorial.Visible = false;
            }
            finally
            {
                Cursor.Current = currentCursor;
            }
            RaiseModuleChanged();
        }

        public static void ExportToHtml()
        {
            ExportTo("HTML Document", "HTML Documents (*.htm; *.html)|*.htm; *.html", "HTML");
        }
        public static void ExportToMht()
        {
            ExportTo("MHT Document", "MHT Documents (*.mht)|*.mht", "MHT");
        }
        public static void ExportToPdf()
        {
            ExportTo("PDF Document", "PDF Documents (*.pdf)|*.pdf", "PDF");
        }
        public static void ExportToXls()
        {
            ExportTo("XLS Document", "XLS Documents (*.xls)|*.xls", "XLS");
        }
        public static void ExportToImage(ImageCodecInfo imageCodecInfo, ImageFormat format)
        {
            string formatTitle = String.Format("{0} image", imageCodecInfo.FormatDescription);
            string fileMask = imageCodecInfo.FilenameExtension;
            ExportTo(formatTitle, String.Format("{0} ({1})|{1}", formatTitle, fileMask), "IMAGE", format, false);
        }
        public static void PrintPreview()
        {
            ChartControl chart = PrepeareForPrintOrExport(true);
            if (chart != null)
            {
                chart.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom;
                chart.ShowPrintPreview();
            }
        }
        public static void RunChartWizard()
        {
            if (Instance.CurrentModuleBase != null)
            {
                ChartModuleBase chartModule = Instance.CurrentModuleBase.TModule as ChartModuleBase;
                if (chartModule != null)
                {
                    ChartWizard chartWizard = new ChartWizard(chartModule.ChartControl);
                    chartWizard.ShowDialog();
                    chartModule.UpdateControls();
                }
            }
        }
        static ChartControl PrepeareForPrintOrExport(bool checkPrinterAvailable)
        {
            if (!PrintHelper.IsPrintingAvailable && checkPrinterAvailable)
            {
                XtraMessageBox.Show("XtraPrinting Library is currently inaccesible.", "XtraCharts Demo");
                return null;
            }
            if (Instance.CurrentModuleBase == null)
                return null;
            ChartModuleBase chartDemoBase = Instance.CurrentModuleBase.TModule as ChartModuleBase;
            if (chartDemoBase == null)
                return null;
            return chartDemoBase.ChartControl;
        }
        static string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            string name = Application.ProductName;
            int n = name.LastIndexOf(".") + 1;
            if (n > 0) name = name.Substring(n, name.Length - n);
            dlg.Title = "Export To " + title;
            dlg.FileName = name;
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }
        static void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    XtraMessageBox.Show("Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        static void ExportTo(string title, string filter, string exportFormat)
        {
            ExportTo(title, filter, exportFormat, null, true);
        }
        static void ExportTo(string title, string filter, string exportFormat, ImageFormat format, bool checkPrinterAvailable)
        {
            ChartControl chart = PrepeareForPrintOrExport(checkPrinterAvailable);
            if (chart == null)
                return;
            string fileName = ShowSaveFileDialog(title, filter);
            if (fileName != "")
            {
                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                switch (exportFormat)
                {
                    case "HTML":
                        chart.ExportToHtml(fileName);
                        break;
                    case "MHT":
                        chart.ExportToMht(fileName);
                        break;
                    case "PDF":
                        PrintSizeMode sizeMode = chart.OptionsPrint.SizeMode;
                        chart.OptionsPrint.SizeMode = PrintSizeMode.Zoom;
                        try
                        {
                            chart.ExportToPdf(fileName);
                        }
                        finally
                        {
                            chart.OptionsPrint.SizeMode = sizeMode;
                        }
                        break;
                    case "XLS":
                        chart.ExportToXls(fileName);
                        break;
                    case "IMAGE":
                        chart.ExportToImage(fileName, format);
                        break;
                }
                Cursor.Current = currentCursor;
                OpenFile(fileName);
            }
        }

        public static void SaveTemplate(ChartTemplateInfo template)
        {
            try
            {

                if (template.ID <= 0)
                {
                    string tempType = template.TEMPLATE_TYPE;
                    string name = template.NAME;

                    FrmSaveTemplate frm = new FrmSaveTemplate(tempType, name);
                    frm.ShowDialog();

                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {

                        template.NAME = frm.TemplateName;
                        template.TEMPLATE_TYPE = frm.TemplateType;


                    }
                    else return;
                }

                string path = AppDomain.CurrentDomain.BaseDirectory + "\\ChartTemplate\\" + template.TEMPLATE_TYPE + "_" + template.NAME + ".xml";

                if (Instance.CurrentModuleBase == null)
                    return;
                ChartModuleBase chartDemoBase = Instance.CurrentModuleBase.TModule as ChartModuleBase;
                if (chartDemoBase == null)
                    return;
                chartDemoBase.ChartControl.SaveToFile(path);

                byte[] file = File.ReadAllBytes(path);

                template.TEMPLATE_FILE = file;
                File.Delete(path);

                MB.WinDxChart.Chart.DxChartControlHelper.Instance.SaveChartTemplate(template);

            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }
    }
}
