using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel.Design; 
using System.Windows.Forms;

using MB.WinClientDefault.UICommand; 
namespace MB.WinClientDefault.Images {
    /// <summary>
    /// Image ico 处理相关。
    /// </summary>
    public sealed class ImageListHelper {
        private static readonly string IAMGE_RESOURCE_BASE_PATH = "MB.WinClientDefault.Images.";   
        private static readonly string MODULE_IMAGE_FILE = "MB.WinClientDefault.Images.Module.ico";
        //private static readonly string MODULE_IMAGE_SELECT_FILE = "MB.WinClientDefault.Images.ModuleSelected.ico";
        //private static readonly string MODULE_LEAF_IMAGE_FILE = "MB.WinClientDefault.Images.ModuleFunction.ico";
        //private static readonly string MODULE_EXISTS_CMD_IMAGE_FILE = "MB.WinClientDefault.Images.ExistsCommand.ico";

        private ImageInfo<CommandID>[] _XCommandsImage = {
                                                         new ImageInfo<CommandID>("AddNew.ico",0,UICommands.AddNew), 
                                                         new ImageInfo<CommandID>("AdvanceSearch.ico",1,UICommands.Query),
                                                         new ImageInfo<CommandID>("Aggregation.ico",2,UICommands.Aggregation),
                                                         new ImageInfo<CommandID>("Calculator.ico",3,UICommands.Calculator),
                                                         new ImageInfo<CommandID>("Copy.ico",4,UICommands.Copy),
                                                         new ImageInfo<CommandID>("Delete.ico",5,UICommands.Delete),
                                                         new ImageInfo<CommandID>("HelpList.ico",6,UICommands.HelpList),
                                                         new ImageInfo<CommandID>("OnlineMessage.ico",7,UICommands.OnlineMessage),
                                                         new ImageInfo<CommandID>("Refresh.ico",8,UICommands.Refresh),
                                                         new ImageInfo<CommandID>("Save.ico",9,UICommands.Save),
                                                         new ImageInfo<CommandID>("Submit.ico",10,UICommands.Submit),
                                                         new ImageInfo<CommandID>("CancelSubmit.ico",11,UICommands.CancelSubmit),
                                                         new ImageInfo<CommandID>("Open.ico",12,UICommands.Open),
                                                         new ImageInfo<CommandID>("Print.ico",13,UICommands.Print),
                                                         new ImageInfo<CommandID>("PrintPreview.ico",14,UICommands.PrintPreview),
                                                         new ImageInfo<CommandID>("SearchTemplet.ico",15,UICommands.SearchTemplet),
                                                         new ImageInfo<CommandID>("Output.ico",16,UICommands.DataExport),
                                                         new ImageInfo<CommandID>("DynamicGroupSetting.ico",17,UICommands.DynamicAggregation)
                                                         };


        /// <summary>
        /// Instance.
        /// </summary>
        public static ImageListHelper Instance {
            get {
                return MB.Util.SingletonProvider<ImageListHelper>.Instance;  
            }
        }
        /// <summary>
        /// 获取树型节点的默认图标信息。
        /// </summary>
        /// <returns></returns>
        public ImageList GetDefaultTreeNodeImage() {
            ImageList imageList = new ImageList();

            var img = MB.WinClientDefault.Images.ImageListHelper.Instance.CreateBitmapFromResources(MODULE_IMAGE_FILE);

            return imageList;
        }
        /// <summary>
        /// 获取XCommand 命令的图标。
        /// </summary>
        /// <returns></returns>
        public ImageList CreateXCommandsImage() {
            ImageList lst = new ImageList();
            //lst.ImageSize = new Size(32, 32);
            foreach (ImageInfo<CommandID> info in _XCommandsImage) {
                Bitmap img =  CreateBitmapFromResources(IAMGE_RESOURCE_BASE_PATH + info.Name);
                lst.Images.Add(img); 
            }
            return lst;
        }
		/// <summary>
		/// 根据CommandID 获取对应的图标Index.
		/// </summary>
		/// <param name="commandID"></param>
		/// <returns></returns>
		public  int GetImageIndexByCommandID(CommandID commandID){
            IEnumerable<ImageInfo<CommandID>> imgs = _XCommandsImage.Where<ImageInfo<CommandID>>(o => o.RelationData.Equals(commandID));
            if (imgs != null && imgs.Count<ImageInfo<CommandID>>() > 0)
                return imgs.First < ImageInfo<CommandID>>().Index;
            else
                return -1;
		}
        /// <summary>
        /// 从资源中创建一个位图。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Bitmap CreateBitmapFromResources(string fileName) {
            //System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            //System.IO.Stream stream = asm.GetManifestResourceStream(fileName);
            //if (stream == null)
            //    throw new MB.Util.APPException(string.Format("文件名:{0} 在资源文件中找不到,请确认是否已经添加该文件作为嵌入资源?",fileName));

            //Bitmap image = (Bitmap)Bitmap.FromStream(stream);
            //return image;

            return MB.WinBase.ShareLib.Instance.CreateBitmapFromResources(System.Reflection.Assembly.GetExecutingAssembly(),fileName);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ImageInfo<T> {
        private string _Name;
        private int _Index;
        private T _RelationData;
        public ImageInfo(string imageName, int index, T relationData) {
            _Name = imageName;
            _Index = index;
            _RelationData = relationData;
        }
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        public int Index {
            get {
                return _Index;
            }
            set {
                _Index = value;
            }
        }
        public T RelationData {
            get {
                return _RelationData;
            }
            set {
                _RelationData = value;
            }
        }
    }
}
