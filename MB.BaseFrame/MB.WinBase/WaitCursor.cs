using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinBase {
    public class WaitCursor : IDisposable {
        private Cursor mOldCursor;
		private Form mFrm = null;
		private bool setCurror = false;
		public WaitCursor(){
			setCurror = true;
			mOldCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
		}
		public WaitCursor( object winObj ) {
			if(winObj!=null) {
				mFrm = winObj as Form;
				mFrm.Cursor = Cursors.WaitCursor ;
			}
		}
        public WaitCursor(System.Windows.Forms.Cursor newCursor, object winObj)
		{
			if(winObj!=null)
			{
				mFrm = winObj as Form; 
				if(newCursor == null)
				{
					mFrm.Cursor = Cursors.WaitCursor ;
				}
				else
				{
					mFrm.Cursor = newCursor;
				}
			}

		}
		public Cursor OldCursor
		{
			set
			{
				mOldCursor = value;
			}
		}
		public void Dispose()
		{
            if (mFrm == null) return;

            if (mOldCursor != null ) {
				mFrm.Cursor = mOldCursor;
				if(setCurror){
					Cursor.Current = mOldCursor;
				}
			}
			else{
				mFrm.Cursor = Cursors.Arrow; 
			}
		}
    }
}
