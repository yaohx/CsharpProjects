using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel; 

namespace MB.WinBase.DesignEditor {
   /// <summary>
   /// 编辑对象的抽象基类。
   /// </summary>
    public abstract class BaseEditorObject : ICustomTypeDescriptor {
        private PropertyDescriptorCollection _GlobalizedProps;
        public String GetClassName() {
            return TypeDescriptor.GetClassName(this, true);
        }
        public AttributeCollection GetAttributes() {
            return TypeDescriptor.GetAttributes(this, true);
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
            if (_GlobalizedProps == null) {
                PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, attributes, true);
                _GlobalizedProps = new PropertyDescriptorCollection(null);
                foreach (PropertyDescriptor oProp in baseProps) {
                    _GlobalizedProps.Add(new BasePropertyDescriptor(oProp));
                }
            }
            return _GlobalizedProps;
        }
        public PropertyDescriptorCollection GetProperties() {
            if (_GlobalizedProps == null) {
                PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, true);
                _GlobalizedProps = new PropertyDescriptorCollection(null);
                foreach (PropertyDescriptor oProp in baseProps) {
                    _GlobalizedProps.Add(new BasePropertyDescriptor(oProp));
                }
            }
            return _GlobalizedProps;
        }
        public object GetPropertyOwner(PropertyDescriptor pd) {
            return this;
        }
    }

    public class BasePropertyDescriptor : PropertyDescriptor {
        private PropertyDescriptor _BasePropertyDescriptor;

        public BasePropertyDescriptor(PropertyDescriptor basePropertyDescriptor)
            : base(basePropertyDescriptor) {
            this._BasePropertyDescriptor = basePropertyDescriptor;

        }
        public override bool CanResetValue(object component) {
            return _BasePropertyDescriptor.CanResetValue(component);
        }
        public override Type ComponentType {
            get { return _BasePropertyDescriptor.ComponentType; }
        }
        public override string DisplayName {
            get {
                string svalue = "";
                foreach (Attribute attribute in this._BasePropertyDescriptor.Attributes) {
                    if (attribute is EditorDescription) {
                        svalue = attribute.ToString();
                        break;
                    }
                }
                if (svalue == "") return this._BasePropertyDescriptor.Name;
                else return svalue;
            }
        }
        public override string Description {
            get {
                return this._BasePropertyDescriptor.Description;
            }
        }
        public override object GetValue(object component) {
            return this._BasePropertyDescriptor.GetValue(component);
        }
        public override bool IsReadOnly {
            get { return this._BasePropertyDescriptor.IsReadOnly; }
        }
        public override string Name {
            get { return this._BasePropertyDescriptor.Name; }
        }
        public override Type PropertyType {
            get { return this._BasePropertyDescriptor.PropertyType; }
        }
        public override void ResetValue(object component) {
            this._BasePropertyDescriptor.ResetValue(component);
        }
        public override bool ShouldSerializeValue(object component) {
            return this._BasePropertyDescriptor.ShouldSerializeValue(component);
        }
        public override void SetValue(object component, object value) {
            this._BasePropertyDescriptor.SetValue(component, value);
        }
    }
    /// <summary>
    /// 编辑属性的中文描述。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorDescription : System.Attribute {
        private string _Description = string.Empty;

        public EditorDescription(string description) {
            this._Description = description;
        }
        public string Description {
            get {
                return this._Description;
            }
        }
        public override string ToString() {
            return this._Description;
        }
    }
}
