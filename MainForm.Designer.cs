namespace SyncFolders
{
    partial class MainForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.syncButton = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.originLabel = new System.Windows.Forms.Label();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.originFolder = new System.Windows.Forms.Button();
            this.destinationFolder = new System.Windows.Forms.Button();
            this.checkDirectories = new System.Windows.Forms.Label();
            this.infoChangeLabel = new System.Windows.Forms.Label();
            this.lastChangeLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.realTimeCheckbox = new System.Windows.Forms.CheckBox();
            this.numericInputTimer = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericInputTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // syncButton
            // 
            this.syncButton.Location = new System.Drawing.Point(290, 182);
            this.syncButton.Name = "syncButton";
            this.syncButton.Size = new System.Drawing.Size(101, 42);
            this.syncButton.TabIndex = 0;
            this.syncButton.Text = "Sincronizar";
            this.syncButton.UseVisualStyleBackColor = true;
            this.syncButton.Click += new System.EventHandler(this.SyncButtonClick);
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(12, 35);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(349, 20);
            this.inputTextBox.TabIndex = 1;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(12, 87);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(349, 20);
            this.outputTextBox.TabIndex = 2;
            // 
            // originLabel
            // 
            this.originLabel.AutoSize = true;
            this.originLabel.Location = new System.Drawing.Point(12, 19);
            this.originLabel.Name = "originLabel";
            this.originLabel.Size = new System.Drawing.Size(85, 13);
            this.originLabel.TabIndex = 3;
            this.originLabel.Text = "Pasta de Origem";
            // 
            // destinationLabel
            // 
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(12, 71);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(88, 13);
            this.destinationLabel.TabIndex = 4;
            this.destinationLabel.Text = "Pasta de Destino";
            // 
            // originFolder
            // 
            this.originFolder.BackColor = System.Drawing.Color.Transparent;
            this.originFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("originFolder.BackgroundImage")));
            this.originFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.originFolder.Location = new System.Drawing.Point(367, 35);
            this.originFolder.Name = "originFolder";
            this.originFolder.Size = new System.Drawing.Size(22, 20);
            this.originFolder.TabIndex = 7;
            this.originFolder.UseVisualStyleBackColor = false;
            this.originFolder.Click += new System.EventHandler(this.InputButtonClick);
            // 
            // destinationFolder
            // 
            this.destinationFolder.BackColor = System.Drawing.Color.Transparent;
            this.destinationFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("destinationFolder.BackgroundImage")));
            this.destinationFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.destinationFolder.Location = new System.Drawing.Point(367, 87);
            this.destinationFolder.Name = "destinationFolder";
            this.destinationFolder.Size = new System.Drawing.Size(22, 20);
            this.destinationFolder.TabIndex = 8;
            this.destinationFolder.UseVisualStyleBackColor = false;
            this.destinationFolder.Click += new System.EventHandler(this.OutputButtonClick);
            // 
            // checkDirectories
            // 
            this.checkDirectories.AutoSize = true;
            this.checkDirectories.Cursor = System.Windows.Forms.Cursors.No;
            this.checkDirectories.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkDirectories.Location = new System.Drawing.Point(361, 161);
            this.checkDirectories.Name = "checkDirectories";
            this.checkDirectories.Size = new System.Drawing.Size(0, 13);
            this.checkDirectories.TabIndex = 9;
            // 
            // infoChangeLabel
            // 
            this.infoChangeLabel.AutoSize = true;
            this.infoChangeLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.infoChangeLabel.Location = new System.Drawing.Point(9, 211);
            this.infoChangeLabel.Name = "infoChangeLabel";
            this.infoChangeLabel.Size = new System.Drawing.Size(110, 13);
            this.infoChangeLabel.TabIndex = 10;
            this.infoChangeLabel.Text = "Última atualização às:";
            // 
            // lastChangeLabel
            // 
            this.lastChangeLabel.AutoSize = true;
            this.lastChangeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastChangeLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.lastChangeLabel.Location = new System.Drawing.Point(125, 211);
            this.lastChangeLabel.Name = "lastChangeLabel";
            this.lastChangeLabel.Size = new System.Drawing.Size(39, 13);
            this.lastChangeLabel.TabIndex = 11;
            this.lastChangeLabel.Text = "00:00";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(9, 182);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(140, 13);
            this.statusLabel.TabIndex = 13;
            this.statusLabel.Text = "Não está sincronizando";
            // 
            // realTimeCheckbox
            // 
            this.realTimeCheckbox.AutoSize = true;
            this.realTimeCheckbox.Location = new System.Drawing.Point(290, 114);
            this.realTimeCheckbox.Name = "realTimeCheckbox";
            this.realTimeCheckbox.Size = new System.Drawing.Size(85, 17);
            this.realTimeCheckbox.TabIndex = 14;
            this.realTimeCheckbox.Text = "Tempo real?";
            this.realTimeCheckbox.UseVisualStyleBackColor = true;
            this.realTimeCheckbox.CheckedChanged += new System.EventHandler(this.RealTimeCheckboxCheckedChanged);
            // 
            // numericInputTimer
            // 
            this.numericInputTimer.Location = new System.Drawing.Point(290, 154);
            this.numericInputTimer.Name = "numericInputTimer";
            this.numericInputTimer.Size = new System.Drawing.Size(49, 20);
            this.numericInputTimer.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Sincronizar em";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "minutos";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 236);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericInputTimer);
            this.Controls.Add(this.realTimeCheckbox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.lastChangeLabel);
            this.Controls.Add(this.infoChangeLabel);
            this.Controls.Add(this.checkDirectories);
            this.Controls.Add(this.destinationFolder);
            this.Controls.Add(this.originFolder);
            this.Controls.Add(this.destinationLabel);
            this.Controls.Add(this.originLabel);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.syncButton);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Sincronize suas pastas";
            ((System.ComponentModel.ISupportInitialize)(this.numericInputTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button syncButton;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Label originLabel;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button originFolder;
        private System.Windows.Forms.Button destinationFolder;
        private System.Windows.Forms.Label checkDirectories;
        private System.Windows.Forms.Label infoChangeLabel;
        private System.Windows.Forms.Label lastChangeLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.CheckBox realTimeCheckbox;
        private System.Windows.Forms.NumericUpDown numericInputTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

