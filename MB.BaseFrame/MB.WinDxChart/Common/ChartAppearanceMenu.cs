using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using DevExpress.LookAndFeel;
using DevExpress.XtraCharts;
using DevExpress.DXperience.Demos;
using System.Drawing.Imaging;

namespace MB.WinDxChart.Common
{
    public class ChartAppearanceMenu : DevExpress.DXperience.Demos.LookAndFeelMenu
    {
        BarSubItem miAppearances;
        BarSubItem miPalettes;
        BarSubItem miPrintAndExport;
        BarSubItem miWizard;
        BarSubItem miExtend;

        string appearanceName;
        string paletteName;

        public ChartAppearanceMenu(BarManager manager, DefaultLookAndFeel lookAndFeel, string about)
            : base(manager, lookAndFeel, about)
        {
            CreateAppearancesMenu();
            CreatePalettesMenu();
            CreatePrintAndExportToMenu();
            CreateWizardMenu();
            CreateExtendMenu();

            miFeatures.Caption = "XtraCharts Features";
            miAboutProduct.Caption = "功能说明";

            AddItems();
        }

        public void EnableWizardAndPrintAndAppearancesMenu(bool enable)
        {
            if (miPrintAndExport == null)
                return;
            miPrintAndExport.Enabled = enable;
            miPalettes.Enabled = enable;
            miAppearances.Enabled = enable;
            miWizard.Enabled = enable;
        }
        void UpdateMenu()
        {
            int count = miAppearances.ItemLinks.Count;
            for (int i = 0; i < count; i++)
            {
                BarCheckItem item = miAppearances.ItemLinks[i].Item as BarCheckItem;
                if (item != null)
                    item.Checked = item.Caption == appearanceName;
            }
            if (miPalettes == null)
                return;
            count = miPalettes.ItemLinks.Count;
            for (int i = 0; i < count; i++)
            {
                BarCheckItem item = miPalettes.ItemLinks[i].Item as BarCheckItem;
                if (item != null)
                    item.Checked = item.Caption == paletteName;
            }
        }
        void SetAppearanceName(string name)
        {
            if (miAppearances == null)
                return;
            appearanceName = name;
            string paletteName = ChartModulesInfo.SetAppearanceName(appearanceName);
            if (paletteName.Length > 0)
                this.paletteName = paletteName;
            UpdateMenu();
        }
        void SetPaletteName(string name)
        {
            if (miPalettes == null)
                return;
            paletteName = name;
            string appearanceName = ChartModulesInfo.SetPaletteName(paletteName);
            if (appearanceName.Length > 0)
                this.appearanceName = appearanceName;
            UpdateMenu();
        }
        internal void UpdateAppearanceAndPalette()
        {
            ChartModulesInfo.SetAppearanceName(appearanceName);
            ChartModulesInfo.SetPaletteName(paletteName);
        }

        protected override void AddItems()
        {
            if (miPrintAndExport == null)
                return;

            manager.MainMenu.ItemLinks.Add(miLookAndFeel);
            //manager.MainMenu.ItemLinks.Add(miView);
            manager.MainMenu.ItemLinks.Add(miAppearances);
            manager.MainMenu.ItemLinks.Add(miPalettes);
            manager.MainMenu.ItemLinks.Add(miPrintAndExport);
            manager.MainMenu.ItemLinks.Add(miWizard);
            manager.MainMenu.ItemLinks.Add(miExtend);
            //manager.MainMenu.ItemLinks.Add(miHelp);
            InitLookAndFeelMenu();
        }

        void CreateAppearancesMenu()
        {
            miAppearances = new BarSubItem(this.manager, "&外观");
            ChartControl chart = new ChartControl();
            string[] appearanceNames = chart.GetAppearanceNames();
            int naturalColorIndex = 0;
            for (int i = 0; i < appearanceNames.Length; i++)
            {
                BarItem miAppearanceName = new BarCheckItem(this.manager);
                miAppearanceName.Caption = appearanceNames[i];
                if (appearanceNames[i] == "Dark")  //Nature Colors
                    naturalColorIndex = i;
                miAppearanceName.ItemClick += new ItemClickEventHandler(this.miAppearanceName_Click);
                miAppearances.ItemLinks.Add(miAppearanceName);
            }
            chart.Dispose();
            if (appearanceNames.Length > 0)
            {
                BarCheckItem item = miAppearances.ItemLinks[naturalColorIndex].Item as BarCheckItem;
                if (miAppearances != null)
                {
                    miAppearanceName_Click(this.manager, new ItemClickEventArgs(item, null));
                    item.Checked = true;
                }
            }
        }
        void CreatePalettesMenu()
        {
            miPalettes = new BarSubItem(this.manager, "&调色板");
            ChartControl chart = new ChartControl();
            string[] paletteNames = chart.GetPaletteNames();
            int naturalColorIndex = 0;
            for (int i = 0; i < paletteNames.Length; i++)
            {
                BarItem miPaletteName = new BarCheckItem(this.manager);
                miPaletteName.Caption = paletteNames[i];
                if (paletteNames[i] == "Nature Colors")
                    naturalColorIndex = i;
                miPaletteName.ItemClick += new ItemClickEventHandler(this.miPaletteName_Click);
                miPalettes.ItemLinks.Add(miPaletteName);
            }
            chart.Dispose();
            if (paletteNames.Length > 0)
            {
                BarCheckItem item = miPalettes.ItemLinks[naturalColorIndex].Item as BarCheckItem;
                if (miPalettes != null)
                {
                    miPaletteName_Click(this.manager, new ItemClickEventArgs(item, null));
                    item.Checked = true;
                }
            }
        }
        void CreatePrintAndExportToMenu()
        {
            BarItem miExportToPdf = new ButtonBarItem(this.manager, "导出到PDF", new ItemClickEventHandler(miExportToPdf_Click));
            BarItem miExportToHtml = new ButtonBarItem(this.manager, "导出到HTML", new ItemClickEventHandler(miExportToHtml_Click));
            BarItem miExportToMht = new ButtonBarItem(this.manager, "导出到MHT", new ItemClickEventHandler(miExportToMht_Click));
            BarItem miExportToXls = new ButtonBarItem(this.manager, "导出到XLS", new ItemClickEventHandler(miExportToXls_Click));
            BarItem miPrintPreview = new ButtonBarItem(this.manager, "打印预览", new ItemClickEventHandler(miPrintPreview_Click));

            BarSubItem miExportToImage = new BarSubItem(this.manager, "导出到Image");
            AddImageFormat(miExportToImage, ImageFormat.Bmp);
            AddImageFormat(miExportToImage, ImageFormat.Emf);
            AddImageFormat(miExportToImage, ImageFormat.Exif);
            AddImageFormat(miExportToImage, ImageFormat.Gif);
            AddImageFormat(miExportToImage, ImageFormat.Icon);
            AddImageFormat(miExportToImage, ImageFormat.Jpeg);
            AddImageFormat(miExportToImage, ImageFormat.Png);
            AddImageFormat(miExportToImage, ImageFormat.Tiff);
            AddImageFormat(miExportToImage, ImageFormat.Wmf);

            miPrintAndExport = new BarSubItem(this.manager, "&打印和导出");
            miPrintAndExport.ItemLinks.AddRange(new BarItem[] {
																  miExportToPdf,
																  miExportToHtml,
																  miExportToMht,
																  miExportToXls,
																  miExportToImage
															  });
            miPrintAndExport.ItemLinks.Add(miPrintPreview).BeginGroup = true;
        }
        void CreateWizardMenu()
        {
            BarItem miRunChartWizard = new ButtonBarItem(this.manager, "&运行向导...", new ItemClickEventHandler(miRunChartWizard_Click));
            miWizard = new BarSubItem(this.manager, "&自定义向导");
            miWizard.ItemLinks.Add(miRunChartWizard);
        }

        void CreateExtendMenu()
        {
            if (miExtend == null)
            {
                miExtend = new BarSubItem(this.manager, "&扩展");
            }
        }

        public void ResetExtendMenu(BarSubItem subItem)
        {
            if (subItem != null && subItem.ItemLinks.Count > 0)
            {
                miExtend.ItemLinks.Assign(subItem.ItemLinks);
            }
        }

        void AddImageFormat(BarSubItem biImagesMenuItem, ImageFormat format)
        {
            ImageCodecInfo codecInfo = FindImageCodec(format);
            if (codecInfo == null)
                return;
            BarExportToImageItem item = new BarExportToImageItem(this.manager, format, codecInfo);
            item.ItemClick += new ItemClickEventHandler(OnExportImageClick);
            biImagesMenuItem.AddItem(item);
        }
        ImageCodecInfo FindImageCodec(ImageFormat format)
        {
            ImageCodecInfo[] infos = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo item in infos)
            {
                if (item.FormatID.Equals(format.Guid))
                    return item;
            }
            return null;
        }

        void miAppearanceName_Click(object sender, ItemClickEventArgs e)
        {
            FrmEditChart parentForm = this.manager.Form as FrmEditChart;
            if (parentForm == null)
                return;
            BarItem item = e.Item;
            if (item == null)
                return;
            SetAppearanceName(item.Caption);
        }
        void miPaletteName_Click(object sender, ItemClickEventArgs e)
        {
            FrmEditChart parentForm = this.manager.Form as FrmEditChart;
            if (parentForm == null)
                return;
            BarItem item = e.Item;
            if (item == null)
                return;
            SetPaletteName(item.Caption);
        }
        void miPrintPreview_Click(object sender, ItemClickEventArgs e)
        {
            ChartModulesInfo.PrintPreview();
        }
        void miExportToHtml_Click(object sender, ItemClickEventArgs e)
        {
            ChartModulesInfo.ExportToHtml();
        }
        void miExportToMht_Click(object sender, ItemClickEventArgs e)
        {
            ChartModulesInfo.ExportToMht();
        }
        void miExportToPdf_Click(object sender, ItemClickEventArgs e)
        {
            ChartModulesInfo.ExportToPdf();
        }
        void miExportToXls_Click(object sender, ItemClickEventArgs e)
        {
            ChartModulesInfo.ExportToXls();
        }
        void miRunChartWizard_Click(object sender, ItemClickEventArgs e)
        {
            ChartModulesInfo.RunChartWizard();
        }
        void OnExportImageClick(object sender, ItemClickEventArgs e)
        {
            BarExportToImageItem item = e.Item as BarExportToImageItem;
            if (item == null)
                return;
            ChartModulesInfo.ExportToImage(item.ImageCodecInfo, item.ImageFormat);
        }
        protected override void miAboutProduct_Click(object sender, ItemClickEventArgs e)
        {
            DevExpress.Utils.About.AboutForm.Show(typeof(DevExpress.XtraCharts.Native.Chart), DevExpress.Utils.About.ProductKind.XtraCharts, DevExpress.Utils.About.ProductInfoStage.Registered);
        }
        protected override string ProductName { get { return "XtraCharts"; } }
        protected override void biProductWebPage_Click(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.devexpress.com/Products/NET/WinForms/XtraCharts/");
        }
    }

    #region BarExportToImageItem
    public class BarExportToImageItem : BarButtonItem
    {
        ImageFormat imageFormat;
        ImageCodecInfo imageCodecInfo;
        public BarExportToImageItem(BarManager barManager, ImageFormat imageFormat, ImageCodecInfo imageCodecInfo)
            : base(barManager, String.Empty)
        {
            this.imageFormat = imageFormat;
            this.imageCodecInfo = imageCodecInfo;
            this.Caption = String.Format("{0}", this.imageCodecInfo.FormatDescription);
        }
        public ImageFormat ImageFormat { get { return imageFormat; } }
        public ImageCodecInfo ImageCodecInfo { get { return imageCodecInfo; } }
    }
    #endregion
}
