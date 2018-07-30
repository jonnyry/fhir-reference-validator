namespace FhirReferenceValidator
{
    partial class FhirValidatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FhirValidatorForm));
            this.gbFileSelector = new System.Windows.Forms.GroupBox();
            this.btnChooseDirectory = new System.Windows.Forms.Button();
            this.lblProfileDirectory = new System.Windows.Forms.Label();
            this.tbProfileDirectory = new System.Windows.Forms.TextBox();
            this.btnValidateReferences = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbFileSelector.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFileSelector
            // 
            this.gbFileSelector.Controls.Add(this.btnChooseDirectory);
            this.gbFileSelector.Controls.Add(this.lblProfileDirectory);
            this.gbFileSelector.Controls.Add(this.tbProfileDirectory);
            this.gbFileSelector.Controls.Add(this.btnValidateReferences);
            this.gbFileSelector.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFileSelector.Location = new System.Drawing.Point(0, 42);
            this.gbFileSelector.Name = "gbFileSelector";
            this.gbFileSelector.Size = new System.Drawing.Size(1818, 187);
            this.gbFileSelector.TabIndex = 0;
            this.gbFileSelector.TabStop = false;
            // 
            // btnChooseDirectory
            // 
            this.btnChooseDirectory.Location = new System.Drawing.Point(1065, 76);
            this.btnChooseDirectory.Name = "btnChooseDirectory";
            this.btnChooseDirectory.Size = new System.Drawing.Size(216, 51);
            this.btnChooseDirectory.TabIndex = 4;
            this.btnChooseDirectory.Text = "Choose directory...";
            this.btnChooseDirectory.UseVisualStyleBackColor = true;
            this.btnChooseDirectory.Click += new System.EventHandler(this.btnChooseDirectory_Click);
            // 
            // lblProfileDirectory
            // 
            this.lblProfileDirectory.AutoSize = true;
            this.lblProfileDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfileDirectory.Location = new System.Drawing.Point(36, 44);
            this.lblProfileDirectory.Name = "lblProfileDirectory";
            this.lblProfileDirectory.Size = new System.Drawing.Size(162, 25);
            this.lblProfileDirectory.TabIndex = 3;
            this.lblProfileDirectory.Text = "Profile directory";
            // 
            // tbProfileDirectory
            // 
            this.tbProfileDirectory.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FhirReferenceValidator.Properties.Settings.Default, "ChosenFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbProfileDirectory.Location = new System.Drawing.Point(41, 86);
            this.tbProfileDirectory.Name = "tbProfileDirectory";
            this.tbProfileDirectory.ReadOnly = true;
            this.tbProfileDirectory.Size = new System.Drawing.Size(998, 31);
            this.tbProfileDirectory.TabIndex = 2;
            this.tbProfileDirectory.Text = global::FhirReferenceValidator.Properties.Settings.Default.ChosenFolder;
            // 
            // btnValidateReferences
            // 
            this.btnValidateReferences.Location = new System.Drawing.Point(1425, 66);
            this.btnValidateReferences.Name = "btnValidateReferences";
            this.btnValidateReferences.Size = new System.Drawing.Size(318, 65);
            this.btnValidateReferences.TabIndex = 1;
            this.btnValidateReferences.Text = "Validate references";
            this.btnValidateReferences.UseVisualStyleBackColor = true;
            this.btnValidateReferences.Click += new System.EventHandler(this.btnValidateReferences_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutput.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.Location = new System.Drawing.Point(0, 229);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(1818, 824);
            this.tbOutput.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1818, 42);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(77, 38);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(268, 38);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // FhirValidatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1818, 1053);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.gbFileSelector);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FhirValidatorForm";
            this.Text = "FHIR© STU3 profile reference validator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FhirValidatorForm_FormClosing);
            this.Load += new System.EventHandler(this.FhirValidatorForm_Load);
            this.gbFileSelector.ResumeLayout(false);
            this.gbFileSelector.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFileSelector;
        private System.Windows.Forms.Button btnValidateReferences;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Label lblProfileDirectory;
        private System.Windows.Forms.TextBox tbProfileDirectory;
        private System.Windows.Forms.Button btnChooseDirectory;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
    }
}

