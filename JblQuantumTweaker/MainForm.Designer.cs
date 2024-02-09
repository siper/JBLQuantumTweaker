using CoreAudio;
using System;

namespace JblQuantumTweaker
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.jblDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.deviceStatusLable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.defaultComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // jblDeviceComboBox
            // 
            this.jblDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.jblDeviceComboBox.FormattingEnabled = true;
            this.jblDeviceComboBox.Location = new System.Drawing.Point(6, 28);
            this.jblDeviceComboBox.Name = "jblDeviceComboBox";
            this.jblDeviceComboBox.Size = new System.Drawing.Size(294, 24);
            this.jblDeviceComboBox.TabIndex = 0;
            this.jblDeviceComboBox.SelectionChangeCommitted += new System.EventHandler(this.jblDeviceComboBox_SelectionChangeCommitted);
            // 
            // deviceStatusLable
            // 
            this.deviceStatusLable.AutoSize = true;
            this.deviceStatusLable.Location = new System.Drawing.Point(3, 126);
            this.deviceStatusLable.Name = "deviceStatusLable";
            this.deviceStatusLable.Size = new System.Drawing.Size(177, 16);
            this.deviceStatusLable.TabIndex = 1;
            this.deviceStatusLable.Text = "Наушники не подключены";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Устройство JBL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Устройство по умолчанию";
            // 
            // defaultComboBox
            // 
            this.defaultComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultComboBox.FormattingEnabled = true;
            this.defaultComboBox.Location = new System.Drawing.Point(6, 89);
            this.defaultComboBox.Name = "defaultComboBox";
            this.defaultComboBox.Size = new System.Drawing.Size(294, 24);
            this.defaultComboBox.TabIndex = 11;
            this.defaultComboBox.SelectionChangeCommitted += new System.EventHandler(this.defaultComboBox_SelectionChangeCommitted);
            // 
            // JBLQuantumTweaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 148);
            this.Controls.Add(this.defaultComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deviceStatusLable);
            this.Controls.Add(this.jblDeviceComboBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JBLQuantumTweaker";
            this.Text = "JBLQuantumTweaker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox jblDeviceComboBox;
        private System.Windows.Forms.Label deviceStatusLable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox defaultComboBox;
    }
}

