namespace Domo_PLavarello
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblMQTT = new System.Windows.Forms.Label();
            this.lblEstado1 = new System.Windows.Forms.Label();
            this.lblEstado2 = new System.Windows.Forms.Label();
            this.lblMSSQL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(71, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(201, 31);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Domo Demonio";
            // 
            // lblMQTT
            // 
            this.lblMQTT.AutoSize = true;
            this.lblMQTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMQTT.Location = new System.Drawing.Point(8, 46);
            this.lblMQTT.Name = "lblMQTT";
            this.lblMQTT.Size = new System.Drawing.Size(61, 20);
            this.lblMQTT.TabIndex = 1;
            this.lblMQTT.Text = "MQTT:";
            // 
            // lblEstado1
            // 
            this.lblEstado1.AutoSize = true;
            this.lblEstado1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado1.Location = new System.Drawing.Point(63, 46);
            this.lblEstado1.Name = "lblEstado1";
            this.lblEstado1.Size = new System.Drawing.Size(51, 20);
            this.lblEstado1.TabIndex = 2;
            this.lblEstado1.Text = "label1";
            // 
            // lblEstado2
            // 
            this.lblEstado2.AutoSize = true;
            this.lblEstado2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado2.Location = new System.Drawing.Point(80, 73);
            this.lblEstado2.Name = "lblEstado2";
            this.lblEstado2.Size = new System.Drawing.Size(51, 20);
            this.lblEstado2.TabIndex = 3;
            this.lblEstado2.Text = "label1";
            // 
            // lblMSSQL
            // 
            this.lblMSSQL.AutoSize = true;
            this.lblMSSQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMSSQL.Location = new System.Drawing.Point(8, 73);
            this.lblMSSQL.Name = "lblMSSQL";
            this.lblMSSQL.Size = new System.Drawing.Size(75, 20);
            this.lblMSSQL.TabIndex = 4;
            this.lblMSSQL.Text = "MSSQL:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 416);
            this.Controls.Add(this.lblMSSQL);
            this.Controls.Add(this.lblEstado2);
            this.Controls.Add(this.lblEstado1);
            this.Controls.Add(this.lblMQTT);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Domo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblMQTT;
        private System.Windows.Forms.Label lblEstado1;
        private System.Windows.Forms.Label lblEstado2;
        private System.Windows.Forms.Label lblMSSQL;
    }
}

