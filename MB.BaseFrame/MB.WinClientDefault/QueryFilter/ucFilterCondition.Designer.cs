namespace MB.WinClientDefault.QueryFilter {
    partial class ucFilterCondition {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.panFilterPan = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // panFilterPan
            // 
            this.panFilterPan.ColumnCount = 2;
            this.panFilterPan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.panFilterPan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.panFilterPan.Location = new System.Drawing.Point(21, 23);
            this.panFilterPan.Name = "panFilterPan";
            this.panFilterPan.RowCount = 2;
            this.panFilterPan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.23077F));
            this.panFilterPan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.76923F));
            this.panFilterPan.Size = new System.Drawing.Size(376, 130);
            this.panFilterPan.TabIndex = 2;
            // 
            // ucFilterCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panFilterPan);
            this.Name = "ucFilterCondition";
            this.Size = new System.Drawing.Size(526, 271);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panFilterPan;

    }
}
