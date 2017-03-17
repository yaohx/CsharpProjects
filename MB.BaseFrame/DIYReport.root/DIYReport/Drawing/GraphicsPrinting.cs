//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-08
// Description	:	处理设备之间的单位换算。 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

namespace DIYReport.Drawing
{
	public class GraphicsDpi {
		public static readonly float Display = 75f;
		public static readonly float Inch = 1f;
		public static readonly float Document = 300f;
		public static readonly float Millimeter = 25.4f;
		public static readonly float Pixel = 96f;
		public static readonly float Point = 72f;
		public static readonly float HundredthsOfAnInch = 100f;
		public static readonly float TenthsOfAMillimeter = 254f;
		public static readonly float Twips = 1440f; 
		
		static GraphicsDpi() {
			Graphics graph = Graphics.FromHwnd(IntPtr.Zero);
			Pixel = graph.DpiX;
			graph.Dispose();
		}
		public static float UnitToDpi(GraphicsUnit unit) {
			switch(unit) {
				case GraphicsUnit.Display :
					return Display;
				case GraphicsUnit.Inch :
					return Inch;
				case GraphicsUnit.Document :
					return Document;
				case GraphicsUnit.Millimeter :
					return Millimeter;
				case GraphicsUnit.Pixel :
					return Pixel;
				case GraphicsUnit.Point :
					return Point;
			}
			throw new ArgumentException("unit");
		}
		public static GraphicsUnit DpiToUnit(float dpi) {
			if(dpi.Equals(Display))
				return GraphicsUnit.Display;
			if(dpi.Equals(Inch))
				return GraphicsUnit.Inch;
			if(dpi.Equals(Document))
				return GraphicsUnit.Document;
			if(dpi.Equals(Millimeter))
				return GraphicsUnit.Millimeter;
			if(dpi.Equals(Pixel))
				return GraphicsUnit.Pixel;
			if(dpi.Equals(Point))
				return GraphicsUnit.Point;
			throw new ArgumentException("dpi");
		}
	}

	public class GraphicsUnitConverter {
		static float scale = 1.0f;

		private static void SetScale(float fromDpi, float toDpi) {
			scale = toDpi / fromDpi;
		}
		static private float ConvF(float val) {
			return val * scale;
		}
		static private int Conv(int val) {
			return System.Convert.ToInt32(val * scale);
		}

		static public Rectangle Convert(Rectangle val, GraphicsUnit fromUnit, GraphicsUnit toUnit) {
			return Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));
		}
		static public Size Convert(Size val, GraphicsUnit fromUnit, GraphicsUnit toUnit) {
			return Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));
		}
		static public Point Convert(Point val, GraphicsUnit fromUnit, GraphicsUnit toUnit) {
			return Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));
		}
		static public RectangleF Convert(RectangleF val, GraphicsUnit fromUnit, GraphicsUnit toUnit) {
			return Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));
		}
		static public SizeF Convert(SizeF val, GraphicsUnit fromUnit, GraphicsUnit toUnit) {
			return Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));
		}
		static public PointF Convert(PointF val, GraphicsUnit fromUnit, GraphicsUnit toUnit) {
			return Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));
		}
		static public float Convert(float val, GraphicsUnit fromUnit, GraphicsUnit toUnit) {
			return Convert(val, GraphicsDpi.UnitToDpi(fromUnit), GraphicsDpi.UnitToDpi(toUnit));
		}
		static public Rectangle Round(RectangleF rect) {
			Point p = new Point(System.Convert.ToInt32(rect.Left), System.Convert.ToInt32(rect.Top));
			Size s = new Size(System.Convert.ToInt32(rect.Right) - p.X, System.Convert.ToInt32(rect.Bottom) - p.Y);
			return new Rectangle(p, s);
		}
		static public Rectangle Convert(Rectangle val, float fromDpi, float toDpi) {
			return Round(Convert((RectangleF)val, fromDpi, toDpi));
		}
		static public Size Convert(Size val, float fromDpi, float toDpi) {
			SetScale(fromDpi, toDpi);
			return new Size(Conv(val.Width), Conv(val.Height));
		}
		static public Point Convert(Point val, float fromDpi, float toDpi) {
			SetScale(fromDpi, toDpi);
			return new Point(Conv(val.X), Conv(val.Y));
		}
		static public int Convert(int val, float fromDpi, float toDpi) {
			SetScale(fromDpi, toDpi);
			return Conv(val);
		}
		static public RectangleF Convert(RectangleF val, float fromDpi, float toDpi) {
			SetScale(fromDpi, toDpi);
			return RectangleF.FromLTRB(ConvF(val.Left), ConvF(val.Top), ConvF(val.Right), ConvF(val.Bottom));
		}
		static public SizeF Convert(SizeF val, float fromDpi, float toDpi) {
			SetScale(fromDpi, toDpi);
			return new SizeF(ConvF(val.Width), ConvF(val.Height));
		}
		static public PointF Convert(PointF val, float fromDpi, float toDpi) {
			SetScale(fromDpi, toDpi);
			return new PointF(ConvF(val.X), ConvF(val.Y));
		}
		public static float Convert(float val, float fromDpi, float toDpi) {
			SetScale(fromDpi, toDpi);
			return ConvF(val);
		}

		static public RectangleF PixelToDoc(RectangleF val) {
			return Convert(val, GraphicsDpi.Pixel, GraphicsDpi.Document);
		}
		static public SizeF PixelToDoc(SizeF val) {
			return Convert(val, GraphicsDpi.Pixel, GraphicsDpi.Document);
		}
		static public PointF PixelToDoc(PointF val) {
			return Convert(val, GraphicsDpi.Pixel, GraphicsDpi.Document);
		}
		static public float PixelToDoc(float val) {
			return Convert(val, GraphicsDpi.Pixel, GraphicsDpi.Document);
		}
		static public RectangleF DocToPixel(RectangleF val) {
			return Convert(val, GraphicsDpi.Document, GraphicsDpi.Pixel);
		}
		static public SizeF DocToPixel(SizeF val) {
			return Convert(val, GraphicsDpi.Document, GraphicsDpi.Pixel);
		}
		static public PointF DocToPixel(PointF val) {
			return Convert(val, GraphicsDpi.Document, GraphicsDpi.Pixel);
		}
		static public float DocToPixel(float val) {
			return Convert(val, GraphicsDpi.Document, GraphicsDpi.Pixel);
		}
	}
}
