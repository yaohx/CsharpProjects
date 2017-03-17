namespace MB.WinBase.Ctls
{
    partial class frmEditDateFilter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.radNone = new System.Windows.Forms.RadioButton();
            this.radValue = new System.Windows.Forms.RadioButton();
            this.radOther = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dTimeBegin = new DevExpress.XtraEditors.DateEdit();
            this.dTimeEnd = new DevExpress.XtraEditors.DateEdit();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dTimeBegin.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTimeBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTimeEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTimeEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // radNone
            // 
            this.radNone.AutoSize = true;
            this.radNone.Checked = true;
            this.radNone.Location = new System.Drawing.Point(2, 1);
            this.radNone.Name = "radNone";
            this.radNone.Size = new System.Drawing.Size(61, 18);
            this.radNone.TabIndex = 0;
            this.radNone.TabStop = true;
            this.radNone.Text = "不记得";
            this.radNone.UseVisualStyleBackColor = true;
            this.radNone.CheckedChanged += new System.EventHandler(this.radNone_CheckedChanged);
            // 
            // radValue
            // 
            this.radValue.AutoSize = true;
            this.radValue.Location = new System.Drawing.Point(2, 25);
            this.radValue.Name = "radValue";
            this.radValue.Size = new System.Drawing.Size(61, 18);
            this.radValue.TabIndex = 1;
            this.radValue.Text = "固定值";
            this.radValue.UseVisualStyleBackColor = true;
            this.radValue.CheckedChanged += new System.EventHandler(this.radNone_CheckedChanged);
            // 
            // radOther
            // 
            this.radOther.AutoSize = true;
            this.radOther.Location = new System.Drawing.Point(2, 50);
            this.radOther.Name = "radOther";
            this.radOther.Size = new System.Drawing.Size(73, 18);
            this.radOther.TabIndex = 6;
            this.radOther.Text = "指定日期";
            this.radOther.UseVisualStyleBackColor = true;
            this.radOther.CheckedChanged += new System.EventHandler(this.radNone_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "从：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "至：";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dTimeBegin
            // 
            this.dTimeBegin.EditValue = new System.DateTime(2012, 8, 14, 13, 40, 25, 0);
            this.dTimeBegin.Location = new System.Drawing.Point(65, 75);
            this.dTimeBegin.Name = "dTimeBegin";
            this.dTimeBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dTimeBegin.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dTimeBegin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dTimeBegin.Properties.ShowWeekNumbers = true;
            this.dTimeBegin.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dTimeBegin.Size = new System.Drawing.Size(147, 21);
            this.dTimeBegin.TabIndex = 11;
            this.dTimeBegin.DateTimeChanged += new System.EventHandler(this.dTimeBegin_DateTimeChanged);
            this.dTimeBegin.EditValueChanged += new System.EventHandler(this.dTimeBegin_ValueChanged);
            this.dTimeBegin.Validated += new System.EventHandler(this.dTimeBegin_Validated);
            // 
            // dTimeEnd
            // 
            this.dTimeEnd.EditValue = new System.DateTime(2012, 8, 14, 13, 42, 0, 0);
            this.dTimeEnd.Location = new System.Drawing.Point(65, 103);
            this.dTimeEnd.Name = "dTimeEnd";
            this.dTimeEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dTimeEnd.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dTimeEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dTimeEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dTimeEnd.Size = new System.Drawing.Size(147, 21);
            this.dTimeEnd.TabIndex = 12;
            this.dTimeEnd.DateTimeChanged += new System.EventHandler(this.dTimeEnd_DateTimeChanged);
            this.dTimeEnd.EditValueChanged += new System.EventHandler(this.dTimeEnd_ValueChanged);
            this.dTimeEnd.Validated += new System.EventHandler(this.dTimeEnd_Validated);
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.EditValue = "今天";
            this.buttonEdit1.Location = new System.Drawing.Point(65, 24);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.buttonEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.buttonEdit1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.buttonEdit1.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.buttonEdit1.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Left, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right)});
            this.buttonEdit1.Properties.ReadOnly = true;
            this.buttonEdit1.Size = new System.Drawing.Size(147, 21);
            this.buttonEdit1.TabIndex = 13;
            this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
            // 
            // frmEditDateFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(214, 127);
            this.ControlBox = false;
            this.Controls.Add(this.buttonEdit1);
            this.Controls.Add(this.dTimeEnd);
            this.Controls.Add(this.dTimeBegin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radOther);
            this.Controls.Add(this.radValue);
            this.Controls.Add(this.radNone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditDateFilter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEditDateFilter_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dTimeBegin.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTimeBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTimeEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTimeEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radNone;
        private System.Windows.Forms.RadioButton radValue;
        private System.Windows.Forms.RadioButton radOther;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        internal DevExpress.XtraEditors.DateEdit dTimeBegin;
        internal DevExpress.XtraEditors.DateEdit dTimeEnd;
    }
}