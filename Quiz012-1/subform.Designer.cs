﻿
namespace Quiz012_1
{
    partial class subform
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
            this.label1 = new System.Windows.Forms.Label();
            this.numUp_value = new System.Windows.Forms.NumericUpDown();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUp_value)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(76, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "입력값 :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // numUp_value
            // 
            this.numUp_value.DecimalPlaces = 2;
            this.numUp_value.Font = new System.Drawing.Font("굴림", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numUp_value.Location = new System.Drawing.Point(230, 57);
            this.numUp_value.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numUp_value.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numUp_value.Name = "numUp_value";
            this.numUp_value.Size = new System.Drawing.Size(120, 39);
            this.numUp_value.TabIndex = 2;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(104, 152);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(95, 36);
            this.btn_ok.TabIndex = 3;
            this.btn_ok.Text = "확인";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(230, 152);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(92, 36);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "취소";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // subform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 214);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.numUp_value);
            this.Controls.Add(this.label1);
            this.Name = "subform";
            this.Text = "subform";
            ((System.ComponentModel.ISupportInitialize)(this.numUp_value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        public System.Windows.Forms.NumericUpDown numUp_value;
    }
}