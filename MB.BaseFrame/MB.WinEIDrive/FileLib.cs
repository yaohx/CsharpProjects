using System;

namespace MB.WinEIDrive
{
	/// <summary>
	/// FileLib 文件操作类。
	/// </summary>
	public class FileLib
	{
		#region private construct function...
		/// <summary>
		/// add private construct function to prevent instance.
		/// </summary>
		private FileLib()
		{
		}
		#endregion private construct function...

		#region public static function...
		/// <summary>
		///  打开文件。
		/// </summary>
		/// <param name="fileName"></param>
		public static void TryToOpenFile(string fileName) {
			try {
				System.Diagnostics.Process.Start(fileName);
			}
			catch(Exception e) {
				throw new Exception("文件:" + fileName + "不能被打开，请检查该文件是否存在。" + e.Message);
			}
		}//
		#endregion public static function...
	}
}
