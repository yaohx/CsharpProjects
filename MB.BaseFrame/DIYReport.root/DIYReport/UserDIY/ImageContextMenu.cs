////---------------------------------------------------------------- 
//// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
//// All rights reserved. 
//// 
//// Author		:	Nick
//// Create date	:	2006-04-20
//// Description	:	创建可以绑定图标的弹出菜单。 
//// Modify date	:			By:					Why: 
////----------------------------------------------------------------
//using System;
//using System.Windows.Forms;
//using System.Drawing;
//using System.Collections;
//using System.ComponentModel;
//
//namespace DIYReport.UserDIY
//{
//	/// <summary>
//	/// 创建可以绑定图标的弹出菜单。
//	/// </summary>
//	[ToolboxItem(false)]
//	public class ImageContextMenu : ContextMenu {
//		private ImageList images;
//		private int width;
//		
//
//		private Font Font { get { return System.Windows.Forms.Control.DefaultFont; }
//		}
//		internal ImageList Images { 
//			get { return images; } 
//			set { 
//				images = value;
//				//CreateContent();
//			}
//		}
//
//		internal event EventHandler ItemClick;
//		private void DoItemClick(object sender, System.EventArgs e) {
//			if(ItemClick != null) ItemClick(sender, e);
//		}
//		public ImageContextMenu(){
//			this.width = 100;
//		}
//		public ImageContextMenu(int width) {
//			this.width = width;
//		}
//		protected void CreateContent() {
//			MenuItems.Clear();
//			if(images == null) return;
//			for(int i = 0; i < images.Images.Count; i++) {
//				MenuItem item = new MenuItem( i.ToString() );
//				item.OwnerDraw = true;
//				item.DrawItem += new DrawItemEventHandler(menuItem_DrawItem);
//				item.MeasureItem += new MeasureItemEventHandler(menuItem_MeasureItem);
//				item.Click += new System.EventHandler(menuItem_Click);
//				MenuItems.Add(item);
//			}
//		}
//		private void menuItem_Click(object sender, System.EventArgs e) {
//			DoItemClick(sender, e);
//		}
//		private void menuItem_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e) {
//			e.ItemWidth = width;
//			e.ItemHeight = Font.Height + 4;
//		}
//		
//		private void menuItem_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e) {
//			bool selected = (e.State & DrawItemState.Selected) > 0;
//			if(selected) {
//				e.DrawBackground();
//			} else {
//				e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds);
//			}
//			Rectangle r = new Rectangle(e.Bounds.X, e.Bounds.Y, (int)(1.5 * e.Bounds.Height), e.Bounds.Height);
//			r.Inflate(-2, -2);
//			e.Graphics.DrawImage((Image)images.Images[e.Index], r);
//			e.Graphics.DrawRectangle(new Pen(SystemBrushes.ControlText), r);
//			r.Offset(r.Width + r.Height, 0);
//			StringFormat sf = new StringFormat();
//			sf.LineAlignment = StringAlignment.Center;
//			e.Graphics.DrawString(((MenuItem)sender).Text, Font, new SolidBrush(e.ForeColor), r, sf);
//		}
//	}
//}
