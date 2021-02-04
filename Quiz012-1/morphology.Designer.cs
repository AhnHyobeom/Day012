
namespace Quiz012_1
{
    partial class morphology
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
            this.btnGray = new System.Windows.Forms.Button();
            this.btnBW = new System.Windows.Forms.Button();
            this.grayLabel = new System.Windows.Forms.Label();
            this.bwLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGray
            // 
            this.btnGray.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGray.Location = new System.Drawing.Point(293, 82);
            this.btnGray.Name = "btnGray";
            this.btnGray.Size = new System.Drawing.Size(126, 43);
            this.btnGray.TabIndex = 0;
            this.btnGray.Text = "click";
            this.btnGray.UseVisualStyleBackColor = true;
            this.btnGray.Click += new System.EventHandler(this.btnGray_Click);
            // 
            // btnBW
            // 
            this.btnBW.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnBW.Location = new System.Drawing.Point(293, 164);
            this.btnBW.Name = "btnBW";
            this.btnBW.Size = new System.Drawing.Size(130, 41);
            this.btnBW.TabIndex = 1;
            this.btnBW.Text = "click";
            this.btnBW.UseVisualStyleBackColor = true;
            this.btnBW.Click += new System.EventHandler(this.btnBW_Click);
            // 
            // grayLabel
            // 
            this.grayLabel.AutoSize = true;
            this.grayLabel.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grayLabel.Location = new System.Drawing.Point(100, 85);
            this.grayLabel.Name = "grayLabel";
            this.grayLabel.Size = new System.Drawing.Size(137, 28);
            this.grayLabel.TabIndex = 2;
            this.grayLabel.Text = "grayscale";
            // 
            // bwLabel
            // 
            this.bwLabel.AutoSize = true;
            this.bwLabel.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bwLabel.Location = new System.Drawing.Point(84, 166);
            this.bwLabel.Name = "bwLabel";
            this.bwLabel.Size = new System.Drawing.Size(180, 28);
            this.bwLabel.TabIndex = 3;
            this.bwLabel.Text = "이진화이미지";
            // 
            // morphology
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 263);
            this.Controls.Add(this.bwLabel);
            this.Controls.Add(this.grayLabel);
            this.Controls.Add(this.btnBW);
            this.Controls.Add(this.btnGray);
            this.Name = "morphology";
            this.Text = "morphology";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label grayLabel;
        private System.Windows.Forms.Label bwLabel;
        public System.Windows.Forms.Button btnGray;
        public System.Windows.Forms.Button btnBW;
    }
}