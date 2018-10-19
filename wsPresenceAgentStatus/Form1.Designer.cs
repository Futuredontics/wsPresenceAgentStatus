namespace wsPresenceAgentStatus
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axPresenceInterfaceX1 = new AxPresenceInterfaceXControl1.AxPresenceInterfaceX();
            ((System.ComponentModel.ISupportInitialize)(this.axPresenceInterfaceX1)).BeginInit();
            this.SuspendLayout();
            // 
            // axPresenceInterfaceX1
            // 
            this.axPresenceInterfaceX1.Location = new System.Drawing.Point(225, 184);
            this.axPresenceInterfaceX1.Name = "axPresenceInterfaceX1";
            this.axPresenceInterfaceX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPresenceInterfaceX1.OcxState")));
            this.axPresenceInterfaceX1.Size = new System.Drawing.Size(75, 23);
            this.axPresenceInterfaceX1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.axPresenceInterfaceX1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axPresenceInterfaceX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxPresenceInterfaceXControl1.AxPresenceInterfaceX axPresenceInterfaceX1;
    }
}