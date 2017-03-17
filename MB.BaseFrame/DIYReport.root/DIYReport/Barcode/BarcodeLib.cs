using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.IO;

namespace DIYReport.Barcode
{
	interface IBarcode {
		string Encoded_Value {
			get;
		}//Encoded_Value
	}//interface

	abstract class BarcodeVariables {
		public string Raw_Data = "";
	}//BarcodeVariables abstract class

	#region Enums
	public enum TYPE : int { UNSPECIFIED, UPCA, UPCE, UPC_SUPPLEMENTAL_2DIGIT, UPC_SUPPLEMENTAL_5DIGIT, EAN13, EAN8, Interleaved2of5, Standard2of5, Industrial2of5, CODE39, CODE39Extended, Codabar, PostNet, BOOKLAND, ISBN, JAN13, MSI_Mod10, MSI_2Mod10, MSI_Mod11, MSI_Mod11_Mod10, Modified_Plessey, CODE11, USD8, UCC12, UCC13, LOGMARS, CODE128, CODE128A, CODE128B, CODE128C };
	public enum SaveTypes : int { JPG, BMP, PNG, GIF, TIFF, UNSPECIFIED };
	#endregion

	public class Barcode {
		#region Variables
		private string Raw_Data = "";
		private string Encoded_Value = "";
		private string _Country_Assigning_Manufacturer_Code = "N/A";
		private TYPE Encoded_Type = TYPE.UNSPECIFIED;
		private Image Encoded_Image = null;
		private Color _ForeColor = Color.Black;
		private Color _BackColor = Color.White;
		private bool bEncoded = false;
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor.  Does not populate the raw data.  MUST be done via the RawData property before encoding.
		/// </summary>
		public Barcode() {
			//constructor
		}//Barcode
		/// <summary>
		/// Constructor. Populates the raw data. No whitespace will be added before or after the barcode.
		/// </summary>
		/// <param name="data">String to be encoded.</param>
		public Barcode(string data) {
			//constructor
			this.Raw_Data = data;
		}//Barcode
		public Barcode(string data, TYPE iType) {
			this.Raw_Data = data;
			this.Encoded_Type = iType;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the raw data to encode.
		/// </summary>
		public string RawData {
			get { return Raw_Data; }
			set { Raw_Data = value; bEncoded = false; }
		}//EncodedValue
		/// <summary>
		/// Gets the encoded value.
		/// </summary>
		public string EncodedValue {
			get { return Encoded_Value; }
		}//EncodedValue
		/// <summary>
		/// Gets the Country that assigned the Manufacturer Code.
		/// </summary>
		public string Country_Assigning_Manufacturer_Code {
			get { return _Country_Assigning_Manufacturer_Code; }
		}//Country_Assigning_Manufacturer_Code
		/// <summary>
		/// Gets or sets the Encoded Type (ex. UPC-A, EAN-13 ... etc)
		/// </summary>
		public TYPE EncodedType {
			set { Encoded_Type = value; }
			get { return Encoded_Type;  }
		}//EncodedType
		/// <summary>
		/// Gets the Image of the generated barcode.
		/// </summary>
		public Image EncodedImage {
			get { if (bEncoded) return Encoded_Image; else return null; }
		}//EncodedImage
		/// <summary>
		/// Gets or sets the color of the bars. (Default is black)
		/// </summary>
		public Color ForeColor {
			get { return this._ForeColor; }
			set { this._ForeColor = value; }
		}//ForeColor
		/// <summary>
		/// Gets or sets the background color. (Default is white)
		/// </summary>
		public Color BackColor {
			get { return this._BackColor; }
			set { this._BackColor = value; }
		}//BackColor
		#endregion

		#region Functions
		#region General Encode

		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <param name="percent">Percentage of the original size to size the result.</param>
		/// <returns>Image representing the barcode.</returns>
		public Image Encode(TYPE iType, string StringToEncode, double percent) {
			return ResizeImage(Encode(iType, StringToEncode), percent);
		}//Encode(TYPE, string, percent)
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="percent">Percentage of the original size to size the result.</param>
		/// <returns>Image representing the barcode.</returns>
		public Image Encode(TYPE iType, double percent) {
			return ResizeImage(Encode(iType), percent);
		}//Encode(TYPE, double)
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <param name="Width">Width of the resulting barcode.(pixels)</param>
		/// <param name="Height">Height of the resulting barcode.(pixels)</param>
		/// <returns>Image representing the barcode.</returns>
		public Image Encode(TYPE iType, string StringToEncode, int Width, int Height) {
			return ResizeImage(Encode(iType, StringToEncode), Width, Height);
		}//Encode(TYPE, string, int, int)
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="Width">Width of the resulting barcode.(pixels)</param>
		/// <param name="Height">Height of the resulting barcode.(pixels)</param>
		/// <returns>Image representing the barcode.</returns>
		public Image Encode(TYPE iType, int Width, int Height) {
			return ResizeImage(Encode(iType), Width, Height);
		}//Encode(TYPE, int, int)
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <param name="DrawColor">Foreground color</param>
		/// <param name="BackColor">Background color</param>
		/// <param name="percent">Percentage of the original size to size the result.</param>
		/// <returns></returns>
		public Image Encode(TYPE iType, string StringToEncode, Color DrawColor, Color BackColor, double percent) {
			return ResizeImage(Encode(iType, StringToEncode, DrawColor, BackColor), percent);
		}//Encode(TYPE, string, Color, Color, Double)
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <param name="DrawColor">Foreground color</param>
		/// <param name="BackColor">Background color</param>
		/// <param name="Width">Width of the resulting barcode.(pixels)</param>
		/// <param name="Height">Height of the resulting barcode.(pixels)</param>
		/// <returns>Image representing the barcode.</returns>
		public Image Encode(TYPE iType, string StringToEncode, Color DrawColor, Color BackColor, int Width, int Height) {
			return ResizeImage(Encode(iType, StringToEncode, DrawColor, BackColor), Width, Height);
		}//Encode(TYPE, string, Color, Color, int, int)
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <param name="DrawColor">Foreground color</param>
		/// <param name="BackColor">Background color</param>
		/// <returns>Image representing the barcode.</returns>
		public Image Encode(TYPE iType, string StringToEncode, Color DrawColor, Color BackColor) {
			Raw_Data = StringToEncode;
			return Encode(iType, DrawColor, BackColor);
		}//(Image)Encode(Type, string, Color, Color)
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="DrawColor">Foreground color</param>
		/// <param name="BackColor">Background color</param>
		/// <returns>Image representing the barcode.</returns>
		public Image Encode(TYPE iType, Color DrawColor, Color BackColor) {
			this.BackColor = BackColor;
			this.ForeColor = DrawColor;
			return Encode(iType);
		}//(Image)Encode(TYPE, Color, Color)
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <returns>Image representing the barcode.</returns>
		public Image Encode(TYPE iType, string StringToEncode) {
			Raw_Data = StringToEncode;
			return Encode(iType);
		}//(Image)Encode(TYPE, string)
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		public Image Encode(TYPE iType) {
			Encoded_Type = iType;
			return Encode();
		}//Encode()
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.
		/// </summary>
		public Image Encode() {
			//make sure there is something to encode
			if (Raw_Data.Trim() == "") 
				throw new Exception("EENCODE-1: Input data not allowed to be blank.");

			if (this.EncodedType == TYPE.UNSPECIFIED) 
				throw new Exception("EENCODE-2: Symbology type not allowed to be unspecified.");

			this.Encoded_Value = "";
			this._Country_Assigning_Manufacturer_Code = "N/A";

			IBarcode ibarcode;
			switch (this.Encoded_Type) {
				case TYPE.UCC12:
				case TYPE.UPCA: //Encode_UPCA();
					ibarcode = new UPCA(Raw_Data);
					break;
//				case TYPE.UCC13:
//				case TYPE.EAN13: //Encode_EAN13();
//					ibarcode = new EAN13(Raw_Data);
//					break;
//				case TYPE.Interleaved2of5: //Encode_Interleaved2of5();
//					ibarcode = new Interleaved2of5(Raw_Data);
//					break;
//				case TYPE.Industrial2of5:
//				case TYPE.Standard2of5: //Encode_Standard2of5();
//					ibarcode = new Standard2of5(Raw_Data);
//					break;
//				case TYPE.LOGMARS:
//				case TYPE.CODE39: //Encode_Code39();
//					ibarcode = new Code39(Raw_Data);
//					break;
//				case TYPE.CODE39Extended:
//					ibarcode = new Code39(Raw_Data, true);
//					break;
//				case TYPE.Codabar: //Encode_Codabar();
//					ibarcode = new Codabar(Raw_Data);
//					break;
//				case TYPE.PostNet: //Encode_PostNet();
//					ibarcode = new Postnet(Raw_Data);
//					break;
//				case TYPE.ISBN:
//				case TYPE.BOOKLAND: //Encode_ISBN_Bookland();
//					ibarcode = new ISBN(Raw_Data);
//					break;
//				case TYPE.JAN13: //Encode_JAN13();
//					ibarcode = new JAN13(Raw_Data);
//					break;
//				case TYPE.UPC_SUPPLEMENTAL_2DIGIT: //Encode_UPCSupplemental_2();
//					ibarcode = new UPCSupplement2(Raw_Data);
//					break;
//				case TYPE.MSI_Mod10:
//				case TYPE.MSI_2Mod10:
//				case TYPE.MSI_Mod11:
//				case TYPE.MSI_Mod11_Mod10:
//				case TYPE.Modified_Plessey: //Encode_MSI();
//					ibarcode = new MSI(Raw_Data, Encoded_Type);
//					break;
//				case TYPE.UPC_SUPPLEMENTAL_5DIGIT: //Encode_UPCSupplemental_5();
//					ibarcode = new UPCSupplement5(Raw_Data);
//					break;
//				case TYPE.UPCE: //Encode_UPCE();
//					ibarcode = new UPCE(Raw_Data);
//					break;
//				case TYPE.EAN8: //Encode_EAN8();
//					ibarcode = new EAN8(Raw_Data);
//					break;
//				case TYPE.USD8:
//				case TYPE.CODE11: //Encode_Code11();
//					ibarcode = new Code11(Raw_Data);
//					break;
//				case TYPE.CODE128: //Encode_Code128();
//					ibarcode = new Code128(Raw_Data);
//					break;
//				case TYPE.CODE128A:
//					ibarcode = new Code128(Raw_Data, Code128.TYPES.A);
//					break;
//				case TYPE.CODE128B:
//					ibarcode = new Code128(Raw_Data, Code128.TYPES.B);
//					break;
//				case TYPE.CODE128C:
//					ibarcode = new Code128(Raw_Data, Code128.TYPES.C);
//					break;
				default: throw new Exception("EENCODE-2: Unsupported encoding type specified.");
			}//switch

			this.Encoded_Value = ibarcode.Encoded_Value;

			return (Image)Generate_Image();
		}//Encode
        
		#endregion

		#region Image Functions
		/// <summary>
		/// Gets a bitmap representation of the encoded data.
		/// </summary>
		/// <returns>Bitmap of encoded value.</returns>
		private Bitmap Generate_Image() {
			return Generate_Image(this.ForeColor, this.BackColor);
		}//Generate_Image()
		/// <summary>
		/// Gets a bitmap representation of the encoded data.
		/// </summary>
		/// <param name="DrawColor">Color to draw the bars.</param>
		/// <param name="BackColor">Color to draw the spaces.</param>
		/// <returns>Bitmap of encoded value.</returns>
		private Bitmap Generate_Image(Color DrawColor, Color BackColor) {
			if (Encoded_Value == "") throw new Exception("EGENERATE_IMAGE-1: Must be encoded first.");
			Bitmap b = null;

			switch(this.Encoded_Type) {
				case TYPE.UPCA:
				case TYPE.EAN13:
				case TYPE.EAN8:
				case TYPE.Standard2of5:
				case TYPE.Industrial2of5:
				case TYPE.Interleaved2of5:
				case TYPE.CODE11:
				case TYPE.CODE39:
				case TYPE.CODE39Extended:
				case TYPE.CODE128:
				case TYPE.CODE128A:
				case TYPE.CODE128B:
				case TYPE.CODE128C:
				case TYPE.LOGMARS:
				case TYPE.Codabar:
				case TYPE.BOOKLAND:
				case TYPE.ISBN:
				case TYPE.UPC_SUPPLEMENTAL_2DIGIT:
				case TYPE.UPC_SUPPLEMENTAL_5DIGIT:
				case TYPE.JAN13:
				case TYPE.MSI_Mod10:
				case TYPE.MSI_2Mod10:
				case TYPE.MSI_Mod11:
				case TYPE.UPCE:
				case TYPE.USD8:
				case TYPE.UCC12:
				case TYPE.UCC13: {
					b = new Bitmap(Encoded_Value.Length * 2, Encoded_Value.Length);
                        
					//draw image
					Color c = DrawColor;

					int pos = 0;

					using (Graphics g = Graphics.FromImage(b)) {
						while ((pos * 2 + 1) < b.Width) {
							if (pos < Encoded_Value.Length) {
								if (Encoded_Value[pos] == '1')
									c = DrawColor;
								if (Encoded_Value[pos] == '0')
									c = BackColor;
							}//if

							//lines are 2px wide so draw the appropriate color line vertically
							g.DrawLine(new Pen(c, (float)2), new Point(pos * 2 + 1, 0), new Point(pos * 2 + 1, b.Height));

							pos++;
						}//while
					}//using
					break;
				}//case
				case TYPE.PostNet: {
					b = new Bitmap(Encoded_Value.Length * 4, 20);

					//draw image
					for (int y = b.Height-1; y > 0; y--) {
						int x = 0;
						if (y < b.Height / 2) { 
							//top
							while (x < b.Width) {
								if (Encoded_Value[x/4] == '1') {
									//draw bar
									b.SetPixel(x, y, DrawColor);
									b.SetPixel(x + 1, y, DrawColor);
									b.SetPixel(x + 2, y, BackColor);
									b.SetPixel(x + 3, y, BackColor);
								}//if
								else { 
									//draw space
									b.SetPixel(x, y, BackColor);
									b.SetPixel(x + 1, y, BackColor);
									b.SetPixel(x + 2, y, BackColor);
									b.SetPixel(x + 3, y, BackColor);
								}//else
								x += 4;
							}//while
						}//if
						else { 
							//bottom
							while (x < b.Width) {
								b.SetPixel(x, y, DrawColor);
								b.SetPixel(x + 1, y, DrawColor);
								b.SetPixel(x + 2, y, BackColor);
								b.SetPixel(x + 3, y, BackColor);
								x += 4;
							}//while
						}//else
                                    
					}//for

					break;
				}//case
				default: return null;
			}//switch

			Encoded_Image = (Image)b;
			bEncoded = true;

			return b;
		}//Generate_Image
		/// <summary>
		/// Gets the bytes that represent the image.
		/// </summary>
		/// <param name="savetype">File type to put the data in before returning the bytes.</param>
		/// <returns>Bytes representing the encoded image.</returns>
		public byte[] GetImageData(SaveTypes savetype) {
			byte[] imageData = null;
              
			try {
				if (Encoded_Image != null) {
					//Save the image to a memory stream so that we can get a byte array!      
					using (MemoryStream ms = new MemoryStream()) {
						SaveImage(ms, savetype);
						imageData = ms.ToArray();
						ms.Flush();
						ms.Close();
					}//using
				}//if
			}//try
			catch (Exception ex) {
				throw new Exception("EGETIMAGEDATA-1: Could not retrieve image data. " + ex.Message);
			}//catch  
			return imageData;
		}
		/// <summary>
		/// Resizes the image to a defined width and height.
		/// </summary>
		/// <param name="img">Image to resize.</param>
		/// <param name="nWidth">New width.</param>
		/// <param name="nHeight">New height.</param>
		/// <returns>Resized image.</returns>
		private Image ResizeImage(Image img, int nWidth, int nHeight) {
			Bitmap result = new Bitmap(nWidth, nHeight);
			using (Graphics g = Graphics.FromImage((Image)result)) {
				//g.InterpolationMode = InterpolationMode.NearestNeighbor;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.DrawImage(img, 0, 0, nWidth, nHeight);
			}//using
			return (Image)result;
		}//ResizeImage
		/// <summary>
		/// Resizes an image to a defined percentage of the original size.
		/// </summary>
		/// <param name="img">Image to resize.</param>
		/// <param name="percent">Percent of original size to resize to.</param>
		/// <returns>Resized image.</returns>
		private Image ResizeImage(Image img, double percent) {
			return ResizeImage(img, Convert.ToInt32(((double)(percent / (double)100)) * (double)img.Width), Convert.ToInt32(((double)(percent / (double)100)) * img.Height));
		}//ResizeImage
		/// <summary>
		/// Saves an encoded image to a specified file and type.
		/// </summary>
		/// <param name="Filename">Filename to save to.</param>
		/// <param name="FileType">Format to use.</param>
		public void SaveImage(string Filename, SaveTypes FileType) {
			try {
				if (Encoded_Image != null) {
					System.Drawing.Imaging.ImageFormat imageformat;
					switch (FileType) {
						case SaveTypes.BMP: imageformat = System.Drawing.Imaging.ImageFormat.Bmp; break;
						case SaveTypes.GIF: imageformat = System.Drawing.Imaging.ImageFormat.Gif; break;
						case SaveTypes.JPG: imageformat = System.Drawing.Imaging.ImageFormat.Jpeg; break;
						case SaveTypes.PNG: imageformat = System.Drawing.Imaging.ImageFormat.Png; break;
						case SaveTypes.TIFF: imageformat = System.Drawing.Imaging.ImageFormat.Tiff; break;
						default: imageformat = System.Drawing.Imaging.ImageFormat.Bmp; break;
					}//switch
					((Bitmap)Encoded_Image).Save(Filename, imageformat);
				}//if
			}//try
			catch(Exception ex) {
				throw new Exception("ESAVEIMAGE-1: Could not save image.\n\n=======================\n\n" + ex.Message);
			}//catch
		}//SaveImage(string, SaveTypes)
		/// <summary>
		/// Saves an encoded image to a specified stream.
		/// </summary>
		/// <param name="stream">Stream to write image to.</param>
		/// <param name="FileType">Format to use.</param>
		public void SaveImage(Stream stream, SaveTypes FileType) {
			try {
				if (Encoded_Image != null) {
					System.Drawing.Imaging.ImageFormat imageformat;
					switch (FileType) {
						case SaveTypes.BMP: imageformat = System.Drawing.Imaging.ImageFormat.Bmp; break;
						case SaveTypes.GIF: imageformat = System.Drawing.Imaging.ImageFormat.Gif; break;
						case SaveTypes.JPG: imageformat = System.Drawing.Imaging.ImageFormat.Jpeg; break;
						case SaveTypes.PNG: imageformat = System.Drawing.Imaging.ImageFormat.Png; break;
						case SaveTypes.TIFF: imageformat = System.Drawing.Imaging.ImageFormat.Tiff; break;
						default: imageformat = System.Drawing.Imaging.ImageFormat.Bmp; break;
					}//switch
					((Bitmap)Encoded_Image).Save(stream, imageformat);
				}//if
			}//try
			catch (Exception ex) {
				throw new Exception("ESAVEIMAGE-2: Could not save image.\n\n=======================\n\n" + ex.Message);
			}//catch
		}//SaveImage(Stream, SaveTypes)
		#endregion
        
		#region Label Generation
		public Image Generate_Labels(Image img) {
			if (bEncoded) {
				switch (this.Encoded_Type) {
					case TYPE.EAN13:
					case TYPE.EAN8:
					case TYPE.Standard2of5:
					case TYPE.Industrial2of5:
					case TYPE.Interleaved2of5:
					case TYPE.CODE11:
					case TYPE.CODE39:
					case TYPE.CODE39Extended:
					case TYPE.CODE128:
					case TYPE.CODE128A:
					case TYPE.CODE128B:
					case TYPE.CODE128C:
					case TYPE.LOGMARS:
					case TYPE.Codabar:
					case TYPE.BOOKLAND:
					case TYPE.ISBN:
					case TYPE.UPC_SUPPLEMENTAL_2DIGIT:
					case TYPE.UPC_SUPPLEMENTAL_5DIGIT:
					case TYPE.JAN13:
					case TYPE.MSI_Mod10:
					case TYPE.MSI_2Mod10:
					case TYPE.MSI_Mod11:
					case TYPE.UPCE:
					case TYPE.USD8:
					case TYPE.UCC13: return Label_Generic(img);
					case TYPE.UCC12:
					case TYPE.UPCA: return Label_UPCA(img);
					default: throw new Exception("EGENERATE_LABELS-1: Invalid type.");
				}//switch
			}//if
			else
				throw new Exception("EGENERATE_LABELS-2: Encode the image first.");
		}//Generate_Labels
		private Image Label_UPCA(Image img) {
			System.Drawing.Font font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            
			Graphics g = Graphics.FromImage(img);

			g.DrawImage(img, (float)0, (float)0);

			g.SmoothingMode = SmoothingMode.HighQuality;
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;
			g.CompositingQuality = CompositingQuality.HighQuality;

			//draw boxes
			g.FillRectangle(new SolidBrush(Color.White), new Rectangle(img.Width/16, img.Height-12, (int)(img.Width * 0.42), img.Height / 2));
			g.FillRectangle(new SolidBrush(Color.White), new Rectangle((int)(img.Width * 0.52), img.Height - 12, (int)(img.Width * 0.42), img.Height / 2));

			string drawstring1 = "";
			string drawstring2 = "";
			foreach (char c in Raw_Data.Substring(1, 5)) {
				drawstring1 += c.ToString() + "  ";
			}//foreach
			foreach (char c in Raw_Data.Substring(6, 5)) {
				drawstring2 += c.ToString() + "  ";
			}//foreach

			drawstring1 = drawstring1.Substring(0, drawstring1.Length - 1);
			drawstring2 = drawstring2.Substring(0, drawstring2.Length - 1);
			g.DrawString(drawstring1, font, new SolidBrush(this.ForeColor), new Rectangle(img.Width / 14, img.Height - 13, (int)(img.Width * 0.50), img.Height / 2));
			g.DrawString(drawstring2, font, new SolidBrush(this.ForeColor), new Rectangle((int)(img.Width * 0.55), img.Height - 13, (int)(img.Width * 0.50), img.Height / 2));
			g.Save();
			g.Dispose();

			Bitmap borderincluded = new Bitmap((int)(img.Width * 1.12), (int)(img.Height * 1.12));
			for (int y = 0; y < borderincluded.Height; y++)
				for (int x = 0; x < borderincluded.Width; x++)
					borderincluded.SetPixel(x, y, Color.White);

			Graphics g2 = Graphics.FromImage((Image)borderincluded);

			g2.SmoothingMode = SmoothingMode.HighQuality;
			g2.InterpolationMode = InterpolationMode.NearestNeighbor;
			g2.PixelOffsetMode = PixelOffsetMode.HighQuality;
			g2.CompositingQuality = CompositingQuality.HighQuality;
                    
			g2.DrawImage((Image)img, (float)((float)img.Width * 0.06), (float)((float)img.Height * 0.06), (float)img.Width, (float)img.Height);

			//UPC-A check digit and number system chars are a little smaller than the other numbers
			font = new Font("MS Sans Serif", 9, FontStyle.Regular);

			//draw the number system digit
			g2.DrawString(Raw_Data[0].ToString(), font, new SolidBrush(this.ForeColor), new Rectangle(-1, img.Height + (int)(img.Height * 0.07) - 13, (int)(img.Width * 0.35), img.Height / 2));

			//draw the check digit
			g2.DrawString(Raw_Data[Raw_Data.Length-1].ToString(), font, new SolidBrush(this.ForeColor), new Rectangle((int)(borderincluded.Width * 0.96), img.Height + (int)(img.Height * 0.07) - 13, (int)(img.Width * 0.35), img.Height / 2));

			g2.Save();
			g2.Dispose();

			this.Encoded_Image = (Image)borderincluded;

			return (Image)borderincluded;
		}//Label_UPCA
		private Image Label_Generic(Image img) {
			System.Drawing.Font font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

			using (Graphics g = Graphics.FromImage(img)) {
				g.DrawImage(img, (float)0, (float)0);

				g.SmoothingMode = SmoothingMode.HighQuality;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.PixelOffsetMode = PixelOffsetMode.HighQuality;
				g.CompositingQuality = CompositingQuality.HighQuality;

				//color a white box at the bottom of the barcode to hold the string of data
				g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, img.Height - 16, img.Width, 16));

				//draw datastring under the barcode image
				StringFormat f = new StringFormat();
				f.Alignment = StringAlignment.Center;
				g.DrawString(this.Raw_Data, font, new SolidBrush(this.ForeColor), (float)(img.Width/2), img.Height - 16, f);

				g.Save();
			}//using
			return img;
		}//Label_Generic
		#endregion
		#endregion

		#region Misc
		public static bool CheckNumericOnly(string Data) {
			//This function takes a string of data and breaks it into parts and trys to do Int64.TryParse
			//This will verify that only numeric data is contained in the string passed in.  The complexity below
			//was done to ensure that the minimum number of interations and checks could be performed.

			//9223372036854775808 is the largest number a 64bit number(signed) can hold so ... make sure its less than that by one place
			int STRING_LENGTHS = 18;
            
			string temp = Data;
			string [] strings = new string[(Data.Length / STRING_LENGTHS) + ((Data.Length % STRING_LENGTHS == 0) ? 0 : 1)];
            
			int i = 0;
			while (i < strings.Length)
				if (temp.Length >= STRING_LENGTHS) {
					strings[i++] = temp.Substring(0, STRING_LENGTHS);
					temp = temp.Substring(STRING_LENGTHS);
				}//if
				else
					strings[i++] = temp.Substring(0);

			foreach (string s in strings) {

				try{
					Int64.Parse(s);
				}
				catch{
					return false;
				}
			
			}//foreach

			return true;
		}//CheckNumericOnly
		#endregion

		#region Static Methods
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <returns>Image representing the barcode.</returns>
		public static Image DoEncode(TYPE iType, string data) {
			Barcode b = new Barcode();
			b.Encode(iType, data);
			return b.Generate_Image();
		}
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <param name="percent">Percentage of the original size to size the result.</param>
		/// <returns>Image representing the barcode.</returns>
		public static Image DoEncode(TYPE iType, string data, double percent) {
			Barcode b = new Barcode();
			b.Encode(iType, data, percent);
			return b.Generate_Image();
		}
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <param name="Width">Width of the resulting barcode.(pixels)</param>
		/// <param name="Height">Height of the resulting barcode.(pixels)</param>
		/// <returns>Image representing the barcode.</returns>
		public static Image DoEncode(TYPE iType, string data, int Width, int Height) {
			Barcode b = new Barcode();
			b.Encode(iType, data, Width, Height);
			return b.Generate_Image();
		}
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <param name="DrawColor">Foreground color</param>
		/// <param name="BackColor">Background color</param>
		/// <returns>Image representing the barcode.</returns>
		public static Image DoEncode(TYPE iType, string data, Color DrawColor, Color BackColor) {
			Barcode b = new Barcode();
			b.Encode(iType, data, DrawColor, BackColor);
			return b.Generate_Image();
		}
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <param name="DrawColor">Foreground color</param>
		/// <param name="BackColor">Background color</param>
		/// <param name="Width">Width of the resulting barcode.(pixels)</param>
		/// <param name="Height">Height of the resulting barcode.(pixels)</param>
		/// <returns>Image representing the barcode.</returns>
		public static Image DoEncode(TYPE iType, string data, Color DrawColor, Color BackColor, int Width, int Height) {
			Barcode b = new Barcode();
			b.Encode(iType, data, DrawColor, BackColor, Width, Height);
			return b.Generate_Image();
		}
		/// <summary>
		/// Encodes the raw data into binary form representing bars and spaces.  Also generates an Image of the barcode.
		/// </summary>
		/// <param name="iType">Type of encoding to use.</param>
		/// <param name="StringToEncode">Raw data to encode.</param>
		/// <param name="DrawColor">Foreground color</param>
		/// <param name="BackColor">Background color</param>
		/// <param name="percent">Percentage of the original size to size the result.</param>
		/// <returns></returns>
		public static Image DoEncode(TYPE iType, string data, Color DrawColor, Color BackColor, double percent) {
			Barcode b = new Barcode();
			b.Encode(iType, data, DrawColor, BackColor, percent);
			return b.Generate_Image();
		}        
		#endregion
	}//Barcode Class
}
