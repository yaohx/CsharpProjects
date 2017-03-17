//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2005-10-27
// Description	:	在PropertyGrid 中显示class 的属性描述信息。
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
/*
 我用一个思路：从System.ComponentModel.PropertyDescriptor继承自己的类，然后
 从在DisplayName，在里面实现一个查表功能，使对应的英文名返回中文。最后要把PropertyGrid.SelectObject
  所选的对象从ICustomTypeDescriptor继承，并在其中的GetProperties中构造自己的PropertyDescriptorCollection用于返回。
*/
using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;

namespace DIYReport.Design
{
	/// <summary>
	/// 在PropertyGrid 中显示class 的属性描述信息。
	/// </summary>
	public   class DynamicTypeDescriptor : ICustomTypeDescriptor {
		private PropertyDescriptorCollection dynamicProps;

		public DynamicTypeDescriptor() {
		
		}
	
		public bool ShouldSerializePropertyCommands(){return true;}

		public bool ShouldSerializeCategoryCommands(){return true;}

	
		public virtual  string GetLocalizedName(string Name) {
			return Name;
		}
		
		public virtual string GetLocalizedDescription(string Description) {
			return Description;
		}

		
		#region "TypeDescriptor Implementation"
	
		public String GetClassName() {
			return TypeDescriptor.GetClassName(this,true);
		}

		public AttributeCollection GetAttributes() {
			return TypeDescriptor.GetAttributes(this,true);
		}

		public String GetComponentName() {
			return TypeDescriptor.GetComponentName(this, true);
		}

		public TypeConverter GetConverter() {
			return TypeDescriptor.GetConverter(this, true);
		}

		public EventDescriptor GetDefaultEvent() {
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public PropertyDescriptor GetDefaultProperty() {
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		public object GetEditor(Type editorBaseType) {
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes) {
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public EventDescriptorCollection GetEvents() {
			return TypeDescriptor.GetEvents(this, true);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
			return TypeDescriptor.GetProperties(this, attributes, true);
			
		}

		public PropertyDescriptorCollection GetProperties() {
			
			if ( dynamicProps == null) {
				PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, true);
				dynamicProps = new PropertyDescriptorCollection(null);

				foreach( PropertyDescriptor oProp in baseProps ) {			
				
					dynamicProps.Add(new DynamicPropertyDescriptor(this,oProp));
					
				}
			}
			return dynamicProps;
		}

		public object GetPropertyOwner(PropertyDescriptor pd) {
			return this;
		}
		#endregion
	}


	public class DynamicPropertyDescriptor : PropertyDescriptor {
		private PropertyDescriptor basePropertyDescriptor; 
		private DynamicTypeDescriptor instance;

		public DynamicPropertyDescriptor(DynamicTypeDescriptor instance,PropertyDescriptor basePropertyDescriptor) : base(basePropertyDescriptor) {
			this.instance=instance;
			this.basePropertyDescriptor = basePropertyDescriptor;
		}

		public override bool CanResetValue(object component) {
			return basePropertyDescriptor.CanResetValue(component);
		}

		public override Type ComponentType {
			get { return basePropertyDescriptor.ComponentType; }
		}

		public override object GetValue(object component) {
			return this.basePropertyDescriptor.GetValue(component);
		}

		public override string Description {
			get {
				return instance.GetLocalizedDescription(base.Name);
			}
		}
		public override string Category {
			get {
				return instance.GetLocalizedName(base.Category);
			}
		}

		public override string DisplayName {
			get {
				return instance.GetLocalizedName(base.Name);
			}
		}

		public override bool IsReadOnly {
			get {
 
					return this.basePropertyDescriptor.IsReadOnly; 
 
			}
		}

		public override Type PropertyType {
			get { return this.basePropertyDescriptor.PropertyType; }
		}

		public override void ResetValue(object component) {
			this.basePropertyDescriptor.ResetValue(component);
		}

		public override bool ShouldSerializeValue(object component) {
			return this.basePropertyDescriptor.ShouldSerializeValue(component);
		}

		public override void SetValue(object component, object value) {
			this.basePropertyDescriptor.SetValue(component, value);
		}
	}

}
