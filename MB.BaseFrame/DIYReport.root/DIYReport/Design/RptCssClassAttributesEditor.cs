using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DIYReport.Design
{
	/// <summary>
	/// RptCassClassAttributesEditor ±‡º≠CssClassµƒ Ù–‘°£
	/// </summary>
	public class RptCssClassAttributesEditor : UITypeEditor
	{
		public RptCssClassAttributesEditor() {
		} 
        
		public override object EditValue(ITypeDescriptorContext context, 
			IServiceProvider provider, object value) {

//			// Create the listbox for display
//			CheckedListBox listBox = new CheckedListBox();
//			listBox.CheckOnClick = true;
//		
//			// Get the type of the enumeration.
//			Type t = context.PropertyDescriptor.PropertyType;
//			
//			// Loop and populate the listbox.
//			foreach(string name in Enum.GetNames(t)) {
//				
//				// Is the value selected?
//				bool isChecked = (((int)value) & 
//					(int)Enum.Parse(t, name)) != 0;
//				
//				// Add the value. 
//				listBox.Items.Add(name, isChecked);
//
//			} // End foreach
//
//			// Display the list box
//			((IWindowsFormsEditorService)provider.GetService(
//				typeof(IWindowsFormsEditorService))).DropDownControl(listBox);
//
//			int enumValue = 0;
//
//			// Loop and get all the selected values.
//			foreach(string str in listBox.CheckedItems) 
//				enumValue |= (int)Enum.Parse(t, str);
//
//			// Ensure the "Normal" value is selected as a default.
//			if (enumValue == 0)
//				enumValue |= (int)Enum.Parse(t, "Normal");
//
//			// Return the new enumeration.
//			return Enum.ToObject(t, enumValue); 
			return null;
		
		} // End EditValue()
        
		// *******************************************************************
        
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
			
//			// Should we return the style for our listbox?
//			if ((context != null) && (context.Instance != null))
//				return UITypeEditorEditStyle.DropDown;
//
//			// Return the default edit style.
//			return base.GetEditStyle(context);
			return UITypeEditorEditStyle.DropDown ;
		} // End GetEditStyle()
	}
}
