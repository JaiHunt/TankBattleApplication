namespace TankBattle
{
    partial class BattleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleForm));
            this.displayPanel = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.Fire = new System.Windows.Forms.Button();
            this.PowerBar = new System.Windows.Forms.TrackBar();
            this.AngleNo = new System.Windows.Forms.NumericUpDown();
            this.WeaponSelect = new System.Windows.Forms.ComboBox();
            this.PowerNo = new System.Windows.Forms.Label();
            this.Power = new System.Windows.Forms.Label();
            this.Angle = new System.Windows.Forms.Label();
            this.Weapon = new System.Windows.Forms.Label();
            this.WindSpeed = new System.Windows.Forms.Label();
            this.Wind = new System.Windows.Forms.Label();
            this.Player = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PowerBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleNo)).BeginInit();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(0, 32);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(800, 600);
            this.displayPanel.TabIndex = 0;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel.BackColor = System.Drawing.Color.OrangeRed;
            this.controlPanel.Controls.Add(this.Fire);
            this.controlPanel.Controls.Add(this.PowerBar);
            this.controlPanel.Controls.Add(this.AngleNo);
            this.controlPanel.Controls.Add(this.WeaponSelect);
            this.controlPanel.Controls.Add(this.PowerNo);
            this.controlPanel.Controls.Add(this.Power);
            this.controlPanel.Controls.Add(this.Angle);
            this.controlPanel.Controls.Add(this.Weapon);
            this.controlPanel.Controls.Add(this.WindSpeed);
            this.controlPanel.Controls.Add(this.Wind);
            this.controlPanel.Controls.Add(this.Player);
            this.controlPanel.Enabled = false;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(800, 32);
            this.controlPanel.TabIndex = 1;
            this.controlPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.controlPanel_Paint);
            // 
            // Fire
            // 
            this.Fire.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fire.Location = new System.Drawing.Point(729, 4);
            this.Fire.Name = "Fire";
            this.Fire.Size = new System.Drawing.Size(61, 23);
            this.Fire.TabIndex = 8;
            this.Fire.Text = "Fire!";
            this.Fire.UseVisualStyleBackColor = true;
            this.Fire.Click += new System.EventHandler(this.Fire_Click);
            // 
            // PowerBar
            // 
            this.PowerBar.LargeChange = 10;
            this.PowerBar.Location = new System.Drawing.Point(577, 0);
            this.PowerBar.Maximum = 100;
            this.PowerBar.Minimum = 5;
            this.PowerBar.Name = "PowerBar";
            this.PowerBar.Size = new System.Drawing.Size(113, 45);
            this.PowerBar.TabIndex = 7;
            this.PowerBar.Value = 5;
            this.PowerBar.Scroll += new System.EventHandler(this.PowerBar_Scroll);
            // 
            // AngleNo
            // 
            this.AngleNo.Location = new System.Drawing.Point(460, 4);
            this.AngleNo.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.AngleNo.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.AngleNo.Name = "AngleNo";
            this.AngleNo.Size = new System.Drawing.Size(46, 20);
            this.AngleNo.TabIndex = 6;
            this.AngleNo.ValueChanged += new System.EventHandler(this.AngleNo_ValueChanged);
            // 
            // WeaponSelect
            // 
            this.WeaponSelect.FormattingEnabled = true;
            this.WeaponSelect.Location = new System.Drawing.Point(249, 5);
            this.WeaponSelect.Name = "WeaponSelect";
            this.WeaponSelect.Size = new System.Drawing.Size(145, 21);
            this.WeaponSelect.TabIndex = 0;
            this.WeaponSelect.SelectedIndexChanged += new System.EventHandler(this.WeaponSelect_SelectedIndexChanged);
            // 
            // PowerNo
            // 
            this.PowerNo.AutoSize = true;
            this.PowerNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PowerNo.Location = new System.Drawing.Point(696, 6);
            this.PowerNo.Name = "PowerNo";
            this.PowerNo.Size = new System.Drawing.Size(27, 20);
            this.PowerNo.TabIndex = 5;
            this.PowerNo.Text = "20";
            this.PowerNo.Click += new System.EventHandler(this.PowerNo_Click);
            // 
            // Power
            // 
            this.Power.AutoSize = true;
            this.Power.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Power.Location = new System.Drawing.Point(523, 4);
            this.Power.Name = "Power";
            this.Power.Size = new System.Drawing.Size(57, 20);
            this.Power.TabIndex = 4;
            this.Power.Text = "Power:";
            // 
            // Angle
            // 
            this.Angle.AutoSize = true;
            this.Angle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Angle.Location = new System.Drawing.Point(400, 5);
            this.Angle.Name = "Angle";
            this.Angle.Size = new System.Drawing.Size(54, 20);
            this.Angle.TabIndex = 3;
            this.Angle.Text = "Angle:";
            this.Angle.Click += new System.EventHandler(this.Angle_Click);
            // 
            // Weapon
            // 
            this.Weapon.AutoSize = true;
            this.Weapon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Weapon.Location = new System.Drawing.Point(170, 5);
            this.Weapon.Name = "Weapon";
            this.Weapon.Size = new System.Drawing.Size(73, 20);
            this.Weapon.TabIndex = 0;
            this.Weapon.Text = "Weapon:";
            this.Weapon.Click += new System.EventHandler(this.Weapon_Click);
            // 
            // WindSpeed
            // 
            this.WindSpeed.AutoSize = true;
            this.WindSpeed.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.WindSpeed.Location = new System.Drawing.Point(97, 17);
            this.WindSpeed.Name = "WindSpeed";
            this.WindSpeed.Size = new System.Drawing.Size(27, 13);
            this.WindSpeed.TabIndex = 2;
            this.WindSpeed.Text = "0 W";
            this.WindSpeed.Click += new System.EventHandler(this.WindSpeed_Click);
            // 
            // Wind
            // 
            this.Wind.AutoSize = true;
            this.Wind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Wind.Location = new System.Drawing.Point(97, 4);
            this.Wind.Name = "Wind";
            this.Wind.Size = new System.Drawing.Size(36, 13);
            this.Wind.TabIndex = 1;
            this.Wind.Text = "Wind";
            this.Wind.Click += new System.EventHandler(this.Wind_Click);
            // 
            // Player
            // 
            this.Player.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player.Location = new System.Drawing.Point(3, 4);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(120, 20);
            this.Player.TabIndex = 0;
            this.Player.Text = "Player 1";
            this.Player.Click += new System.EventHandler(this.Player_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 629);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.displayPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "BattleForm";
            this.Text = "Form1";
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PowerBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleNo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label WindSpeed;
        private System.Windows.Forms.Label Wind;
        private System.Windows.Forms.Label Player;
        private System.Windows.Forms.Label Angle;
        private System.Windows.Forms.Label Weapon;
        private System.Windows.Forms.Button Fire;
        private System.Windows.Forms.TrackBar PowerBar;
        private System.Windows.Forms.NumericUpDown AngleNo;
        private System.Windows.Forms.ComboBox WeaponSelect;
        private System.Windows.Forms.Label PowerNo;
        private System.Windows.Forms.Label Power;
    }
}

